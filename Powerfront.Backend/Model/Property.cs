using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Powerfront.Backend.Model
{
    public class Property : IProperty
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }
}
