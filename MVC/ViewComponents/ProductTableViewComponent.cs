using System.Collections.Generic;
using Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using MVC.ApiServices;

namespace MVC.ViewComponents
{
    public class ProductTableViewComponent : ViewComponent
    {
        
        public IViewComponentResult Invoke(List<Product> products)
        {
            return View("Default",products);
        }
    }
}