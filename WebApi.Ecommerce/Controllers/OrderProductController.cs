using Domain.Ecommerce.Model;
using Microsoft.AspNetCore.Mvc;
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

        // GET: api/OrderProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderProduct>> GetOrderProduct(int id)
        {
            var user = await _orderProductRepository.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // POST: api/OrderProducts
        [HttpPost]
        public async Task<ActionResult<OrderProduct>> PostOrderProduct(OrderProduct user)
        {
            await _orderProductRepository.PostAsync(user);
            return CreatedAtAction(nameof(GetOrderProduct), new { id = user.Id }, user);
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
