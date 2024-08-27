using Data.Ecommerce.Interface;
using Domain.Ecommerce.Model;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.Protected;
using System.Net;
using WebApi.Ecommerce.Controllers;
using Xunit;

namespace Test.Ecommerce.WebApi.Controllers
{
    public class AccessLogControllerTests
    {
        private readonly Mock<IAccessLogRepository> _accessLogRepositoryMock;
        private readonly Mock<IGeolocationRepository> _geolocationRepositoryMock;
        private readonly Mock<HttpClient> _httpClientMock;
        private readonly AccessLogController _controller;

        public AccessLogControllerTests()
        {
            _accessLogRepositoryMock = new Mock<IAccessLogRepository>();
            _geolocationRepositoryMock = new Mock<IGeolocationRepository>();
            _httpClientMock = new Mock<HttpClient>();

            _controller = new AccessLogController(
                _accessLogRepositoryMock.Object,
                _geolocationRepositoryMock.Object,
                _httpClientMock.Object);
        }

        [Fact]
        public async Task Get_ReturnsOk_WithListOfAccessLogs()
        {
            // Arrange
            var accessLogs = new List<AccessLog> { new AccessLog { Id = 1 } };
            _accessLogRepositoryMock.Setup(repo => repo.GetAsync())
                .ReturnsAsync(accessLogs);

            // Act
            var result = await _controller.Get();

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.Value.Should().BeEquivalentTo(accessLogs);
        }

        [Fact]
        public async Task GetAccessLog_ReturnsNotFound_WhenAccessLogDoesNotExist()
        {
            // Arrange
            _accessLogRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((AccessLog)null);

            // Act
            var result = await _controller.Get(1);

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetAccessLog_ReturnsOk_WithAccessLog()
        {
            // Arrange
            var accessLog = new AccessLog { Id = 1 };
            _accessLogRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(accessLog);

            // Act
            var result = await _controller.Get(1);

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.Value.Should().Be(accessLog);
        }

        [Fact]
        public async Task Post_ReturnsBadRequest_WhenAccessLogIsNull()
        {
            // Act
            var result = await _controller.Post(null);

            // Assert
            result.Result.Should().BeOfType<BadRequestObjectResult>()
                .Which.Value.Should().Be("AccessLog is null");
        }

        [Fact]
        public async Task Post_ReturnsCreatedAtAction_WithValidAccessLog()
        {
            // Arrange
            var accessLog = new AccessLog { Id = 1, Latitude = "0", Longitude = "0", InternetProtocol = "127.0.0.1" };
            _accessLogRepositoryMock.Setup(repo => repo.PostAsync(It.IsAny<AccessLog>()))
                                    .Returns(Task.CompletedTask);

            var place = new Place { PlaceId = 1 };
            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"placeId\":1}")
            };

            var httpMessageHandlerMock = new Mock<HttpMessageHandler>();

            httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponseMessage);

            var httpClient = new HttpClient(httpMessageHandlerMock.Object);

            var controller = new AccessLogController(_accessLogRepositoryMock.Object, _geolocationRepositoryMock.Object, httpClient);

            // Act
            var result = await controller.Post(accessLog);

            // Assert
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            createdAtActionResult.Should().NotBeNull();
            createdAtActionResult.ActionName.Should().Be(nameof(controller.Get));
            createdAtActionResult.RouteValues["id"].Should().Be(accessLog.Id);
            createdAtActionResult.Value.Should().Be(accessLog);
        }

        [Fact]
        public async Task Put_ReturnsNoContent_WhenAccessLogIsUpdated()
        {
            // Arrange
            var accessLog = new AccessLog { Id = 1 };
            _accessLogRepositoryMock.Setup(repo => repo.PutAsync(It.IsAny<AccessLog>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Put(accessLog);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenAccessLogDoesNotExist()
        {
            // Arrange
            _accessLogRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((AccessLog)null);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenAccessLogIsDeleted()
        {
            // Arrange
            var accessLog = new AccessLog { Id = 1 };
            _accessLogRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(accessLog);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }
    }
}