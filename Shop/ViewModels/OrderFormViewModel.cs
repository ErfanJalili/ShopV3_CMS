using Shop.Models.Order;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shop.ViewModels
{
    public class OrderFormViewModel
    {
        public IEnumerable<OrderCopon> OrderCopons{ get; set; }
        public IEnumerable<OrderStatus> OrderStatuses{ get; set; }
        public int Id { get; set; }
        [Display(Name = "شماره فاکتور")]
        public string FactorNumber { get; set; }
        [Display(Name = "خريدار")]
        public string UserId { get; set; }
        [Display(Name = "نام شركت")]
        public string UserCompany { get; set; }
        [Display(Name = "کشور")]
        public string Country { get; set; }
        [Display(Name = "شهر")]
        public string city { get; set; }
        [Display(Name = "شهرستان")]
        public string State { get; set; }
        [Display(Name = "خیابان")]
        public string UserStreet { get; set; }
        [Display(Name = "کوچه")]
        public string UserSuburb { get; set; }
        [Display(Name = "پلاک")]
        public string Code { get; set; }
        [Display(Name = "کدپستی")]
        public string PostCode { get; set; }
        [Display(Name = "تلفن")]
        public string Telephone { get; set; }
        [Display(Name = "موبایل")]
        public string Mobile { get; set; }
        [Display(Name = "ایمیل")]
        public string Email { get; set; }
        [Display(Name = "تاریخ ایجاد")]
        public DateTime? Created_at { get; set; }
        public OrderStatus OrderStatus { get; set; }
        [Display(Name = "وضعیت سفارش")]
        public int OrderStatusId { get; set; }
        public OrderCopon OrderCopon { get; set; }
        [Display(Name = "کوپن تخفیف")]
        public int OrderCoponId { get; set; }
        [Display(Name = "مبلغ کل")]
        public string TotalPrice { get; set; }
        [Display(Name = "فیش واریزی")]
        public string PaymentRecipe { get; set; }

    }
}