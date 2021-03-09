using Shop.Models.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shop.Models.Page
{
    public class Page
    {
        public int Id { get; set; }
        [Display(Name = "نام نوشته")]
        public string Name { get; set; }
        [UIHint("Html")]
        [Display(Name = "توضیحات ")]
        public string FullDescription { get; set; }
        [UIHint("Html")]
        [Display(Name = "توضیحات کوتاه ")]
        public string ShortDescription { get; set; }
        public string ImageUrl { get; set; }
        public Category Category { get; set; }
        [Display(Name = "دسته بندی ")]
        public int CategoryId { get; set; }
    }
}