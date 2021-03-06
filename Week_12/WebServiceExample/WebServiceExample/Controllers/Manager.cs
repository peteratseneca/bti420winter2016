﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using WebServiceExample.Models;
using System.Security.Claims;

namespace WebServiceExample.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private DataContext ds = new DataContext();

        // Declare a property to hold the user account for the current request
        // Can use this property here in the Manager class to control logic and flow
        // Can also use this property in a controller 
        // Can also use this property in a view; for best results, 
        // near the top of the view, add this statement:
        // var userAccount = new ConditionalMenu.Controllers.UserAccount(User as System.Security.Claims.ClaimsPrincipal);
        // Then, you can use "userAccount" anywhere in the view to render content
        public UserAccount UserAccount { get; private set; }

        public Manager()
        {
            // If necessary, add constructor code here

            // Initialize the UserAccount property
            UserAccount = new UserAccount(HttpContext.Current.User as ClaimsPrincipal);

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
        // Artist

        // Attention - 3 - Normal "Manager" methods to get-all and get-one

        public IEnumerable<ArtistBase> ArtistGetAll()
        {
            return Mapper.Map<IEnumerable<ArtistBase>>(ds.Artists.OrderBy(a => a.Name));
        }

        public IEnumerable<ArtistWithAlbums> ArtistGetAllWithAlbums()
        {
            var c = ds.Artists.Include("Albums");

            return Mapper.Map<IEnumerable<ArtistWithAlbums>>(c.OrderBy(a => a.Name));
        }

        public ArtistBase ArtistGetById(int id)
        {
            var o = ds.Artists.Find(id);

            return (o == null) ? null : Mapper.Map<ArtistBase>(o);
        }

        public ArtistWithAlbums ArtistGetByIdWithAlbums(int id)
        {
            var o = ds.Artists.Include("Albums").SingleOrDefault(a => a.ArtistId == id);

            return (o == null) ? null : Mapper.Map<ArtistWithAlbums>(o);
        }

        // ############################################################
        // Employee

        public IEnumerable<EmployeeBase> EmployeeGetAll()
        {
            return Mapper.Map<IEnumerable<EmployeeBase>>(ds.Employees.OrderBy(el => el.LastName).ThenBy(ef => ef.FirstName));
        }

        public EmployeeBase EmployeeGetById(int id)
        {
            var o = ds.Employees.Find(id);

            return (o == null) ? null : Mapper.Map<EmployeeBase>(o);
        }

        public EmployeeBase EmployeeGetByIdWithDetails(int id)
        {
            var o = ds.Employees
                .Include("Employee1")
                .Include("Employee2")
                .SingleOrDefault(e => e.EmployeeId == id);

            return (o == null) ? null : Mapper.Map<EmployeeWithDetails>(o);
        }





        // Add some programmatically-generated objects to the data store
        // Can write one method, or many methods - your decision
        // The important idea is that you check for existing data first
        // Call this method from a controller action/method

        public bool LoadData()
        {
            // Return if there's existing data

            //if (ds.Your_Entity_Set.Count() > 0) { return false; }

            // Otherwise...
            // Create and add objects
            // Save changes

            return true;
        }

        public bool RemoveData()
        {
            try
            {
                //foreach (var e in ds.Your_Entity_Set)
                //{
                //    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                //}
                //ds.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveDatabase()
        {
            try
            {
                return ds.Database.Delete();
            }
            catch (Exception)
            {
                return false;
            }
        }

    }

    // New "UserAccount" class for the authenticated user
    // Includes many convenient members to make it easier to render user account info
    // Study the properties and methods, and think about how you could use it
    public class UserAccount
    {
        // Constructor, pass in the security principal
        public UserAccount(ClaimsPrincipal user)
        {
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                Principal = user;

                // Extract the role claims
                RoleClaims = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

                // User name
                Name = user.Identity.Name;

                // Extract the given name(s); if null or empty, then set an initial value
                string gn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.GivenName).Value;
                if (string.IsNullOrEmpty(gn)) { gn = "(empty given name)"; }
                GivenName = gn;

                // Extract the surname; if null or empty, then set an initial value
                string sn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Surname).Value;
                if (string.IsNullOrEmpty(sn)) { sn = "(empty surname)"; }
                Surname = sn;

                IsAuthenticated = true;
                IsAdmin = user.HasClaim(ClaimTypes.Role, "Admin") ? true : false;
            }
            else
            {
                RoleClaims = new List<string>();
                Name = "anonymous";
                GivenName = "Unauthenticated";
                Surname = "Anonymous";
                IsAuthenticated = false;
                IsAdmin = false;
            }

            NamesFirstLast = $"{GivenName} {Surname}";
            NamesLastFirst = $"{Surname}, {GivenName}";
        }

        // Public properties
        public ClaimsPrincipal Principal { get; private set; }
        public IEnumerable<string> RoleClaims { get; private set; }

        public string Name { get; set; }

        public string GivenName { get; private set; }
        public string Surname { get; private set; }

        public string NamesFirstLast { get; private set; }
        public string NamesLastFirst { get; private set; }

        public bool IsAuthenticated { get; private set; }

        // Add other role-checking properties here as needed
        public bool IsAdmin { get; private set; }

        public bool HasRoleClaim(string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(ClaimTypes.Role, value) ? true : false;
        }

        public bool HasClaim(string type, string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(type, value) ? true : false;
        }
    }

}