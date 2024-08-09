using AutoFixture;
using Domain.Ecommerce.Enum;
using Domain.Ecommerce.Model;
using Xunit;

namespace Test.Ecommerce.Domain.Model.ProductTests
{
    public class ProductTests
    {
        [Fact]
        public void Propriedades_DevemTerValoresPadraoCorretos()
        {
            // Arrange
            var product = new Product();

            // Act & Assert
            Assert.Equal(0, product.Id);
            Assert.Equal(string.Empty, product.Name);
            Assert.Equal(0, product.Amount);
            Assert.Equal(string.Empty, product.Details);
            Assert.Equal(string.Empty, product.Picture);
            Assert.Equal(0m, product.ValueOf);
            Assert.Equal(0m, product.ValueFor);
            Assert.Equal(0m, product.Discount);
            Assert.Equal(default(DateTime), product.DateInsert);
            Assert.Equal(default(DateTime), product.DateUpdate);
            Assert.Equal(default(ProductStatus), product.ProductStatus);
        }

        [Fact]
        public void Propriedades_DevemPermitirAlteracaoDeValoresPadrao_GerandoValoresDinamicos()
        {
            // Arrange
            var fixture = new Fixture(); // Cria uma instância do AutoFixture
            var productDinamico = fixture.Build<Product>()
                                         .With(p => p.ProductStatus, ProductStatus.Available) // Se precisar definir algum valor específico
                                         .Create();

            var productManual = new Product
            {
                Id = productDinamico.Id,
                Name = productDinamico.Name,
                Amount = productDinamico.Amount,
                Details = productDinamico.Details,
                Picture = productDinamico.Picture,
                ValueOf = productDinamico.ValueOf,
                ValueFor = productDinamico.ValueFor,
                Discount = productDinamico.Discount,
                DateInsert = productDinamico.DateInsert,
                DateUpdate = productDinamico.DateUpdate,
                ProductStatus = productDinamico.ProductStatus
            };

            // Act & Assert
            Assert.Equal(productDinamico.Id, productManual.Id);
            Assert.Equal(productDinamico.Name, productManual.Name);
            Assert.Equal(productDinamico.Amount, productManual.Amount);
            Assert.Equal(productDinamico.Details, productManual.Details);
            Assert.Equal(productDinamico.Picture, productManual.Picture);
            Assert.Equal(productDinamico.ValueOf, productManual.ValueOf);
            Assert.Equal(productDinamico.ValueFor, productManual.ValueFor);
            Assert.Equal(productDinamico.Discount, productManual.Discount);
            Assert.Equal(productDinamico.DateInsert, productManual.DateInsert);
            Assert.Equal(productDinamico.DateUpdate, productManual.DateUpdate);
            Assert.Equal(ProductStatus.Available, productManual.ProductStatus); // Verifica se o valor específico definido foi atribuído
        }

        [Fact]
        public void Propriedades_DevemAceitarEManterValorNulo()
        {
            // Arrange
            var fixture = new Fixture();
            var product = fixture.Build<Product>()
                                 .With(p => p.Id, (int?)null)
                                 .With(p => p.Name, (string?)null)
                                 .With(p => p.Amount, (int?)null)
                                 .With(p => p.Details, (string?)null)
                                 .With(p => p.Picture, (string?)null)
                                 .With(p => p.ValueOf, (decimal?)null)
                                 .With(p => p.ValueFor, (decimal?)null)
                                 .With(p => p.Discount, (decimal?)null)
                                 .Create();

            // Act & Assert
            Assert.Null(product.Id);
            Assert.Null(product.Name);
            Assert.Null(product.Amount);
            Assert.Null(product.Details);
            Assert.Null(product.Picture);
            Assert.Null(product.ValueOf);
            Assert.Null(product.ValueFor);
            Assert.Null(product.Discount);
        }

        [Fact]
        public void Propriedades_DevemTerTiposCorretos()
        {
            // Arrange
            var fixture = new Fixture();
            var product = fixture.Create<Product>();

            // Act & Assert
            Assert.IsType<int>(product.Id);
            Assert.IsType<string>(product.Name);
            Assert.IsType<int>(product.Amount);
            Assert.IsType<string>(product.Details);
            Assert.IsType<string>(product.Picture);
            Assert.IsType<decimal>(product.ValueOf);
            Assert.IsType<decimal>(product.ValueFor);
            Assert.IsType<decimal>(product.Discount);
            Assert.IsType<DateTime>(product.DateInsert);
            Assert.IsType<DateTime>(product.DateUpdate);
            Assert.IsType<ProductStatus>(product.ProductStatus);
        }
    }
}
