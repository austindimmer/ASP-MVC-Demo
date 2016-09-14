using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Powerfront.Backend.Model
{
    [DataContract]
    public class CustomerRecordViewModel
    {
        [DataMember]
        public string TypeId { get; set; }
        [DataMember]
        public string PropertyId { get; set; }
        [DataMember]
        public string Value { get; set; }
        [DataMember]
        public string CustomerId { get; set; }
        [DataMember]
        public System.Guid RecordId { get; set; }
        [DataMember]
        public virtual PropertyViewModel Property { get; set; }
        [DataMember]
        public virtual TypeViewModel Type { get; set; }
    }
}
