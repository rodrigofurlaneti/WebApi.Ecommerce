using Domain.Ecommerce.Model;
using Microsoft.AspNetCore.Mvc;
using WebApi.Ecommerce.Data.Interface;

namespace WebApi.Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessLogController : ControllerBase
    {
        private readonly IAccessLogRepository _accessLogRepository;

        public AccessLogController(IAccessLogRepository accessLogRepository)
        {
            _accessLogRepository = accessLogRepository;
        }

        // GET: api/AccessLogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccessLog>>> Get()
        {
            var product = await _accessLogRepository.GetAsync();
            return Ok(product);
        }

        // GET: api/AccessLogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccessLog>> GetAccessLog(int id)
        {
            var user = await _accessLogRepository.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<AccessLog>> Post([FromBody] AccessLog accessLog)
        {
            if (accessLog == null)
            {
                return BadRequest("AccessLog is null");
            }

            // Log para verificar os dados recebidos
            Console.WriteLine($"Latitude: {accessLog.Latitude}, Longitude: {accessLog.Longitude}, IP: {accessLog.InternetProtocol}");

            await _accessLogRepository.PostAsync(accessLog);
            return CreatedAtAction(nameof(GetAccessLog), new { id = accessLog.Id }, accessLog);
        }

        // PUT: api/AccessLogs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, AccessLog user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            await _accessLogRepository.PutAsync(user);
            return NoContent();
        }

        // DELETE: api/AccessLogs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _accessLogRepository.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            await _accessLogRepository.DeleteAsync(id); // Certifique-se de que esse método exista no repositório
            return NoContent();
        }
    }
}
