using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shop.Dtos
{
    public class OrderCoponsDto
    {
        public int Id { get; set; }
        [Display(Name = "نام کوپن")]
        public string Name { get; set; }
        [Display(Name = "درصد تخفيف")]
        public int Percent { get; set; }

    }
}