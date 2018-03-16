using BusinessLogic;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using System.Web.Mvc;
using ViewModels;

namespace API.Controllers
{
    public class EmployeeController : ApiController
    {
        EmployeeService employeeService = null;
        public EmployeeController(EmployeeService serviceInjected)
        {
            employeeService = serviceInjected;
        }

        // GET: api/Employee
        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                var employees = employeeService.GetAll();
                if (employees == null)
                    return NotFound();
                
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Exception - " + ex.Message));
            }
        }

        // GET: api/Employee/5
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var employee = employeeService.GetById(id);
                if (employee == null)
                    return NotFound();

                return Ok(employee);
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Exception - " + ex.Message));
            }
        }

        // POST: api/Employee        
        [HttpPost]
        [System.Web.Mvc.ValidateAntiForgeryToken]      // CSRF
        public IHttpActionResult Post(EmployeeViewModel employee)
        {
            try
            {
                employeeService.Insert(employee);
                return Ok();
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Exception - " + ex.Message));
            }
        }

        // PUT: api/Employee/5
        [HttpPut]
        [System.Web.Mvc.ValidateAntiForgeryToken]       // CSRF
        public IHttpActionResult Put(EmployeeViewModel employee)
        {
            try
            {
                employeeService.Update(employee);
                return Ok();
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Exception - " + ex.Message));
            }
        }

        // DELETE: api/Employee/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                employeeService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Exception - " + ex.Message));
            }
        }
    }
}
