using Powerfront.Backend.EntityFramework;
using Powerfront.Backend.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Powerfront.Backend.Model
{
    public class CustomersDbContext:PowerfrontEntities, IDbContext
    {
        public CustomersDbContext():base()
        {

        }
    }
}
