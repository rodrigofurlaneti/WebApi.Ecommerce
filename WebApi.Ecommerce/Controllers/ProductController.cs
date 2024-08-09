using Domain.Ecommerce.Model;
using Microsoft.AspNetCore.Mvc;
using Data.Ecommerce.Interface;

namespace WebApi.Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            var product = await _productRepository.GetAsync();
            return Ok(product);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var user = await _productRepository.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Post([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Product is null");
            }

            // Log para verificar os dados recebidos
            Console.WriteLine($"Nome do Produto: {product.Name}, Quantidade: {product.Amount}, Foto: {product.Picture}");

            await _productRepository.PostAsync(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Product user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            await _productRepository.PutAsync(user);
            return NoContent();
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _productRepository.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            await _productRepository.DeleteAsync(id); // Certifique-se de que esse método exista no repositório
            return NoContent();
        }
    }
}
