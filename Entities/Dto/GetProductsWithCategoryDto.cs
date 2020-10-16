using Entities.Abstract;

namespace Entities.Dto
{
    public class GetProductsWithCategoryDto : IDto
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int CategoryID { get; set; }


        public string CategoryName { get; set; }
    }
}