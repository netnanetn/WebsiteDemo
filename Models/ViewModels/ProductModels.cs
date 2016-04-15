using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
   public class ProductModels
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Alias { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> SalePrice { get; set; }
        public string Barcode { get; set; }
        public Nullable<int> CategorieId { get; set; }
        public int ManufacturerId { get; set; }
        public string StockStatus { get; set; }
        public string Available { get; set; }
        public Nullable<System.DateTime> CreateOn { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public string Material { get; set; }
        public string Size { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public string ProductKind { get; set; }
        public string ProductManu { get; set; }
        public List<ManufacturerModels> ListManufacturers { get; set; }
        public List<AllCategoriesModels> ListCategories { get; set; }
    }
}
