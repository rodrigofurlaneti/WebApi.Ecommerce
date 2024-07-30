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
                return BadRequest("Authentication request is null");
            }

            try
            {
                var authenticationResponse = await _authenticationRepository.PostAsync(authenticationRequest);

                if (authenticationResponse == null)
                {
                    return Unauthorized();
                }

                return Ok(authenticationResponse);
            }
            catch (Exception ex)
            {
                // Log de erro
                Console.Error.WriteLine($"Erro durante a autenticação: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
