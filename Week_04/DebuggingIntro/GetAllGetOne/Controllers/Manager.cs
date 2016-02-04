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

        public CustomerBase CustomerAdd(CustomerAdd newItem)
        {
            // Attempt to add the new item
            // Notice how we map the incoming data to the design model object
            var addedItem = ds.Customers.Add(Mapper.Map<Customer>(newItem));
            ds.SaveChanges();

            // If successful, return the added item, mapped to a view model object
            return (addedItem == null) ? null : Mapper.Map<CustomerBase>(addedItem);
        }

        public CustomerBase CustomerEditContactInfo(CustomerEditContactInfo newItem)
        {
            // Attempt to fetch the object
            var o = ds.Customers.Find(newItem.CustomerId);

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
                return Mapper.Map<CustomerBase>(o);
            }
        }

        public bool CustomerDelete(int id)
        {
            // Attempt to fetch the object to be deleted
            var itemToDelete = ds.Customers.Find(id);

            if (itemToDelete == null)
            {
                return false;
            }
            else
            {
                // Remove the object
                ds.Customers.Remove(itemToDelete);
                ds.SaveChanges();

                return true;
            }
        }
    }
}