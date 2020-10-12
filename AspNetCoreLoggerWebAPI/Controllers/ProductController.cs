using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace AspNetCoreLoggerWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductService _productService;

        //DI
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }



        [HttpGet("Products")]
        public IActionResult Products()
        {
            var result = _productService.GetAll();

            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest("Ürünler Listelenemedi.");
        }




        [HttpPost("AddProduct")]
        public IActionResult AddProduct(Product product)
        {
            var result = _productService.Add(product);

            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }




        [HttpPost("AddProducts")]
        public IActionResult AddProducts(List<Product> products)
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




        [HttpPut("Update")]
        public IActionResult UpdateProduct(Product product)
        {
            var result = _productService.Update(product);

            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }

    }
}
