using Shop.Models;
using Shop.Models.Order;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Shop.Controllers
{
    public class OrderCoponsController : Controller
    {
        private ApplicationDbContext _context;

        public OrderCoponsController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: OrderCopons
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: Brands/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Percent")] OrderCopon orderCopon)
        {
            if (ModelState.IsValid)
            {
                _context.OrderCopons.Add(orderCopon);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(orderCopon);
        }


        // GET: Brands/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderCopon orderCopon = _context.OrderCopons.Find(id);
            if (orderCopon == null)
            {
                return HttpNotFound();
            }
            return View("Create", orderCopon);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Percent")] OrderCopon orderCopon)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(orderCopon).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(orderCopon);
        }
    }
}