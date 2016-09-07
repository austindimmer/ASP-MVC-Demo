using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Powerfront.Backend.Model
{
    public class Customer : ICustomer
    {
        public string Age { get; set; }

        public string Name { get; set; }
    }
}
