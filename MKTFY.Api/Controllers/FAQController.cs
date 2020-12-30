using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MKTFY.App.Exceptions;
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
    public class FAQController : ControllerBase
    {
        private readonly IFAQRepository _faqRepository;

        public FAQController(IFAQRepository faqRepository)
        {
            _faqRepository = faqRepository;
        }

        [HttpPost]
        public async Task<ActionResult<FAQVM>> Create([FromBody] FAQCreateVM src)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data");
            var result = await _faqRepository.Create(src);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<FAQVM>>> GetFAQ([FromQuery] string search)
        {
            if (search != null)
            {
                var filteredResults = await _faqRepository.FilterFAQ(search);
                return Ok(filteredResults);
            }

            var results = await _faqRepository.GetAll();
            return Ok(results);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FAQVM>> GetById([FromRoute] Guid id)
        {
            var results = await _faqRepository.GetById(id);
            return Ok(results);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> Delete([FromRoute] Guid id)
        {
            var delFAQ = await _faqRepository.Delete(id);
            if (delFAQ == "FAQ not found")
                return NotFound(delFAQ);
            return Ok(delFAQ);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Update([FromRoute] Guid id, [FromBody] FAQUpdateVM src)
        {
            if (id != src.Id || !ModelState.IsValid)
                return BadRequest("Please, check the data provided.");

            var results = await _faqRepository.Update(id, src);
            return Ok(results);
        }
    }
}
