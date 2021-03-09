using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shop.Models.Product
{
    public class Brand
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "نام")]
        public string Name { get; set; }
        [Display(Name ="توضیحات")]
        public string Description { get; set; }
        [Display(Name = "تصویر")]
        public string Image { get; set; }
        [StringLength(11)]
        [Display(Name = "تلفن")]
        public string Tell { get; set; }
        [StringLength(11)]
        [Display(Name = "موبایل")]
        public string Phone { get; set; }
        [MaxLength]
        [Display(Name = "آدرس")]
        public string Address { get; set; }
    }
}