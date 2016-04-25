using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Models.DAO;
using Models.EF;
using Models.ViewModels;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using PagedList;

namespace WebSiteBanHangNoiThat.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        ProductDAO pr = new ProductDAO();
        public static string pathimg;
        public int saveidprocess;
        public  List<string> ListImg = new List<string>();
        web_interiorEntities db = new web_interiorEntities();
        // GET: Admin/Product
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
            var allproduct = pr.ListAllProduct(sortOrder, currentFilter, searchString, page);
            return View(allproduct.ToPagedList(pageNumber, pageSize));
            // return View(pr.ListAllProduct(sortOrder, currentFilter,searchString,page));
        }
        public ActionResult Details(int id)
        {
            return View();
        }
        // GET: Admin/Product/Create
        public ActionResult Create()
        {
            // trong Product Model khai bao 1 list danh sach nha san xuat
            // get danh sach nha san xuat tu db
            // dua vao trong product model
            // return View(ProductModel)
            ProductModels productModels = new ProductModels();
            productModels.ListManufacturers = pr.ListManufacturer();
            productModels.ListCategories = pr.ListCategories();
            // thêm ListImg
            productModels.ListAllImg = ListImg;
            return View(productModels);
        }

        // POST: Admin/Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Id,Name,Code,Image,Description,Alias,ProductKind,ProductManu,Price,SalePrice,Barcode,Size,Unit,StockStatus,Material,Available,ManufacturerId,ListCategories,CategorieId")] ProductModels product)
        {
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var pic = System.Web.HttpContext.Current.Request.Files["HelpSectionImages"];
            }


            if (ModelState.IsValid)
            {
                pr.CreateNewProducts(product, pathimg, ListImg);
                return RedirectToAction("Index");
            }

            return View(product);
        }
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
                    pathimg = "/Upload/MVC_" + _imgname + _ext;
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
        [HttpPost]
        public ActionResult UploadFiles2()
        {
            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                            //code bunus
                            pathimg = fname;
                            //allfile[i] = pathimg;

                            ListImg.Add(fname);
                        }

                        // Get the complete folder path and store the file inside it.  
                        fname = Path.Combine(Server.MapPath("~/Upload/ProductImg/"), fname);
                        file.SaveAs(fname);
                        // resizing image
                        MemoryStream ms = new MemoryStream();
                        WebImage img = new WebImage(fname);

                        if (img.Width > 200)
                            img.Resize(200, 200);
                        img.Save(fname);
                    }
                    // Returns message that successfully uploaded  
                    return Json(ListImg);
                    //  return Json(Convert.ToString(pathimg), JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }



        // GET: Admin/Product/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(pr.EditProduct(id));
        }

        // POST: Admin/Product/Edit/5
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult SaveEdit(int id)
        {
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var pic = System.Web.HttpContext.Current.Request.Files["HelpSectionImages"];
            }

            //Kiểm tra hợp lệ dữ liệu phía server
            var product = db.Products.Find(id);

            if (TryUpdateModel(product, "", new string[] { "Name", "Image", "Code", "Barcode", "Description", "CategorieId", "ManufacturerId", "Price", "SalePrice", "Alias", "StockStatus", "Available", "Material", "Unit", "Size" }))
            {
                if (pathimg != product.Image)
                {
                    product.Image = pathimg;
                }
                product.ModifiedOn = DateTime.Now;
                //Cập nhật thông tin 
                db.Entry(product).State = System.Data.Entity.EntityState.Modified;


                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // GET: Admin/Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Product/Delete/5
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
