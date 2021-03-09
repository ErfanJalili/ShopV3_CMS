using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shop.Models.Order
{
    public class OrderStatus
    {
        public int Id { get; set; }
        [Display(Name = "نام وضعیت")]
        public string Name { get; set; }

    }
}