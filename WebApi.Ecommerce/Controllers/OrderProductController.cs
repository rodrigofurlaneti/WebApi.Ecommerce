using Domain.Ecommerce.Model;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Data.Ecommerce.Interface;

namespace WebApi.Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderProductController : ControllerBase
    {
        private readonly IOrderProductRepository _orderProductRepository;

        public OrderProductController(IOrderProductRepository orderProductRepository)
        {
            _orderProductRepository = orderProductRepository;
        }

        // GET: api/OrderProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderProduct>>> Get()
        {
            var orderProduct = await _orderProductRepository.GetAsync();

            return Ok(orderProduct);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderProductResponse>> Get(int id)
        {
            if (id == 0)
            {
                return BadRequest("A solicitação do pedido do produto ID é zero");
            }

            try
            {
                var orderProductResponse = await _orderProductRepository.GetByIdAsync(id);

                if (orderProductResponse == null)
                {
                    return NotFound();
                }

                return Ok(orderProductResponse);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Erro durante a solicitação do pedido do produto: {ex.Message}");

                return StatusCode(500, "Erro do Servidor Interno");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderProductRequest orderProductRequest)
        {
            if (orderProductRequest == null)
            {
                return BadRequest("A solicitação do pedido do produto é nula");
            }

            try
            {
                var orderProductResponses = await _orderProductRepository.PostAsync(orderProductRequest);

                if (orderProductResponses == null || !orderProductResponses.Any())
                {
                    return NotFound();
                }

                var orderProductResponseDTOs = orderProductResponses
                    .SelectMany(opr => opr.Product?.Select(p => new OrderProductResponseDto
                    {
                        Id = p?.Id ?? 0,
                        Name = p?.Name ?? string.Empty,
                        Amount = p?.Amount ?? 0,
                        Details = p?.Details ?? string.Empty,
                        Picture = p?.Picture ?? string.Empty,
                        ValueFor = p?.ValueFor ?? 0m
                    }) ?? Enumerable.Empty<OrderProductResponseDto>())
                    .ToList();

                var jsonOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var jsonResponse = JsonSerializer.Serialize(orderProductResponseDTOs, jsonOptions);

                return Ok(jsonResponse);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Erro durante a solicitação do pedido de produto: {ex.Message}");

                return StatusCode(500, "Erro do Servidor Interno");
            }
        }

        // PUT: api/OrderProducts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(OrderProduct orderProduct)
        {
            if (orderProduct == null)
            {
                return BadRequest("A solicitação do pedido de produto é nula");
            }

            if (orderProduct.Id == 0)
            {
                return BadRequest("A solicitação do id do pedido do produto é zero");
            }

            try
            {
                await _orderProductRepository.PutAsync(orderProduct);

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Erro durante a solicitação para atualizar o pedido de produto: {ex.Message}");

                return StatusCode(500, "Erro do Servidor Interno");
            }
        }

        // DELETE: api/OrderProducts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest("A solicitação do id do perfil é zero");
            }

            try
            {
                var user = await _orderProductRepository.GetByIdAsync(id);

                if (user == null)
                {
                    return NotFound();
                }

                await _orderProductRepository.DeleteAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Erro durante a solicitação para apagar um registro de pedido do produto: {ex.Message}");

                return StatusCode(500, "Erro do Servidor Interno");
            }
        }
    }
}
