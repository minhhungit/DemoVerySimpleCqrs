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

namespace DemoVerySimpleCqrs.Domain
{
    using d60.Cirqus.Aggregates;
    using d60.Cirqus.Events;
    using DemoVerySimpleCqrs.Domain.Events;
    using System;

    public partial class SupplierAggregateRoot : AggregateRoot,
        IEmit<SupplierIsCreated>
    {
        public void CreateSupplier(Guid supplierId, string supplierName)
        {
            try
            {
                // validate first
                if (supplierId == null || supplierId == Guid.Empty)
                {
                    throw new Exception("Invaild Supplier Id");
                }

                if (string.IsNullOrWhiteSpace(supplierName))
                {
                    throw new Exception("Supplier need a name");
                }

                // OK
                this.Emit(new SupplierIsCreated(supplierId, supplierName));
            }
            catch (Exception ex)
            {
                // log.Fatal(....)
                throw ex;
            }            
        }

        public void Apply(SupplierIsCreated evt)
        {
            this.SupplierId = evt.SupplierId;
            this.Name = evt.SupplierName;
        }
    }
}
