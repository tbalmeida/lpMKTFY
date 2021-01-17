using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using MKTFY.App.Repositories.Interfaces;
using MKTFY.Models.Entities;
using MKTFY.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace MKTFY.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;

        private readonly IListingRepository _listingRepository;

        public AccountController(SignInManager<User> signInManager, IConfiguration configuration, IUserRepository userRepository, UserManager<User> userManager, IListingRepository listingRepository)
        {
            _signInManager = signInManager;
            _configuration = configuration;
            _userRepository = userRepository;
            _userManager = userManager;
            _listingRepository = listingRepository;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseVM>> Login([FromBody] LoginVM login)
        {
            // Make sure model has all required fields
            if (!ModelState.IsValid)
                return BadRequest("Invalid data");

            // Validate the user login
            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, true).ConfigureAwait(false);

            if (result.IsLockedOut)
            {
                return BadRequest("This user account has been locked out, please try again later");
            }
            else if (!result.Succeeded)
            {
                return BadRequest("Invalid username/password");
            }

            // Get the user profile
            var user = await _userRepository.GetUserByEmail(login.Email).ConfigureAwait(false);

            // Get a token from the identity server
            using var httpClient = new HttpClient();
            var authority = _configuration.GetSection("Identity").GetValue<string>("Authority");

            // Make the call to our identity server
            var tokenResponse = await httpClient.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = authority + "/connect/token",
                UserName = login.Email,
                Password = login.Password,
                ClientId = login.ClientId,
                ClientSecret = "UzKjRFnAHffxUFati8HMjSEzwMGgGHmN",
                Scope = "mktfyapi.scope"

            }).ConfigureAwait(false);

            if (tokenResponse.IsError)
            {
                return BadRequest("Unable to grant access to user account");
            }

            return Ok(new LoginResponseVM(tokenResponse, user));
        }

        [HttpPost("signup")]
        public async Task<ActionResult<UserVM>> Signup([FromBody] UserCreateVM src)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please, check your data");
            }

            var userResult = await _userManager.FindByNameAsync(src.Email);

            if (userResult == null)
            {
                var user = new User
                {
                    UserName = src.Email,
                    Email = src.Email,
                    FirstName = src.FirstName,
                    LastName = src.LastName,
                    CityId = src.CityId
                };
                IdentityResult result = await _userManager.CreateAsync(user, src.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "user");

                    var newUser = new UserVM(user);
                    return Ok(newUser);
                }
                return StatusCode(500);
            }
            return BadRequest("User already exists");
        }

        [HttpGet("{id}/count")]
        public async Task<ActionResult<int>> ListCount([FromRoute] string id, [FromQuery] bool activeOnly, [FromQuery] int? status)
        {
            var result = await _userRepository.ListingCount(id, activeOnly, status);
            
            return Ok(result);
        }

        [HttpGet("{id}/listings")]
        public async Task<ActionResult<List<ListingVM>>> GetListings([FromRoute] string id, [FromQuery] bool activeOnly, [Optional] int statusId)
        {
            var results = await _listingRepository.FilterListings(
                ownerId: id, 
                listingStatusId: statusId,
                cityId: 0,
                activeOnly: activeOnly
            );

            var qty = await _userRepository.ListingCount(id, activeOnly, statusId);

            var models = new List<ListingVM>();
            foreach (var item in results)
            {
                models.Add(new ListingVM(item, qty));
            }

            return models;
        }
    }
}
