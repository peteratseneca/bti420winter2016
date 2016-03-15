using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ManageAccounts.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using AutoMapper;

namespace ManageAccounts.Controllers
{

    [Authorize(Roles = "Admin")]
    public class AccountManagerController : Controller
    {

        private Manager m = new Manager();

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountManagerController()
        {

        }

        public AccountManagerController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;

        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        // GET: Get All Users

        public ActionResult Index()
        {
            return View(m.UsersGetAll());
        }

        // GET: AccountManager/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            // Send the object to the view  
            return View(m.GetUserById(id));
        }

        // GET: AccountManager/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountManager/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // GET: AccountManager/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            // Attempt to fetch the matching object
            var o = m.GetUserById(id);

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Create a form
                var form = new ApplicationUserEditForm();
                form = Mapper.Map<ApplicationUserEditForm>(o);

                // Configure its properties
                // Alternatively, could create a mapper etc.
                var roles = new List<string> { "Admin", "Student", "Faculty" };


                // Configure the select list
                form.RoleList = new MultiSelectList(items: roles, selectedValues: o.Roles.ToList());

                return View(form);
            }
        }

        // POST: AccountManager/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, ApplicationUserEdit newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                // Display the edit form again
                return RedirectToAction("edit", new { id = newItem.Id });
            }

            if (id != newItem.Id)
            {
                // This appears to be data tampering, so redirect the user away
                return RedirectToAction("index");
            }

            // Attempt to do the upate
            var editedItem = m.ApplicationUserEdit(newItem);

            if (editedItem == null)
            {
                // There was a problem updating the object
                // Our "version 1" approach is to display the "edit form" again
                return RedirectToAction("edit", new { id = newItem.Id });
            }
            else
            {
                // Show the details view, which will have the updated data
                return RedirectToAction("details", new { id = newItem.Id });
            }
        }


        // GET: AccountManager/UserFinder
        [HttpGet]
        public ActionResult UserFinder()
        {
            return View();
        }

        // Use partial view to Find users
        // This is the Ajax listener method
        public ActionResult FindUser(string findString)
        {
            //Attempt to find matching objects
            var fetchedObjects = m.FindUsers(findString);
            return PartialView("_FindUser", fetchedObjects);
        }

        // GET: AccountManager/Delete/5
        public ActionResult Delete(string id)
        {
            return View(m.GetUserById(id));
        }

        // POST: AccountManager/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, ApplicationUserBase user)
        {

            if (id == null)
            {
                return null;
            }
            try
            {
                // User stays logged in - need a way to kill the session - unAuth
                // If the user being deleted is user himself
                if (id == System.Web.HttpContext.Current.User.Identity.GetUserId())
                {
                    m.DeleteUser(id);
                }

                m.DeleteUser(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }

        }
    }
}
