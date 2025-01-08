
using Xunit;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MCComputers_WebAPI.Controllers;
using MCComputers_WebAPI.Models.DTOs;
using MCComputers_WebAPI.Services.Interfaces;
using MCComputers_WebAPI.Models;


namespace MCComputers_WebAPI_Test
{
    public class InvoiceControllerTests
    {

        private readonly Mock<IInvoiceService> _mockInvoiceService;
        private readonly InvoiceController _controller;

        public InvoiceControllerTests()
        {
            _mockInvoiceService = new Mock<IInvoiceService>();
            _controller = new InvoiceController(_mockInvoiceService.Object);
        }


        [Fact]
        public async Task GetAllInvoices_ReturnsOkResult_WithListOfInvoices()
        {
            // Arrange
            var invoices = new List<InvoiceDTO>
            {
                new InvoiceDTO { Id = 1, TotalAmount = 100 },
                new InvoiceDTO { Id = 2, TotalAmount = 200 }
            };
            _mockInvoiceService.Setup(service => service.GetAllInvoicesAsync())
                               .ReturnsAsync(invoices);

            // Act
            var result = await _controller.GetAllInvoices();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<InvoiceDTO>>(okResult.Value);
            Assert.Equal(2, ((List<InvoiceDTO>)returnValue).Count);
        }

        [Fact]
        public async Task GetInvoice_ReturnsNotFound_WhenInvoiceDoesNotExist()
        {
            // Arrange
            int invoiceId = 1;
            _mockInvoiceService.Setup(service => service.GetInvoiceByIdAsync(invoiceId))
                               .ReturnsAsync((InvoiceDTO)null);

            // Act
            var result = await _controller.GetInvoice(invoiceId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal($"Invoice with ID {invoiceId} not found.", ((dynamic)notFoundResult.Value).Message);
        }

        [Fact]
        public async Task AddInvoice_ReturnsCreatedAtAction_WhenInvoiceIsCreated()
        {
            // Arrange
            var invoiceCreateRequest = new InvoiceCreateRequestDTO
            {
                InvoiceItemDTO = new List<InvoiceItemDTO>
                {
                    new InvoiceItemDTO { ProductId = 1, Quantity = 2, Price = 50, Discount = 0 }
                }
            };
            var createdInvoice = new InvoiceDTO { Id = 1, TotalAmount = 100 };
            _mockInvoiceService.Setup(service => service.CreateInvoiceAsync(invoiceCreateRequest))
                               .ReturnsAsync(createdInvoice);

            // Act
            var result = await _controller.AddInvoice(invoiceCreateRequest);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnValue = Assert.IsType<InvoiceDTO>(createdAtActionResult.Value);
            Assert.Equal(1, returnValue.Id);
            //Assert.Equal(100, returnValue.TotalAmount);


        }

        [Fact]
        public async Task GetAllInvoices_ReturnsStatusCode500_WhenExceptionIsThrown()
        {
            // Arrange
            _mockInvoiceService.Setup(service => service.GetAllInvoicesAsync())
                               .ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _controller.GetAllInvoices();

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, statusCodeResult.StatusCode);

            var errorResponse = Assert.IsType<ErrorResponse>(statusCodeResult.Value);
            Assert.Equal("An unexpected error occurred.", errorResponse.Message);
            Assert.Equal("Test exception", errorResponse.Details);
        }

    }
}


