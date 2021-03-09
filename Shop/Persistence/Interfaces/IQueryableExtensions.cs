using Shop.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Persistence.Interfaces
{
    public static class IQueryableExtensions
    {
        public static IQueryable<Customer> ApplyFiltering(this IQueryable<Customer> query, CustomerQuery queryObj)
        {
            query = query.Where(v => v.Id == queryObj.Page);
            return query;
        }
    }
}