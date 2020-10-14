using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreLoggerWebAPI.Controllers
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
        public IActionResult Get()
        {
            var result = _categoryService.GetAll();

            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }




        [HttpGet("{categoryID}")]
        public IActionResult Get(int categoryID)
        {
            var result = _categoryService.GetById(categoryID);

            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }




        
        [HttpPost]
        public IActionResult Post(Category category)
        {
            var result = _categoryService.Add(category);

            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }





        [HttpPost("AddRange")]
        public IActionResult Post(List<Category> categories)
        {
            var result = _categoryService.AddRange(categories);

            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }





        [HttpDelete("{categoryID}")]
        public IActionResult Delete(int categoryID)
        {
            var result = _categoryService.Delete(categoryID);

            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }





        [HttpPut]
        public IActionResult Put(Category category)
        {
            var result = _categoryService.Update(category);

            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }






        [HttpGet("Name/{categoryName}")]
        public IActionResult Get(string categoryName)
        {
            var result = _categoryService.GetByName(categoryName);

            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }




    }


}
