using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeddyCourseYT.Data;
using TeddyCourseYT.Dtos.Comment;
using TeddyCourseYT.Dtos.Product;
using TeddyCourseYT.Extensions;
using TeddyCourseYT.Interfaces;
using TeddyCourseYT.Mappers;
using TeddyCourseYT.Models;

namespace TeddyCourseYT.Controllers;

[Route("api/comments")]
[ApiController]
public class CommentController: ControllerBase
{
    private readonly ApplicationDBContext _context;
    private readonly ICommentRepository _commentRepository;
    private readonly IProductRepository _productRepository;
    private readonly UserManager<AppUser> _userManager;
    
    public CommentController(ApplicationDBContext context, ICommentRepository commentRepository, IProductRepository productRepository, UserManager<AppUser> userManager)
    {
        _context = context;
        _commentRepository = commentRepository;
        _productRepository = productRepository;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var comments = await _commentRepository.GetAllAsync();
        var commentsDto = comments.Select(s => s.ToCommentDto());
        return Ok(commentsDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var comment = await _commentRepository.GetByIdAsync(id);

        if (comment is null) return NotFound();

        return Ok(comment.ToCommentDto());
    }

    [HttpPost("{productId}")]
    [Authorize]
    public async Task<IActionResult> Create([FromRoute] int productId, [FromBody] CreateCommentRequestDto createCommentRequestDto)
    {
        var username = User.GetUsername();
        var appUser = await _userManager.FindByNameAsync(username);
        
        var productModel = await _productRepository.ProductExistsAsync(productId);
        if (!productModel) return NotFound();

        var commentModel = createCommentRequestDto.ToCommentFromRequest(productId);
        commentModel.AppUserId = appUser.Id;
        
        await _commentRepository.CreateAsync(commentModel);
        return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());
    }
    
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto updateCommentRequestDto)
    {
        var comment = await _commentRepository.UpdateAsync(id, updateCommentRequestDto);
        if (comment is null) return NotFound();
        return Ok(comment.ToCommentDto());
    }
    
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var comment = await _commentRepository.DeleteAsync(id);
        if (comment is null) return NotFound();
        return NoContent();
    }
}