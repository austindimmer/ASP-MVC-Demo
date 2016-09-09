using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Powerfront.Backend.Model
{
    public class Customers : ICustomers
    {
        ICollection<Customer> ICustomers.Customers { get; set; }
    }
}
