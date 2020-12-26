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

    public class ListingStatusController : Controller
    {
        private readonly IListingStatusRepository _listingStatusRepository;

        public ListingStatusController(IListingStatusRepository listingStatusRepository)
        {
            _listingStatusRepository = listingStatusRepository;
        }

        public async Task<ActionResult<List<ListingStatusVM>>> GetAll()
        {
            try
            {
                var results = await _listingStatusRepository.GetAll();
                return Ok(results);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
