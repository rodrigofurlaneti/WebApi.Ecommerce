using Data.Ecommerce.Interface;
using Domain.Ecommerce.Model;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Ecommerce.Controllers;
using Xunit;

namespace Test.Ecommerce.WebApi.Controllers
{
    public class ContactUsControllerTests
    {
        private readonly Mock<IContactUsRepository> _contactUsRepositoryMock;
        private readonly ContactUsController _controller;

        public ContactUsControllerTests()
        {
            _contactUsRepositoryMock = new Mock<IContactUsRepository>();
            _controller = new ContactUsController(_contactUsRepositoryMock.Object);
        }

        [Fact]
        public async Task GetContactUs_ReturnsOkResult_WithListOfContactUs()
        {
            // Arrange
            var contactUsList = new List<ContactUs> { new ContactUs { Id = 1, Name = "John Doe", Email = "john@example.com" } };
            _contactUsRepositoryMock.Setup(repo => repo.GetAsync()).ReturnsAsync(contactUsList);

            // Act
            var result = await _controller.Get();

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(contactUsList);
        }

        [Fact]
        public async Task GetContactUs_ById_ReturnsOkResult_WithContactUs()
        {
            // Arrange
            var contactUs = new ContactUs { Id = 1, Name = "John Doe", Email = "john@example.com" };
            _contactUsRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(contactUs);

            // Act
            var result = await _controller.Get(1);

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(contactUs);
        }

        [Fact]
        public async Task GetContactUs_ById_ReturnsNotFound_WhenContactUsIsNull()
        {
            // Arrange
            _contactUsRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((ContactUs)null);

            // Act
            var result = await _controller.Get(1);

            // Assert
            var notFoundResult = result.Result as NotFoundResult;
            notFoundResult.Should().NotBeNull();
            notFoundResult.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task PostUser_ReturnsCreatedAtAction_WithContactUs()
        {
            // Arrange
            var contactUs = new ContactUs { Id = 1, Name = "John Doe", Email = "john@example.com" };

            // Act
            var result = await _controller.PostUser(contactUs);

            // Assert
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            createdAtActionResult.Should().NotBeNull();
            createdAtActionResult.StatusCode.Should().Be(201);
            createdAtActionResult.ActionName.Should().Be(nameof(ContactUsController.Get));
            createdAtActionResult.RouteValues["id"].Should().Be(contactUs.Id);
            createdAtActionResult.Value.Should().Be(contactUs);
        }

        [Fact]
        public async Task PutUser_ReturnsNoContent_WhenUpdateIsSuccessful()
        {
            // Arrange
            var contactUs = new ContactUs { Id = 1, Name = "John Doe", Email = "john@example.com" };
            _contactUsRepositoryMock.Setup(repo => repo.PutAsync(contactUs)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Put(contactUs);

            // Assert
            var noContentResult = result as NoContentResult;
            noContentResult.Should().NotBeNull();
            noContentResult.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task PutUser_ReturnsBadRequest_WhenId_EqualsZero()
        {
            // Arrange
            var contactUs = new ContactUs { Id = 0, Name = "John Doe", Email = "john@example.com" };

            // Act
            var result = await _controller.Put(contactUs);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Value.Should().Be("A solicitação do id do contate-nos é zero");
            badRequestResult.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task DeleteUser_ReturnsNoContent_WhenDeletionIsSuccessful()
        {
            // Arrange
            var contactUs = new ContactUs { Id = 1, Name = "John Doe", Email = "john@example.com" };
            _contactUsRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(contactUs);
            _contactUsRepositoryMock.Setup(repo => repo.DeleteAsync(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            var noContentResult = result as NoContentResult;
            noContentResult.Should().NotBeNull();
            noContentResult.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task DeleteUser_ReturnsNotFound_WhenContactUsDoesNotExist()
        {
            // Arrange
            _contactUsRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((ContactUs)null);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            var notFoundResult = result as NotFoundResult;
            notFoundResult.Should().NotBeNull();
            notFoundResult.StatusCode.Should().Be(404);
        }
    }
}