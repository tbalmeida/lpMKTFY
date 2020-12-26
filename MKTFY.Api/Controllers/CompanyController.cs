using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MKTFY.App.Repositories.Interfaces;
using MKTFY.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MKTFY.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        [HttpPost]
        public async Task<ActionResult<CompanyVM>> Create([FromBody] CompanyCreateVM data)
        {
            // Make sure model has all required fields
            if (!ModelState.IsValid)
                return BadRequest("Invalid data");

            try
            {
                var result = await _companyRepository.Create(data);
                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<CompanyVM>>> GetAll()
        {
            try
            {
                var results = await _companyRepository.GetAll();
                return Ok(results);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
