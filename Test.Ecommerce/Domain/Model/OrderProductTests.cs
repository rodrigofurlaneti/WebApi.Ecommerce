using Domain.Ecommerce.Enum;
using Domain.Ecommerce.Model;
using Xunit;

namespace Test.Ecommerce.Domain.Model
{
    public class OrderProductTests
    {
        [Fact]
        public void Propriedades_DevemTerValoresPadraoCorretos()
        {
            // Arrange
            var orderProduct = new OrderProduct();

            // Act & Assert
            Assert.Equal(0, orderProduct.Id);
            Assert.Equal(0, orderProduct.IdOrder);
            Assert.Equal(0, orderProduct.IdProduct);
            Assert.Equal(default(DateTime), orderProduct.DateInsert);
            Assert.Equal(default(DateTime), orderProduct.DateUpdate);
            Assert.Equal(default(Status), orderProduct.Status);
        }

        [Fact]
        public void Propriedades_DevemPermitirAlteracaoDeValoresPadrao()
        {
            // Arrange
            var orderProduct = new OrderProduct
            {
                Id = 123,
                IdOrder = 456,
                IdProduct = 789,
                DateInsert = new DateTime(2023, 1, 1),
                DateUpdate = new DateTime(2023, 1, 2),
                Status = Status.Active // Supondo que 'Active' é um valor válido no enum 'Status'
            };

            // Act & Assert
            Assert.Equal(123, orderProduct.Id);
            Assert.Equal(456, orderProduct.IdOrder);
            Assert.Equal(789, orderProduct.IdProduct);
            Assert.Equal(new DateTime(2023, 1, 1), orderProduct.DateInsert);
            Assert.Equal(new DateTime(2023, 1, 2), orderProduct.DateUpdate);
            Assert.Equal(Status.Active, orderProduct.Status);
        }

        [Fact]
        public void Propriedades_DevemTerTiposCorretos()
        {
            // Arrange
            var orderProduct = new OrderProduct();

            // Act & Assert
            Assert.IsType<int>(orderProduct.Id);
            Assert.IsType<int>(orderProduct.IdOrder);
            Assert.IsType<int>(orderProduct.IdProduct);
            Assert.IsType<DateTime>(orderProduct.DateInsert);
            Assert.IsType<DateTime>(orderProduct.DateUpdate);
            Assert.IsType<Status>(orderProduct.Status);
        }
    }
}
