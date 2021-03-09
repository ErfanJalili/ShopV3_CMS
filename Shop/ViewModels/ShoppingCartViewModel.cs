using Shop.Models.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<Cart> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}