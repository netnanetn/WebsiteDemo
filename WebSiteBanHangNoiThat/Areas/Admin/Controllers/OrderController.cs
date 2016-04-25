using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.DAO;
using Models.EF;
using Models.ViewModels;
using PagedList;

namespace WebSiteBanHangNoiThat.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        // GET: Admin/Order
        web_interiorEntities db = new web_interiorEntities();
        OrderDAO o = new OrderDAO();
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
            var allorder = o.ListAllOrder(sortOrder, currentFilter, searchString, page);
            return View(allorder.ToPagedList(pageNumber, pageSize));
          //  return View(o.ListAllOrder());
        }
       
        // GET: Admin/Order/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Order/Create
        public ActionResult Create()
        {
            AllOrderModels allorder = new AllOrderModels();
            allorder.ListProduct = ListProducts();
            return View();
        }

        // POST: Admin/Order/Create
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

        // GET: Admin/Order/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Order/Edit/5
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

        // GET: Admin/Order/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Order/Delete/5
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



        public List<ProductModels> ListProducts()
        {
            using (web_interiorEntities db = new web_interiorEntities())
            {
                var query = from pro in db.Products

                            select new ProductModels()
                            {
                                Id = pro.Id,
                                Name = pro.Name,
                                Price=pro.Price,


                            };

                return query.ToList();
            }

        }
    }

}
