using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models.DAO;
using Models.EF;
using Models.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Web.Security;

namespace WebSiteBanHangNoiThat.Models
{

    public class TaiKhoanViewModel
    {
    }
    public class DangNhapViewModel
    {
        [Required]
        [Display(Name = "UserLogin")]
        public string UserLogin { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
    public class DangKyViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "UserLogin")]
        public string UserLogin { get; set; }



        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword")]
        [Compare("Password", ErrorMessage = "Mật khẩu không trùng khớp")]
        public string ConfirmPassword { get; set; }
    }
    public class PhanQuyenProvider : RoleProvider
    {
        public override string[] GetRolesForUser(string namelogin)
        {
            using (web_interiorEntities db = new web_interiorEntities())
            {
                var taikhoan = db.Users.FirstOrDefault(x => x.Name == namelogin);
                if (taikhoan == null)
                {
                    return null;
                }
                else
                {
                    string[] listQuyen = taikhoan.Roles.Select(x => x.Name).ToArray();
                    return listQuyen;
                }
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}