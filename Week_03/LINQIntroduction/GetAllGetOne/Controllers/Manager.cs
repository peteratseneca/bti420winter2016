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

            // Original statement, using the Find() method
            //var o = ds.Customers.Find(id);

            // Attention - Replacement statement, using the SingleOrDefault method
            var o = ds.Customers.SingleOrDefault(c => c.CustomerId == id);

            // Return the result, or null if not found
            return (o == null) ? null : Mapper.Map<CustomerBase>(o);
        }

        public IEnumerable<CustomerBase> CustomerGetAllByCountry(string item)
        {
            // Attention - Filtered fetch, using the Where method
            // Attention - Also uses the Contains method, useful for strings
            // Do a case-insensitive comparison
            // Be sure to sanitize (trim) the incoming string item before comparing
            var c = ds.Customers.Where
                (cn => cn.Country.ToLower().Contains(item.Trim().ToLower()));

            return Mapper.Map<IEnumerable<CustomerBase>>(c);
        }

        public IEnumerable<CustomerBase> CustomerGetAllByName(string item)
        {
            // Filtered fetch...
            // Do a case-insensitive comparison
            // Be sure to sanitize (trim) the incoming string item before comparing

            // We're going to do three separate searches
            // And then combine the search results

            var first = ds.Customers.Where
                (fn => fn.FirstName.ToLower().Contains(item.Trim().ToLower()));

            var last = ds.Customers.Where
                (ln => ln.LastName.ToLower().Contains(item.Trim().ToLower()));

            var comp = ds.Customers.Where
                (cn => cn.Company.ToLower().Contains(item.Trim().ToLower()));

            // Attention - The Union method helps join collections together
            // Combine the results, eliminating duplicates
            var results = first.Union(last).Union(comp);

            // Return sorted
            return Mapper.Map<IEnumerable<CustomerBase>>(results.OrderBy(ln => ln.LastName).ThenBy(fn => fn.FirstName));
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