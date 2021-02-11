using Microsoft.AspNetCore.Mvc;
using MKTFY.App.Exceptions;
using MKTFY.App.Repositories.Interfaces;
using MKTFY.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
            var result = await _listingRepository.Create(src);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<ListingVM>>> GetAll([FromQuery] string sTxt, int sCty, int sSts, int sCat, int sIC, [Optional] string sOw, bool aO = true)
        {
            var results = await _listingRepository.GetListings(cityId: sCty, searchText: sTxt, categoryId: sCat,itemConditionId: sIC, listingStatusId: sSts, ownerId: sOw, activeOnly: aO);
            return Ok(results);
        }

        [HttpGet("short")]
        public async Task<ActionResult<List<ListingShortVM>>> GetListingsShort([FromQuery] string sTxt, int sCty, int sSts, int sCat, int sIC, [Optional] string sOw)
        {
            var results = await _listingRepository.GetListingsShort(cityId: sCty, searchText: sTxt, categoryId: sCat, itemConditionId: sIC, listingStatusId: sSts, ownerId: sOw);
            return Ok(results);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ListingVM>> GetById([FromRoute] Guid id)
        {
            var results = await _listingRepository.GetById(id);
            return Ok(results);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<ListingVM>> Update([FromRoute] Guid id, [FromBody] ListingUpdateVM src)
        {
            var result = await _listingRepository.Update(id, src);
            return Ok(result);
        }

        [HttpPatch("{id}/price")]
        public async Task<ActionResult<ListingVM>> UpdatePrice([FromRoute] Guid id, [FromBody] ListingUpdatePriceVM src)
        {
            var result = await _listingRepository.UpdatePrice(id, src);
            return Ok(result);
        }

        [HttpPatch("{id}/status")]
        public async Task<ActionResult<ListingVM>> UpdateStatus(Guid id, int statusId)
        {
            bool result = await _listingRepository.UpdateStatus(id, statusId);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> Delete([FromRoute] Guid id)
        {
            var result = await _listingRepository.Delete(id);
            return Ok(result);
        }

        [HttpPost("{id}/buy")]
        public async Task<ActionResult<OrderVM>> Buy([FromRoute] Guid id, [FromBody] OrderCreateVM src)
        {
            if (id != src.ListingId)
                throw new MismatchingId(id.ToString());

            var result = await _listingRepository.Buy(src);

            return Ok(result);
        }
    }
}
