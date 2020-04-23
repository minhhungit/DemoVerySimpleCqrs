using d60.Cirqus.Views.ViewManagers;
using DemoVerySimpleCqrs.Domain.Events;
using System;

namespace DemoVerySimpleCqrs.Domain.Views
{
    public class SecondView : IViewInstance<SupplierEventHandlerLocator>,
        ISubscribeTo<SupplierIsCreated>,
        ISubscribeTo<SupplierLocationIsCreated>
    {
        public string Id { get; set; }

        public Guid SupplierId { get; set; }
        public string SupplierName { get; set; }

        public Guid SupplierLocationId { get; set; }
        public string LocationName { get; set; }

        public long LastGlobalSequenceNumber { get; set; }

        public void Handle(IViewContext context, SupplierIsCreated domainEvent)
        {
            this.SupplierId = domainEvent.SupplierId;
            this.SupplierName = domainEvent.SupplierName;
        }

        public void Handle(IViewContext context, SupplierLocationIsCreated domainEvent)
        {
            SupplierAggregateRoot supplier = context.TryLoad<SupplierAggregateRoot>(domainEvent.SupplierId.ToString());
            if (supplier == null)
            {
                throw new System.Exception($"Can not find Supplier by Id <{domainEvent.SupplierId}>");
            }

            this.SupplierId = supplier.SupplierId;
            this.SupplierName = supplier.Name;

            this.SupplierLocationId = domainEvent.SupplierLocationId;
            this.LocationName = domainEvent.Location;
        }
    }
}
