using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shop.Models.Gallary
{
    public class Gallary
    {
        public int Id { get; set; }
        [Display(Name ="نام")]
        public string Name { get; set; }
        [Display(Name ="توضیحات")]
        public string Description { get; set; }
        [Display(Name ="خاصیت Alt")]
        public string Alt { get; set; }
        [Display(Name ="آدرس تصویر")]
        public string ImageUrl { get; set; }
    }
}