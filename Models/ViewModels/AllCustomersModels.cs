using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
   public class AllCustomersModels
    {
        public int Id { get; set; }
        [Required]
        [StringLength(60, ErrorMessage = "Tên phải từ 3 đến 60 ký tự", MinimumLength = 3)]
        public string Name { get; set; }
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Yêu cầu nhập Email")]
        [EmailAddress(ErrorMessage = "Sai địa chỉ Email")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail không hợp lệ")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Yêu cầu nhập số điện thoại")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        [RegularExpression(@"^\d{8}|00\d{10}|\+\d{2}\d{8}", ErrorMessage = "Số không hợp lệ")]
        public string PhoneNumber { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Password phải từ 1 đến 20 ký tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string PassWordHash { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public string Address { get; set; }
        public int NumberOrder { get; set; }
        public decimal? TotalCost { get; set; }
        public string OrderRecently { get; set; }
   
    }
}
