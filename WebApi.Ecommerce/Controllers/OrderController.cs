using Domain.Ecommerce.Model;
using Microsoft.AspNetCore.Mvc;
using Data.Ecommerce.Interface;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApi.Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        // GET: api/Order
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> Get()
        {
            var orders = await _orderRepository.GetAsync();

            return Ok(orders);
        }

        // GET: api/Order/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order?>> Get(int id)
        {
            if (id == 0)
            {
                return BadRequest("A solicitação do pedido ID é zero");
            }

            try
            {
                var product = await _orderRepository.GetProductCountByOrderIdAsync(id);

                if (product == null)
                {
                    return NotFound();
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Erro durante a solicitação do pedido: {ex.Message}");

                return StatusCode(500, "Erro do Servidor Interno");
            }
        }

        // POST: api/Order
        [HttpPost]
        public async Task<ActionResult<Order?>> Post(OrderRequest orderRequest)
        {
            if (orderRequest == null)
            {
                return BadRequest("A solicitação do pedido é nula");
            }

            try
            {
                var order = new Order()
                {
                    Id = orderRequest.orderId,
                    User = new User() { Id = orderRequest.userId },
                    Product = new Product() { Id = orderRequest.productId, Amount = orderRequest.amount }
                };

                var result = await _orderRepository.PostAsync(order);

                if (result != null && int.TryParse(result.ToString(), out int orderId))
                {
                    order.Id = orderId;
                }

                return CreatedAtAction(nameof(Get), new { id = order.Id }, order);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Erro durante a autenticação: {ex.Message}");

                return StatusCode(500, "Erro do Servidor Interno");
            }
        }

        // PUT: api/Order/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Order order)
        {
            if (order == null)
            {
                return BadRequest("A solicitação do pedido é nula");
            }

            if (order.Id == 0)
            {
                return BadRequest("A solicitação do id do pedido é zero");
            }

            try
            {
                await _orderRepository.PutAsync(order);

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Erro durante a solicitação: {ex.Message}");

                return StatusCode(500, "Erro do Servidor Interno");
            }
        }

        // DELETE: api/Order/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest("A solicitação do id do pedido é zero");
            }

            try
            {
                var order = await _orderRepository.GetByIdAsync(id);

                if (order == null)
                {
                    return NotFound();
                }

                await _orderRepository.DeleteAsync(id);

                return NoContent();

            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Erro durante a solicitação para apagar o registro do pedido: {ex.Message}");

                return StatusCode(500, "Erro do Servidor Interno");
            }
        }
    }
}
