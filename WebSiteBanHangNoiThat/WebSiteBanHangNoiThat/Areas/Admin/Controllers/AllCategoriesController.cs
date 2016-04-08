using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHangNoiThat.DataBaseModels;
using WebSiteBanHangNoiThat.Areas.Admin.Models;

namespace WebSiteBanHangNoiThat.Areas.Admin.Controllers
{

   
    public class AllCategoriesController : Controller
    {

        web_interiorEntities db = new web_interiorEntities();
        // GET: Admin/AllCategories
         [Authorize(Roles = "SuperAdmin")]
        public ActionResult Index()
        {
           

            return View(ListAll());
        }
         
        public List<AllCategoriesModels> ListAll()
        {
            using (web_interiorEntities db = new web_interiorEntities())
            {
                var query = from pro in db.Categories
                            select new AllCategoriesModels()
                            {
                              Name=  pro.Name,
                              Description=   pro.Description,
                              Image=pro.Image
                            };

                return query.ToList();
            }
            //var prquery = from p in db.Categories
            //              select new { 
                          
            //              p.Name,
            //              p.Description
            //              };
            //var prList = prquery.ToList();
            //return prList;
         
        }
        // GET: Admin/AllCategories/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/AllCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/AllCategories/Create
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

        // GET: Admin/AllCategories/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/AllCategories/Edit/5
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

        // GET: Admin/AllCategories/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/AllCategories/Delete/5
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
