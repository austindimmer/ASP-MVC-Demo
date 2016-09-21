using Powerfront.Backend.Impact.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Powerfront.Backend.Impact.Model
{
    public class ImpactViewModel
    {
        public ImpactViewModel()
        {
            this.BeneficiaryGroups = new HashSet<BeneficiaryGroup>();
        }

        public System.Guid ImpactId { get; set; }
        public string Other { get; set; }
        public string Notes { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime FinishDate { get; set; }
        public string ImpactName { get; set; }
        public Nullable<System.Guid> BeneficiaryGroupId { get; set; }

        public BeneficiaryGroup BeneficiaryGroup { get; set; }
        
        public ICollection<BeneficiaryGroup> BeneficiaryGroups { get; set; }
        public ICollection<BeneficiaryGroup> SelectedBeneficiaryGroups { get; set; }

        // This will convert the passed ImpactViewModel object to JSON string
        public static string Serialize(ImpactViewModel vm)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(vm);
        }

        // This will convert the passed JSON string back to ImpactViewModel object
        public static ImpactViewModel Deserialize(string data)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<ImpactViewModel>(data);
        }
    }
}
