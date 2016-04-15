using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.DAO;
using Models.EF;
using Models.ViewModels;

namespace WebSiteBanHangNoiThat.Areas.Admin.Controllers
{
    public class ProfileController : Controller
    {
        web_interiorEntities db = new web_interiorEntities();
        // GET: Admin/Profile
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Profile(string Name)
        {
            var nguoidung = db.Users.Find(Name);

            return View(nguoidung);
        }
    }
}