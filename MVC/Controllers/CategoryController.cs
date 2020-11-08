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



        public PartialViewResult GetCategoriesViewComponent()
        {
            //VIEWCOMPONENT DÖNÜYOR
            return PartialView("Components/CategoryTable/Default",_categoryApiService.GetAllWithProducts().Result.Data);
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


        
        [HttpGet]
        public  IActionResult Add()
        {

            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Add(Category category)
        {
            if (ModelState.IsValid)
            {
                var result = await _categoryApiService.AddCategory(category);
                return Json(new {success = result.Success, message = result.Message});
            }

            return BadRequest();
        }



        [HttpGet]
        [Route("Category/Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _categoryApiService.GetCategory(id);

            if (!result.Success)
            {
                return Json(new {success = result.Success, message = result.Message});
            }

            return View(result.Data);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _categoryApiService.DeleteCategory(id);
            return Json(new {success = result.Success, message = result.Message});
        }


       
        [HttpGet]
        [Route("Category/Update/{id}")]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _categoryApiService.GetCategory(id);

            if (!result.Success)
            {
                return Json(new {success = result.Success, message = result.Message});
            }

            return View(result.Data);
        }




        [HttpPost]
        //[Route("Category/Update")]
        public async Task<IActionResult> UpdateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                var result = await _categoryApiService.UpdateCategory(category);
                return Json(new {success = result.Success, message = result.Message});
            }

            return BadRequest();
        }

        
    }
}
