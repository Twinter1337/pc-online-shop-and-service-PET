using AutoMapper;
using ComputerAssemblyServiceBackEnd.CrudServices.Interfaces;
using ComputerAssemblyServiceBackEnd.CrudServices.Source;
using ComputerAssemblyServiceBackEnd.Enums.Models;
using ComputerAssemblyServiceBackEnd.Models;
using ComputerAssemblyServiceBackEnd.Models.Dtos.PatchDtos;
using ComputerAssemblyServiceBackEnd.Models.Dtos.PrebuildPatternDtos;
using ComputerAssemblyServiceBackEnd.Models.Dtos.ProductDtos;
using Microsoft.AspNetCore.Mvc;

namespace CompAssemblyServiceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ICrudService<Product> _productCrudService;
        private readonly IMapper _mapper;
        private readonly PrebuildPatternsCrudService _ppcs;

        public ProductController(ICrudService<Product> productCrudService, IMapper mapper)
        {
            _productCrudService = productCrudService;
            _mapper = mapper;
            _ppcs = new PrebuildPatternsCrudService(_productCrudService.Context);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            try
            {
                var products = await _productCrudService.GetAllEntitiesAsync();
                return Ok(BuildProductDtos(products));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving products: {ex.Message}");
            }
        }

        [HttpGet("by-category/{category}")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByCategory(ProductType category)
        {
            try
            {
                if (_productCrudService is not ProductsCrudService productsService)
                    return StatusCode(500, "Cannot cast to ProductsCrudService");

                var products = await productsService.GetProductsByCategoryAsync(category);
                return Ok(BuildProductDtos(products));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error filtering products: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            try
            {
                var product = await _productCrudService.GetEntityByIdAsync(id);
                if (product == null)
                    return NotFound();

                var dto = BuildProductDtos(new List<Product> { product }).First();
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving product: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> PostProduct([FromBody] ProductCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var entity = _mapper.Map<Product>(dto);
                var success = await _productCrudService.CreateEntityAsync(entity);

                if (!success)
                    return BadRequest("Creation failed");

                var resultDto = BuildProductDtos(new List<Product> { entity }).First();
                return CreatedAtAction(nameof(GetProduct), new { id = entity.Sku }, resultDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating product: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, ProductUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _productCrudService.UpdateEntityAsync(id, dto);
                return result ? NoContent() : BadRequest("Update failed");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating product: {ex.Message}");
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchProduct(int id, ProductPatchDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _productCrudService.PatchEntityAsync(id, dto);
                return result ? NoContent() : BadRequest("Patch failed");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error patching product: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var result = await _productCrudService.DeleteEntityAsync(id);
                return result ? NoContent() : NotFound($"Product with ID {id} not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting product: {ex.Message}");
            }
        }

        private List<ProductDto> BuildProductDtos(IEnumerable<Product> products)
        {
            var productDtos = _mapper.Map<List<ProductDto>>(products.ToList());
            foreach (var (product, dto) in products.Zip(productDtos, (p, d) => (p, d)))
            {
                if (product.Computer != null)
                {
                    dto.PrebuildPattern = _mapper.Map<PrebuildPatternDto>(product.Computer);
                    dto.PrebuildPattern.Components = _ppcs.GetComponentsForPattern(product.Computer, _mapper);
                }
            }
            return productDtos;
        }
    }
}