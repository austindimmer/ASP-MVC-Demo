using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Powerfront.Backend.Model
{
    [DataContract]
    public class AggregateCustomer : IAggregateCustomer
    {
        [DataMember]
        public string CustomerId { get; set; }
        [DataMember]
        public List<EntityFramework.CustomerRecord> CustomerDataRecords { get; set; }
    }
}
