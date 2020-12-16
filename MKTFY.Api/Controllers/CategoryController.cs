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
            try
            {
                var results = await _categoryRepository.GetAll();
                return Ok(results);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryVM>> GetById([FromRoute] int id)
        {
            try
            {
                var results = await _categoryRepository.GetById(id);
                return Ok(results);
            }
            catch (Exception ex)
            {
                if (ex.Message == "Category not found")
                    return NotFound(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CategoryVM>> Create([FromBody] CategoryCreateVM data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }

            try
            {
                var result = await _categoryRepository.Create(data);
                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<CategoryUpdateVM>> Update([FromRoute] int id, [FromBody] CategoryUpdateVM data)
        {
            if (id != data.Id || !ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }

            try
            {
                var result = await _categoryRepository.Update(id, data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                if (ex.Message == "Category not found")
                {
                    return NotFound();
                }
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> Delete([FromRoute] int id)
        {
            try
            {
                var delCat = await _categoryRepository.Delete(id);
                if (delCat == "Category not found")
                    return NotFound(delCat);
                return Ok(delCat);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
