using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Powerfront.Backend.Model
{
    public interface ICustomer
    {
        string Name {get; set; }
        string Age { get; set; }

    }
}
