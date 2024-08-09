using AutoFixture;
using Domain.Ecommerce.Enum;
using Domain.Ecommerce.Model;
using Xunit;

namespace Test.Ecommerce.Domain.Model.ContactUsTests
{
    public class ContactUsTests
    {
        [Fact]
        public void Propriedades_DevemTerValoresPadraoCorretos()
        {
            // Arrange
            var contactUs = new ContactUs();

            // Act & Assert
            Assert.Equal(0, contactUs.Id);
            Assert.Equal(string.Empty, contactUs.Name);
            Assert.Equal(string.Empty, contactUs.Email);
            Assert.Equal(string.Empty, contactUs.CellPhone);
            Assert.Equal(string.Empty, contactUs.Message);
            Assert.Equal(default(DateTime), contactUs.DateInsert);
            Assert.Equal(default(DateTime), contactUs.DateUpdate);
            Assert.Equal(default(Status), contactUs.Status);
        }

        [Fact]
        public void Propriedades_DevemPermitirAlteracaoDeValoresPadrao_GerandoValoresDinamicos()
        {
            // Arrange
            var fixture = new Fixture();
            var contactUsDinamico = fixture.Build<ContactUs>().Create();

            var contactUsManual = new ContactUs
            {
                Id = contactUsDinamico.Id,
                Name = contactUsDinamico.Name,
                Email = contactUsDinamico.Email,
                CellPhone = contactUsDinamico.CellPhone,
                Message = contactUsDinamico.Message,
                DateInsert = contactUsDinamico.DateInsert,
                DateUpdate = contactUsDinamico.DateUpdate,
                Status = contactUsDinamico.Status
            };

            // Act & Assert
            Assert.Equal(contactUsDinamico.Id, contactUsManual.Id);
            Assert.Equal(contactUsDinamico.Name, contactUsManual.Name);
            Assert.Equal(contactUsDinamico.Email, contactUsManual.Email);
            Assert.Equal(contactUsDinamico.CellPhone, contactUsManual.CellPhone);
            Assert.Equal(contactUsDinamico.Message, contactUsManual.Message);
            Assert.Equal(contactUsDinamico.DateInsert, contactUsManual.DateInsert);
            Assert.Equal(contactUsDinamico.DateUpdate, contactUsManual.DateUpdate);
            Assert.Equal(contactUsDinamico.Status, contactUsManual.Status);
        }

        [Fact]
        public void Propriedades_DevemAceitarEManterValorNulo()
        {
            // Arrange
            var fixture = new Fixture();
            var contactUs = fixture.Build<ContactUs>()
                                   .With(c => c.Name, (string?)null)
                                   .With(c => c.Email, (string?)null)
                                   .With(c => c.CellPhone, (string?)null)
                                   .With(c => c.Message, (string?)null)
                                   .Create();

            // Act & Assert
            Assert.Null(contactUs.Name);
            Assert.Null(contactUs.Email);
            Assert.Null(contactUs.CellPhone);
            Assert.Null(contactUs.Message);
        }

        [Fact]
        public void Propriedades_DevemTerTiposCorretos()
        {
            // Arrange
            var fixture = new Fixture();
            var contactUs = fixture.Create<ContactUs>();

            // Act & Assert
            Assert.IsType<int>(contactUs.Id);
            Assert.IsType<string>(contactUs.Name);
            Assert.IsType<string>(contactUs.Email);
            Assert.IsType<string>(contactUs.CellPhone);
            Assert.IsType<string>(contactUs.Message);
            Assert.IsType<DateTime>(contactUs.DateInsert);
            Assert.IsType<DateTime>(contactUs.DateUpdate);
            Assert.IsType<Status>(contactUs.Status);
        }
    }
}
