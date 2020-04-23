using d60.Cirqus.Events;

namespace DemoVerySimpleCqrs.Domain.Events
{
    public class SupplierLocationIsCreated : DomainEvent<SupplierAggregateRoot>
    {
        public System.Guid SupplierId { get; }

        public System.Guid SupplierLocationId { get; }
        public string Location { get; }

        public SupplierLocationIsCreated(System.Guid supplierId, System.Guid supplierLocationId, string location)
        {
            this.SupplierId = supplierId;

            this.SupplierLocationId = supplierLocationId;
            this.Location = location;
        }
    }
}
