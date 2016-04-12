using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace WebSiteBanHangNoiThat.Areas.Admin.Models
{
    public class ManufacturerModels
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Logo { get; set; }
        public string PhoneNumber { get; set; }
        public string TaxNumber { get; set; }
        public Nullable<System.DateTime> CreateOn { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }

    }
}