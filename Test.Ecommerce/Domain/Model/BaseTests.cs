using Domain.Ecommerce.Enum;
using Domain.Ecommerce.Model;
using Xunit;

namespace Test.Ecommerce.Domain.Model
{
    public class BaseTests
    {
        [Fact]
        public void Propriedades_DevemTerValoresPadraoCorretos()
        {
            // Arrange
            var baseModel = new Base();

            // Act & Assert
            Assert.Equal(default(DateTime), baseModel.DateInsert);
            Assert.Equal(default(DateTime), baseModel.DateUpdate);
            Assert.Equal(default(Status), baseModel.Status);
        }

        [Fact]
        public void Propriedades_DevemPermitirAlteracaoDeValoresPadrao()
        {
            // Arrange
            var baseModel = new Base
            {
                DateInsert = new DateTime(2023, 1, 1),
                DateUpdate = new DateTime(2023, 1, 2),
                Status = Status.Active // Supondo que 'Active' é um valor válido no enum 'Status'
            };

            // Act & Assert
            Assert.Equal(new DateTime(2023, 1, 1), baseModel.DateInsert);
            Assert.Equal(new DateTime(2023, 1, 2), baseModel.DateUpdate);
            Assert.Equal(Status.Active, baseModel.Status);
        }

        [Fact]
        public void Propriedades_DevemTerTiposCorretos()
        {
            // Arrange
            var baseModel = new Base();

            // Act & Assert
            Assert.IsType<DateTime>(baseModel.DateInsert);
            Assert.IsType<DateTime>(baseModel.DateUpdate);
            Assert.IsType<Status>(baseModel.Status);
        }
    }
}
