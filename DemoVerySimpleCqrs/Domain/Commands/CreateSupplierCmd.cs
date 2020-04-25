namespace DemoVerySimpleCqrs.Domain.Commands
{
    using d60.Cirqus.Commands;
    using System;

    public class CreateSupplierCmd : Command<SupplierAggregateRoot>
    {
        public Guid SupplierId { get; }
        public string SupplierName { get; }

        public CreateSupplierCmd(Guid supplierId, string supplierName) : base(supplierId.ToString())
        {
            this.SupplierId = supplierId;
            this.SupplierName = supplierName;
        }

        public override void Execute(SupplierAggregateRoot aggregateRoot)
        {
            aggregateRoot.CreateSupplier(this.SupplierId, this.SupplierName);
        }
    }
}