using Microsoft.AspNetCore.Mvc;
using MKTFY.App.Repositories.Interfaces;
using MKTFY.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKTFY.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ListingController : ControllerBase
    {
        private readonly IListingRepository _listingRepository;

        public ListingController(IListingRepository listingRepository)
        {
            _listingRepository = listingRepository;
        }

        [HttpPost]
        public async Task<ActionResult<ListingVM>> Create(ListingCreateVM src)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }

            try
            {
                var result = await _listingRepository.Create(src);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error on Listing creation\n" + ex.Message);
            }
        }
        
        [HttpGet]
        public async Task<ActionResult<List<ListingVM>>> GetAll([FromQuery] string searchText)
        {
            try
            {
                var results = await _listingRepository.GetListings(searchText);
                return Ok(results);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ListingVM>> GetById(Guid id)
        {
            try
            {
                var results = await _listingRepository.GetById(id);
                return Ok(results);
            }
            catch
            {
                return StatusCode(500);
            }
        }

    }
}
