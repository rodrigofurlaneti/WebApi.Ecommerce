using Data.Ecommerce.Interface;
using Domain.Ecommerce.Model;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Ecommerce.Controllers;
using Xunit;

namespace Test.Ecommerce.WebApi.Controllers
{
    public class OrderControllerTests
    {
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly OrderController _controller;

        public OrderControllerTests()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _controller = new OrderController(_orderRepositoryMock.Object);
        }

        [Fact]
        public async Task GetOrders_ReturnsOkResult_WithListOfOrders()
        {
            // Arrange
            var orders = new List<Order>
            {
                new Order { Id = 1, User = new User { Id = 1 }, Product = new Product { Id = 1, Amount = 2 } }
            };
            _orderRepositoryMock.Setup(repo => repo.GetAsync()).ReturnsAsync(orders);

            // Act
            var result = await _controller.Get();

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(orders);
        }

        [Fact]
        public async Task Get_ReturnsOkResult_WithProductCount()
        {
            // Arrange
            int orderId = 1;
            int countProduct = 3;
            _orderRepositoryMock.Setup(repo => repo.GetProductCountByOrderIdAsync(orderId)).ReturnsAsync(countProduct);

            // Act
            var result = await _controller.Get(orderId);

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().Be(countProduct);
        }

        [Fact]
        public async Task PutOrder_ReturnsNoContent_WhenUpdateIsSuccessful()
        {
            // Arrange
            var order = new Order { Id = 1, User = new User { Id = 1 }, Product = new Product { Id = 1, Amount = 2 } };
            _orderRepositoryMock.Setup(repo => repo.PutAsync(order)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Put(order);

            // Assert
            var noContentResult = result as NoContentResult;
            noContentResult.Should().NotBeNull();
            noContentResult.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task PutOrder_ReturnsBadRequest_WhenId_EqualsZero()
        {
            // Arrange
            var order = new Order { Id = 0, User = new User { Id = 1 }, Product = new Product { Id = 1, Amount = 2 } };

            // Act
            var result = await _controller.Put(order);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Value.Should().Be("A solicitação do id do pedido é zero");
            badRequestResult.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task DeleteOrder_ReturnsNoContent_WhenDeletionIsSuccessful()
        {
            // Arrange
            var order = new Order { Id = 1, User = new User { Id = 1 }, Product = new Product { Id = 1, Amount = 2 } };
            _orderRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(order);
            _orderRepositoryMock.Setup(repo => repo.DeleteAsync(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            var noContentResult = result as NoContentResult;
            noContentResult.Should().NotBeNull();
            noContentResult.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task DeleteOrder_ReturnsNotFound_WhenOrderIsNotFound()
        {
            // Arrange
            _orderRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Order)null);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            var notFoundResult = result as NotFoundResult;
            notFoundResult.Should().NotBeNull();
            notFoundResult.StatusCode.Should().Be(404);
        }
    }
}