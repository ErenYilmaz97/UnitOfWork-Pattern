using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Entities;

namespace Entities.Entities
{
    public class Category : Entity
    {
        [Key]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Zorunlu Alan")]
        [MinLength(3,ErrorMessage = "3-40 Karakter Olmalı")]
        [MaxLength(40,ErrorMessage = "3-40 Karakter Olmalı")]
        public string Name { get; set; }


       virtual public IEnumerable<Product> Products { get; set; }
    }
}