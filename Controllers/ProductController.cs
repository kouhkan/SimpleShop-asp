using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeddyCourseYT.Data;
using TeddyCourseYT.Dtos.Product;
using TeddyCourseYT.Helpers;
using TeddyCourseYT.Interfaces;
using TeddyCourseYT.Mappers;

namespace TeddyCourseYT.Controllers;

[Route("api/products")]
[ApiController]
public class ProductController: ControllerBase
{
    private readonly ApplicationDBContext _context;
    private readonly IProductRepository _productRepository;
    
    public ProductController(ApplicationDBContext context, IProductRepository productRepository)
    {
        _context = context;
        _productRepository = productRepository;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
    {
        var products = await _productRepository.GetAllAsync(query);
        var productsDto = products.Select(s => s.ToProductDto()).ToList();
        return Ok(productsDto);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product is null) return NotFound();

        return Ok(product.ToProductDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductRequestDto createProductRequestDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var productModel = createProductRequestDto.ToProductFromCreateDto();
        await _productRepository.CreateAsync(productModel);
        return CreatedAtAction(nameof(GetById), new { id = productModel.Id }, productModel.ToProductDto());
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateProductRequestDto updateProductRequestDto)
    {
        var product = await _productRepository.UpdateAsync(id, updateProductRequestDto);
        if (product is null) return NotFound();
        return Ok(product.ToProductDto());
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var product = await _productRepository.DeleteAsync(id);
        if (product is null) return NotFound();
        return NoContent();
    }
}