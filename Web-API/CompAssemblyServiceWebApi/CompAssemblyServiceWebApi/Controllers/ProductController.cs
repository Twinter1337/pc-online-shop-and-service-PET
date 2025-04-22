using AutoMapper;
using ComputerAssemblyServiceBackEnd.CrudServices.Interfaces;
using ComputerAssemblyServiceBackEnd.CrudServices.Source;
using ComputerAssemblyServiceBackEnd.Enums.Models;
using Microsoft.AspNetCore.Mvc;
using ComputerAssemblyServiceBackEnd.Models;
using ComputerAssemblyServiceBackEnd.Models.Dtos.PatchDtos;
using ComputerAssemblyServiceBackEnd.Models.Dtos.ProductDtos;

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
            try
            {
                var products = await _productCrudService.GetAllEntitiesAsync();
                return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving products: {ex.Message}");
            }
        }

        [HttpGet("by-category/{category}")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByCategory([FromRoute] ProductType category)
        {
            try
            {
                if (_productCrudService is not ProductsCrudService productsService)
                    return StatusCode(500, "Cannot cast to ProductsCrudService");

                var products = await productsService.GetProductsByCategoryAsync(category);
                return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
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

                return Ok(_mapper.Map<ProductDto>(product));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving product: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, ProductUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var validationResult = await ValidateProductIdsAsync(dto.ComponentId, dto.ComputerId);
                if (validationResult != null)
                    return validationResult;

                if (_productCrudService is not ProductsCrudService pcs)
                    return StatusCode(500, "Product service is not available");

                bool result = await pcs.UpdateEntityAsync(id, dto);
                return result ? NoContent() : BadRequest("Update failed due to invalid input or constraint violation.");
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
                bool result = await _productCrudService.PatchEntityAsync(id, dto);
                return result ? NoContent() : BadRequest("Patch failed");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error patching product: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> PostProduct(ProductCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var validationResult = await ValidateProductIdsAsync(dto.ComponentId, dto.ComputerId);
                if (validationResult != null)
                    return validationResult;

                var entity = _mapper.Map<Product>(dto);
                bool result = await _productCrudService.CreateEntityAsync(entity);

                if (!result)
                    return BadRequest("Creation failed");

                return CreatedAtAction(nameof(GetProduct), new { id = entity.Sku }, _mapper.Map<ProductDto>(entity));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating product: {ex.Message}");
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

        private async Task<ActionResult> ValidateProductIdsAsync(int? componentId, int? computerId)
        {
            if (componentId != null && computerId != null)
                return BadRequest("Product can have either ComponentId or ComputerId, not both");

            if (componentId != null)
            {
                var componentService = new ComponentsCrudService(_productCrudService.Context);
                var component = await componentService.GetEntityByIdAsync(componentId.Value);
                if (component == null)
                    return NotFound($"Component with ID {componentId} not found");
            }

            if (computerId != null)
            {
                var computerService = new PrebuildPatternsCrudService(_productCrudService.Context);
                var computer = await computerService.GetEntityByIdAsync(computerId.Value);
                if (computer == null)
                    return NotFound($"Computer with ID {computerId} not found");
            }

            return null;
        }
    }
}