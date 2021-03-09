using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shop.Models.Product
{
    public class Tag
    {
        public int Id { get; set; }
        [Display(Name="نام برچسب")]
        public string Name { get; set; }
        [Display(Name = "توضیحات")]
        public string Description { get; set; }
    }
}