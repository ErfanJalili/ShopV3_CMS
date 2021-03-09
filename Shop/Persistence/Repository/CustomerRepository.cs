using Shop.Models;
using Shop.Persistence.Interfaces;
using Shop.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Shop.Persistence.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;
        public CustomerRepository()
        {
            _context = new ApplicationDbContext();
        }
        Task<Customer> ICustomerRepository.GetCustomer(int customerId)
        {
            return  _context.Customers.FindAsync(customerId);

        }
    }
}