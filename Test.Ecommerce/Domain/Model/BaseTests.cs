using AutoFixture;
using Domain.Ecommerce.Enum;
using Domain.Ecommerce.Model;
using Xunit;

namespace Test.Ecommerce.Domain.Model.BaseTests
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
            var fixture = new Fixture();
            var baseModelDinamico = fixture.Build<Base>()
                                           .With(b => b.Status, Status.Active) // Define o Status manualmente
                                           .Create();

            var baseModelManual = new Base
            {
                DateInsert = baseModelDinamico.DateInsert,
                DateUpdate = baseModelDinamico.DateUpdate,
                Status = baseModelDinamico.Status
            };

            // Act & Assert
            Assert.Equal(baseModelDinamico.DateInsert, baseModelManual.DateInsert);
            Assert.Equal(baseModelDinamico.DateUpdate, baseModelManual.DateUpdate);
            Assert.Equal(baseModelDinamico.Status, baseModelManual.Status);
        }

        [Fact]
        public void Propriedades_DevemTerTiposCorretos()
        {
            // Arrange
            var fixture = new Fixture();
            var baseModel = fixture.Create<Base>();

            // Act & Assert
            Assert.IsType<DateTime>(baseModel.DateInsert);
            Assert.IsType<DateTime>(baseModel.DateUpdate);
            Assert.IsType<Status>(baseModel.Status);
        }
    }
}
