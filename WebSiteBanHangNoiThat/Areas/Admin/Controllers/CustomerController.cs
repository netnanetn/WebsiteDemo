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
    public class CustomerController : Controller
    {
        CustomerDAO c = new CustomerDAO();
        OrderDAO o = new OrderDAO();
        // GET: Admin/Customer
        public ActionResult Index()
        {
            return View(c.ListAllCustomer());
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
                c.CreateCustomer(customer);
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
      
        public ActionResult EditName(int id)
        {
            return View(c.ListOrder(id));
        }
        
        public ActionResult ViewOrderDetails(int id)
        {

            return View(o.ListProductOnOrderDetails(id));
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
