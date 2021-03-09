using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shop.Models.Slider
{
    public class Slider
    {
        public int Id { get; set; }
        [Display(Name ="آدرس تصویر ")]
        public string SliderImageUrl { get; set; }
        [Display(Name = "نام تصویر ")]
        public string Description { get; set; }
        [Display(Name = "alt ")]
        public string Alt { get; set; }


    }
}