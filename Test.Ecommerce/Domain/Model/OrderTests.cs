using AutoFixture;
using Domain.Ecommerce.Enum;
using Domain.Ecommerce.Model;
using Xunit;

namespace Test.Ecommerce.Domain.Model.OrderTests
{
    public class OrderTests
    {
        [Fact]
        public void Propriedades_DevemTerValoresPadraoCorretos()
        {
            // Arrange
            var order = new Order();

            // Act & Assert
            Assert.Null(order.Id);
            Assert.Null(order.User);
            Assert.Null(order.Product);
            Assert.Equal(default(DateTime), order.DateInsert);
            Assert.Equal(default(DateTime), order.DateUpdate);
            Assert.Equal(default(OrderStatus), order.OrderStatus);
        }

        [Fact]
        public void Propriedades_DevemPermitirAlteracaoDeValoresPadrao_GerandoValoresDinamicos()
        {
            // Arrange
            var fixture = new Fixture();
            var orderDinamico = fixture.Build<Order>()
                                       .With(o => o.OrderStatus, OrderStatus.Approved) // Se precisar definir algum valor específico
                                       .Create();

            var orderManual = new Order
            {
                Id = orderDinamico.Id,
                User = orderDinamico.User,
                Product = orderDinamico.Product,
                DateInsert = orderDinamico.DateInsert,
                DateUpdate = orderDinamico.DateUpdate,
                OrderStatus = orderDinamico.OrderStatus
            };

            // Act & Assert
            Assert.Equal(orderDinamico.Id, orderManual.Id);
            Assert.Equal(orderDinamico.User, orderManual.User);
            Assert.Equal(orderDinamico.Product, orderManual.Product);
            Assert.Equal(orderDinamico.DateInsert, orderManual.DateInsert);
            Assert.Equal(orderDinamico.DateUpdate, orderManual.DateUpdate);
            Assert.Equal(orderDinamico.OrderStatus, orderManual.OrderStatus);
        }

        [Fact]
        public void Propriedades_DevemAceitarEManterValorNulo()
        {
            // Arrange
            var fixture = new Fixture();
            var order = fixture.Build<Order>()
                               .With(o => o.Id, (int?)null)
                               .With(o => o.User, (User?)null)
                               .With(o => o.Product, (Product?)null)
                               .Create();

            // Act & Assert
            Assert.Null(order.Id);
            Assert.Null(order.User);
            Assert.Null(order.Product);
        }

        [Fact]
        public void Propriedades_DevemTerTiposCorretos()
        {
            // Arrange
            var fixture = new Fixture();
            var order = fixture.Create<Order>();

            // Act & Assert
            Assert.IsType<int>(order.Id);
            Assert.IsType<User>(order.User);
            Assert.IsType<Product>(order.Product);
            Assert.IsType<DateTime>(order.DateInsert);
            Assert.IsType<DateTime>(order.DateUpdate);
            Assert.IsType<OrderStatus>(order.OrderStatus);
        }
    }
}
