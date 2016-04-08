using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHangNoiThat.Models;
using WebSiteBanHangNoiThat.DataBaseModels;
using System.Web.Security;

namespace WebSiteBanHangNoiThat.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        web_interiorEntities db = new web_interiorEntities();
        
        // GET: Admin/Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CheckLogin(DangNhapViewModel taikhoan)
        {
            if (ModelState.IsValid)
            {
                //cấu trúc linq kiểm tra xem có tài khoản nào khớp hay không
                var checkLogin = db.Users.Any(s => s.Name == taikhoan.UserLogin && s.PassWordHash == taikhoan.Password);
                //nếu có checklogin = true
                if (checkLogin)
                {
                    FormsAuthentication.SetAuthCookie(taikhoan.UserLogin, false);
                     return RedirectToAction("Index", "Home");
                  
                }
                else
                {
                    ModelState.AddModelError("", "Sai tên đăng nhập hoặc mật khẩu, kiểm tra lại nhé!");
                    //trở lại trang đăng nhập, báo lỗi
                    return View("Login", taikhoan);
                }
            }
            //Đăng nhập thành công, trở về trang chủ
            return Redirect("/");
        }

        public ActionResult LogOut()
        {
            //Đăng xuất khỏi ứng dụng
            FormsAuthentication.SignOut();
            //Về trang chủ
            return Redirect("/");
        }
    }
}