using Shop.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Persistence.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> GetCustomer(int customerId);
    }
}
