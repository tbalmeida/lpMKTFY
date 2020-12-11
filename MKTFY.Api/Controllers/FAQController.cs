using Microsoft.AspNetCore.Authorization;
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

            try
            {
                var result = await _faqRepository.Create(src);
                return Ok(result);
            }
            catch
            {
                throw new ArgumentException("Error on FAQ creation.");
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<FAQVM>>> GetFAQ([FromQuery] string search)
        {
            try
            {
                if (search != null)
                {
                    var filteredResults = await _faqRepository.FilterFAQ(search);
                    return Ok(filteredResults);
                }

                var results = await _faqRepository.GetAll();
                return Ok(results);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FAQVM>> GetById([FromRoute] Guid id)
        {
            try
            {
                var results = await _faqRepository.GetById(id);
                return Ok(results);
            }
            catch (Exception ex)
            {
                if (ex.Message == "FAQ not found")
                    return NotFound(ex.Message);

                return BadRequest(ex.Message);
            }
        }
    }
}
