using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Entities;

namespace Entities.Entities
{
    public class Category : IEntity
    {
        [Key]
        public int CategoryID { get; set; }
        public string Name { get; set; }


       virtual public IEnumerable<Product> Products { get; set; }
    }
}