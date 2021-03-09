using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Shop.Models;
using Shop.Models.Order;

namespace Shop.Controllers
{
    public class OrderDetailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OrderDetails
        public ActionResult Index()
        {
            //db.OrderDetails.Include(o => o.Order).Include(o => o.Product).ToList()


            return View();
        }

        // GET: OrderDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // GET: OrderDetails/Create
        public ActionResult Create()
        {
            ViewBag.OrderId = new SelectList(db.Orders, "Id", "FactorNumber");
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name");
            return View();
        }

        // POST: OrderDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,OrderId,ProductId,Quantity,SinglePrice,SubTotalPrice,Created_at,Updated_at")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                db.OrderDetails.Add(orderDetail);
                orderDetail.Created_at = DateTime.Now;
                orderDetail.SubTotalPrice = orderDetail.Quantity * orderDetail.SinglePrice;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrderId = new SelectList(db.Orders, "Id", "FactorNumber", orderDetail.OrderId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", orderDetail.ProductId);
            return View(orderDetail);
        }

        // GET: OrderDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
          
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderId = new SelectList(db.Orders, "Id", "FactorNumber", orderDetail.OrderId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", orderDetail.ProductId);
            return View(orderDetail);
        }

        // POST: OrderDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OrderId,ProductId,Quantity,SinglePrice,SubTotalPrice,Created_at,Updated_at")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                
                db.Entry(orderDetail).State = EntityState.Modified;
                orderDetail.Updated_at = DateTime.Now;
                orderDetail.SubTotalPrice = orderDetail.Quantity * orderDetail.SinglePrice;
                 db.SaveChanges();
               
                return RedirectToAction("Index");
            }
            ViewBag.OrderId = new SelectList(db.Orders, "Id", "FactorNumber", orderDetail.OrderId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", orderDetail.ProductId);
            return View(orderDetail);
        }

        // GET: OrderDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            db.OrderDetails.Remove(orderDetail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteByCustomer(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Include(o => o.Product).SingleOrDefault(o => o.Id == id);
            var order = db.Orders.Include(o => o.OrderStatus).Single(o => o.Id == orderDetail.OrderId);
            if (order.OrderStatus.Name == "ثبت اولیه")
            {
                if (orderDetail == null)
                {
                    return HttpNotFound();
                }
                return View(orderDetail);
            }
            return Content("شما نمی توانید سفارشی که در وضغیت پیگیری یا ارسال است را حذف نمایید.");


        }
        [HttpPost, ActionName("DeleteByCustomer")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteByCustomerConfirmed(int id)
        {
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            db.OrderDetails.Remove(orderDetail);
            db.SaveChanges();
            return RedirectToAction("Profile_Orders", "Manage");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
