using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MKTFY.App.Repositories.Interfaces;
using MKTFY.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKTFY.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CityController : ControllerBase
    {
        private readonly ICityRepository _cityRepository;

        public CityController(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<CityVM>>> GetAll()
        {
            try
            {
                var results = await _cityRepository.GetAll();
                return Ok(results);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CityVM>> GetById(int id)
        {
            try
            {
                var results = await _cityRepository.GetById(id);
                return Ok(results);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CityVM>> Create([FromBody] CityCreateVM src)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data");

            try
            {
                var result = await _cityRepository.Create(src);
                return Ok(result);
            }
            catch
            {
                throw new Exception("Error on FAQ creation.");
            }
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] CityUpdateVM src)
        {
            if (id != src.Id || !ModelState.IsValid)
                return BadRequest("Please, check the data provided.");

            try
            {
                var results = await _cityRepository.Update(id, src);
                return Ok(results);
            }
            catch (Exception ex)
            {
                if (ex.Message == "FAQ not found")
                    return NotFound(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> Delete([FromRoute] int id)
        {
            try
            {
                var delCity = await _cityRepository.Delete(id);
                if (delCity == "City not found")
                    return NotFound(delCity);
                return Ok(delCity);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
