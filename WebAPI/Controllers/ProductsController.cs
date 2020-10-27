using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackgroundJob.Schedules;
using Core.Business;
using Entities.Entities;
using Hangfire.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Management.Smo;
using WebAPI.ActionAttributes;

namespace WebAPI.Controllers
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
        public  IActionResult GetProducts()
        {
            var result =  _productService.GetAll();


            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest("Ürünler Listelenemedi.");
        }



        [HttpGet("{productID}")]
        public IActionResult GetProduct(int productID)
        {
            var result = _productService.GetById(productID);

            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }



        [HttpPost]
        [ValidationFilter]
        public IActionResult AddProduct(Product product)
        {
          var result =  _productService.Add(product);

          if (result.Success)
          {
              return Ok(result.Message);
          }

          return BadRequest(result.Message);
        }



        [HttpPost("AddRange")]
        public IActionResult AddProducts(List<Product> products)
        {
            var result = _productService.AddRange(products);

            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }



        [HttpDelete("{productID}")]
        public IActionResult DeleteProduct(int productID)
        {
            var result = _productService.Delete(productID);

            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }



        [HttpPut]
        [ValidationFilter]
        public IActionResult UpdateProduct(Product product)
        {
            var result = _productService.Update(product);

            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }



        [HttpGet("Name/{productName}")]
        public IActionResult GetProductByName(string productName)
        {
            var result = _productService.GetByName(productName);

            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }



        [HttpGet("Category/{categoryID}")]
        public IActionResult GetProductsByCategory(int categoryID)
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
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }




        [HttpGet("{productID}/WithCategory")]
        public IActionResult GetProductWithCategory(int productID)
        {
            var result = _productService.GetProductWithCategory(productID);

            if (result.Success)
            {
                return Ok(result.Data);
            }


            return BadRequest(result.Message);
        }



        [HttpGet("Deneme")]
        public IActionResult Deneme()
        {
            DelayedJobs.DatabaseBackupOperation();
            return Ok("Veritabanı Yedeği Alındı.");
        }
    }
}
