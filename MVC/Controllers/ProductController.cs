using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.ValidationRules.FluentValidation.Validators;
using Core.ApiServices;
using Core.Business;
using Entities.DbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Management.Smo;
using MVC.ApiServices;
using MVC.Models;
using MVC.PRG;


namespace MVC.Controllers
{
    
    [Controller]
    public class ProductController : Controller
    {

        private readonly ProductApiService _productApiService;
        private readonly CategoryApiService _categoryApiService;


        //DI
        public ProductController(ProductApiService productApiService, CategoryApiService categoryApiService)
        {
            _productApiService = productApiService;
            _categoryApiService = categoryApiService;
        }




        [HttpGet]
        public PartialViewResult GetProductsViewComponent()
        {
            //VİEWCOMPONENT DÖNÜYOR
            return PartialView("Components/ProductTable/Default", _productApiService.GetAll().Result.Data);
        }



        
        public async Task<IActionResult> Index()
        {
            var result = await _productApiService.GetAll();
            return View(result.Data);
        }



        
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var categories = await _categoryApiService.GetAll();
            return View(new AddProductModel(){Categories = categories.Data});
        }


        
        [HttpPost]
        public async Task<IActionResult> Add(AddProductModel addProductModel)
        {

            if (ModelState.IsValid)
            {
                var result = await _productApiService.AddProduct(addProductModel.Product);

                //AJAX İÇİN JSON DÖNÜYORUZ
                return Json(new { success = result.Success, message = result.Message });
            }

            return BadRequest();
        }


        
        [HttpGet]
        [Route("Product/Update/{id}")]
        public async Task<IActionResult> Update(int id)
        {

            var result = await _productApiService.Get(id);

            if (!result.Success)
            {
                return Json(new {success = result.Success, message = result.Message});
            }

            return View(new AddProductModel(){Product = result.Data, Categories = _categoryApiService.GetAll().Result.Data});

        }



        
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(AddProductModel addProductModel)
        {

            if(ModelState.IsValid)
            {
                var result = await _productApiService.UpdateProduct(addProductModel.Product);

                return Json(new { success = result.Success, message = result.Message });
                
            }

            return BadRequest();
        }



        [HttpGet]
        [Route("Product/Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productApiService.GetproductWithCategory(id);

            if (!result.Success)
            {
                return Json(new {success = result.Success, message = result.Message});
            }

            return View(result.Data);
        }



        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productApiService.DeleteProduct(id);
            return Json(new {success = result.Success, message = result.Message});
        }



        [HttpGet]
        public async Task<IActionResult> WithCategory()
        {
            var result = await _productApiService.GetProductsWithCategory();

            if (!result.Success)
            {
                ViewBag.ProductsNotFound = result.Message;
                
            }

            return View(result.Data);
        }





        [HttpGet("[controller]/Category/{categoryID}")]
        public async Task<IActionResult> GetByCategory(int categoryID)
        {
            var result = await _productApiService.GetByCategory(categoryID);

            if (!result.Success)
            {
                TempData["ProcessStatus"] = "Failed";
                TempData["ResponseMessage"] = result.Message;
                return RedirectToAction("Index");
            }

            return View("Index", result.Data);
        }





    }
}
