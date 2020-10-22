using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Business;
using Entities.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.ActionAttributes;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;


        //DI
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }



        [HttpGet]
        public IActionResult GetCategories()
        {
            var result = _categoryService.GetAll();

            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest("Kategoriler Listelenemedi.");
        }


        [HttpGet("{categoryID}")]
        public IActionResult GetCategoryById(int categoryID)
        {
            var result = _categoryService.GetById(categoryID);

            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }



        [HttpPost]
        [ValidationFilter]
        public IActionResult AddCategory(Category category)
        {
            var result = _categoryService.Add(category);

            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }



        [HttpPost("AddRange")]
        [ValidationFilter]
        public IActionResult AddCategories(List<Category> categories)
        {
            var result = _categoryService.AddRange(categories);

            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }



        [HttpDelete("{categoryID}")]
        public IActionResult DeleteCategory(int categoryID)
        {
            var result = _categoryService.Delete(categoryID);

            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }



        [HttpPut]
        [ValidationFilter]
        public IActionResult UpdateCategory(Category category)
        {
            var result = _categoryService.Update(category);

            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }



        [HttpGet("Name/{categoryName}")]
        public IActionResult GetCategoryByName(string categoryName)
        {
            var result = _categoryService.GetByName(categoryName);

            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }



        [HttpGet("WithProducts")]
        public IActionResult GetCategoriesWithProducts()
        {
            var result = _categoryService.GetCategoriesWithProducts();

            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }



        [HttpGet("{categoryID}/WithProducts")]
        public IActionResult GetCategoryWithProducts(int categoryID)
        {
            var result = _categoryService.GetCategoryWithProducts(categoryID);

            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }
    }
}
