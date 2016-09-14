using Powerfront.Backend.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Powerfront.Backend.Model
{
    [DataContract]
    public class AggregateCustomerViewModel
    {
        [DataMember]
        public string CustomerId { get; set; }
        [DataMember]
        public List<CustomerRecordViewModel> CustomerDataRecords { get; set; }

        // This will convert the passed AggregateCustomer object to JSON string
        public static string Serialize(AggregateCustomerViewModel vm)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(vm);
        }

        // This will convert the passed JSON string back to XYZ object
        public static AggregateCustomerViewModel Deserialize(string data)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<AggregateCustomerViewModel>(data);
        }
    }
}
