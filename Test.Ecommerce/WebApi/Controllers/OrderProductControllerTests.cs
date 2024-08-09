﻿using Data.Ecommerce.Interface;
using Domain.Ecommerce.Model;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Ecommerce.Controllers;
using Xunit;

namespace Test.Ecommerce.WebApi.Controllers
{
    public class OrderProductControllerTests
    {
        private readonly Mock<IOrderProductRepository> _orderProductRepositoryMock;
        private readonly OrderProductController _controller;

        public OrderProductControllerTests()
        {
            _orderProductRepositoryMock = new Mock<IOrderProductRepository>();
            _controller = new OrderProductController(_orderProductRepositoryMock.Object);
        }

        [Fact]
        public async Task PutOrderProduct_ReturnsNoContent_WhenUpdateIsSuccessful()
        {
            // Arrange
            var orderProduct = new OrderProduct { Id = 1 };
            _orderProductRepositoryMock.Setup(repo => repo.PutAsync(orderProduct)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PutOrderProduct(1, orderProduct);

            // Assert
            var noContentResult = result as NoContentResult;
            noContentResult.Should().NotBeNull();
            noContentResult.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task PutOrderProduct_ReturnsBadRequest_WhenIdDoesNotMatch()
        {
            // Arrange
            var orderProduct = new OrderProduct { Id = 1 };

            // Act
            var result = await _controller.PutOrderProduct(2, orderProduct);

            // Assert
            var badRequestResult = result as BadRequestResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task DeleteOrderProduct_ReturnsNoContent_WhenDeletionIsSuccessful()
        {
            // Arrange
            var orderProduct = new OrderProduct { Id = 1 };
            _orderProductRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(orderProduct);
            _orderProductRepositoryMock.Setup(repo => repo.DeleteAsync(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteOrderProduct(1);

            // Assert
            var noContentResult = result as NoContentResult;
            noContentResult.Should().NotBeNull();
            noContentResult.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task DeleteOrderProduct_ReturnsNotFound_WhenOrderProductIsNotFound()
        {
            // Arrange
            _orderProductRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((OrderProduct)null);

            // Act
            var result = await _controller.DeleteOrderProduct(1);

            // Assert
            var notFoundResult = result as NotFoundResult;
            notFoundResult.Should().NotBeNull();
            notFoundResult.StatusCode.Should().Be(404);
        }
    }
}