using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Data.Ecommerce.Interface;
using Domain.Ecommerce.Model;
using FluentAssertions;
using Xunit;

namespace Test.Ecommerce.Data.Interface.IAuthenticationRepositoryTests
{
    public class IAuthenticationRepositoryTests
    {
        private readonly MethodInfo[] _interfaceMethods;

        public IAuthenticationRepositoryTests()
        {
            _interfaceMethods = typeof(IAuthenticationRepository).GetMethods();
        }

        [Fact]
        public void Deve_Ter_Metodo_PostAsync_Corretamente_Definido()
        {
            // Arrange
            var method = _interfaceMethods.FirstOrDefault(m => m.Name == "PostAsync");

            // Assert
            method.Should().NotBeNull();
            method.ReturnType.Should().Be<Task<AuthenticationResponse?>>();
            method.GetParameters().Should().ContainSingle(p => p.ParameterType == typeof(AuthenticationRequest));
        }
    }
}
