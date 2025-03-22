using ASPNETCoreMVCTraining.Api.Controllers;
using ASPNETCoreMVCTraining.Application.Interfaces;
using ASPNETCoreMVCTraining.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ASPNETCoreMVCTraining.Api.Tests.Controllers
{
    [TestFixture]
    public class EmployeeControllerTests
    {
        private Mock<ILogger<EmployeeController>> _loggerMock;
        private Mock<IEmployeeService> _employeeServiceMock;
        private EmployeeController _employeeController;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<EmployeeController>>();
            _employeeServiceMock = new Mock<IEmployeeService>();
            _employeeController = new EmployeeController(_loggerMock.Object, _employeeServiceMock.Object);
        }

        [Test]
        public void GetAllEmployees_ReturnsOkResultWithEmployees()
        {
            // Arrange
            var employees = new List<EmployeeViewModel>
            {
                new EmployeeViewModel { Id = 1, Name = "Test Employee 1", Email = "test1@example.com" },
                new EmployeeViewModel { Id = 2, Name = "Test Employee 2", Email = "test2@example.com" }
            };
            _employeeServiceMock.Setup(x => x.GetEmployees()).Returns(employees);

            // Act
            var result = _employeeController.GetAll();

            // Assert
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
            Assert.That(okResult.Value, Is.InstanceOf<List<EmployeeViewModel>>());
            var model = okResult.Value as List<EmployeeViewModel>;
            Assert.That(model, Is.Not.Null);
            Assert.That(model.Count, Is.EqualTo(2));
            Assert.That(model[0].Name, Is.EqualTo("Test Employee 1"));
            Assert.That(model[1].Name, Is.EqualTo("Test Employee 2"));
        }

        [Test]
        public void GetAllEmployees_LogsErrorWhenExceptionThrown()
        {
            // Arrange
            _employeeServiceMock.Setup(x => x.GetEmployees()).Throws(new Exception("Test exception"));

            // Act
            var result = _employeeController.GetAll();

            // Assert
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => o.ToString() == "Test exception"),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
            Assert.That(okResult.Value, Is.InstanceOf<List<EmployeeViewModel>>());
            var model = okResult.Value as List<EmployeeViewModel>;
            Assert.That(model, Is.Not.Null.And.Empty);
        }

        [Test]
        public void GetEmployees_ReturnsOkResultWithPageData()
        {
            // Arrange
            var pageData = new PageData<EmployeeViewModel>
            {
                Data = new List<EmployeeViewModel>
                {
                    new EmployeeViewModel { Id = 1, Name = "Test Employee 1", Email = "test1@example.com" },
                    new EmployeeViewModel { Id = 2, Name = "Test Employee 2", Email = "test2@example.com" }
                },
                PageInfo = new PageInfo() { PageIndex = 0, PageSize = 20, TotalPages = 2, TotalRecords = 2 }
            };
            _employeeServiceMock.Setup(x => x.GetEmployees(0, 20, null, null)).Returns(pageData);

            // Act
            var result = _employeeController.GetEmployee(null, null, 0, 20);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
            Assert.That(okResult.Value, Is.InstanceOf<PageData<EmployeeViewModel>>());
            var model = okResult.Value as PageData<EmployeeViewModel>;
            Assert.That(model, Is.Not.Null);
            Assert.That(model.Data.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetEmployees_LogsErrorWhenExceptionThrown()
        {
            // Arrange
            _employeeServiceMock.Setup(x => x.GetEmployees(0, 20, null, null)).Throws(new Exception("Test exception"));

            // Act
            var result = _employeeController.GetEmployee(null, null, 0, 20);

            // Assert
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => o.ToString() == "Test exception"),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
            Assert.That(result.Result, Is.InstanceOf<BadRequestResult>());
        }

        [Test]
        public void GetEmployeeById_ReturnsOkResultWithEmployee()
        {
            // Arrange
            var employee = new EmployeeCreateEditViewModel() { Id = 1, Name = "Test Employee 1", Email = "test1@example.com" };
            _employeeServiceMock.Setup(x => x.GetById(1)).Returns(employee);

            // Act
            var result = _employeeController.GetEmployeeById(1);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
            Assert.That(okResult.Value, Is.InstanceOf<EmployeeCreateEditViewModel>());
            var model = okResult.Value as EmployeeCreateEditViewModel;
            Assert.That(model.Id, Is.EqualTo(1));
        }

        [Test]
        public void GetEmployeeById_ReturnsBadRequestWhenIdIsNull()
        {
            // Arrange

            // Act
            var result = _employeeController.GetEmployeeById(null);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<BadRequestResult>());
        }

        [Test]
        public void GetEmployeeById_LogsErrorWhenExceptionThrown()
        {
            // Arrange
            _employeeServiceMock.Setup(x => x.GetById(1)).Throws(new Exception("Test exception"));

            // Act
            var result = _employeeController.GetEmployeeById(1);

            // Assert
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => o.ToString() == "Test exception"),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
            Assert.That(result.Result, Is.InstanceOf<BadRequestResult>());
        }

        [Test]
        public void AddEmployee_ReturnsOkResultWhenSuccessful()
        {
            // Arrange
            var employee = new EmployeeViewModel { Id = 1, Name = "Test Employee 1", Email = "test1@example.com" };
            _employeeServiceMock.Setup(x => x.CreateEmployee(employee)).Returns(true);

            // Act
            var result = _employeeController.Create(employee);

            // Assert
            Assert.That(result, Is.InstanceOf<OkResult>());
        }

        [Test]
        public void AddEmployee_ReturnsBadRequestWhenModelStateIsInvalid()
        {
            // Arrange
            _employeeController.ModelState.AddModelError("Name", "Name is required");
            var employee = new EmployeeViewModel { Id = 1, Name = "", Email = "test1@example.com" };

            // Act
            var result = _employeeController.Create(employee);

            // Assert
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public void AddEmployee_LogsErrorWhenExceptionThrown()
        {
            // Arrange
            var employee = new EmployeeViewModel { Id = 1, Name = "Test Employee 1", Email = "test1@example.com" };
            _employeeServiceMock.Setup(x => x.CreateEmployee(employee)).Throws(new Exception("Test exception"));

            // Act
            var result = _employeeController.Create(employee);

            // Assert
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => o.ToString() == "Test exception"),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
            Assert.That(result, Is.InstanceOf<BadRequestResult>());
        }

        [Test]
        public void UpdateEmployee_ReturnsNoContentResultWhenSuccessful()
        {
            // Arrange
            var employee = new EmployeeViewModel { Id = 1, Name = "Test Employee 1", Email = "test1@example.com" };
            _employeeServiceMock.Setup(x => x.UpdateEmployee(employee)).Returns(true);

            // Act
            var result = _employeeController.Update(1, employee);

            // Assert
            Assert.That(result, Is.InstanceOf<NoContentResult>());
        }

        [Test]
        public void UpdateEmployee_ReturnsBadRequestWhenModelStateIsInvalid()
        {
            // Arrange
            _employeeController.ModelState.AddModelError("Name", "Name is required");
            var employee = new EmployeeViewModel { Id = 1, Name = "", Email = "test1@example.com" };

            // Act
            var result = _employeeController.Update(1, employee);

            // Assert
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public void UpdateEmployee_ReturnsBadRequestWhenIdIsNull()
        {
            // Arrange
            var employee = new EmployeeViewModel { Id = 1, Name = "Test Employee 1", Email = "test1@example.com" };

            // Act
            var result = _employeeController.Update(null, employee);

            // Assert
            Assert.That(result, Is.InstanceOf<BadRequestResult>());
        }

        [Test]
        public void UpdateEmployee_LogsErrorWhenExceptionThrown()
        {
            // Arrange
            var employee = new EmployeeViewModel { Id = 1, Name = "Test Employee 1", Email = "test1@example.com" };
            _employeeServiceMock.Setup(x => x.UpdateEmployee(employee)).Throws(new Exception("Test exception"));

            // Act
            var result = _employeeController.Update(1, employee);

            // Assert
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => o.ToString() == "Test exception"),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
            Assert.That(result, Is.InstanceOf<BadRequestResult>());
        }

        [Test]
        public void DeleteEmployee_ReturnsNoContentResultWhenSuccessful()
        {
            // Arrange
            _employeeServiceMock.Setup(x => x.RemoveEmployee(1)).Returns(true);

            // Act
            var result = _employeeController.Delete(1);

            // Assert
            Assert.That(result, Is.InstanceOf<NoContentResult>());
        }

        [Test]
        public void DeleteEmployee_ReturnsBadRequestWhenModelStateIsInvalid()
        {
            // Arrange
            _employeeController.ModelState.AddModelError("Name", "Name is required");

            // Act
            var result = _employeeController.Delete(1);

            // Assert
            Assert.That(result, Is.InstanceOf<BadRequestResult>());
        }

        [Test]
        public void DeleteEmployee_LogsErrorWhenExceptionThrown()
        {
            // Arrange
            _employeeServiceMock.Setup(x => x.RemoveEmployee(1)).Throws(new Exception("Test exception"));

            // Act
            var result = _employeeController.Delete(1);

            // Assert
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => o.ToString() == "Test exception"),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
            Assert.That(result, Is.InstanceOf<BadRequestResult>());
        }
    }
}