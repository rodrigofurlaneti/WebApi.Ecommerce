using AutoFixture;
using Domain.Ecommerce.Enum;
using Domain.Ecommerce.Model;
using Xunit;

namespace Test.Ecommerce.Domain.Model.UserTests
{
    public class UserTests
    {
        [Fact]
        public void Propriedades_DevemTerValoresPadraoCorretos()
        {
            // Arrange
            var user = new User();

            // Act & Assert
            Assert.Equal(0, user.Id);
            Assert.Equal(string.Empty, user.Name);
            Assert.Equal(string.Empty, user.Email);
            Assert.Equal(string.Empty, user.Address);
            Assert.Equal(string.Empty, user.Number);
            Assert.Equal(string.Empty, user.Complement);
            Assert.Equal(string.Empty, user.Neighborhood);
            Assert.Equal(string.Empty, user.City);
            Assert.Equal(string.Empty, user.State);
            Assert.Equal(string.Empty, user.ZipCode);
            Assert.Equal(string.Empty, user.CellPhone);
            Assert.Equal(string.Empty, user.Username);
            Assert.Equal(string.Empty, user.Password);
        }

        [Fact]
        public void Propriedades_DevemPermitirAlteracaoDeValoresPadrao_GerandoValoresDinamicos()
        {
            // Arrange
            var fixture = new Fixture();
            var userDinamico = fixture.Build<User>().Create();

            var userManual = new User
            {
                Id = userDinamico.Id,
                Name = userDinamico.Name,
                Email = userDinamico.Email,
                Address = userDinamico.Address,
                Number = userDinamico.Number,
                Complement = userDinamico.Complement,
                Neighborhood = userDinamico.Neighborhood,
                City = userDinamico.City,
                State = userDinamico.State,
                ZipCode = userDinamico.ZipCode,
                CellPhone = userDinamico.CellPhone,
                Username = userDinamico.Username,
                Password = userDinamico.Password,
                DateInsert = userDinamico.DateInsert,
                DateUpdate = userDinamico.DateUpdate,
                Status = userDinamico.Status
            };

            // Act & Assert
            Assert.Equal(userDinamico.Id, userManual.Id);
            Assert.Equal(userDinamico.Name, userManual.Name);
            Assert.Equal(userDinamico.Email, userManual.Email);
            Assert.Equal(userDinamico.Address, userManual.Address);
            Assert.Equal(userDinamico.Number, userManual.Number);
            Assert.Equal(userDinamico.Complement, userManual.Complement);
            Assert.Equal(userDinamico.Neighborhood, userManual.Neighborhood);
            Assert.Equal(userDinamico.City, userManual.City);
            Assert.Equal(userDinamico.State, userManual.State);
            Assert.Equal(userDinamico.ZipCode, userManual.ZipCode);
            Assert.Equal(userDinamico.CellPhone, userManual.CellPhone);
            Assert.Equal(userDinamico.Username, userManual.Username);
            Assert.Equal(userDinamico.Password, userManual.Password);
            Assert.Equal(userDinamico.DateInsert, userManual.DateInsert);
            Assert.Equal(userDinamico.DateUpdate, userManual.DateUpdate);
            Assert.Equal(userDinamico.Status, userManual.Status);
        }

        [Fact]
        public void Propriedades_DevemAceitarEManterValorNulo()
        {
            // Arrange
            var fixture = new Fixture();
            var user = fixture.Build<User>()
                              .With(u => u.Id, (int?)null)
                              .With(u => u.Name, (string?)null)
                              .With(u => u.Email, (string?)null)
                              .With(u => u.Address, (string?)null)
                              .With(u => u.Number, (string?)null)
                              .With(u => u.Complement, (string?)null)
                              .With(u => u.Neighborhood, (string?)null)
                              .With(u => u.City, (string?)null)
                              .With(u => u.State, (string?)null)
                              .With(u => u.ZipCode, (string?)null)
                              .With(u => u.CellPhone, (string?)null)
                              .With(u => u.Username, (string?)null)
                              .With(u => u.Password, (string?)null)
                              .Create();

            // Act & Assert
            Assert.Null(user.Id);
            Assert.Null(user.Name);
            Assert.Null(user.Email);
            Assert.Null(user.Address);
            Assert.Null(user.Number);
            Assert.Null(user.Complement);
            Assert.Null(user.Neighborhood);
            Assert.Null(user.City);
            Assert.Null(user.State);
            Assert.Null(user.ZipCode);
            Assert.Null(user.CellPhone);
            Assert.Null(user.Username);
            Assert.Null(user.Password);
        }

        [Fact]
        public void Propriedades_DevemTerTiposCorretos()
        {
            // Arrange
            var fixture = new Fixture();
            var user = fixture.Create<User>();

            // Act & Assert
            Assert.IsType<int>(user.Id);
            Assert.IsType<string>(user.Name);
            Assert.IsType<string>(user.Email);
            Assert.IsType<string>(user.Address);
            Assert.IsType<string>(user.Number);
            Assert.IsType<string>(user.Complement);
            Assert.IsType<string>(user.Neighborhood);
            Assert.IsType<string>(user.City);
            Assert.IsType<string>(user.State);
            Assert.IsType<string>(user.ZipCode);
            Assert.IsType<string>(user.CellPhone);
            Assert.IsType<string>(user.Username);
            Assert.IsType<string>(user.Password);
            Assert.IsType<DateTime>(user.DateInsert);
            Assert.IsType<DateTime>(user.DateUpdate);
            Assert.IsType<Status>(user.Status);
        }
    }
}
