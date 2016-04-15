using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
   public class OrderDetailsModels
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public Nullable<decimal> ProductPrice { get; set; }
        public string ProductCode { get; set; }
        public Nullable<int> Number { get; set; }
        public Nullable<System.DateTime> CreateOn { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<int> OrderId { get; set; }
        public Nullable<decimal> PriceItem { get; set; }
        public string ProductSize { get; set; }
    }
}
