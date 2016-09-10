using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Powerfront.Backend.Model
{
    public interface IAggregateCustomers
    {
        ICollection<AggregateCustomer> Customers {get; set;}
    }
}
