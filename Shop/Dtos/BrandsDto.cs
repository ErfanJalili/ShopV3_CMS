using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Dtos
{
    public class BrandsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Tell { get; set; }
       
        public string Phone { get; set; }
       
        public string Address { get; set; }
    }
}