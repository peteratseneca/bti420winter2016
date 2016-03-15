using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using ManageAccounts.Models;
using System.Security.Claims;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace ManageAccounts.Controllers
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

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                // Null coalescing operator
                // https://msdn.microsoft.com/en-us/library/ms173224.aspx
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


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




        // Add some programmatically-generated objects to the data store
        // Can write one method, or many methods - your decision
        // The important idea is that you check for existing data first
        // Call this method from a controller action/method

        public bool LoadData()
        {
            // Return if there's existing data
            // If no data exists, add the following users
            if (UserManager.Users.Count() == 0)
            {
                var user = new ApplicationUser { UserName = "anna@example.com", Email = "anna@example.com" };
                var result = UserManager.Create(user, "Password123!");
                // Add claims
                UserManager.AddClaim(user.Id, new Claim(ClaimTypes.Email, "anna@example.com"));
                UserManager.AddClaim(user.Id, new Claim(ClaimTypes.Role, "Admin"));
                UserManager.AddClaim(user.Id, new Claim(ClaimTypes.Role, "Student"));
                UserManager.AddClaim(user.Id, new Claim(ClaimTypes.GivenName, "Anna"));
                UserManager.AddClaim(user.Id, new Claim(ClaimTypes.Surname, "Administrator"));

                var user2 = new ApplicationUser { UserName = "peter@example.com", Email = "peter@example.com" };
                result = UserManager.Create(user2, "Password123!");
                // Add claims
                UserManager.AddClaim(user2.Id, new Claim(ClaimTypes.Email, "peter@example.com"));
                UserManager.AddClaim(user2.Id, new Claim(ClaimTypes.Role, "Admin"));
                UserManager.AddClaim(user2.Id, new Claim(ClaimTypes.Role, "Faculty"));
                UserManager.AddClaim(user2.Id, new Claim(ClaimTypes.GivenName, "Peter"));
                UserManager.AddClaim(user2.Id, new Claim(ClaimTypes.Surname, "Administrator"));

                var student = new ApplicationUser { UserName = "student@example.com", Email = "student@example.com" };
                result = UserManager.Create(student, "Password123!");
                // Add claims
                UserManager.AddClaim(student.Id, new Claim(ClaimTypes.Email, "student@example.com"));
                UserManager.AddClaim(student.Id, new Claim(ClaimTypes.Role, "Student"));
                UserManager.AddClaim(student.Id, new Claim(ClaimTypes.GivenName, "Student"));
                UserManager.AddClaim(student.Id, new Claim(ClaimTypes.Surname, "Just A Student"));

                return true;
            }

            return false;

            //if (ds.Your_Entity_Set.Count() > 0) { return false; }

            // Otherwise...
            // Create and add objects
            // Save changes


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


        // Get All Users
        public IEnumerable<ApplicationUserBase> UsersGetAll()
        {
            // Fetch all users        
            var allUsers = UserManager.Users;

            if(allUsers == null)
            {
                return null;
            }

            var userList = new List<ApplicationUserBase>();
            foreach (var user in allUsers)
            {
                // Map the values all users to view model
                var appUser = Mapper.Map<ApplicationUserBase>(user);
                var userClaims = user.Claims.Where
                     (c => c.ClaimType == ClaimTypes.Role).Select(roles => roles.ClaimValue).ToArray();

                // Add Role Claims
                appUser.Roles = userClaims;
                userList.Add(appUser);
            }
            return userList;
        }

        // Find a user based on a search string - partial
        public IEnumerable<ApplicationUserBase> FindUsers(string findString)
        {
            // Fetch all the matching users
            var allUsers = UserManager.Users
              .Where(e => e.UserName.Contains(findString) || e.Email.Contains(findString));
            if (allUsers == null)
            {
                return null;
            }

            // Map the users to the view model
            var userList = new List<ApplicationUserBase>(); 
            foreach (var user in allUsers)
            {
                var appUser = Mapper.Map<ApplicationUserBase>(user);
                var userClaims = user.Claims.Where
                     (c => c.ClaimType == ClaimTypes.Role).Select(roles => roles.ClaimValue).ToArray();

                appUser.Roles = userClaims;
                userList.Add(appUser);
            }

            return Mapper.Map<IEnumerable<ApplicationUserBase>>(userList);
        }


        // Get User by Id
        public ApplicationUserDetail GetUserById(string id)
        {
            // Fetch the User by Id
            var user = UserManager.FindById(id);

            if( user == null)
            {
                return null;
            }

            // Initialize UserAccount
            var userIdentity = UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie) as ClaimsIdentity;
            var claimsPrincipal = new ClaimsPrincipal(userIdentity);

            var userAccount = new UserAccount(claimsPrincipal);

            // Map user details
            var details = Mapper.Map<ApplicationUserDetail>(userAccount);
            details.UserName = user.UserName;
            details.Email = user.UserName;
            details.Roles = userAccount.RoleClaims;

            return details;
        }


        // Delete User
        public void DeleteUser(string id)
        {
            var user = UserManager.FindById(id);

            // Initialize UserAccount
            var userIdentity = UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie) as ClaimsIdentity;
            var claimsPrincipal = new ClaimsPrincipal(userIdentity);

            var userAccount = new UserAccount(claimsPrincipal);

            // Get all claims
            var claims = claimsPrincipal.Claims;
            // Set a flag for successful remove
            var check = true;       
            // Remove all claims from user
            foreach (var claim in claims)
            {
                var r =  UserManager.RemoveClaimAsync(user.Id, new Claim(claim.Type, claim.Value)).Result;
                if (!r.Succeeded) { check = false; }
            }

            // Finally remove the user
            if (check)
            {
                var result = UserManager.DeleteAsync(user).Result;
            }       
        }

        // Edit User Claims - For Now Only Roles
        public ApplicationUserDetail ApplicationUserEdit(ApplicationUserEdit newItem)
        {
            var result = new IdentityResult();

            // Attempt to fetch the object
            var o = UserManager.FindById(newItem.Id);

            if (o == null)
            {
                return null;
            }

            var userIdentity = UserManager.CreateIdentity(o, DefaultAuthenticationTypes.ApplicationCookie) as ClaimsIdentity;
            var claimsPrincipal = new ClaimsPrincipal(userIdentity);
            var userAccount = new UserAccount(claimsPrincipal);

            // Remove all roles
            foreach (var role in userAccount.RoleClaims)
            {
               result = UserManager.RemoveClaimAsync(o.Id, new Claim(ClaimTypes.Role, role)).Result;    
            }

            // If successful removal, Add Roles
            if (result.Succeeded)
            {
                foreach (var newRole in newItem.Roles)
                {
                    result = UserManager.AddClaimAsync(o.Id, new Claim(ClaimTypes.Role, newRole)).Result;
                }
                if (result.Succeeded)
                {
                    return Mapper.Map<ApplicationUserDetail>(newItem);
                }
            }
            return null;
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