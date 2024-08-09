using Data.Ecommerce.Interface;
using Domain.Ecommerce.Model;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Ecommerce.Controllers;
using Xunit;

namespace Test.Ecommerce.WebApi.Controllers
{
    public class UsersControllerTests
    {
        private readonly Mock<IUsersRepository> _usersRepositoryMock;
        private readonly UsersController _controller;

        public UsersControllerTests()
        {
            _usersRepositoryMock = new Mock<IUsersRepository>();
            _controller = new UsersController(_usersRepositoryMock.Object);
        }

        [Fact]
        public async Task GetUsers_ReturnsOkResult_WithListOfUsers()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = 1, Name = "User 1" },
                new User { Id = 2, Name = "User 2" }
            };
            _usersRepositoryMock.Setup(repo => repo.GetAsync()).ReturnsAsync(users);

            // Act
            var result = await _controller.GetUsers();

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(users);
        }

        [Fact]
        public async Task GetUser_ReturnsOkResult_WithUser()
        {
            // Arrange
            var user = new User { Id = 1, Name = "User 1" };
            _usersRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(user);

            // Act
            var result = await _controller.GetUser(1);

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(user);
        }

        [Fact]
        public async Task GetUser_ReturnsNotFound_WhenUserIsNull()
        {
            // Arrange
            _usersRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((User)null);

            // Act
            var result = await _controller.GetUser(1);

            // Assert
            var notFoundResult = result.Result as NotFoundResult;
            notFoundResult.Should().NotBeNull();
            notFoundResult.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task PostUser_ReturnsCreatedAtAction_WithValidUser()
        {
            // Arrange
            var user = new User { Id = 1, Name = "User 1" };

            // Act
            var result = await _controller.PostUser(user);

            // Assert
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            createdAtActionResult.Should().NotBeNull();
            createdAtActionResult.StatusCode.Should().Be(201);
            createdAtActionResult.ActionName.Should().Be(nameof(_controller.GetUser));
            createdAtActionResult.RouteValues["id"].Should().Be(user.Id);
            createdAtActionResult.Value.Should().Be(user);
        }

        [Fact]
        public async Task PutUser_ReturnsNoContent_WhenUpdateIsSuccessful()
        {
            // Arrange
            var user = new User { Id = 1, Name = "User 1" };
            _usersRepositoryMock.Setup(repo => repo.PutAsync(user)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PutUser(1, user);

            // Assert
            var noContentResult = result as NoContentResult;
            noContentResult.Should().NotBeNull();
            noContentResult.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task PutUser_ReturnsBadRequest_WhenIdDoesNotMatch()
        {
            // Arrange
            var user = new User { Id = 1, Name = "User 1" };

            // Act
            var result = await _controller.PutUser(2, user);

            // Assert
            var badRequestResult = result as BadRequestResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task DeleteUser_ReturnsNoContent_WhenDeletionIsSuccessful()
        {
            // Arrange
            var user = new User { Id = 1 };
            _usersRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(user);
            _usersRepositoryMock.Setup(repo => repo.DeleteAsync(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteUser(1);

            // Assert
            var noContentResult = result as NoContentResult;
            noContentResult.Should().NotBeNull();
            noContentResult.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task DeleteUser_ReturnsNotFound_WhenUserIsNotFound()
        {
            // Arrange
            _usersRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((User)null);

            // Act
            var result = await _controller.DeleteUser(1);

            // Assert
            var notFoundResult = result as NotFoundResult;
            notFoundResult.Should().NotBeNull();
            notFoundResult.StatusCode.Should().Be(404);
        }
    }
}