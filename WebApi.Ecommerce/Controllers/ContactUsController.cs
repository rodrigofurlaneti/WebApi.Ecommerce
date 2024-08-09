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
        public async Task<ActionResult<IEnumerable<ContactUs>>> GetContactUs()
        {
            var contactUs = await _contactUsRepository.GetAsync();
            return Ok(contactUs);
        }

        // GET: api/ContactUs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactUs>> GetContactUs(int id)
        {
            var contactUs = await _contactUsRepository.GetByIdAsync(id);

            if (contactUs == null)
            {
                return NotFound();
            }

            return Ok(contactUs);
        }

        // POST: api/ContactUs
        [HttpPost]
        public async Task<ActionResult<ContactUs>> PostUser(ContactUs contactUs)
        {
            await _contactUsRepository.PostAsync(contactUs);
            return CreatedAtAction(nameof(GetContactUs), new { id = contactUs.Id }, contactUs);
        }

        // PUT: api/ContactUs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, ContactUs contactUs)
        {
            if (id != contactUs.Id)
            {
                return BadRequest();
            }

            await _contactUsRepository.PutAsync(contactUs);
            return NoContent();
        }

        // DELETE: api/ContactUs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _contactUsRepository.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            await _contactUsRepository.DeleteAsync(id); // Certifique-se de que esse método exista no repositório
            return NoContent();
        }
    }
}
