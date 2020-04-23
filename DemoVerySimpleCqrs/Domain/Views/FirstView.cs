using d60.Cirqus.Views.ViewManagers;
using d60.Cirqus.Views.ViewManagers.Locators;
using DemoVerySimpleCqrs.Domain.Events;

namespace DemoVerySimpleCqrs.Domain.Views
{
    public class FirstView : IViewInstance<InstancePerAggregateRootLocator>,
        ISubscribeTo<SupplierIsCreated>
    {
        public string Id { get; set; }

        public System.Guid SupplierId { get; set; }
        public string SupplierName { get; set; }

        public long LastGlobalSequenceNumber { get; set; }

        public void Handle(IViewContext context, SupplierIsCreated domainEvent)
        {
            this.SupplierId = domainEvent.SupplierId;
            this.SupplierName = domainEvent.SupplierName;
        }
    }
}
