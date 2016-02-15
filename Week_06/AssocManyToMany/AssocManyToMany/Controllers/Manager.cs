using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using AssocManyToMany.Models;

namespace AssocManyToMany.Controllers
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

        // ############################################################
        // Employee

        public IEnumerable<EmployeeBase> EmployeeGetAll()
        {
            return Mapper.Map<IEnumerable<EmployeeBase>>(ds.Employees.OrderBy(e => e.Name));
        }

        public IEnumerable<EmployeeWithJobDuties> EmployeeGetAllWithJobDuties()
        {
            return Mapper.Map<IEnumerable<EmployeeWithJobDuties>>
                (ds.Employees.Include("JobDuties").OrderBy(e => e.Name));
        }

        public EmployeeWithJobDuties EmployeeGetByIdWithDetail(int id)
        {
            // Attempt to fetch the object
            var o = ds.Employees.Include("JobDuties").SingleOrDefault(e => e.Id == id);

            // Return the result, or null if not found
            return (o == null) ? null : Mapper.Map<EmployeeWithJobDuties>(o);
        }

        // Attention - Edit an employee's job duties
        public EmployeeWithJobDuties EmployeeEditJobDuties(EmployeeEditJobDuties newItem)
        {
            // Attempt to fetch the object

            // When editing an object with a to-many collection,
            // and you wish to edit the collection,
            // MUST fetch its associated collection
            var o = ds.Employees.Include("JobDuties")
                .SingleOrDefault(e => e.Id == newItem.Id);

            if (o == null)
            {
                // Problem - object was not found, so return
                return null;
            }
            else
            {
                // Update the object with the incoming values

                // First, clear out the existing collection
                o.JobDuties.Clear();

                // Then, go through the incoming items
                // For each one, add to the fetched object's collection
                foreach (var item in newItem.JobDutyIds)
                {
                    var a = ds.JobDuties.Find(item);
                    o.JobDuties.Add(a);
                }
                // Save changes
                ds.SaveChanges();

                return Mapper.Map<EmployeeWithJobDuties>(o);
            }
        }

        // ############################################################
        // JobDuty

        public IEnumerable<JobDutyBase> JobDutyGetAll()
        {
            return Mapper.Map<IEnumerable<JobDutyBase>>(ds.JobDuties.OrderBy(j => j.Name));
        }

        public IEnumerable<JobDutyWithEmployees> JobDutyGetAllWithEmployees()
        {
            return Mapper.Map<IEnumerable<JobDutyWithEmployees>>
                (ds.JobDuties.Include("Employees").OrderBy(j => j.Name));
        }

        public JobDutyWithEmployees JobDutyGetByIdWithDetail(int id)
        {
            // Attempt to fetch the object
            var o = ds.JobDuties.Include("Employees").SingleOrDefault(j => j.Id == id);

            // Return the result, or null if not found
            return (o == null) ? null : Mapper.Map<JobDutyWithEmployees>(o);
        }

        // Edit a job duty's list of employees
        public JobDutyWithEmployees JobDutyEditEmployees(JobDutyEditEmployees newItem)
        {
            // Attempt to fetch the object

            // When editing an object with a to-many collection,
            // and you wish to edit the collection,
            // MUST fetch its associated collection
            var o = ds.JobDuties.Include("Employees")
                .SingleOrDefault(e => e.Id == newItem.Id);

            if (o == null)
            {
                // Problem - object was not found, so return
                return null;
            }
            else
            {
                // Update the object with the incoming values

                // First, clear out the existing collection
                o.Employees.Clear();

                // Then, go through the incoming items
                // For each one, add to the fetched object's collection
                foreach (var item in newItem.EmployeeIds)
                {
                    var a = ds.Employees.Find(item);
                    o.Employees.Add(a);
                }
                // Save changes
                ds.SaveChanges();

                return Mapper.Map<JobDutyWithEmployees>(o);
            }
        }
    }
}