using Microsoft.AspNet.Identity;
using Shop.Models;
using Shop.Models.Order;
using Shop.Models.ShippingCart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Shop.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        ApplicationDbContext storeDB = new ApplicationDbContext();
        const string PromoCode = "FREE";
        public ActionResult AddressAndPayment()
        {

            return View();
        }
        //
        // POST: /Checkout/AddressAndPayment
        [HttpPost]
        public ActionResult AddressAndPayment(FormCollection values)
        {
            var order = new Order();
            TryUpdateModel(order);

            try
            {
                if (string.Equals(values["PromoCode"], PromoCode,
                    StringComparison.OrdinalIgnoreCase) == false)
                {
                    return View(order);
                }
                else
                {
                    var cart = ShoppingCart.GetCart(this.HttpContext);

                    order.UserCompany = User.Identity.Name;
                    order.Created_at = DateTime.Now;
                    order.TotalPrice = cart.GetTotal().ToString();
                    order.OrderStatusId = 1;
                    order.OrderCoponId = 2;
                    order.UserId = User.Identity.GetUserId();
                    order.FactorNumber =  DateTime.Now.ToString().GetHashCode().ToString("x"); 
                    //Save Order
                    storeDB.Orders.Add(order);
                    storeDB.SaveChanges();
                    //Process the order
                    cart.CreateOrder(order);

                    return RedirectToAction("Complete",
                        new { id = order.Id });
                }
            }
            catch
            {
                //Invalid - redisplay with errors
                return View(order);
            }
        }
        //
        // GET: /Checkout/Complete
        public ActionResult Complete(int id)
        {
            // Validate customer owns this order
            bool isValid = storeDB.Orders.Any(
                o => o.Id == id &&
                o.UserCompany == User.Identity.Name);

            if (isValid)
            {
                var model = storeDB.Orders.Include(o=>o.OrderStatus).Single(o => o.Id == id && o.UserCompany == User.Identity.Name);
                ViewBag.Order = model;
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }
    }
}