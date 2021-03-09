using Shop.Models;
using Shop.Models.Slider;
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
    public class IndexSlidersController : Controller
    {
        private ApplicationDbContext _context;

        public IndexSlidersController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: IndexSliders
        public ActionResult Index()
        {
            var Sliders = _context.Sliders.ToList();
            return View("IndexSliders", Sliders);
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider= _context.Sliders.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // GET: Brands/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Brands/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SliderImageUrl,Description,Alt")] Slider slider)
        {
            if (ModelState.IsValid)
            {
                _context.Sliders.Add(slider);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(slider);
        }

        // GET: Brands/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider= _context.Sliders.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View( slider);
        }

        // POST: Brands/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SliderImageUrl,Description,Alt")] Slider slider)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(slider).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(slider);
        }

        // GET: Brands/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider= _context.Sliders.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // POST: Brands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Slider slider= _context.Sliders.Find(id);
            _context.Sliders.Remove(slider);
            _context.SaveChanges();
            return RedirectToAction("IndexSliders");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}