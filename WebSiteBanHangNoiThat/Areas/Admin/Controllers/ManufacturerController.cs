using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHangNoiThat.DataBaseModels;
using WebSiteBanHangNoiThat.Areas.Admin.Models;
namespace WebSiteBanHangNoiThat.Areas.Admin.Controllers
{
    public class ManufacturerController : Controller
    {
        // GET: Admin/Manufacturer
        public ActionResult Index()
        {
            return View(ListAll());
        }
        //lấy ra danh sách
        public List<ManufacturerModels> ListAll()
        {
            using (web_interiorEntities db = new web_interiorEntities())
            {
                var query = from pro in db.Manufacturers
                          
                            select new ManufacturerModels()
                            {
                                Id = pro.Id,
                                Name = pro.Name,
                                Address = pro.Address,
                                Logo = pro.Logo,
                                PhoneNumber = pro.PhoneNumber,
                                TaxNumber=pro.TaxNumber

                            };


                return query.ToList();
            }

        }

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
