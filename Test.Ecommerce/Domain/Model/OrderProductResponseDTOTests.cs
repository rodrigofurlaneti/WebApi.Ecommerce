using Domain.Ecommerce.Model;
using Xunit;

namespace Test.Ecommerce.Domain.Model
{
    public class OrderProductResponseDTOTests
    {
        [Fact]
        public void Propriedades_DevemTerValoresPadraoCorretos()
        {
            // Arrange
            var orderProductResponseDTO = new OrderProductResponseDTO();

            // Act & Assert
            Assert.Equal(0, orderProductResponseDTO.Id);
            Assert.Equal(string.Empty, orderProductResponseDTO.Name);
            Assert.Equal(0, orderProductResponseDTO.Amount);
            Assert.Equal(string.Empty, orderProductResponseDTO.Details);
            Assert.Equal(string.Empty, orderProductResponseDTO.Picture);
            Assert.Equal(0m, orderProductResponseDTO.ValueFor);
        }

        [Fact]
        public void Propriedades_DevemPermitirAlteracaoDeValoresPadrao()
        {
            // Arrange
            var orderProductResponseDTO = new OrderProductResponseDTO
            {
                Id = 123,
                Name = "Product Name",
                Amount = 10,
                Details = "Product Details",
                Picture = "http://example.com/picture.jpg",
                ValueFor = 99.99m
            };

            // Act & Assert
            Assert.Equal(123, orderProductResponseDTO.Id);
            Assert.Equal("Product Name", orderProductResponseDTO.Name);
            Assert.Equal(10, orderProductResponseDTO.Amount);
            Assert.Equal("Product Details", orderProductResponseDTO.Details);
            Assert.Equal("http://example.com/picture.jpg", orderProductResponseDTO.Picture);
            Assert.Equal(99.99m, orderProductResponseDTO.ValueFor);
        }

        [Fact]
        public void Propriedades_DevemTerTiposCorretos()
        {
            // Arrange
            var orderProductResponseDTO = new OrderProductResponseDTO();

            // Act & Assert
            Assert.IsType<int>(orderProductResponseDTO.Id);
            Assert.IsType<string>(orderProductResponseDTO.Name);
            Assert.IsType<int>(orderProductResponseDTO.Amount);
            Assert.IsType<string>(orderProductResponseDTO.Details);
            Assert.IsType<string>(orderProductResponseDTO.Picture);
            Assert.IsType<decimal>(orderProductResponseDTO.ValueFor);
        }
    }
}
