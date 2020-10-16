using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Concrete;
using Core.Log;
using Core.Validations;
using Entities;
using Entities.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Repository.Concrete;
using Repository.UnıtOfWork.Concrete;

namespace AspNetCoreLoggerWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        
        private readonly IProductService _productService;
        
        //DI
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }



        [HttpGet]
        public IActionResult Get()
        {
            var result = _productService.GetAll();

            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest("Ürünler Listelenemedi.");
        }




        [HttpPost]
        public IActionResult Post(Product product)
        {
            var result = _productService.Add(product);

            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }




        [HttpPost("AddRange")]
        public IActionResult Post(List<Product> products)
        {
            var result = _productService.AddRange(products);

            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }




        [HttpDelete("Delete/{productID:int}")]
        public IActionResult DeleteProduct(int productID)
        {
            var result = _productService.Delete(productID);

            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }




        [HttpGet("{productID:int}")]
        public IActionResult GetProduct(int productID)
        {
            var result = _productService.GetById(productID);

            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }




        [HttpPut]
        public IActionResult Put(Product product)
        {
            var result = _productService.Update(product);

            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }




        [HttpGet("Name/{productName}")]
        public IActionResult Get(string productName)
        {
            var result = _productService.GetByName(productName);

            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }




        [HttpGet("Category/{categoryID}")]
        public IActionResult Get(int categoryID)
        {
            var result = _productService.GetByCategory(categoryID);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }




        [HttpGet("WithCategory")]
        public IActionResult GetProductsWithCategory()
        {
            var result = _productService.GetProductsWithCategory();

            if (result.Success)
            {
                return Ok(result.Data.First().Category);
            }

            return BadRequest("Ürünler Listelenemedi");
        }



       

    }
}
