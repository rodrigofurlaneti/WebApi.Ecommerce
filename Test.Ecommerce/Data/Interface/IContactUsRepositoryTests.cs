using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Data.Ecommerce.Interface;
using Domain.Ecommerce.Model;
using FluentAssertions;
using Xunit;

namespace Test.Ecommerce.Data.Interface.IContactUsRepositoryTests
{
    public class IContactUsRepositoryTests
    {
        private readonly MethodInfo[] _interfaceMethods;

        public IContactUsRepositoryTests()
        {
            _interfaceMethods = typeof(IContactUsRepository).GetMethods();
        }

        [Fact]
        public void Deve_Ter_Metodo_GetAsync_Corretamente_Definido()
        {
            // Arrange
            var method = _interfaceMethods.FirstOrDefault(m => m.Name == "GetAsync");

            // Assert
            method.Should().NotBeNull();
            method.ReturnType.Should().Be<Task<IEnumerable<ContactUs>>>();
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
            method.GetParameters().Should().ContainSingle(p => p.ParameterType == typeof(ContactUs));
        }

        [Fact]
        public void Deve_Ter_Metodo_PutAsync_Corretamente_Definido()
        {
            // Arrange
            var method = _interfaceMethods.FirstOrDefault(m => m.Name == "PutAsync");

            // Assert
            method.Should().NotBeNull();
            method.ReturnType.Should().Be<Task>();
            method.GetParameters().Should().ContainSingle(p => p.ParameterType == typeof(ContactUs));
        }

        [Fact]
        public void Deve_Ter_Metodo_GetByIdAsync_Corretamente_Definido()
        {
            // Arrange
            var method = _interfaceMethods.FirstOrDefault(m => m.Name == "GetByIdAsync");

            // Assert
            method.Should().NotBeNull();
            method.ReturnType.Should().Be<Task<ContactUs?>>();
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
