using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ProductManagementSystem.API.Controllers;
using ProductManagementSystem.Application.Customers;
using ProductManagementSystem.Domain.Customers;
using ProductManagementSystem.Shared.Dtos.Customers;

namespace ProductManagerSystem.Test.Customers
{
    public class Customers
    {
        [TestFixture]
        public class CustomersControllerTests
        {
            private Mock<CustomerHandler> _customerHandlerMock;
            private Mock<IMapper> _mapperMock;
            private CustomersController _controller;

            [SetUp]
            public void Setup()
            {
                _customerHandlerMock = new Mock<CustomerHandler>();
                _mapperMock = new Mock<IMapper>();
                _controller = new CustomersController(_customerHandlerMock.Object, _mapperMock.Object);
            }

            [Test]
            public async Task Get_ReturnsListOfCustomers()
            {
                // Arrange
                var customers = new List<CustomerDto> { new CustomerDto { Id = 1, Name = "Customer 1" } };
                _customerHandlerMock.Setup(handler => handler.GetAllAsync()).ReturnsAsync(customers);

                // Act
                var result = await _controller.Get();

                // Assert
                var okResult = result as OkObjectResult;
                Assert.IsNotNull(okResult);
                Assert.AreEqual(200, okResult.StatusCode);
                Assert.AreSame(customers, okResult.Value);
            }

            [Test]
            public async Task Get_WithValidId_ReturnsCustomer()
            {
                // Arrange
                var customerId = 1;
                var customer = new CustomerDto { Id = customerId, Name = "Customer 1" };
                _customerHandlerMock.Setup(handler => handler.GetByIdAsync(customerId)).ReturnsAsync(customer);

                // Act
                var result = await _controller.Get(customerId);

                // Assert
                var okResult = result as OkObjectResult;
                Assert.IsNotNull(okResult);
                Assert.AreEqual(200, okResult.StatusCode);
                Assert.AreSame(customer, okResult.Value);
            }

            [Test]
            public async Task Get_WithInvalidId_ReturnsNotFound()
            {
                // Arrange
                var customerId = 1;
                _customerHandlerMock.Setup(handler => handler.GetByIdAsync(customerId)).ReturnsAsync((CustomerDto)null);

                // Act
                var result = await _controller.Get(customerId);

                // Assert
                Assert.IsInstanceOf<NotFoundResult>(result);
            }

            [Test]
            public async Task Post_WithValidData_ReturnsCreatedAtAction()
            {
                // Arrange
                var customerRequest = new CustomerCreateRequest { /* fill with valid data */ };
                var customerDto = new CustomerDto { /* fill with created customer data */ };
                _mapperMock.Setup(mapper => mapper.Map<Customer>(customerRequest)).Returns(new Customer());
                _customerHandlerMock.Setup(handler => handler.AddAsync(It.IsAny<Customer>())).ReturnsAsync(new CustomerDto());

                // Act
                var result = await _controller.Post(customerRequest);

                // Assert
                var createdAtActionResult = result as CreatedAtActionResult;
                Assert.IsNotNull(createdAtActionResult);
                Assert.AreEqual(nameof(_controller.Get), createdAtActionResult.ActionName);
            }

            [Test]
            public async Task Post_WithInvalidData_ReturnsBadRequest()
            {
                // Arrange
                var customerRequest = new CustomerCreateRequest { /* fill with invalid data */ };

                // Act
                var result = await _controller.Post(customerRequest);

                // Assert
                Assert.IsInstanceOf<BadRequestObjectResult>(result);
            }

            [Test]
            public async Task Put_WithValidIdAndData_ReturnsOk()
            {
                // Arrange
                var customerId = 1;
                var customerRequest = new CustomerUpdateRequest { /* fill with updated data */ };
                var updatedCustomerDto = new CustomerDto { /* fill with updated customer data */ };
                _mapperMock.Setup(mapper => mapper.Map<Customer>(customerRequest)).Returns(new Customer());
                _customerHandlerMock.Setup(handler => handler.UpdateAsync(It.IsAny<Customer>())).ReturnsAsync(updatedCustomerDto);

                // Act
                var result = await _controller.Put(customerId, customerRequest);

                // Assert
                var okResult = result as OkObjectResult;
                Assert.IsNotNull(okResult);
                Assert.AreEqual(200, okResult.StatusCode);
            }

            [Test]
            public async Task Put_WithInvalidId_ReturnsBadRequest()
            {
                // Arrange
                var customerId = 1;
                var customerRequest = new CustomerUpdateRequest { /* fill with updated data */ };

                // Act
                var result = await _controller.Put(customerId, customerRequest);

                // Assert
                Assert.IsInstanceOf<BadRequestObjectResult>(result);
            }

            // Similarly, add tests for Put_WithValidIdAndInvalidData, Put_WithNonExistentId, and other scenarios

            [Test]
            public async Task Delete_WithValidId_ReturnsNoContent()
            {
                // Arrange
                var customerId = 1;
                var customerDto = new CustomerDto { /* fill with customer data */ };
                _customerHandlerMock.Setup(handler => handler.GetByIdAsync(customerId)).ReturnsAsync(customerDto);

                // Act
                var result = await _controller.Delete(customerId);

                // Assert
                Assert.IsInstanceOf<NoContentResult>(result);
            }

            [Test]
            public async Task Delete_WithInvalidId_ReturnsNotFound()
            {
                // Arrange
                var customerId = 1;
                _customerHandlerMock.Setup(handler => handler.GetByIdAsync(customerId)).ReturnsAsync((CustomerDto)null);

                // Act
                var result = await _controller.Delete(customerId);

                // Assert
                Assert.IsInstanceOf<NotFoundResult>(result);
            }

            [Test]
            public async Task Post_WithValidData_ReturnsCreatedAtAction()
            {
                // Arrange
                var customerRequest = new CustomerCreateRequest { /* fill with valid data */ };
                var customerDto = new CustomerDto { /* fill with created customer data */ };
                _mapperMock.Setup(mapper => mapper.Map<Customer>(customerRequest)).Returns(new Customer());
                _customerHandlerMock.Setup(handler => handler.AddAsync(It.IsAny<Customer>())).ReturnsAsync(new CustomerDto());

                // Act
                var result = await _controller.Post(customerRequest);

                // Assert
                var createdAtActionResult = result as CreatedAtActionResult;
                Assert.IsNotNull(createdAtActionResult);
                Assert.AreEqual(nameof(_controller.Get), createdAtActionResult.ActionName);
            }
        }
    }
}