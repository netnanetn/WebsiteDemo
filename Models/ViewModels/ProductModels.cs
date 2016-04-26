using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Models.ViewModels
{
   public class ProductModels
    {
        public int Id { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Name { get; set; }
        public string Image { get; set; }
        public string Alias { get; set; }
        [Range(1, 100000000)]
        [DataType(DataType.Currency)]
        public Nullable<decimal> Price { get; set; }
        [Range(1, 100000000)]
        [DataType(DataType.Currency)]
        public Nullable<decimal> SalePrice { get; set; }
        public string Barcode { get; set; }
        public Nullable<int> CategorieId { get; set; }
        public int ManufacturerId { get; set; }
        public string StockStatus { get; set; }
        public string Available { get; set; }
        [Display(Name = "Create Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> CreateOn { get; set; }
        [Display(Name = "Modifile Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public string Material { get; set; }
        public string Size { get; set; }
        public string Code { get; set; }
       [AllowHtml]
        public string Description { get; set; }
        public string Unit { get; set; }
        public string ProductKind { get; set; }
        public string ProductManu { get; set; }
        public List<ManufacturerModels> ListManufacturers { get; set; }
        public List<AllCategoriesModels> ListCategories { get; set; }
        public List<string> ListAllImg { get; set; }
    }
}
