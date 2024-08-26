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
        public async Task<ActionResult<Product>> Get(int id)
        {
            if (id == 0)
            {
                return BadRequest("A solicitação do pedido ID é zero");
            }

            try
            {
                var product = await _productRepository.GetByIdAsync(id);

                if (product == null)
                {
                    return NotFound("Erro durante a solicitação do produto, não existe este registro na base dados ID: " + id);
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Erro durante a solicitação do produto: {ex.Message}");

                return StatusCode(500, "Erro do Servidor Interno");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Post([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("A solicitação do produto é nula");
            }

            try
            {
                Console.WriteLine($"Nome do Produto: {product.Name}, Quantidade: {product.Amount}, Foto: {product.Picture}");

                await _productRepository.PostAsync(product);

                return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Erro durante a solicitação para salvar o registro de um novo produto: {ex.Message}");

                return StatusCode(500, "Erro do Servidor Interno");
            }
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Product product)
        {
            if (product == null)
            {
                return BadRequest("A solicitação do produto é nula");
            }

            if (product.Id == 0)
            {
                return BadRequest("A solicitação do id do produto é zero");
            }

            try
            {
                await _productRepository.PutAsync(product);

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Erro durante a solicitação para atualizar um registro do produto: {ex.Message}");

                return StatusCode(500, "Erro do Servidor Interno");
            }
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest("A solicitação do id do produto é zero");
            }

            try
            {
                var user = await _productRepository.GetByIdAsync(id);

                if (user == null)
                {
                    return NotFound();
                }

                await _productRepository.DeleteAsync(id); 

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Erro durante a solicitação, para apagar um registro do produto ID: {id} - {ex.Message}");

                return StatusCode(500, "Erro do Servidor Interno");
            }
        }
    }
}
