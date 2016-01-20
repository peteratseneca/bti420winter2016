using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using GetAllGetOne.Models;

namespace GetAllGetOne.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private DataContext ds = new DataContext();

        public Manager()
        {
            // If necessary, add constructor code here
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

        // Use AutoMapper to map objects, source to target
        public IEnumerable<CustomerBase> CustomerGetAll()
        {
            return Mapper.Map<IEnumerable<CustomerBase>>(ds.Customers);
        }

        public CustomerBase CustomerGetById(int id)
        {
            // Attempt to fetch the object
            var o = ds.Customers.Find(id);

            // Return the result, or null if not found
            return (o == null) ? null : Mapper.Map<CustomerBase>(o);
        }



        // Manually map objects, source to target
        public IEnumerable<CustomerBase> CustomerGetAllManual()
        {
            // Define a new empty view model collection
            var results = new List<CustomerBase>();

            // Fetch all the objects from the data store
            var allCustomers = ds.Customers;

            // Manually map each source object to a target object
            foreach (var customer in allCustomers)
            {
                // Create and configure a new target object
                var c = new CustomerBase();
                c.Address = customer.Address;
                c.City = customer.City;
                c.Company = customer.Company;
                c.Country = customer.Country;
                c.CustomerId = customer.CustomerId;
                c.Email = customer.Email;
                c.Fax = customer.Fax;
                c.FirstName = customer.FirstName;
                c.LastName = customer.LastName;
                c.Phone = customer.Phone;
                c.PostalCode = customer.PostalCode;
                c.State = customer.State;

                // Add the new target object to the results collection
                results.Add(c);
            }

            // Return the results
            return results;
        }
    }
}