using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Persistence.Interfaces
{
    public interface IUnitOfWork
    {
          Task CompleteAsync();
        
    }
}
