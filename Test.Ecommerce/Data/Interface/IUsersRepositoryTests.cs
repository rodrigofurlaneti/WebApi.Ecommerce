using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Data.Ecommerce.Interface;
using Domain.Ecommerce.Model;
using FluentAssertions;
using Xunit;

namespace Test.Ecommerce.Data.Interface.IUsersRepositoryTests
{
    public class IUsersRepositoryTests
    {
        private readonly MethodInfo[] _interfaceMethods;

        public IUsersRepositoryTests()
        {
            _interfaceMethods = typeof(IUsersRepository).GetMethods();
        }

        [Fact]
        public void Deve_Ter_Metodo_GetAsync_Corretamente_Definido()
        {
            // Arrange
            var method = _interfaceMethods.FirstOrDefault(m => m.Name == "GetAsync");

            // Assert
            method.Should().NotBeNull();
            method.ReturnType.Should().Be<Task<IEnumerable<User>>>();
            method.GetParameters().Should().BeEmpty();
        }

        [Fact]
        public void Deve_Ter_Metodo_PostAsync_Corretamente_Definido()
        {
            // Arrange
            var method = _interfaceMethods.FirstOrDefault(m => m.Name == "PostAsync");

            // Assert
            method.Should().NotBeNull();
            method.ReturnType.Should().Be<Task>();
            method.GetParameters().Should().ContainSingle(p => p.ParameterType == typeof(User));
        }

        [Fact]
        public void Deve_Ter_Metodo_PutAsync_Corretamente_Definido()
        {
            // Arrange
            var method = _interfaceMethods.FirstOrDefault(m => m.Name == "PutAsync");

            // Assert
            method.Should().NotBeNull();
            method.ReturnType.Should().Be<Task>();
            method.GetParameters().Should().ContainSingle(p => p.ParameterType == typeof(User));
        }

        [Fact]
        public void Deve_Ter_Metodo_GetByIdAsync_Corretamente_Definido()
        {
            // Arrange
            var method = _interfaceMethods.FirstOrDefault(m => m.Name == "GetByIdAsync");

            // Assert
            method.Should().NotBeNull();
            method.ReturnType.Should().Be<Task<User>>();
            method.GetParameters().Should().ContainSingle(p => p.ParameterType == typeof(int));
        }

        [Fact]
        public void Deve_Ter_Metodo_DeleteAsync_Corretamente_Definido()
        {
            // Arrange
            var method = _interfaceMethods.FirstOrDefault(m => m.Name == "DeleteAsync");

            // Assert
            method.Should().NotBeNull();
            method.ReturnType.Should().Be<Task>();
            method.GetParameters().Should().ContainSingle(p => p.ParameterType == typeof(int));
        }
    }
}
