using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Powerfront.Backend.Model
{
    [DataContract]
    public class AggregateCustomer : IAggregateCustomer
    {
        public string CustomerId { get; set; }
        public List<EntityFramework.CustomerRecord> CustomerDataRecords { get; set; }
    }
}
