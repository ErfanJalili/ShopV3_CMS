using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Dtos
{
    public class SKUsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ManagerName { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string WebSite { get; set; }
        public string Instagram { get; set; }
        public string Facebook { get; set; }
        public string Telegram { get; set; }
        public string Whatsapp { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string GoogleLocation { get; set; }
        public string NeshanLocation { get; set; }
    }
}