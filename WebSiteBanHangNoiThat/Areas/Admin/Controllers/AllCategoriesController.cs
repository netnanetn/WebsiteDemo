using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHangNoiThat.DataBaseModels;
using WebSiteBanHangNoiThat.Areas.Admin.Models;
using System.Net;
using System.Data.Entity;
using System.IO;
using System.Web.Helpers;

namespace WebSiteBanHangNoiThat.Areas.Admin.Controllers
{

  
    public class AllCategoriesController : Controller
    {
        public static string fileimg;

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
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var pic = System.Web.HttpContext.Current.Request.Files["HelpSectionImages"];
            }
      
            if (ModelState.IsValid)
            {
                Category savecategory = new Category();
                savecategory.Id = category.Id;
                savecategory.Name = category.Name;
                savecategory.Code = category.Code;
                savecategory.Description = category.Description;
                savecategory.Alias = category.Alias;
                savecategory.Image =fileimg;
                savecategory.CreateOn = DateTime.Now;

                db.Categories.Add(savecategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }
  
        //Post: upload file
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult UploadFile()
        {
            string _imgname = string.Empty;
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var pic = System.Web.HttpContext.Current.Request.Files["MyImages"];
                if (pic.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(pic.FileName);
                    var _ext = Path.GetExtension(pic.FileName);

                    _imgname = Guid.NewGuid().ToString();
                    var _comPath = Server.MapPath("/Upload/MVC_") + _imgname + _ext;
                    fileimg = "/Upload/MVC_"+_imgname+_ext;
                    _imgname = "MVC_" + _imgname + _ext;
                
                    ViewBag.Msg = _comPath;
                    var path = _comPath;
                   
                    // Saving Image in Original Mode
                    pic.SaveAs(path);
                  

                    // resizing image
                    MemoryStream ms = new MemoryStream();
                    WebImage img = new WebImage(_comPath);

                    if (img.Width > 200)
                        img.Resize(200, 200);
                    img.Save(_comPath);
                    // end resize
                }
            }
            return Json(Convert.ToString(_imgname), JsonRequestBehavior.AllowGet);
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
    

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category cate = db.Categories.Find(id);
            if (cate == null)
            {
                return HttpNotFound();
            }
            return View(cate);
        }
        // POST: Admin/AllCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category cate = db.Categories.Find(id);
            db.Categories.Remove(cate);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
       
     
    }
    }

