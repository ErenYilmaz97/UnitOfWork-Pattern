using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using MVC.ApiServices;
using MVC.PRG;

namespace MVC.Controllers
{
    [Controller]
    public class CategoryController : Controller
    {


        private readonly CategoryApiService _categoryApiService;


        //DI
        public CategoryController(CategoryApiService categoryApiService)
        {
            _categoryApiService = categoryApiService;
        }



        public async Task<IActionResult> Index()
        {

            if (TempData["CategoryProcessStatus"] != null && TempData["CategoryResponseMessage"] != null)
            {
                ViewBag.CategoryProcessStatus = TempData["CategoryProcessStatus"].ToString();
                ViewBag.CategoryResponseMessage = TempData["CategoryResponseMessage"].ToString();
            }



            var categories = await _categoryApiService.GetAllWithProducts();

            return View(categories.Data);
        }


        [ImportModelState]
        [HttpGet]
        public  IActionResult Add()
        {
            if (TempData["CategoryUpdateFailed"] != null)
            {
                ViewBag.CategoryUpdateFailed = TempData["CategoryUpdateFailed"].ToString();
            }


            return View();
        }



        [HttpPost]
        [ExportModelState]
        public async Task<IActionResult> Add(Category category)
        {
            var result = await _categoryApiService.AddCategory(category);

            if (!result.Success)
            {
                TempData["CategoryUpdateFailed"] = result.Message;
                return RedirectToAction("Add");
            }

            TempData["CategoryProcessStatus"] = "Succeeded";
            TempData["CategoryResponseMessage"] = result.Message;
            return RedirectToAction("Index", "Category");
        }


        [HttpGet("Category/Delete/{categoryID}")]
        public async Task<IActionResult> Delete(int categoryID)
        {
            var result = await _categoryApiService.DeleteCategory(categoryID);


            if (!result.Success)
            {
                TempData["CategoryProcessStatus"] = "Failed";
                TempData["CategoryResponseMessage"] = result.Message;
                return RedirectToAction("Index", "Category");
            }


            TempData["CategoryProcessStatus"] = "Succeeded";
            TempData["CategoryResponseMessage"] = result.Message;
            return RedirectToAction("Index", "Category");

        }


        [ImportModelState]
        [HttpGet("Category/Update/{categoryID}")]
        public async Task<IActionResult> Update(int categoryID)
        {
            if (TempData["CategoryUpdateFailed"] != null)
            {
                ViewBag.CategoryUpdateFailed = TempData["CategoryUpdateFailed"].ToString();
            }


            var result = await _categoryApiService.GetCategory(categoryID);

            if (!result.Success)
            {
                TempData["CategoryProcessStatus"] = "Failed";
                TempData["CategoryResponseMessage"] = result.Message;
                return RedirectToAction("Index", "Category");
            }

            return View(result.Data);
        }




        [HttpPost("Category/Update/{categoryID}")]
        [ExportModelState]
        public async Task<IActionResult> Update(Category category)
        {
            var result = await _categoryApiService.UpdateCategory(category);

            if (!result.Success)
            {
                TempData["CategoryUpdateFailed"] = result.Message;
                return RedirectToAction("Update", "Category");
            }

            TempData["CategoryProcessStatus"] = "Succeeded";
            TempData["CategoryResponseMessage"] = result.Message;
            return RedirectToAction("Index", "Category");
        }

        
    }
}
