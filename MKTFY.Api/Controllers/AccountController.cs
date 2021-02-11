﻿using System.Net.Http;
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
using MKTFY.App.Exceptions;
using System;

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
        private readonly IOrderRepository _orderRepository;

        public AccountController(SignInManager<User> signInManager, 
            IConfiguration configuration, 
            IUserRepository userRepository, 
            UserManager<User> userManager, 
            IListingRepository listingRepository,
            IOrderRepository orderRepository)
        {
            _signInManager = signInManager;
            _configuration = configuration;
            _userRepository = userRepository;
            _userManager = userManager;
            _listingRepository = listingRepository;
            _orderRepository = orderRepository;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseVM>> Login([FromBody] LoginVM login)
        {
            // Make sure model has all required fields
            if (!ModelState.IsValid)
                return BadRequest("Invalid data");

            //// Validate the user login
            var result = await _userRepository.IsValid(login.Email, login.Password);
            if (!result)
                return StatusCode(401);

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

        [HttpGet("{guid}")]
        public async Task<ActionResult<UserVM>> GetById([FromRoute] string guid)
        {
            var results = await _userRepository.GetById(guid);

            return Ok(results);
        }

        [HttpGet("{id}/count")]
        public async Task<ActionResult<int>> ListCount([FromRoute] string id, [FromQuery] int? status, [FromQuery] bool activeOnly = true)
        {
            var result = await _userRepository.ListingCount(id, activeOnly, status);
            
            return Ok(result);
        }

        [HttpGet("{id}/listings")]
        public async Task<ActionResult<List<ListingVM>>> GetListings([FromRoute] string id, [Optional] int statusId, [FromQuery] bool activeOnly = true)
        {
            var results = await _listingRepository.FilterListings(
                ownerId: id,
                listingStatusId: statusId,
                activeOnly: activeOnly
            );

            var qty = await _userRepository.ListingCount(id, activeOnly, statusId);

            var models = new List<ListingVM>();
            foreach (var item in results)
            {
                models.Add(new ListingVM(item, qty));
            }

            return Ok(models);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<UserVM>> UpdateUser([FromRoute] string id, [FromBody] UserUpdateVM src)
        {
            // check if route Id and VM Id match
            if (id != src.Id)
                throw new MismatchingId(id);

            // check user credential
            var loginOk = await _userRepository.IsValid(src.Email, src.Password);
            if (!loginOk)
                return StatusCode(401);

            var result = await _userRepository.UpdateUser(src);

            return Ok(result);
        }

        [HttpGet("{id}/purchases/{orderId}")]
        public async Task<ActionResult<OrderVM>> GetOrderById([FromRoute] string id, Guid orderId)
        {
            var order = await _orderRepository.GetById(orderId);  //_userRepository.GetOrderById(orderId);

            // not found
            if (order == null)
                return NotFound("The order " + orderId.ToString() + " was not found");

            // the UserId provided doesn't match buyer nor seller
            if (order.BuyerId != id && order.SellerId != id)
                return Unauthorized("You do not have access to this Order.");

            //return Ok(order);
            return Ok(order);
        }

        [HttpPatch("{id}/purchases/{orderId}")]
        public async Task<ActionResult<bool>> UpdateOrder([FromRoute] string id, Guid orderId, [FromBody] OrderUpdateVM src)
        {
            return Ok("Not implemented yet");
        }
    }
}
