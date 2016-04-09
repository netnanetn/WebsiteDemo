using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHangNoiThat.DataBaseModels;
using WebSiteBanHangNoiThat.Areas.Admin.Models;
using System.Net;
using System.Data.Entity;

namespace WebSiteBanHangNoiThat.Areas.Admin.Controllers
{

   
    public class AllCategoriesController : Controller
    {

        web_interiorEntities db = new web_interiorEntities();
        public ActionResult Test()
        {
            return View();
        }
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
                                Id=pro.Id,
                              Name=  pro.Name,
                              Description= pro.Description,
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
        // edit danh mục sản phẩm
  
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
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Id,Name,Code,Image,Description,Alias")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Admin/AllCategories/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
                               
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/AllCategories/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,Name,Code,Image,Description,Alias,CreateOn,ModifiedOn,Status")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }
        public ActionResult SaveEdit(int id)
        {
            //Kiểm tra hợp lệ dữ liệu phía server
            var category = db.Categories.Find(id);

            if (TryUpdateModel(category, "", new string[] { "Name","Code","Image","Description","Alias","CreateOn","ModifiedOn","Status" }))
            {
                //Cập nhật thông tin người dùng
                db.Entry(category).State = System.Data.Entity.EntityState.Modified;
                //Thêm quyền người dùng
            
                db.SaveChanges();
            }
            return RedirectToAction("Index");
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
