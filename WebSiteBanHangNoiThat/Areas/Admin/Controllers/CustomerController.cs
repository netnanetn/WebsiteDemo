using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHangNoiThat.Areas.Admin.Models;
using WebSiteBanHangNoiThat.DataBaseModels;
namespace WebSiteBanHangNoiThat.Areas.Admin.Controllers
{
    public class CustomerController : Controller
    {
        web_interiorEntities db = new web_interiorEntities();
        // GET: Admin/Customer
        public ActionResult Index()
        {
            return View(ListAll());
        }
        public List<AllCustomersModels> ListAll()
        {
            using (web_interiorEntities db = new web_interiorEntities())
            {
                var query = from pro in db.Users
                            from or in pro.Orders
                            //join or in db.Orders on pro.Id equals or.UserId
                            where 1 == 1
                            select new AllCustomersModels()
                            {
                                Id = pro.Id,
                                Name = pro.Name,
                               Email=pro.Email,
                              TotalCost=or.TotalPrice,
                            OrderRecently=or.Name,
                            NumberOrder=1
             
                            };
               
                return query.ToList();
            }


        }

        // GET: Admin/Customer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Id,Name,Email,Address,Phonenumber")] AllCustomersModels customer)
        {
            if (ModelState.IsValid)
            {
                User user = new User();
                user.Name = customer.Name;
                user.Address = customer.Address;
                user.PhoneNumber = customer.PhoneNumber;
                var cate = db.Roles.Find(customer.Id);
                user.Roles.Add(cate);
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Admin/Customer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Customer/Edit/5
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

        // GET: Admin/Customer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Customer/Delete/5
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
