using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Dtos
{
    public class SliderDto
    {
        public int Id { get; set; }
        public string SliderImageUrl { get; set; }
        public string Description { get; set; }
        public string Alt { get; set; }
    }
}