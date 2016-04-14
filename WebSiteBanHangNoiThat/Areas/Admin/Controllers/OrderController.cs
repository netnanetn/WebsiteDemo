﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHangNoiThat.DataBaseModels;
using WebSiteBanHangNoiThat.Areas.Admin.Models;

namespace WebSiteBanHangNoiThat.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        // GET: Admin/Order
        public ActionResult Index()
        {
            return View(ListAll());
        }
        public List<AllOrderModels> ListAll()
        {
            using (web_interiorEntities db = new web_interiorEntities())
            {
                var query = from pro in db.Orders

                            select new AllOrderModels()
                            {
                                Id = pro.Id,
                                Name = pro.Name,
                                CreateOn = pro.CreateOn,
                                UserName = pro.UserName,
                                PaymentStatus = pro.PaymentStatus.Value,
                                ShippingStatus = pro.ShippingStatus,
                                TotalPrice=pro.TotalPrice,
                           

                            };

                return query.ToList();
            }


        }


  
       
        // GET: Admin/Order/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Order/Create
        public ActionResult Create()
        {
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
    }
}
