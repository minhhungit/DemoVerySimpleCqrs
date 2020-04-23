using d60.Cirqus.Views.ViewManagers;
using d60.Cirqus.Views.ViewManagers.Locators;
using DemoVerySimpleCqrs.Domain.Events;
using System.Collections.Generic;

namespace DemoVerySimpleCqrs.Domain.Views
{
    public class SupplierEventHandlerLocator : HandlerViewLocator,
         IGetViewIdsFor<SupplierLocationIsCreated>
    {
        public IEnumerable<string> GetViewIds(IViewContext context, SupplierLocationIsCreated domainEvent)
        {
            if (domainEvent.SupplierLocationId == null || domainEvent.SupplierLocationId == System.Guid.Empty)
                return new List<string>();

            return new List<string>() { domainEvent.SupplierLocationId.ToString() };
        }
    }
}
