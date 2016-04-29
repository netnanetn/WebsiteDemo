using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Data.Entity;
using System.IO;
using System.Web.Helpers;
using Models.DAO;
using Models.EF;
using Models.ViewModels;
using PagedList;

namespace WebSiteBanHangNoiThat.Areas.Admin.Controllers
{

  [Authorize(Roles = "SuperAdmin")]
    public class AllCategoriesController : Controller
    {
        CategoryDAO x = new CategoryDAO();

        public static string fileimg;

        web_interiorEntities db = new web_interiorEntities();
        public ActionResult Test()
        {
          
            return View();
        }
        // GET: Admin/AllCategories
         [Authorize(Roles = "SuperAdmin")]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            var allcategory = x.ListAllCategory(sortOrder, currentFilter, searchString, page);
            return View(allcategory.ToPagedList(pageNumber, pageSize));
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
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Code,Image,Description,Alias")] Category category)
        {
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var pic = System.Web.HttpContext.Current.Request.Files["HelpSectionImages"];
            }
      
            if (ModelState.IsValid)
            { 
                x.CreateCategory(category,fileimg);
                
                //Category savecategory = new Category();
                //savecategory.Id = category.Id;
                //savecategory.Name = category.Name;
                //savecategory.Code = category.Code;
                //savecategory.Description = category.Description;
                //savecategory.Alias = category.Alias;
                //savecategory.Image =fileimg;
                //savecategory.CreateOn = DateTime.Now;

                //db.Categories.Add(savecategory);
                //db.SaveChanges();
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
              Category c= x.EditCategory(id);
            if (c == null)
            {
                return HttpNotFound();
            }
            return View(c);
        }

        // POST: Admin/AllCategories/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,Name,Code,Image,Description,Alias,CreateOn,ModifiedOn,Status")] Category category)
        {
            if (ModelState.IsValid)
            {
                x.SaveEditCategory(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult SaveEdit(int id)
        {
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var pic = System.Web.HttpContext.Current.Request.Files["HelpSectionImages"];
            }
      
            //Kiểm tra hợp lệ dữ liệu phía server
         //   Category category = db.Categories.Find(id);
            Category category = x.FindCategory(id);
       
            if (TryUpdateModel(category, "", new string[] { "Name","Code","Image","Description","Alias","CreateOn","ModifiedOn","Status" }))
            {
                if (fileimg != category.Image)
                {
                    category.Image = fileimg;
                }
          
                //Cập nhật thông tin 
              
                x.SaveEditCategory(category);
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
            x.DeleteCategory(id);
            return RedirectToAction("Index");
        }
      [HttpGet]
        public ActionResult CategoryPatialView(int id)
        {
  
            Category cate = db.Categories.Find(id);
            AllCategoriesModels mod = new AllCategoriesModels();
            var pr = db.Products.Where(x => x.CategorieId == id);
            int count = 0;
            foreach (var item in pr)
            {
                count++;
            }
            mod.Count = count;
            mod.Id = cate.Id;
            mod.Name = cate.Name;
            return PartialView(mod);
        }
      public ActionResult DeleteCategory(int id)
      {
          try
          {
              x.DeleteCategoryDao(id);
              return RedirectToAction("Index");
          }
          catch
          {
              return View();
          }
      }
     
    }
    }

