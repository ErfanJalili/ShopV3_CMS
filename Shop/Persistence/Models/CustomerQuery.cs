using Shop.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Persistence.Models
{
    public class CustomerQuery : IQueryObject
    {
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public byte PageSize { get; set; }
    }
}