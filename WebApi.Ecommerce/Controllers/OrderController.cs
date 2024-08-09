using Domain.Ecommerce.Model;
using Microsoft.AspNetCore.Mvc;
using Data.Ecommerce.Interface;

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
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            var orders = await _orderRepository.GetAsync();
            return Ok(orders);
        }

        // GET: api/Order/5
        [HttpGet("{id}")]
        public async Task<ActionResult<int?>> Get(int id)
        {
            var countProduct = await _orderRepository.GetProductCountByOrderIdAsync(id);

            if (countProduct == null)
            {
                return NotFound();
            }

            return Ok(countProduct);
        }

        // POST: api/Order
        [HttpPost]
        public async Task<ActionResult<Order?>> Post(OrderRequest orderRequest)
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

        // PUT: api/Order/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            await _orderRepository.PutAsync(order);
            return NoContent();
        }

        // DELETE: api/Order/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            await _orderRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
