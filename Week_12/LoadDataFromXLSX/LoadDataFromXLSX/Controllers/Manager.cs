using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using LoadDataFromXLSX.Models;
using System.Security.Claims;
using Excel;
using System.IO;
using System.Data;
using System.Reflection;

namespace LoadDataFromXLSX.Controllers
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

        public IEnumerable<PlayerBase> PlayerGetAll()
        {
            return Mapper.Map<IEnumerable<PlayerBase>>(ds.Players.OrderBy(p => p.PlayerName));
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
            // RoleClaim

            if (ds.RoleClaims.Count() == 0)
            {
                // Add role claims

                //ds.SaveChanges();
                //done = true;
            }

            // ############################################################
            // Player

            // Attention - 4 - Method to load data from the XLSX file, with ExcelDataReader add-on

            if (ds.Players.Count() == 0)
            {
                // Add players

                // Path to the XLSX file
                var path = HttpContext.Current.Server.MapPath("~/App_Data/NFLPlayoffTeams.xlsx");

                // Load the workbook into a System.Data.DataSet "sourceData"
                var stream = File.Open(path, FileMode.Open, FileAccess.Read);
                IExcelDataReader reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                reader.IsFirstRowAsColumnNames = true;
                DataSet sourceData = reader.AsDataSet();
                reader.Close();

                // At this point in time, sourceData holds ALL the data from the XLSX in memory

                // Worksheet name
                string worksheetName;

                // Load the first worksheet...
                // ===========================

                // Get worksheet by its name
                worksheetName = "DEN";
                var worksheet = sourceData.Tables[worksheetName];

                // Convert it to a collection of the desired type
                List<PlayerAdd> items = worksheet.DataTableToList<PlayerAdd>();

                // Go through the collection, and add the items to the data context
                foreach (var item in items)
                {
                    // Fill in the team name
                    item.Team = worksheetName;
                    ds.Players.Add(Mapper.Map<Player>(item));
                }

                // Save changes
                ds.SaveChanges();

                // Load the next worksheet...
                // ==========================

                // Get worksheet by its name
                worksheetName = "CAR";
                worksheet = sourceData.Tables[worksheetName];

                // Convert it to a collection of the desired type
                items = worksheet.DataTableToList<PlayerAdd>();

                // Go through the collection, and add the items to the data context
                foreach (var item in items)
                {
                    item.Team = worksheetName;
                    ds.Players.Add(Mapper.Map<Player>(item));
                }

                // Save changes
                ds.SaveChanges();

                // Load the next worksheet...
                // ==========================

                // Get worksheet by its name
                worksheetName = "NE";
                worksheet = sourceData.Tables[worksheetName];

                // Convert it to a collection of the desired type
                items = worksheet.DataTableToList<PlayerAdd>();

                // Go through the collection, and add the items to the data context
                foreach (var item in items)
                {
                    item.Team = worksheetName;
                    ds.Players.Add(Mapper.Map<Player>(item));
                }

                // Save changes
                ds.SaveChanges();

                // Load the next worksheet...
                // ==========================

                // Get worksheet by its name
                worksheetName = "ARI";
                worksheet = sourceData.Tables[worksheetName];

                // Convert it to a collection of the desired type
                items = worksheet.DataTableToList<PlayerAdd>();

                // Go through the collection, and add the items to the data context
                foreach (var item in items)
                {
                    item.Team = worksheetName;
                    ds.Players.Add(Mapper.Map<Player>(item));
                }

                // Save changes
                ds.SaveChanges();

                done = true;
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


    public static class Helper
    {
        public static List<T> DataTableToList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                List<T> list = new List<T>();

                foreach (var row in table.AsEnumerable())
                {
                    T obj = new T();

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
        }

    } // public static class Helper

}