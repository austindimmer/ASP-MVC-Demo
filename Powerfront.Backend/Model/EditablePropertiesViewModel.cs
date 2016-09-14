using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Powerfront.Backend.Model
{
    public class EditablePropertiesViewModel
    {
        public string CustomerId { get; set; }
        public Dictionary<string, string> EditableProperties { get; set; }
    }
}
