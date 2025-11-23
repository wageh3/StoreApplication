using Microsoft.AspNetCore.Mvc;
using Store.G02.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllProducts(int? BrandId, int? TypeId, string? sort, string? search)
        {
           var result = await _serviceManager.productService.GetAllProductsAsync(BrandId, TypeId, sort, search);
            if(result is null) return BadRequest("No products found");
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int? id)
        {
            if(id is null)
                return BadRequest();
            var result = await _serviceManager.productService.GetProductByIdAsync(id.Value);
            if(result is null) return NotFound();
            return Ok(result);
        }
        [HttpGet("brands")]
        public async Task<IActionResult> GetAllBrands()
        {
            var result = await _serviceManager.productService.GetAllBrandsAsync();
            if(result is null) return NotFound();
            return Ok(result);
        } 
        [HttpGet("types")]
        public async Task<IActionResult> GetAllTypes()
        {
            var result = await _serviceManager.productService.GetAllTypesAsync();
            if(result is null) return NotFound();
            return Ok(result);
        }
    }
}
