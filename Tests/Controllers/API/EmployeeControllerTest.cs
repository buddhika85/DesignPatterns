using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using API.Controllers;
using Moq;
using BusinessLogic;
using DataAccess;
using ViewModels;
using EF;
using System.Web.Http;
using System.Net;
using System.Web.Http.Results;
using FluentAssertions;
using System.Collections.Generic;

namespace Tests.Controllers.API
{
    [TestClass]
    public class EmployeeControllerTest
    {
        // api controller
        private EmployeeController employeeController;


        [TestMethod]
        public void Post_ShouldSaveEmployee()
        {
            try
            {
                // mock objects
                var mockRepository = new Mock<IGenericRepository<TBL_EMPLOYEE>>();
                var mockUnitOfWork = new Mock<IUnitOfWork>();
                mockUnitOfWork.SetupGet(u => u.EmployeeRepository).Returns(mockRepository.Object);
                var mockEmployeeService = new Mock<EmployeeService>(mockUnitOfWork.Object);
                var mockEmployeeViewModel = Mock.Of<EmployeeViewModel>(
                    e => e.EmployeeId == -1 &&
                    e.FirstName == "whatever" &&
                    e.LastName == "whatever" &&
                    e.Salary == 1.0m &&
                    e.Permanent == false &&
                    e.DepartmentId == 1);
                
                // controller
                employeeController = new EmployeeController(mockEmployeeService.Object);

                // execute 
                IHttpActionResult result = employeeController.Post(mockEmployeeViewModel);
                //var createdResult = result as CreatedAtRouteNegotiatedContentResult<EmployeeViewModel>;

                //Assert.IsNotNull(createdResult);
                //Assert.AreEqual(HttpStatusCode.Accepted, createdResult.C);
                //Assert.IsNotNull(contentResult.Content);
                result.Should().BeOfType<OkResult>();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [TestMethod]
        public void Get_GetAllEmployee()
        {
            try
            {
                // mock objects   
                var mockRepository = new Mock<IGenericRepository<TBL_EMPLOYEE>>();
                var mockUnitOfWork = new Mock<IUnitOfWork>();
                mockUnitOfWork.SetupGet(u => u.EmployeeRepository).Returns(mockRepository.Object);
                var mockEmployeeService = new Mock<EmployeeService>(mockUnitOfWork.Object);
                

                // controller
                employeeController = new EmployeeController(mockEmployeeService.Object);

                // execute 
                IHttpActionResult result = employeeController.Get();
                
                result.Should().BeOfType<OkNegotiatedContentResult<IEnumerable<EmployeeViewModel>>>();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
