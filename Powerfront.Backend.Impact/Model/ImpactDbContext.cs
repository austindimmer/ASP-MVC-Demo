using Powerfront.Backend.EntityFramework;
using Powerfront.Backend.Impact.EntityFramework;
using Powerfront.Backend.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Powerfront.Backend.Model
{
    public class ImpactDbContext:ImpactEntities, IDbContext
    {
        public ImpactDbContext():base()
        {
            //Disabling proxy creation

            //Sometimes it is useful to prevent the Entity Framework from creating proxy instances.For example, serializing non - proxy instances is considerably easier than serializing proxy instances.Proxy creation can be turned off by clearing the ProxyCreationEnabled flag.One place you could do this is in the constructor of your context.For example:

            // https://msdn.microsoft.com/en-us/data/jj592886.aspx?f=255&MSPPError=-2147217396

            //When proxy object creation is enabled for POCO entities, changes that are made to the graph and the property values of objects are tracked automatically by the Entity Framework as they occur.For information about change tracking options with and without proxies, see Tracking Changes in POCO Entities.

            this.Configuration.ProxyCreationEnabled = false;
        }
    }
}
