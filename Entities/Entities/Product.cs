using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Entities.Entities
{
    public class Product : Entity
    {
        [Key]
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Zorunlu Alan")]
        [MinLength(2,ErrorMessage = "Ürün Adı 2 Karakterden Az Olamaz")]
        [MaxLength(50,ErrorMessage = "Ürün Adı 50 Karakterden Fazla Olamaz")]
        [DisplayName("Ürün Adı")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Zorunlu Alan")]
        [Range(1,double.MaxValue,ErrorMessage = "Fiyat Bilgisi 0 Olamaz")]
        [DisplayName("Ürün Fiyatı")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Zorunlu Alan")]
        [Range(1, int.MaxValue, ErrorMessage = "Stok Bilgisi 0 Olamaz")]
        [DisplayName("Stok Sayısı")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "Zorunlu Alan")]
        [Range(1, int.MaxValue, ErrorMessage = "Kategori Numarası 0 Olamaz")]
        [DisplayName("Kategori")]
        public int CategoryID { get; set; }

       virtual public Category Category { get; set; }
    }
}