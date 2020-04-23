using d60.Cirqus.Aggregates;
using DemoVerySimpleCqrs.Domain.Entities;
using System;
using System.Collections.Generic;

namespace DemoVerySimpleCqrs.Domain
{
    public partial class SupplierAggregateRoot : AggregateRoot
    {
        public SupplierAggregateRoot()
        {
            Contact = new List<SupplierContact>();
            Locations = new List<SupplierLocation>();
        }

        public Guid SupplierId { get; set; }
        public string Name { get; set; }

        public List<SupplierContact> Contact { get; set; }
        public List<SupplierLocation> Locations { get; set; }
    }
}
