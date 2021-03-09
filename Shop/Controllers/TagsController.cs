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
    public class TagsController : Controller
    {
        private ApplicationDbContext _context;

        public TagsController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Tags
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View("TagForm");
        }

        // POST: Brands/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description")] Tag tag)
        {
            if (ModelState.IsValid)
            {
                _context.Tags.Add(tag);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tag);
        }


        // GET: Brands/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tag tag = _context.Tags.Find(id);
            if (tag == null)
            {
                return HttpNotFound();
            }
            return View("TagForm", tag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description")] Tag tag)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(tag).State = EntityState.Modified;
               _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tag);
        }

    }
}