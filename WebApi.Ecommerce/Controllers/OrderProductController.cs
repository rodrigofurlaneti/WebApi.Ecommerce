using Domain.Ecommerce.Model;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebApi.Ecommerce.Data.Interface;

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
        public async Task<ActionResult<IEnumerable<OrderProduct>>> GetOrderProducts()
        {
            var orderProduct = await _orderProductRepository.GetAsync();
            return Ok(orderProduct);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderProductResponse>> GetOrderProduct(int id)
        {
            var orderProductResponse = await _orderProductRepository.GetByIdAsync(id);

            if (orderProductResponse == null)
            {
                return NotFound();
            }

            return Ok(orderProductResponse);
        }

        [HttpPost]
        public async Task<IActionResult> PostOrderProduct([FromBody] OrderProductRequest orderProductRequest)
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

        // PUT: api/OrderProducts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderProduct(int id, OrderProduct user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            await _orderProductRepository.PutAsync(user);
            return NoContent();
        }

        // DELETE: api/OrderProducts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderProduct(int id)
        {
            var user = await _orderProductRepository.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            await _orderProductRepository.DeleteAsync(id); // Certifique-se de que esse método exista no repositório
            return NoContent();
        }
    }
}
