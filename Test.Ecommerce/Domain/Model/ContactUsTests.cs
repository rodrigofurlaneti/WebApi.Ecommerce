using Domain.Ecommerce.Enum;
using Domain.Ecommerce.Model;
using Xunit;

namespace Test.Ecommerce.Domain.Model
{
    public class ContactUsTests
    {
        [Fact]
        public void Propriedades_DevemAceitarValoresNulos()
        {
            // Arrange
            var contactUs = new ContactUs
            {
                Name = null,
                Email = null,
                CellPhone = null,
                Message = null
            };

            // Act & Assert
            Assert.Null(contactUs.Name);
            Assert.Null(contactUs.Email);
            Assert.Null(contactUs.CellPhone);
            Assert.Null(contactUs.Message);
        }

        [Fact]
        public void Propriedades_DevemTerValoresPadraoCorretos()
        {
            // Arrange
            var contactUs = new ContactUs();

            // Act & Assert
            Assert.Equal(string.Empty, contactUs.Name);
            Assert.Equal(string.Empty, contactUs.Email);
            Assert.Equal(string.Empty, contactUs.CellPhone);
            Assert.Equal(string.Empty, contactUs.Message);
            Assert.Equal(default(DateTime), contactUs.DateInsert);
            Assert.Equal(default(DateTime), contactUs.DateUpdate);
            Assert.Equal(default(Status), contactUs.Status);
        }

        [Fact]
        public void Propriedades_DevemPermitirAlteracaoDeValoresPadrao()
        {
            // Arrange
            var contactUs = new ContactUs
            {
                Id = 123,
                Name = "John Doe",
                Email = "john.doe@example.com",
                CellPhone = "123-456-7890",
                Message = "Hello, I need help!",
                DateInsert = new DateTime(2023, 1, 1),
                DateUpdate = new DateTime(2023, 1, 2),
                Status = Status.Active // Supondo que 'Active' é um valor válido no enum 'Status'
            };

            // Act & Assert
            Assert.Equal(123, contactUs.Id);
            Assert.Equal("John Doe", contactUs.Name);
            Assert.Equal("john.doe@example.com", contactUs.Email);
            Assert.Equal("123-456-7890", contactUs.CellPhone);
            Assert.Equal("Hello, I need help!", contactUs.Message);
            Assert.Equal(new DateTime(2023, 1, 1), contactUs.DateInsert);
            Assert.Equal(new DateTime(2023, 1, 2), contactUs.DateUpdate);
            Assert.Equal(Status.Active, contactUs.Status);
        }

        [Fact]
        public void Propriedades_DevemTerTiposCorretos()
        {
            // Arrange
            var contactUs = new ContactUs();

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
