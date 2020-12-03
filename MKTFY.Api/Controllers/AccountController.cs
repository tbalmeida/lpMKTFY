using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using MKTFY.App.Repositories.Interfaces;
using MKTFY.Models.Entities;
using MKTFY.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace MKTFY.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public AccountController(SignInManager<User> signInManager, IConfiguration configuration, IUserRepository userRepository)
        {
            _signInManager = signInManager;
            _configuration = configuration;
            _userRepository = userRepository;
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
            using (var httpClient = new HttpClient())
            {
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
        }
    }
}
