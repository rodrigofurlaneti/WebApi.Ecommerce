using Domain.Ecommerce.Model;

namespace Data.Ecommerce.Interface
{
    public interface IAuthenticationRepository
    {
        Task<AuthenticationResponse?> PostAsync(AuthenticationRequest authenticationRequest);
    }
}
