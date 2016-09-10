using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Powerfront.Backend.Model
{
    public class AggregateCustomers : IAggregateCustomers
    {
        public string AggregateCustomersId { get; set; }
        ICollection<AggregateCustomer> IAggregateCustomers.Customers { get; set; }
    }
}
