using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Models.DAO;
using Models.EF;
using Models.ViewModels;
using WebSiteBanHangNoiThat.Models;

namespace WebSiteBanHangNoiThat.Controllers
{
    public class TaiKhoanController : Controller
    {
        // GET: TaiKhoan
        public ActionResult Index()
        {
            return View();
        }
        web_interiorEntities db = new web_interiorEntities();
        //Hàm trả về trang view để đăng ký tài khoản
        public ActionResult DangKy()
        {
            return View();
        }

        //Hàm lưu thông tin đăng ký
        [HttpPost]
        public ActionResult LuuDangKy([Bind(Include = "Email, UserLogin, Password")]User taikhoan)
        {
            //Kiểm tra hợp lệ dữ liệu
            if (ModelState.IsValid)
            {
                //kiểm tra tên đăng nhập, email có bị đã tồn tại hay chưa?
                var checkUserName = db.Users.Any(s => s.Name == taikhoan.Name);
                var checkEmail = db.Users.Any(s => s.Email == taikhoan.Email);

                //Các lỗi nếu có trong quá trình đăng ký tài khoản
                if (checkUserName)
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại!");
                }
                if (checkEmail)
                {
                    ModelState.AddModelError("", "Email đã có người sử dụng!");
                }
                if (checkUserName == true || checkEmail == true)
                {
                    //trả về view đăng ký và thông báo các lỗi ở trên
                    return View("DangKy");
                }
                else
                {
                    //Lưu thông tin tài khoản vào CSDL
                    db.Users.Add(taikhoan);
                    db.SaveChanges();
                    //xác thực tài khoản trong ứng dụng
                    FormsAuthentication.SetAuthCookie(taikhoan.Name, false);
                    //trả về trang chủ đăng ký thành công
                    return Redirect("/");

                }
            }
            else
            {
                //Trang báo lỗi nhập liệu không hợp lệ!
                return View("Error");
            }
        }

        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult KiemTraDangNhap(LoginModel taikhoan)
        {
            if (ModelState.IsValid)
            {
                //cấu trúc linq kiểm tra xem có tài khoản nào khớp hay không
                var checkLogin = db.Users.Any(s => s.Name == taikhoan.UserLogin && s.PassWordHash == taikhoan.Password);
                //nếu có checklogin = true
                if (checkLogin)
                {
                    //FormsAuthentication.SetAuthCookie(taikhoan.UserLogin, false);
                    // return RedirectToAction("Index", "Home");
                    if (Session["DuongDan"] != null)
                    {
                       Response.Redirect("~" + Session["DuongDan"].ToString()); 
                       // Response.Redirect("~/Admin/Home/Index");
                      
                    }
                    else RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Sai tên đăng nhập hoặc mật khẩu, kiểm tra lại nhé!");
                    //trở lại trang đăng nhập, báo lỗi
                    return View("DangNhap", taikhoan);
                }
            }
            //Đăng nhập thành công, trở về trang chủ
            return Redirect("/");
        }

        public ActionResult DangXuat()
        {
            //Đăng xuất khỏi ứng dụng
            FormsAuthentication.SignOut();
            //Về trang chủ
            return Redirect("/");
        }
    }
}