using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FormsApp.Models
{
    public class Product
    {
        [Display(Name="Ürün Id")]
        public int ProductId { get; set; }

         [Display(Name="Ürün Adı")]
         [Required]
        public string Name { get; set; } = string.Empty;

         [Display(Name="Fiyat")]
         [Required]
         [Range(0,100000)] //validation sınırlama
        public decimal? Price { get; set; }

         [Display(Name="Resim")]
        public string Image { get; set; }= string.Empty;
        public bool IsActive { get; set; }
        
        [Display(Name ="Kategori")]
        public int CategoryId { get; set; }

    }

}