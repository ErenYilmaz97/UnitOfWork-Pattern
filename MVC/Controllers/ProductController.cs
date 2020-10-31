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


        
        public async Task<IActionResult> Index()
        {
            if(TempData["ProcessStatus"] != null && TempData["ResponseMessage"] != null)
            {
                ViewBag.ProcessStatus = TempData["ProcessStatus"].ToString();
                ViewBag.ResponseMessage = TempData["ResponseMessage"].ToString();
            }




            var result = await _productApiService.GetAll();
            return View(result.Data);
        }



        [ImportModelState]
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            if (TempData["AddProductFailed"] != null)
            {
                ViewBag.AddProductFailed = TempData["AddProductFailed"].ToString();
            }


            var categories = await _categoryApiService.GetAll();
            return View(new AddProductModel(){Categories = categories.Data});
        }


        
        [HttpPost]
        [ExportModelState]
        public async Task<IActionResult> Add(AddProductModel addProductModel)
        {
            var result = await _productApiService.AddProduct(addProductModel.Product);

            if (!result.Success)
            {
                TempData["AddProductFailed"] = result.Message;
                return RedirectToAction("Add");
            }

            TempData["ProcessStatus"] = "Succeeded";
            TempData["ResponseMessage"] = result.Message;
            return RedirectToAction("Index");
        }


        [ImportModelState]
        [HttpGet("Product/Update/{productID}")]
        public async Task<IActionResult> Update(int productID)
        {

            if (TempData["UpdateProductFailed"] != null)
            {
                ViewBag.UpdateProductFailed = TempData["UpdateProductFailed"].ToString();
            }



            var result = await _productApiService.Get(productID);

            if (!result.Success)
            {
                TempData["ProcessStatus"] = "Failed";
                TempData["ResponseMessage"] = result.Message;
                return RedirectToAction("Index");
            }

            return View(new AddProductModel(){Product = result.Data, Categories = _categoryApiService.GetAll().Result.Data});


        }



        
        [HttpPost("Product/Update/{productID}")]
        [ExportModelState]
        public async Task<IActionResult> Update(AddProductModel addProductModel)
        {
            var result = await _productApiService.UpdateProduct(addProductModel.Product);

            if (!result.Success)
            {
                TempData["UpdateProductFailed"] = result.Message;
                return RedirectToAction("Update");
            }

            TempData["ProcessStatus"] = "Succeeded";
            TempData["ResponseMessage"] = result.Message;
            return RedirectToAction("Index");
        }



        [HttpGet("Product/Delete/{productID}")]
        public async Task<IActionResult> Delete(int productID)
        {
            var result = await _productApiService.DeleteProduct(productID);

            if (!result.Success)
            {
                TempData["ProcessStatus"] = "Failed";
                TempData["ResponseMessage"] = result.Message;
                return RedirectToAction("Index");
            }


            TempData["ProcessStatus"] = "Succeeded";
            TempData["ResponseMessage"] = result.Message;
            return RedirectToAction("Index");
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
