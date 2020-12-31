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
            var results = await _cityRepository.GetAll();
            return Ok(results);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CityVM>> GetById(int id)
        {
            var results = await _cityRepository.GetById(id);
            return Ok(results);
        }

        [HttpPost]
        public async Task<ActionResult<CityVM>> Create([FromBody] CityCreateVM src)
        {
            var result = await _cityRepository.Create(src);
            return Ok(result);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] CityUpdateVM src)
        {
            var results = await _cityRepository.Update(id, src);
            return Ok(results);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> Delete([FromRoute] int id)
        {
            var delCity = await _cityRepository.Delete(id);
            return Ok(delCity);
        }
    }
}
