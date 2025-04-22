using AutoMapper;
using ComputerAssemblyServiceBackEnd.CrudServices.Interfaces;
using ComputerAssemblyServiceBackEnd.CrudServices.Source;
using ComputerAssemblyServiceBackEnd.Enums.Models;
using Microsoft.AspNetCore.Mvc;
using ComputerAssemblyServiceBackEnd.Models;
using ComputerAssemblyServiceBackEnd.Models.Dtos;

namespace CompAssemblyServiceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ICrudService<Product> _productCrudService;
        private readonly IMapper _mapper;

        public ProductController(ICrudService<Product> productCrudService, IMapper mapper)
        {
            _productCrudService = productCrudService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            List<Product> products = await _productCrudService.GetAllEntitiesAsync();
            return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
        }

        [HttpGet("by-category/{category}")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByCategory([FromRoute] ProductType category)
        {
            ProductsCrudService productsCrudService = (_productCrudService as ProductsCrudService)!;
            List<Product> products = await productsCrudService.GetProductsByCategoryAsync(category);
            return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _productCrudService.GetEntityByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ProductDto>(product));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, ProductDto productDto)
        {
            if (id != productDto.Sku)
            {
                return BadRequest();
            }

            bool result = await _productCrudService.UpdateEntityAsync(id, _mapper.Map<Product>(productDto));

            if (!result)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> PostProduct(ProductDto productDto)
        {
            try
            {
                var createdProduct = _mapper.Map<Product>(productDto);
                bool result = await _productCrudService.CreateEntityAsync(createdProduct);

                if (!result)
                    return BadRequest();

                return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.Sku },
                    _mapper.Map<ProductDto>(createdProduct));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            bool result = await _productCrudService.DeleteEntityAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}