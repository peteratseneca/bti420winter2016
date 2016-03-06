using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using NewDataModelSecuredOnAzure.Models;

namespace NewDataModelSecuredOnAzure.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private ApplicationDbContext ds = new ApplicationDbContext();

        public Manager()
        {
            // If necessary, add constructor code here

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }

        // Add methods below
        // Controllers will call these methods
        // Ensure that the methods accept and deliver ONLY view model objects and collections
        // The collection return type is almost always IEnumerable<T>

        // Suggested naming convention: Entity + task/action
        // For example:
        // ProductGetAll()
        // ProductGetById()
        // ProductAdd()
        // ProductEdit()
        // ProductDelete()

        // ############################################################
        // Country

        public IEnumerable<CountryBase> CountryGetAll()
        {
            return Mapper.Map<IEnumerable<CountryBase>>(ds.Countries.OrderBy(c => c.Name));
        }

        // ############################################################
        // Manufacturer

        public IEnumerable<ManufacturerBase> ManufacturerGetAll()
        {
            return Mapper.Map<IEnumerable<ManufacturerBase>>(ds.Manufacturers.OrderBy(m => m.Name));
        }

        public IEnumerable<ManufacturerWithDetail> ManufacturerGetAllWithDetail()
        {
            var c = ds.Manufacturers.Include("Country");

            return Mapper.Map<IEnumerable<ManufacturerWithDetail>>(c.OrderBy(m => m.Name));
        }

        public ManufacturerBase ManufacturerGetById(int id)
        {
            // Attempt to fetch the object
            var o = ds.Manufacturers.Find(id);

            // Return the result, or null if not found
            return (o == null) ? null : Mapper.Map<ManufacturerBase>(o);
        }

        public ManufacturerWithDetail ManufacturerGetByIdWithDetail(int id)
        {
            var o = ds.Manufacturers.Include("Country").Include("Vehicles")
                .SingleOrDefault(m => m.Id == id);

            return Mapper.Map<ManufacturerWithDetail>(o);
        }

        // ############################################################
        // Vehicle

        public IEnumerable<VehicleBase> VehicleGetAll()
        {
            return Mapper.Map<IEnumerable<VehicleBase>>(ds.Vehicles.OrderBy(v => v.Model).ThenBy(v => v.Trim));
        }

        public IEnumerable<VehicleWithDetail> VehicleGetAllWithDetail()
        {
            var c = ds.Vehicles.Include("Manufacturer.Country");

            return Mapper.Map<IEnumerable<VehicleWithDetail>>(c.OrderBy(v => v.Model).ThenBy(v => v.Trim));
        }

        public VehicleBase VehicleGetById(int id)
        {
            // Attempt to fetch the object
            var o = ds.Vehicles.Find(id);

            // Return the result, or null if not found
            return (o == null) ? null : Mapper.Map<VehicleBase>(o);
        }

        public VehicleWithDetail VehicleGetByIdWithDetail(int id)
        {
            var o = ds.Vehicles.Include("Manufacturer.Country")
                .SingleOrDefault(v => v.Id == id);

            return Mapper.Map<VehicleWithDetail>(o);
        }

        public VehicleWithDetail VehicleAdd(VehicleAdd newItem)
        {
            // This method is called from the Vehicles controller...
            // ...AND the Manufacturers controller

            // When adding an object with a required to-one association,
            // MUST fetch the associated object first

            // Attempt to find the associated object
            var a = ds.Manufacturers.Find(newItem.ManufacturerId);

            if (a == null)
            {
                return null;
            }
            else
            {
                // Attempt to add the new item
                var addedItem = ds.Vehicles.Add(Mapper.Map<Vehicle>(newItem));
                // Set the associated item property
                addedItem.Manufacturer = a;
                ds.SaveChanges();

                return (addedItem == null) ? null : Mapper.Map<VehicleWithDetail>(addedItem);
            }
        }

        public VehicleWithDetail VehicleEditMSRP(VehicleEdit newItem)
        {
            // Attempt to fetch the object

            // When editing an object with a required to-one association,
            // MUST fetch its associated object
            var o = ds.Vehicles.Include("Manufacturer")
                .SingleOrDefault(v => v.Id == newItem.Id);

            if (o == null)
            {
                // Problem - item was not found, so return
                return null;
            }
            else
            {
                // Update the object with the incoming values
                ds.Entry(o).CurrentValues.SetValues(newItem);
                ds.SaveChanges();

                // Prepare and return the object
                return Mapper.Map<VehicleWithDetail>(o);
            }
        }

        public bool VehicleDelete(int id)
        {
            // Attempt to fetch the object to be deleted
            var itemToDelete = ds.Vehicles.Find(id);

            if (itemToDelete == null)
            {
                return false;
            }
            else
            {
                // Remove the object
                ds.Vehicles.Remove(itemToDelete);
                ds.SaveChanges();

                return true;
            }
        }

        // Attention - 13 - Add some programmatically-generated objects to the data store
        // Can write one method, or many methods - your decision
        // The important idea is that you check for existing data first
        // Call this method from a controller action/method

        public bool LoadDataCountry()
        {
            // Return if there's existing data
            if (ds.Countries.Count() > 0) { return false; }

            // Otherwise...
            // Create and add objects
            ds.Countries.Add(new Country { Name = "Germany" });
            ds.Countries.Add(new Country { Name = "South Korea" });
            ds.Countries.Add(new Country { Name = "Japan" });
            ds.Countries.Add(new Country { Name = "United States of America" });

            // Save changes
            ds.SaveChanges();

            return true;
        }

        public bool LoadDataManufacturer()
        {
            // Return if there's existing data
            if (ds.Manufacturers.Count() > 0) { return false; }

            // Otherwise...
            // Create and add objects

            // Germany...
            // Fetch the country object, because we need it
            var germany = ds.Countries.SingleOrDefault(c => c.Name == "Germany");
            if (germany == null) { return false; }
            // Continue...
            ds.Manufacturers.Add(new Manufacturer { Country = germany, Name = "BMW AG", YearStarted = 1916 });
            ds.Manufacturers.Add(new Manufacturer { Country = germany, Name = "Daimler AG", YearStarted = 1926 });
            ds.Manufacturers.Add(new Manufacturer { Country = germany, Name = "Volkswagen AG", YearStarted = 1937 });
            ds.SaveChanges();

            // South Korea...
            var korea = ds.Countries.SingleOrDefault(c => c.Name == "South Korea");
            if (korea == null) { return false; }
            ds.Manufacturers.Add(new Manufacturer { Country = korea, Name = "Hyundai Motor Company", YearStarted = 1968 });
            ds.Manufacturers.Add(new Manufacturer { Country = korea, Name = "Kia Motors Company", YearStarted = 1944 });
            ds.SaveChanges();

            // Japan...
            var japan = ds.Countries.SingleOrDefault(c => c.Name == "Japan");
            if (japan == null) { return false; }
            ds.Manufacturers.Add(new Manufacturer { Country = japan, Name = "Honda Motor Co. Ltd.", YearStarted = 1946 });
            ds.Manufacturers.Add(new Manufacturer { Country = japan, Name = "Mazda Motor Corporation", YearStarted = 1920 });
            ds.Manufacturers.Add(new Manufacturer { Country = japan, Name = "Toyota Motor Company", YearStarted = 1937 });
            ds.SaveChanges();

            // United States of America
            var usa = ds.Countries.SingleOrDefault(c => c.Name == "United States of America");
            if (usa == null) { return false; }
            ds.Manufacturers.Add(new Manufacturer { Country = usa, Name = "Chrysler", YearStarted = 1925 });
            ds.Manufacturers.Add(new Manufacturer { Country = usa, Name = "Ford Motor Company", YearStarted = 1903 });
            ds.Manufacturers.Add(new Manufacturer { Country = usa, Name = "General Motors", YearStarted = 1908 });
            ds.SaveChanges();

            return true;
        }

        public bool LoadDataVehicle()
        {
            // Return if there's existing data
            if (ds.Vehicles.Count() > 0) { return false; }

            // Otherwise...
            // Create and add objects

            // Honda...
            var honda = ds.Manufacturers.SingleOrDefault(m => m.Name == "Honda Motor Co. Ltd.");
            if (honda == null) { return false; }
            ds.Vehicles.Add(new Vehicle { Manufacturer = honda, Model = "Accord", Trim = "Sedan LX", ModelYear = 2016, MSRP = 24150 });
            ds.Vehicles.Add(new Vehicle { Manufacturer = honda, Model = "Civic", Trim = "Coupe Si", ModelYear = 2016, MSRP = 26850 });
            ds.Vehicles.Add(new Vehicle { Manufacturer = honda, Model = "CR-V", Trim = "EX", ModelYear = 2016, MSRP = 32190 });

            // Save changes
            ds.SaveChanges();

            return true;
        }

    }
}