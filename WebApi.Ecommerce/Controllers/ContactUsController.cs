using Domain.Ecommerce.Model;
using Microsoft.AspNetCore.Mvc;
using Data.Ecommerce.Interface;

namespace WebApi.Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactUsController : ControllerBase
    {
        private readonly IContactUsRepository _contactUsRepository;

        public ContactUsController(IContactUsRepository contactUsRepository)
        {
            _contactUsRepository = contactUsRepository;
        }

        // GET: api/ContactUs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactUs>>> Get()
        {
            var contactUs = await _contactUsRepository.GetAsync();
            return Ok(contactUs);
        }

        // GET: api/ContactUs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactUs>> Get(int id)
        {
            if (id == 0)
            {
                return BadRequest("A solicitação do contate-nos ID é zero");
            }

            try
            {
                var contactUs = await _contactUsRepository.GetByIdAsync(id);

                if (contactUs == null)
                {
                    return NotFound();
                }

                return Ok(contactUs);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Erro durante a solicitação do contate-nos: {ex.Message}");

                return StatusCode(500, "Erro do Servidor Interno");
            }
        }

        // POST: api/ContactUs
        [HttpPost]
        public async Task<ActionResult<ContactUs>> PostUser(ContactUs contactUs)
        {
            if (contactUs == null)
            {
                return BadRequest("A solicitação do contate-nos é nula");
            }

            try
            {
                await _contactUsRepository.PostAsync(contactUs);

                return CreatedAtAction(nameof(Get), new { id = contactUs.Id }, contactUs);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Erro durante a autenticação: {ex.Message}");

                return StatusCode(500, "Erro do Servidor Interno");
            }
        }

        // PUT: api/ContactUs/5
        [HttpPut]
        public async Task<IActionResult> Put(ContactUs contactUs)
        {
            if (contactUs == null)
            {
                return BadRequest("A solicitação do contate-nos é nula");
            }

            if (contactUs.Id == 0)
            {
                return BadRequest("A solicitação do id do contate-nos é zero");
            }

            try
            {
                await _contactUsRepository.PutAsync(contactUs);

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Erro durante a solicitação para atualizar o contate-nos: {ex.Message}");

                return StatusCode(500, "Erro do Servidor Interno");
            }
        }

        // DELETE: api/ContactUs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest("A solicitação do id do contate-nos é zero");
            }

            try
            {
                var user = await _contactUsRepository.GetByIdAsync(id);

                if (user == null)
                {
                    return NotFound();
                }

                await _contactUsRepository.DeleteAsync(id); 
                
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
