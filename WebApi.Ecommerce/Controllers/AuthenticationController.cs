using Domain.Ecommerce.Model;
using Microsoft.AspNetCore.Mvc;
using WebApi.Ecommerce.Data.Interface;

namespace WebApi.Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationRepository _authenticationRepository;

        public AuthenticationController(IAuthenticationRepository authenticationRepository)
        {
            _authenticationRepository = authenticationRepository;
        }

        [HttpPost]
        public async Task<ActionResult<AuthenticationResponse>> Post([FromBody] AuthenticationRequest authenticationRequest)
        {
            if (authenticationRequest == null)
            {
                return BadRequest("Authentication is null");
            }
            else
            {
                // Log para verificar os dados recebidos Authentication Request
                Console.WriteLine($"Nome do usuário: {authenticationRequest.Username}, senha do usuário: {authenticationRequest.Password}");

                var authenticationResponse = await _authenticationRepository.PostAsync(authenticationRequest);

                if (authenticationResponse != null)
                    // Log para verificar os dados recebidos Authentication Response
                    Console.WriteLine($"Status do usuário: {authenticationResponse.Status}");

                if (authenticationResponse == null)
                    return Unauthorized();
                else
                    return Ok(authenticationResponse);
            }
        }
    }
}
