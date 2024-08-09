using System;
using AutoFixture;
using Domain.Ecommerce.Enum;
using Domain.Ecommerce.Model;
using Xunit;

namespace Test.Ecommerce.Domain.Model.OrderProductTests
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
            var fixture = new Fixture(); // Cria uma instância do AutoFixture
            var orderProduct = fixture.Build<OrderProduct>()
                                      .With(op => op.Id, 123)
                                      .With(op => op.IdOrder, 456)
                                      .With(op => op.IdProduct, 789)
                                      .With(op => op.DateInsert, new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc))
                                      .With(op => op.DateUpdate, new DateTime(2023, 1, 2, 0, 0, 0, DateTimeKind.Utc))
                                      .With(op => op.Status, Status.Active) // Supondo que 'Active' é um valor válido no enum 'Status'
                                      .Create();

            // Act & Assert
            Assert.Equal(123, orderProduct.Id);
            Assert.Equal(456, orderProduct.IdOrder);
            Assert.Equal(789, orderProduct.IdProduct);
            Assert.Equal(new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc), orderProduct.DateInsert);
            Assert.Equal(new DateTime(2023, 1, 2, 0, 0, 0, DateTimeKind.Utc), orderProduct.DateUpdate);
            Assert.Equal(Status.Active, orderProduct.Status);
        }

        [Fact]
        public void Propriedades_DevemTerTiposCorretos()
        {
            // Arrange
            var fixture = new Fixture();
            var orderProduct = fixture.Create<OrderProduct>();

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
