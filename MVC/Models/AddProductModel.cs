using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Entities.Entities;

namespace MVC.Models
{
    public class AddProductModel
    {
        public List<Category> Categories { get; set; }
        public Product Product { get; set; }

    }
}