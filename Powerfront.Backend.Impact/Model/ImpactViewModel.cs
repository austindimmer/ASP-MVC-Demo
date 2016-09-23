using Newtonsoft.Json;
using Powerfront.Backend.Impact.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Powerfront.Backend.Impact.Model
{
    [DataContract]
    public class ImpactViewModel
    {
        public ImpactViewModel()
        {
        }

        [DataMember]
        public System.Guid ImpactId { get; set; }
        [DataMember]
        public string Other { get; set; }
        [DataMember]
        public string Notes { get; set; }
        [DataMember]
        public System.DateTime StartDate { get; set; }
        [DataMember]
        public System.DateTime FinishDate { get; set; }
        [DataMember]
        public string ImpactName { get; set; }

        [DataMember]
        public ICollection<BeneficiaryGroup> BeneficiaryGroups { get; set; }
        [DataMember]
        public ICollection<BeneficiaryGroup> SelectedBeneficiaryGroups { get; set; }

        [IgnoreDataMember]
        public ICollection<ImpactBeneficiary> ImpactBeneficiaries { get; set; }

    }
}
