using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using LoadDataFromWebService.Models;
using System.Security.Claims;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace LoadDataFromWebService.Controllers
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
        // HTTP request factory

        // Attention - 3 - This is a factory, which creates an HttpClient object for a web service request...
        // Configured with the base URI, and headers (accept and authorization)

        private HttpClient CreateRequest(string acceptValue = "application/json")
        {
            var request = new HttpClient();

            // Could also fetch the base address string from the app's global configuration
            // Base URI of the web service we are interacting with
            request.BaseAddress = new Uri("http://localhost:36431/api/");

            // Accept header configuration
            request.DefaultRequestHeaders.Accept.Clear();
            request.DefaultRequestHeaders.Accept
                .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(acceptValue));

            // Attempt to get the token from session state memory
            // Info: https://msdn.microsoft.com/en-us/library/system.web.httpcontext.session(v=vs.110).aspx

            var token = HttpContext.Current.Session["token"] as string;

            if (string.IsNullOrEmpty(token)) { token = "empty"; }

            // Authorization header configuration
            request.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue
                ("Bearer", token);

            return request;
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

        // Attention - 4 - "Manager" methods or get-all and get-one - these call a web service

        // ############################################################
        // Artist

        // Attention - 5 - Notice that ALL methods that call a web service are asynchronous...
        // We don't know if - or when - a response will be returned
        // As a result, we want the task to be done in the background,
        // so that foreground tasks (UI etc. or whatever) are not blocked

        public async Task<IEnumerable<ArtistBase>> ArtistGetAll(CancellationToken cancellationToken = default(CancellationToken))
        {
            // Create an HttpClient object
            // Enclose it in a "using" statement
            // Info - https://msdn.microsoft.com/en-us/library/yh598w02.aspx
            // It ensures the correct use of an object (HttpClient)
            // that must be disposed of when it has completed its work

            using (HttpClient request = CreateRequest())
            {
                // Send the request
                var response = await request.GetAsync("artists");

                if (response.IsSuccessStatusCode)
                {
                    // Read the response data, and return it
                    // The ReadAsAsync method is in System.Net.Http.Formatting
                    // Must "add reference", it's in the "Extensions" category
                    // This sweet little method marshals the response content, which is JSON text,
                    // into whatever we want (in this case an IEnum of ArtistBase)
                    return (await response.Content.ReadAsAsync<IEnumerable<ArtistBase>>());
                }
                else
                {
                    // For this simple app, return null
                    return null;
                }
            }
        }

        public async Task<IEnumerable<ArtistWithAlbums>> ArtistGetAllWithAlbums(CancellationToken cancellationToken = default(CancellationToken))
        {
            using (HttpClient request = CreateRequest())
            {
                // Send the request
                var response = await request.GetAsync("artists/withalbums");

                if (response.IsSuccessStatusCode)
                {
                    // Read the response data, and return it
                    return (await response.Content.ReadAsAsync<IEnumerable<ArtistWithAlbums>>());
                }
                else
                {
                    // For this simple app, return null
                    return null;
                }
            }
        }

        public async Task<ArtistWithAlbums> ArtistGetByIdWithAlbums(int id, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (HttpClient request = CreateRequest())
            {
                // Send the request
                var response = await request.GetAsync($"artists/{id}/withalbums");

                if (response.IsSuccessStatusCode)
                {
                    // Read the response data, and return it
                    return (await response.Content.ReadAsAsync<ArtistWithAlbums>());
                }
                else
                {
                    // For this simple app, return null
                    return null;
                }
            }
        }

        public async Task<IEnumerable<EmployeeBase>> EmployeeGetAll(CancellationToken cancellationToken = default(CancellationToken))
        {
            using (HttpClient request = CreateRequest())
            {
                // Send the request
                var response = await request.GetAsync("employees");

                if (response.IsSuccessStatusCode)
                {
                    // Read the response data, and return it
                    return (await response.Content.ReadAsAsync<IEnumerable<EmployeeBase>>());
                }
                else
                {
                    // For this simple app, return null
                    return null;
                }
            }
        }

        public async Task<EmployeeWithDetails> EmployeeGetByIdWithDetails(int id, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (HttpClient request = CreateRequest())
            {
                // Send the request
                var response = await request.GetAsync($"employees/{id}/withdetails");

                if (response.IsSuccessStatusCode)
                {
                    // Read the response data, and return it
                    return (await response.Content.ReadAsAsync<EmployeeWithDetails>());
                }
                else
                {
                    // For this simple app, return null
                    return null;
                }
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