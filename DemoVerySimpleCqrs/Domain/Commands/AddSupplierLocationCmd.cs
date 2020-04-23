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


namespace DemoVerySimpleCqrs.Domain
{
    using d60.Cirqus.Aggregates;
    using d60.Cirqus.Events;
    using DemoVerySimpleCqrs.Domain.Events;
    using System;

    public partial class SupplierAggregateRoot : AggregateRoot,
        IEmit<SupplierLocationIsCreated>
    {
        public void AddSupplierLocation(Guid supplierId, Guid supplierLocationId, string locationName)
        {
            try
            {
                // demo some validate
                if (supplierId == null || supplierId == Guid.Empty)
                {
                    throw new Exception("Invaild Supplier Id");
                }

                if (string.IsNullOrWhiteSpace(locationName))
                {
                    throw new Exception("location name is empty");
                }

                // validate if location name is existed
                if (this.Locations.Exists(x=>x.LocationName == locationName))
                {
                    throw new Exception("Location is exited");
                }

                // OK
                this.Emit(new SupplierLocationIsCreated(supplierId, supplierLocationId, locationName));
            }
            catch (Exception ex)
            {
                // log.Fatal(....)
                throw ex;
            }
        }

        public void Apply(SupplierLocationIsCreated evt)
        {
            this.SupplierId = evt.SupplierId;

            this.Locations.Add(new Entities.SupplierLocation
            {
                SuplierlocationId = evt.SupplierLocationId,
                LocationName = evt.Location
            });
        }
    }
}