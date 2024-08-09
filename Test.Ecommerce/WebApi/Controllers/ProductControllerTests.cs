using Data.Ecommerce.Interface;
using Domain.Ecommerce.Model;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Ecommerce.Controllers;
using Xunit;

namespace Test.Ecommerce.WebApi.Controllers
{
    public class ProductControllerTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly ProductController _controller;

        public ProductControllerTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _controller = new ProductController(_productRepositoryMock.Object);
        }

        [Fact]
        public async Task Get_ReturnsOkResult_WithListOfProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", Amount = 10 },
                new Product { Id = 2, Name = "Product 2", Amount = 5 }
            };
            _productRepositoryMock.Setup(repo => repo.GetAsync()).ReturnsAsync(products);

            // Act
            var result = await _controller.Get();

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(products);
        }

        [Fact]
        public async Task GetProduct_ReturnsOkResult_WithProduct()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Product 1", Amount = 10 };
            _productRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(product);

            // Act
            var result = await _controller.GetProduct(1);

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(product);
        }

        [Fact]
        public async Task GetProduct_ReturnsNotFound_WhenProductIsNull()
        {
            // Arrange
            _productRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Product)null);

            // Act
            var result = await _controller.GetProduct(1);

            // Assert
            var notFoundResult = result.Result as NotFoundResult;
            notFoundResult.Should().NotBeNull();
            notFoundResult.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task Post_ReturnsCreatedAtAction_WithValidProduct()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Product 1", Amount = 10, Picture = "product1.jpg" };

            // Act
            var result = await _controller.Post(product);

            // Assert
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            createdAtActionResult.Should().NotBeNull();
            createdAtActionResult.StatusCode.Should().Be(201);
            createdAtActionResult.ActionName.Should().Be(nameof(_controller.GetProduct));
            createdAtActionResult.RouteValues["id"].Should().Be(product.Id);
            createdAtActionResult.Value.Should().Be(product);
        }

        [Fact]
        public async Task Post_ReturnsBadRequest_WhenProductIsNull()
        {
            // Act
            var result = await _controller.Post(null);

            // Assert
            var badRequestResult = result.Result as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult.StatusCode.Should().Be(400);
            badRequestResult.Value.Should().Be("Product is null");
        }

        [Fact]
        public async Task Put_ReturnsNoContent_WhenUpdateIsSuccessful()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Product 1", Amount = 10 };
            _productRepositoryMock.Setup(repo => repo.PutAsync(product)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Put(1, product);

            // Assert
            var noContentResult = result as NoContentResult;
            noContentResult.Should().NotBeNull();
            noContentResult.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task Put_ReturnsBadRequest_WhenIdDoesNotMatch()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Product 1", Amount = 10 };

            // Act
            var result = await _controller.Put(2, product);

            // Assert
            var badRequestResult = result as BadRequestResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenDeletionIsSuccessful()
        {
            // Arrange
            var product = new Product { Id = 1 };
            _productRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(product);
            _productRepositoryMock.Setup(repo => repo.DeleteAsync(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            var noContentResult = result as NoContentResult;
            noContentResult.Should().NotBeNull();
            noContentResult.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenProductIsNotFound()
        {
            // Arrange
            _productRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Product)null);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            var notFoundResult = result as NotFoundResult;
            notFoundResult.Should().NotBeNull();
            notFoundResult.StatusCode.Should().Be(404);
        }
    }
}