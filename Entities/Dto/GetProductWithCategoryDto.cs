using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;

namespace Entities.Dto
{
    public class GetProductWithCategoryDto : IDto
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string CategoryName { get; set; }
    }
}
