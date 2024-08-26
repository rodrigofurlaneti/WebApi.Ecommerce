using Data.Ecommerce.Interface;
using Data.Ecommerce.Repository;
using Domain.Ecommerce.Model;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApi.Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileRepository _profileRepository;

        public ProfileController(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        // GET: api/Profile
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profile>>> Get()
        {
            var profiles = await _profileRepository.GetAsync();

            return Ok(profiles);
        }

        // GET: api/Profile/5
        [HttpGet("{id}")]
        public async Task<ActionResult<int?>> Get(int id)
        {
            if (id == 0)
            {
                return BadRequest("A solicitação do perfil ID é ZERO");
            }

            try
            {
                var profile = await _profileRepository.GetByIdAsync(id);

                if (profile == null)
                {
                    return NotFound("Não existe este ID de perfil na base de dados");
                }

                return Ok(profile);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Erro durante a solicitação do perfil: {ex.Message}");

                return StatusCode(500, "Erro do Servidor Interno");
            }
        }

        // POST: api/Profile
        [HttpPost]
        public async Task<ActionResult<Profile?>> Post([FromBody] Profile profile)
        {
            if (profile == null)
            {
                return BadRequest("A solicitação do perfil é nula");
            }

            try
            {
                var result = await _profileRepository.PostAsync(profile);

                if (result != null && int.TryParse(result.ToString(), out int profileId))
                {
                    profile.Id = profileId;
                }

                return CreatedAtAction(nameof(Get), new { id = profile.Id }, profile);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Erro durante a autenticação: {ex.Message}");

                return StatusCode(500, "Erro do Servidor Interno");
            }
        }

        // PUT: api/Profile/5
        [HttpPut]
        public async Task<IActionResult> Put(Profile profile)
        {
            if (profile == null)
            {
                return BadRequest("A solicitação do perfil é nula");
            }

            if (profile.Id == 0)
            {
                return BadRequest("A solicitação do id do perfil é zero");
            }

            try
            {
                await _profileRepository.PutAsync(profile);

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Erro durante a solicitação: {ex.Message}");

                return StatusCode(500, "Erro do Servidor Interno");
            }
        }

        // DELETE: api/Profile/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest("A solicitação do id do perfil é zero");
            }

            try
            {
                var profile = await _profileRepository.GetByIdAsync(id);

                if (profile == null)
                {
                    return NotFound();
                }

                await _profileRepository.DeleteAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Erro durante a solicitação: {ex.Message}");

                return StatusCode(500, "Erro do Servidor Interno");
            }
        }
    }
}
