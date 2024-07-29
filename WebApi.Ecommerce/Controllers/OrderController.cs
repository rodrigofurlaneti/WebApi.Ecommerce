using Domain.Ecommerce.Model;
using Microsoft.AspNetCore.Mvc;
using WebApi.Ecommerce.Data.Interface;

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

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            var order = await _orderRepository.GetAsync();
            return Ok(order);
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var user = await _orderRepository.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // POST: api/Orders
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order user)
        {
            await _orderRepository.PostAsync(user);
            return CreatedAtAction(nameof(GetOrder), new { id = user.Id }, user);
        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            await _orderRepository.PutAsync(user);
            return NoContent();
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var user = await _orderRepository.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            await _orderRepository.DeleteAsync(id); // Certifique-se de que esse método exista no repositório
            return NoContent();
        }
    }
}
