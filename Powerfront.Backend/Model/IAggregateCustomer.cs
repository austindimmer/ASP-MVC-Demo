using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Powerfront.Backend.Model
{
    public interface IAggregateCustomer
    {
        string CustomerId { get; set; }

        List<EntityFramework.CustomerRecord> CustomerDataRecords { get; set; }

    }
}
