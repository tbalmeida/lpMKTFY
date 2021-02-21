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
    public class FeeController : ControllerBase
    {
        private readonly IFeeRepository _feeRepository;

        public FeeController(IFeeRepository feeRepository)
        {
            _feeRepository = feeRepository;
        }

        [HttpPost]
        public async Task<ActionResult<FeeVM>> Create([FromBody] FeeCreateVM src)
        {
            var result = await _feeRepository.Create(src);

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<FeeVM>>> GetAll([FromQuery] bool onlyActive)
        {
            var result = await _feeRepository.GetAll(onlyActive);

            return Ok(result);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<FeeVM>> Update(FeeUpdateVM src)
        {
            var results = await _feeRepository.Update(src);

            return Ok(results);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> Delete([FromRoute] int id)
        {
            var results = await _feeRepository.Delete(id);

            return Ok(results);
        }
    }
}
