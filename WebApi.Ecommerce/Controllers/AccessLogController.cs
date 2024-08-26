using Domain.Ecommerce.Model;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Data.Ecommerce.Interface;

namespace WebApi.Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessLogController : ControllerBase
    {
        private readonly IAccessLogRepository _accessLogRepository;
        private readonly IGeolocationRepository _geolocatioRepository;
        private readonly HttpClient _httpClient;

        public AccessLogController(IAccessLogRepository accessLogRepository, 
            IGeolocationRepository geolocatioRepository, HttpClient httpClient)
        {
            _accessLogRepository = accessLogRepository;
            _geolocatioRepository = geolocatioRepository;
            _httpClient = httpClient;
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
        public async Task<ActionResult<AccessLog>> Get(int id)
        {
            if (id == 0)
            {
                return BadRequest("A solicitação do AccessLog ID é ZERO");
            }

            try
            {
                var user = await _accessLogRepository.GetByIdAsync(id);

                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Erro durante a solicitação do AccessLog: {ex.Message}");

                return StatusCode(500, "Erro do Servidor Interno");
            }
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

            var api_key = "66b12b35d8ae3445384266iqodc7c89";
            var apiUrl = $"https://geocode.maps.co/reverse?lat={accessLog.Latitude}&lon={accessLog.Longitude}&api_key={api_key}";

            Place place = null;
            try
            {
                var response = await _httpClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                place = JsonSerializer.Deserialize<Place>(responseBody, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                // Verifique se todos os campos obrigatórios estão preenchidos
                if (place != null)
                {
                    // Preencha qualquer campo obrigatório que esteja faltando
                    // Supondo que o PlaceId seja gerado automaticamente no banco de dados
                    await _geolocatioRepository.PostAsync(place);
                }
            }
            catch (HttpRequestException e)
            {
                Console.Error.WriteLine($"Request error: {e.Message}");
                return StatusCode(500, "Error fetching geolocation data");
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"Error: {e.Message}");
                return StatusCode(500, "Error processing geolocation data");
            }

            return CreatedAtAction(nameof(Get), new { id = accessLog.Id }, accessLog);
        }

        // PUT: api/AccessLogs/5
        [HttpPut]
        public async Task<IActionResult> Put(AccessLog accessLog)
        {
            if (accessLog == null)
            {
                return BadRequest("A solicitação do accessLog é nula");
            }

            if (accessLog.Id == 0)
            {
                return BadRequest("A solicitação do id do accessLog é zero");
            }

            try
            {
                await _accessLogRepository.PutAsync(accessLog);

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Erro durante a solicitação para atualizar o accessLog: {ex.Message}");

                return StatusCode(500, "Erro do Servidor Interno");
            }
        }

        // DELETE: api/AccessLogs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest("A solicitação do id do perfil é zero");
            }

            try
            {
                var user = await _accessLogRepository.GetByIdAsync(id);

                if (user == null)
                {
                    return NotFound();
                }

                await _accessLogRepository.DeleteAsync(id); 
                
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Erro durante a solicitação para apagar o AccessLog: {ex.Message}");

                return StatusCode(500, "Erro do Servidor Interno");
            }
        }
    }
}
