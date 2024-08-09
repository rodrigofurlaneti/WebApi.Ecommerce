using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Data.Ecommerce.Interface;
using Domain.Ecommerce.Model;
using FluentAssertions;
using Xunit;

namespace Test.Ecommerce.Data.Interface.IAccessLogRepositoryTests
{
    public class IAccessLogRepositoryTests
    {
        private readonly MethodInfo[] _interfaceMethods;

        public IAccessLogRepositoryTests()
        {
            _interfaceMethods = typeof(IAccessLogRepository).GetMethods();
        }

        [Fact]
        public void Deve_Ter_Metodo_GetAsync_Corretamente_Definido()
        {
            // Arrange
            var method = _interfaceMethods.FirstOrDefault(m => m.Name == "GetAsync");

            // Assert
            method.Should().NotBeNull();
            method.ReturnType.Should().Be<Task<IEnumerable<AccessLog>>>();
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
            method.GetParameters().Should().ContainSingle(p => p.ParameterType == typeof(AccessLog));
        }

        [Fact]
        public void Deve_Ter_Metodo_PutAsync_Corretamente_Definido()
        {
            // Arrange
            var method = _interfaceMethods.FirstOrDefault(m => m.Name == "PutAsync");

            // Assert
            method.Should().NotBeNull();
            method.ReturnType.Should().Be<Task>();
            method.GetParameters().Should().ContainSingle(p => p.ParameterType == typeof(AccessLog));
        }

        [Fact]
        public void Deve_Ter_Metodo_GetByIdAsync_Corretamente_Definido()
        {
            // Arrange
            var method = _interfaceMethods.FirstOrDefault(m => m.Name == "GetByIdAsync");

            // Assert
            method.Should().NotBeNull();
            method.ReturnType.Should().Be<Task<AccessLog?>>();
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
