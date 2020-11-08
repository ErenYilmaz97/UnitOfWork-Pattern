using System.Collections.Generic;
using Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using MVC.ApiServices;

namespace MVC.ViewComponents
{
    public class CategoryTableViewComponent : ViewComponent
    {

        public IViewComponentResult Invoke(List<Category> categories)
        {
            return View("Default",categories);
        }
    }
}