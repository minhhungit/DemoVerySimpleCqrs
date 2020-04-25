namespace DemoVerySimpleCqrs.Domain.Commands
{
    using d60.Cirqus.Commands;
    using System;

    public class AddSupplierLocationCmd : Command<SupplierAggregateRoot>
    {
        public Guid SupplierId { get; }
        public Guid SupplierLocationId { get; }
        public string LocationName { get; }

        public AddSupplierLocationCmd(Guid supplierId, Guid supplierLocationId, string location) : base(supplierId.ToString())
        {
            this.SupplierId = supplierId;
            this.SupplierLocationId = supplierLocationId;
            this.LocationName = location;
        }

        public override void Execute(SupplierAggregateRoot aggregateRoot)
        {
            aggregateRoot.AddSupplierLocation(this.SupplierId, this.SupplierLocationId, this.LocationName);
        }
    }
}