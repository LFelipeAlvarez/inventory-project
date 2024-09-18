using Business;
using DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {

        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Product> products = await _productService.GetAllProducts();
            return StatusCode(StatusCodes.Status200OK, products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            try
            {
                var product = await _productService.GetProductById(id);
                return StatusCode(StatusCodes.Status200OK, product);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            Product newProduct = await _productService.CreateProduct(product);
            return StatusCode(StatusCodes.Status201Created, newProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Product product)
        {

            product.Id = id;
            Product updatedProduct = await _productService.UpdateProduct(product);
            return StatusCode(StatusCodes.Status200OK, updatedProduct);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productService.DeleteProduct(id);

            if (result)
            {
                return NoContent(); 
            }
            else
            {
                return NotFound();
            }
        }

    }
}
