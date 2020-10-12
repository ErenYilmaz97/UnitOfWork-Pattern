using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Entities.Abstract;

namespace Entities.Entities
{
    public class Category : IEntity
    {
        [Key] 
        public int CategoryID { get; set; }
        public string Name { get; set; }

        public List<Product> Products { get; set; }
    }
}
