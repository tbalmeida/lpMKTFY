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
        public async Task<ActionResult<List<ListingVM>>> GetAll([FromQuery] string sTxt, int? sCty, int? sSts, int? sCat, int? sIC)
        {
            try
            {
                var results = await _listingRepository.GetListings(sTxt, sCty, sSts, sCat, sIC);
                return Ok(results);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("short")]
        public async Task<ActionResult<List<ListingShortVM>>> GetShortListings([FromQuery] string sTxt, int? sCty, int? sSts, int? sCat, int? sIC)
        {
            try
            {
                var results = await _listingRepository.GetShortListings(sTxt, sCty, sSts, sCat, sIC);
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

        [HttpPatch("{id}")]
        public async Task<ActionResult<ListingVM>> Update(Guid id, ListingUpdateVM src)
        {
            if (!ModelState.IsValid || id != src.Id)
            {
                return BadRequest("Invalid data");
            }

            try
            {
                var result = await _listingRepository.Update(id, src);
                return Ok(result);
            }
            catch(Exception ex)
            {
                if (ex.Message == "Listing not found")
                {
                    return NotFound("Listing not found");
                }

                return BadRequest("Error on Listing update\n" + ex.Message);
            }
        }

        [HttpPatch("{id}/price")]
        public async Task<ActionResult<ListingVM>> UpdatePrice(Guid id, ListingPriceUpdateVM src)
        {
            if (!ModelState.IsValid || id != src.Id)
            {
                return BadRequest("Invalid data");
            }

            try
            {
                var result = await _listingRepository.UpdatePrice(id, src);
                return Ok(result);
            }
            catch (Exception ex)
            {
                if (ex.Message == "Listing not found")
                {
                    return NotFound("Listing not found");
                }

                return BadRequest("Error on Listing update\n" + ex.Message);
            }
        }

        [HttpPatch("{id}/status")]
        public async Task<ActionResult<ListingVM>> UpdateStatus(Guid id, ListingStatusUpdateVM src)
        {
            if (!ModelState.IsValid || id != src.Id)
            {
                return BadRequest("Invalid data");
            }

            try
            {
                var result = await _listingRepository.UpdateStatus(id, src);
                return Ok(result);
            }
            catch (Exception ex)
            {
                if (ex.Message == "Listing not found")
                {
                    return NotFound("Listing not found");
                }

                return BadRequest("Error on Listing update\n" + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> Delete([FromRoute] Guid id)
        {
            try
            {
                var result = await _listingRepository.Delete(id);
                if ( result == "Listing not found" )
                {
                    return NotFound("Listing not found");
                }
                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
