using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MKTFY.App.Repositories.Interfaces;
using MKTFY.Models.ViewModels;
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
    }
}
