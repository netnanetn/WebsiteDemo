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
    public class ManufacturerController : Controller
    {
        ManufacturerDAO mn = new ManufacturerDAO();
        // GET: Admin/Manufacturer
        public ActionResult Index()
        {
            return View(mn.ListAll());
        }
        //lấy ra danh sách
      

        // GET: Admin/Manufacturer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Manufacturer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Manufacturer/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Manufacturer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Manufacturer/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Manufacturer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Manufacturer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
