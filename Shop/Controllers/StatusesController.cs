using Shop.Models;
using Shop.Models.Product;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Shop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StatusesController : Controller
    {
        
        // GET: SKUs
        private ApplicationDbContext _context;

        public StatusesController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: SKUs
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Create()
        {
            return View("StatusForm");
        }

        // POST: SKUs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Status status)
        {
            if (ModelState.IsValid)
            {
                _context.Statuses.Add(status);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(status);
        }


        // GET: skus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Status status = _context.Statuses.Find(id);
            if (status == null)
            {
                return HttpNotFound();
            }
            return View("StatusForm", status);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Status status)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(status).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(status);
        }
    }
}