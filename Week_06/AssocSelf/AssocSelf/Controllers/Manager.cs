using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using AssocSelf.Models;

namespace AssocSelf.Controllers
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

        public IEnumerable<EmployeeBase> EmployeeGetAll()
        {
            return Mapper.Map<IEnumerable<EmployeeBase>>
                (ds.Employees.OrderBy(e => e.LastName).ThenBy(e => e.FirstName));
        }

        public IEnumerable<EmployeeWithOrgInfo> EmployeeGetAllWithOrgInfo()
        {
            // Ugh, I hate these property names
            var c = ds.Employees.Include("Employee1").Include("Employee2");

            // Return the result
            return Mapper.Map<IEnumerable<EmployeeWithOrgInfo>>
                (c.OrderBy(e => e.LastName).ThenBy(e => e.FirstName));
        }

        public EmployeeWithOrgInfo EmployeeGetByIdWIthOrgInfo(int id)
        {
            // Attempt to get the matching object
            var o = ds.Employees.Include("Employee1").Include("Employee2")
                .SingleOrDefault(e => e.EmployeeId == id);

            // Return the result, or null if not found
            return (o == null) ? null : Mapper.Map<EmployeeWithOrgInfo>(o);
        }

        public EmployeeWithOrgInfo EmployeeEditSupervisor(EmployeeEditSupervisor newItem)
        {
            // Attention - Update the self-referencing to-one association

            // Attempt to fetch the object
            var o = ds.Employees.Include("Employee1").Include("Employee2")
                .SingleOrDefault(e => e.EmployeeId == newItem.EmployeeId);

            // Attempt to fetch the associated object
            Employee a = null;
            if (newItem.ReportsToId > 0)
            {
                a = ds.Employees.Include("Employee1").Include("Employee2")
                    .SingleOrDefault(e => e.EmployeeId == newItem.ReportsToId);
            }

            // Must do two tests here before continuing
            if (o == null | a == null)
            {
                // Problem - one of the items was not found, so return
                return null;
            }
            else
            {
                // Update the object with the incoming values
                // This will handle the standard properties
                // We could also specifically target the new/updated properties
                ds.Entry(o).CurrentValues.SetValues(newItem);

                // Configure the new supervisor
                // MUST set both properties - the int and the Employee
                o.Employee2 = a;
                o.ReportsTo = a.EmployeeId;

                ds.SaveChanges();

                // Prepare and return the object
                return Mapper.Map<EmployeeWithOrgInfo>(o);
            }
        }

        public EmployeeWithOrgInfo EmployeeEditDirectReports(EmployeeEditDirectReports newItem)
        {
            // Attention - Update the self-referencing to-many association

            // Attempt to fetch the object

            // When editing an object with a to-many collection,
            // and you wish to edit the collection,
            // MUST fetch its associated collection
            var o = ds.Employees.Include("Employee1").Include("Employee2")
                .SingleOrDefault(e => e.EmployeeId == newItem.EmployeeId);

            if (o == null)
            {
                // Problem - object was not found, so return
                return null;
            }
            else
            {
                // Update the object with the incoming values

                // First, clear out the existing collection
                // "Employee1" is the badly-named to-many collection property
                o.Employee1.Clear();

                // Then, go through the incoming items
                // For each one, add to the fetched object's collection
                foreach (var item in newItem.EmployeeIds)
                {
                    var a = ds.Employees.Find(item);
                    o.Employee1.Add(a);
                }
                // Save changes
                ds.SaveChanges();

                return Mapper.Map<EmployeeWithOrgInfo>(o);
            }
        }

    }
}