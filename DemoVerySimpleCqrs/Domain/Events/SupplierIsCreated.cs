using d60.Cirqus.Events;

namespace DemoVerySimpleCqrs.Domain.Events
{
    public class SupplierIsCreated : DomainEvent<SupplierAggregateRoot>
    {
        public System.Guid SupplierId { get; }
        public string SupplierName { get; }

        public SupplierIsCreated(System.Guid supplierId, string supplierName)
        {
            this.SupplierId = supplierId;
            this.SupplierName = supplierName;
        }
    }
}
