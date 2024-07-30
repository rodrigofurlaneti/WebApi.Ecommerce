using Domain.Ecommerce.Model;

namespace WebApi.Ecommerce.Data.Interface
{
    public interface IAuthenticationRepository
    {
        Task<AuthenticationResponse?> PostAsync(AuthenticationRequest authenticationRequest);
    }
}
