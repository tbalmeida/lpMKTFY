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

    public class ItemConditionController : Controller
    {
        private readonly IItemConditionRepository _itemCondRep;

        public ItemConditionController(IItemConditionRepository itemConditionRepository)
        {
            _itemCondRep = itemConditionRepository;
        }

        public async Task<ActionResult<List<ItemConditionVM>>> GetAll()
        {
            try
            {
                var results = await _itemCondRep.GetAll();
                return Ok(results);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
