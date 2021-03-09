using Shop.Models;
using Shop.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Shop.Persistence.UintOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
       
            private readonly ApplicationDbContext context;

            public UnitOfWork(ApplicationDbContext context)
            {
                this.context = context;
            }

            public async Task CompleteAsync()
            {
                await context.SaveChangesAsync();
            }
        
    }
}