using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoVerySimpleCqrs.Domain.Entities
{
    public class SupplierLocation
    {
        public Guid SuplierlocationId { get; set; }
        public string LocationName { get; set; }
    }
}
