using Data.Ecommerce.Interface;
using Domain.Ecommerce.Model;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Ecommerce.Controllers;
using Xunit;

namespace Test.Ecommerce.WebApi.Controllers
{
    public class AuthenticationControllerTests
    {
        private readonly Mock<IAuthenticationRepository> _authenticationRepositoryMock;
        private readonly AuthenticationController _controller;

        public AuthenticationControllerTests()
        {
            _authenticationRepositoryMock = new Mock<IAuthenticationRepository>();
            _controller = new AuthenticationController(_authenticationRepositoryMock.Object);
        }

        [Fact]
        public async Task Post_ReturnsBadRequest_WhenAuthenticationRequestIsNull()
        {
            // Act
            var result = await _controller.Post(null);

            // Assert
            var badRequestResult = result.Result as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult.StatusCode.Should().Be(400);
            badRequestResult.Value.Should().Be("Authentication request is null");
        }

        [Fact]
        public async Task Post_ReturnsUnauthorized_WhenAuthenticationResponseIsNull()
        {
            // Arrange
            var authenticationRequest = new AuthenticationRequest { Username = "testuser", Password = "password" };
            _authenticationRepositoryMock.Setup(repo => repo.PostAsync(authenticationRequest))
                                         .ReturnsAsync((AuthenticationResponse)null);

            // Act
            var result = await _controller.Post(authenticationRequest);

            // Assert
            var unauthorizedResult = result.Result as UnauthorizedResult;
            unauthorizedResult.Should().NotBeNull();
            unauthorizedResult.StatusCode.Should().Be(401);
        }

        [Fact]
        public async Task Post_ReturnsOk_WithAuthenticationResponse()
        {
            // Arrange
            var authenticationRequest = new AuthenticationRequest { Username = "testuser", Password = "password" };
            var authenticationResponse = new AuthenticationResponse { Username = "testuser", Password = "password" };

            _authenticationRepositoryMock.Setup(repo => repo.PostAsync(authenticationRequest))
                                         .ReturnsAsync(authenticationResponse);

            // Act
            var result = await _controller.Post(authenticationRequest);

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().Be(authenticationResponse);
        }

        [Fact]
        public async Task Post_ReturnsInternalServerError_OnException()
        {
            // Arrange
            var authenticationRequest = new AuthenticationRequest { Username = "testuser", Password = "password" };
            _authenticationRepositoryMock.Setup(repo => repo.PostAsync(authenticationRequest))
                                         .ThrowsAsync(new Exception("Something went wrong"));

            // Act
            var result = await _controller.Post(authenticationRequest);

            // Assert
            var statusCodeResult = result.Result as ObjectResult;
            statusCodeResult.Should().NotBeNull();
            statusCodeResult.StatusCode.Should().Be(500);
            statusCodeResult.Value.Should().Be("Internal server error");
        }
    }
}