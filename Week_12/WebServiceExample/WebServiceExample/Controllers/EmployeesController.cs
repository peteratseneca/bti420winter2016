using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebServiceExample.Controllers
{
    // Attention - 5 - Employee *web service* controller, which is very similar to the familiar web app controller

    public class EmployeesController : ApiController
    {
        // Reference to the manager object
        Manager m = new Manager();

        // GET: api/Employees
        public IHttpActionResult Get()
        {
            return Ok(m.EmployeeGetAll());
        }

        // GET: api/Employees/5
        public IHttpActionResult Get(int? id)
        {
            // Fetch the object
            var o = m.EmployeeGetById(id.GetValueOrDefault());

            // Continue?
            if (o == null) { return NotFound(); }

            return Ok(o);
        }

        // GET: api/Employees/5/WithDetails
        [Route("api/employees/{id}/withdetails")]
        public IHttpActionResult GetWithDetails(int? id)
        {
            // Fetch the object
            var o = m.EmployeeGetByIdWithDetails(id.GetValueOrDefault());

            // Continue?
            if (o == null) { return NotFound(); }

            return Ok(o);
        }
    }
}
