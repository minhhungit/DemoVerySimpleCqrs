using d60.Cirqus.Aggregates;
using d60.Cirqus.Events;
using DemoVerySimpleCqrs.Domain.Entities;
using DemoVerySimpleCqrs.Domain.Events;
using System;
using System.Collections.Generic;

namespace DemoVerySimpleCqrs.Domain
{
    public class SupplierAggregateRoot : AggregateRoot,
        IEmit<SupplierIsCreated>,
        IEmit<SupplierLocationIsCreated>
    {
        public SupplierAggregateRoot()
        {
            Contact = new List<SupplierContact>();
            Locations = new List<SupplierLocation>();
        }

        // aggregate root properties (or value objects...)
        public Guid SupplierId { get; set; }
        public string Name { get; set; }

        // aggregate's entities
        public List<SupplierContact> Contact { get; set; }
        public List<SupplierLocation> Locations { get; set; }



        // ==============SupplierIsCreated===================

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

        // ==============SupplierLocationIsCreated===================

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
                if (this.Locations.Exists(x => x.LocationName == locationName))
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

            this.Locations.Add(new SupplierLocation
            {
                SuplierlocationId = evt.SupplierLocationId,
                LocationName = evt.Location
            });
        }
    }
}
