using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using PhotoEntity.Models;
using System.Security.Claims;

namespace PhotoEntity.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private ApplicationDbContext ds = new ApplicationDbContext();

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

        // ############################################################
        // RoleClaim

        public List<string> RoleClaimGetAllStrings()
        {
            return ds.RoleClaims.OrderBy(r => r.Name).Select(r => r.Name).ToList();
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
        // Property

        // Attention - 02 - Methods for the "Property" entity
        // Get all, get one, add new, edit existing, and delete item
        // A few more methods work with associated data
        // For example get one, with photo info

        public IEnumerable<PropertyBase> PropertyGetAll()
        {
            var c = ds.Properties
                .OrderBy(p => p.City)
                .ThenByDescending(p => p.Price);

            return Mapper.Map<IEnumerable<PropertyBase>>(c);
        }

        public PropertyBase PropertyGetById(int id)
        {
            var o = ds.Properties.Find(id);

            return (o == null) ? null : Mapper.Map<PropertyBase>(o);
        }

        // Attention - 03 - Get one with collection of photo info
        // Notice its return types
        public PropertyWithPhotoStringIds PropertyGetByIdWithPhotoInfo(int id)
        {
            var o = ds.Properties.Include("Photos").SingleOrDefault(p => p.Id == id);

            return (o == null) ? null : Mapper.Map<PropertyWithPhotoStringIds>(o);
        }

        // Attention - 06 - Get one photo, including photo data/bytes
        // Notice the identifier is the generated string identifier
        public PhotoContent PropertyPhotoGetById(string stringId)
        {
            var o = ds.Photos.SingleOrDefault(p => p.StringId == stringId);

            return (o == null) ? null : Mapper.Map<PhotoContent>(o);
        }

        public PropertyBase PropertyAdd(PropertyAdd newItem)
        {
            // Attempt to add the new item
            var addedItem = ds.Properties.Add(Mapper.Map<Property>(newItem));

            ds.SaveChanges();

            return (addedItem == null) ? null : Mapper.Map<PropertyBase>(addedItem);
        }

        public PropertyBase PropertyEdit(PropertyBase newItem)
        {
            // Attempt to fetch the item
            var editedItem = ds.Properties.Find(newItem.Id);

            if (editedItem == null)
            {
                return null;
            }
            else
            {
                // Attempt to update the item
                ds.Entry(editedItem).CurrentValues.SetValues(newItem);
                ds.SaveChanges();

                return Mapper.Map<PropertyBase>(editedItem);
            }
        }

        public bool PropertyDelete(int id)
        {
            // Attempt to fetch the object to be deleted
            var itemToDelete = ds.Properties.Find(id);

            if (itemToDelete == null)
            {
                return false;
            }
            else
            {
                try
                {
                    // Remove the object
                    ds.Properties.Remove(itemToDelete);
                    ds.SaveChanges();

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        // Attention - 08 - Add photo to an existing for-sale property
        // This works like a typical add/edit Manager class method
        // that has an associated object
        public PropertyBase PropertyPhotoAdd(PhotoAdd newItem)
        {
            // Validate the associated item
            var a = ds.Properties.Find(newItem.PropertyId);

            if (a == null)
            {
                return null;
            }
            else
            {
                // Attempt to add the new item
                var addedItem = new Photo();
                ds.Photos.Add(addedItem);

                addedItem.Caption = newItem.Caption;
                addedItem.Property = a;

                // Handle the uploaded photo...

                // First, extract the bytes from the HttpPostedFile object
                byte[] photoBytes = new byte[newItem.PhotoUpload.ContentLength];
                newItem.PhotoUpload.InputStream.Read(photoBytes, 0, newItem.PhotoUpload.ContentLength);

                // Then, configure the new object's properties
                addedItem.Content = photoBytes;
                addedItem.ContentType = newItem.PhotoUpload.ContentType;

                ds.SaveChanges();

                return (addedItem == null) ? null : Mapper.Map<PropertyBase>(a);
            }
        }





        // Add some programmatically-generated objects to the data store
        // Can write one method, or many methods - your decision
        // The important idea is that you check for existing data first
        // Call this method from a controller action/method

        public bool LoadData()
        {
            // User name
            var user = HttpContext.Current.User.Identity.Name;

            // Monitor the progress
            bool done = false;

            // ############################################################
            // Genre

            if (ds.RoleClaims.Count() == 0)
            {
                // Add role claims

                //ds.SaveChanges();
                //done = true;
            }

            return done;
        }

        public bool RemoveData()
        {
            try
            {
                foreach (var e in ds.RoleClaims)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

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