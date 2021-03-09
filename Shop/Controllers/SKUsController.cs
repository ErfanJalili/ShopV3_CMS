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
    public class SKUsController : Controller
    {
        // GET: SKUs
        private ApplicationDbContext _context;

        public SKUsController()
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
            return View("SKUsForm");
        }

        // POST: SKUs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,ManagerName,Description,,Address,Phone,Mobile,WebSite,Instagram,Facebook,Telegram,Whatsapp,StartTime,EndTime,GoogleLocation,NeshanLocation")] SKU sku)
        {
            if (ModelState.IsValid)
            {
                _context.SKUs.Add(sku);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sku);
        }


        // GET: skus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SKU sku = _context.SKUs.Find(id);
            if (sku == null)
            {
                return HttpNotFound();
            }
            return View("SKUsForm", sku);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,ManagerName,Description,Address,Phone,Mobile,WebSite,Instagram,Facebook,Telegram,Whatsapp,StartTime,EndTime,GoogleLocation,NeshanLocation")] SKU sku)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(sku).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sku);
        }

    }
}