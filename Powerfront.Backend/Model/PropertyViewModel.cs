using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Powerfront.Backend.Model
{
    public class PropertyViewModel: IProperty
    {
        public string PropertyId { get; set; }

        public string Name { get; set; }

    }
}
