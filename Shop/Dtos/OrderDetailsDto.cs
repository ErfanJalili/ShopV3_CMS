using Shop.Models.Order;
using Shop.Models.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shop.Dtos
{
    public class OrderDetailsDto
    {
        public int Id { get; set; }
        public Order Order { get; set; }
        [Display(Name = "شماره سفارش")]
        public int OrderId { get; set; }
        public Product Product { get; set; }
        [Display(Name = "محصول")]
        public int ProductId { get; set; }
        [Display(Name = "تعداد")]
        public int Quantity { get; set; }
        [Display(Name = "فی")]
        public double SinglePrice { get; set; }
        [Display(Name = "جمع فی")]
        public double SubTotalPrice { get; set; }
        [Display(Name = "تاریخ ثبت")]
        public DateTime? Created_at { get; set; }
        [Display(Name = "تاریخ ویرایش")]
        public DateTime? Updated_at { get; set; }
    }
}