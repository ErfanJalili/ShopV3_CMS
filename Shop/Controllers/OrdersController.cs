using Microsoft.AspNet.Identity;
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
    public class OrdersController : Controller
    {
        private ApplicationDbContext _context;

        public OrdersController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult All_Orders()
        {
            return View("IndexOrders");
        }
        public ActionResult NewOrders()
        {
            var viewModel = new ViewModels.OrderFormViewModel
            {
                OrderStatuses = _context.OrderStatuses.ToList(),
                OrderCopons = _context.OrderCopons.ToList(),
            };
            return View("OrderForm", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save([Bind(Include = "Id,FactorNumber,UserCompany,Country,city,State,UserStreet,UserSuburb,Code,PostCode,Telephone,Mobile,Email,Crated_at,OrderStatusId,OrderCoponId,TotalPrice,PaymentRecipe,UserId")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Orders.Add(order);
                order.Created_at = DateTime.Now;
                _context.SaveChanges();
                return RedirectToAction("All_Orders");
            }

            return RedirectToAction("All_Orders");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = _context.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderCoponId = new SelectList(_context.OrderCopons, "Id", "Name", order.OrderCoponId);
            ViewBag.OrderStatusId = new SelectList(_context.OrderStatuses, "Id", "Name", order.OrderStatusId);
            return View(order);
        }

        // POST: Orders1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FactorNumber,UserId,UserCompany,Country,city,State,UserStreet,UserSuburb,Code,PostCode,Telephone,Mobile,Email,Created_at,OrderStatusId,OrderCoponId,TotalPrice,PaymentRecipe")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(order).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("All_Orders");
            }
            ViewBag.OrderCoponId = new SelectList(_context.OrderCopons, "Id", "Name", order.OrderCoponId);
            ViewBag.OrderStatusId = new SelectList(_context.OrderStatuses, "Id", "Name", order.OrderStatusId);
            return View(order);
        }

        public ActionResult PrintRecipe(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var checkLogin = User.Identity.GetUserId();

            if (checkLogin == null)
            {
                return RedirectToAction("Login", "Account");
            }
            Order order = _context.Orders.Find(id);
            
            if (order == null)
            {
                return HttpNotFound();
            }
            var calculatePrices = _context.OrderDetails.Where(o => o.OrderId == order.Id).Select(o => o.SubTotalPrice);
            var orderCopon = _context.Orders.Include(o => o.OrderCopon).Single(o => o.Id == id).OrderCopon.Percent;
            
            double percent = Convert.ToDouble(orderCopon); 
            double Sum = 0;
            foreach (var item in calculatePrices)
            {
                Sum = Sum + item;
            }
            double Totaly = Sum;
            Sum = Sum - ( (percent/100)*Sum);
            var seletedOrder = _context.Orders.Find(id);
            seletedOrder.TotalPrice = Sum.ToString();
            _context.SaveChanges();

            ViewBag.Totaly = Totaly;
            var orderInDb = _context.Orders.Include(o => o.OrderStatus).Include(o=>o.OrderCopon).Single(o => o.Id == id);
            ViewBag.Status = orderInDb.OrderStatus.Name;
            ViewBag.Copon = orderInDb.OrderCopon;
            ViewBag.OrderStatusId = new SelectList(_context.OrderStatuses, "Id", "Name", order.OrderStatusId);
            ViewBag.orderDetails = _context.OrderDetails.Include(o => o.Product).Where(o => o.OrderId == id).ToList();
            return View(order);
          
        }

        public ActionResult CustomerRecipe(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var checkLogin = User.Identity.GetUserId();

            if (checkLogin == null)
            {
                return RedirectToAction("Login", "Account");
            }
            Order order = _context.Orders.Single(o=>o.Id==id&&o.UserId==checkLogin);

            if (order == null)
            {
                return HttpNotFound();
            }
            var calculatePrices = _context.OrderDetails.Where(o => o.OrderId == order.Id).Select(o => o.SubTotalPrice);
            var orderCopon = _context.Orders.Include(o => o.OrderCopon).Single(o => o.Id == id).OrderCopon.Percent;

            double percent = Convert.ToDouble(orderCopon);
            double Sum = 0;
            foreach (var item in calculatePrices)
            {
                Sum = Sum + item;
            }
            double Totaly = Sum;
            Sum = Sum - ((percent / 100) * Sum);
            var seletedOrder = _context.Orders.Find(id);
            seletedOrder.TotalPrice = Sum.ToString();
            _context.SaveChanges();

            ViewBag.Totaly = Totaly;
            var orderInDb = _context.Orders.Include(o => o.OrderStatus).Include(o => o.OrderCopon).Single(o => o.Id == id);
            ViewBag.Status = orderInDb.OrderStatus.Name;
            ViewBag.Copon = orderInDb.OrderCopon;
            ViewBag.OrderStatusId = new SelectList(_context.OrderStatuses, "Id", "Name", order.OrderStatusId);
            ViewBag.orderDetails = _context.OrderDetails.Include(o => o.Product).Where(o => o.OrderId == id).ToList();
            return View(order);

        }

        public ActionResult Checkout(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = _context.Orders.Include(o=>o.OrderStatus).Single(o=>o.Id==id);
            if (order == null)
            {
                return HttpNotFound();
            }
            if (order.OrderStatus.Name != "پرداخت نشده")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.Order = _context.Orders.Include(o => o.OrderStatus).Include(o => o.OrderCopon).Single(o => o.Id == id);
           
            return View(order);
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout([Bind(Include = "Id,FactorNumber,UserId,UserCompany,Country,city,State,UserStreet,UserSuburb,Code,PostCode,Telephone,Mobile,Email,Created_at,OrderStatusId,OrderCoponId,TotalPrice,PaymentRecipe")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(order).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Profile","Manage");
            }
            ViewBag.OrderCoponId = new SelectList(_context.OrderCopons, "Id", "Name", order.OrderCoponId);
            ViewBag.OrderStatusId = new SelectList(_context.OrderStatuses, "Id", "Name", order.OrderStatusId);
            return View(order);
        }


      
    }
}