using Domain.Ecommerce.Model;
using Microsoft.AspNetCore.Mvc;
using Data.Ecommerce.Interface;

namespace WebApi.Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;

        public UsersController(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            var users = await _usersRepository.GetAsync();

            return Ok(users);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            if (id == 0)
            {
                return BadRequest("A solicitação do usuário ID é ZERO");
            }

            try
            {
                var user = await _usersRepository.GetByIdAsync(id);

                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (Exception)
            {

                throw;
            }


        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<User>> Post(User user)
        {
            if (user == null)
            {
                return BadRequest("A solicitação do usuário é nula");
            }

            try
            {
                await _usersRepository.PostAsync(user);

                return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Erro durante a solicitação para adicionar um novo usuário: {ex.Message}");

                return StatusCode(500, "Erro do Servidor Interno");
            }
        }

        // PUT: api/Users
        [HttpPut]
        public async Task<IActionResult> Put(User user)
        {
            if (user == null)
            {
                return BadRequest("A solicitação do perfil é nula");
            }

            if (user.Id == 0)
            {
                return BadRequest("A solicitação do id do perfil é zero");
            }

            try
            {
                await _usersRepository.PutAsync(user);

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Erro durante a solicitação para atualizar um registro de usuário: {ex.Message}");

                return StatusCode(500, "Erro do Servidor Interno");
            }
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest("A solicitação do id do perfil é zero");
            }

            try
            {
                var user = await _usersRepository.GetByIdAsync(id);

                if (user == null)
                {
                    return NotFound("A solicitação do id do usário não existe na base dados");
                }

                await _usersRepository.DeleteAsync(id); 

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Erro durante a solicitação para deletar o usuário da base dados ID número: {id} - {ex.Message}");

                return StatusCode(500, "Erro do Servidor Interno");
            }
        }
    }
}
