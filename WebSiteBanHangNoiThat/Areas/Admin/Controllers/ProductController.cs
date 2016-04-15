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
namespace WebSiteBanHangNoiThat.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        ProductDAO pr = new ProductDAO();
        public static string pathimg;
        public static int saveidprocess;
        web_interiorEntities db = new web_interiorEntities();
        
        // GET: Admin/Product
        public ActionResult Index()
        {
            return View(pr.ListAllProduct());
        }
        // list product
  
        // GET: Admin/Product/Details/5
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
            productModels.ListManufacturers = ListManufacturer();
            productModels.ListCategories = ListCategories();
       
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
                // thêm vào product
                Product createProduct = new Product();
                createProduct.Id = product.Id;
                createProduct.Name = product.Name;
                createProduct.Code = product.Code;
                createProduct.Description = product.Description;
                createProduct.Alias = product.Alias;
                createProduct.Image = pathimg;
                createProduct.CreateOn = DateTime.Now;
                createProduct.Price = product.Price;
                createProduct.SalePrice = product.SalePrice;
                createProduct.Barcode = product.Barcode;
                createProduct.Size = product.Size;
                createProduct.Unit = product.Unit;
                createProduct.StockStatus = product.StockStatus;
                createProduct.Available = product.Available;
                createProduct.Material = product.Material;
                createProduct.ManufacturerId = product.ManufacturerId;
                createProduct.CategorieId = product.CategorieId;

                var cate = db.Categories.Find(product.CategorieId);
                createProduct.Categories.Add(cate);
                // lay danh sach category theo danh sach cac category_ids duoc chon tu client
                // gan createProduct.Categories = danh sach lay dc o tren
                // save vào db

                // them vào Manufacturor
                //Manufacturer manufacturer = new Manufacturer();
                //manufacturer.Name = product.ProductManu;
                //db.Manufacturers.Add(manufacturer);
                    // lưu bảng categories
                //Category category = new Category();
                //category.Name = product.ProductKind;
                //db.Categories.Add(category);
               
                db.Products.Add(createProduct);
                db.SaveChanges();
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
      

        // GET: Admin/Product/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        
            Product product = db.Products.Find(id);

            ProductModels productModels = new ProductModels();
            productModels.Id = product.Id;
            productModels.Name = product.Name;
            productModels.Alias = product.Alias;
            productModels.Image = product.Image;
            productModels.Price = product.Price;
            productModels.SalePrice = product.SalePrice;
            productModels.Barcode = product.Barcode;
            productModels.CategorieId = product.CategorieId;
            productModels.StockStatus = product.StockStatus;
            productModels.Available = product.Available;
            productModels.Material = product.Material;
            productModels.Size = product.Size;
            productModels.Code = product.Code;
            productModels.Description = product.Description;
            productModels.Unit = product.Unit;

            productModels.ListCategories = ListCategories();
            productModels.ListManufacturers = ListManufacturer();
           
           
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(productModels);
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

            if (TryUpdateModel(product, "", new string[] { "Name","Image", "Code", "Barcode", "Description", "CategorieId", "ManufacturerId", "Price", "SalePrice", "Alias", "StockStatus", "Available", "Material", "Unit", "Size" }))
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
   // lấy ra danh sách các nhà sản xuất
        //lấy ra danh sách
        public List<ManufacturerModels> ListManufacturer()
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
                                TaxNumber = pro.TaxNumber

                            };
               
                return query.ToList();
            }

        }
        public List<AllCategoriesModels> ListCategories()
        {
            using (web_interiorEntities db = new web_interiorEntities())
            {
                var query = from pro in db.Categories

                            select new AllCategoriesModels()
                            {
                                Id = pro.Id,
                                Name = pro.Name,
                               

                            };
               
                return query.ToList();
            }

        }

   

     

    }
}
