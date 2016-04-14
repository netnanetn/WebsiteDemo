using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSiteBanHangNoiThat.Areas.Admin.Models
{
    public class AllOrderModels
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string UserAddress { get; set; }
        public string UserPhoneNumber { get; set; }
        public Nullable<bool> ShippingStatus { get; set; }
        public Nullable<bool> PaymentStatus { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<System.DateTime> CreateOn { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<decimal> TotalPrice { get; set; }
       
        public string StringShipping
        {
            get
            {
                if (ShippingStatus == false)
                {
                    return "Hàng chưa được chuyển";
                }
                return "Hàng đã chuyển";
            }
            set
            {
                this.StringShipping = StringShipping;
            }
        }
        public string StringPayment
        {
            get
            {
                if (PaymentStatus == false)
                {
                    return "Chưa thanh toán";
                }
                return "Đã thanh toán";
            }
            set
            {
                this.StringPayment = StringPayment;
            }
        } 
 
    }
}