using Microsoft.AspNetCore.Mvc;
using MKTFY.App.Repositories.Interfaces;
using MKTFY.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKTFY.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryVM>>> GetAll()
        {
            var results = await _categoryRepository.GetAll();
            return Ok(results);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryVM>> GetById([FromRoute] int id)
        {
            var results = await _categoryRepository.GetById(id);
            return Ok(results);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryVM>> Create([FromBody] CategoryCreateVM data)
        {
            var result = await _categoryRepository.Create(data);
            return Ok(result);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<CategoryUpdateVM>> Update([FromRoute] int id, [FromBody] CategoryUpdateVM data)
        {
            var result = await _categoryRepository.Update(id, data);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> Delete([FromRoute] int id)
        {
            var delCat = await _categoryRepository.Delete(id);
            return Ok(delCat);
        }
    }
}
