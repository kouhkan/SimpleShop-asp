using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TeddyCourseYT.Dtos.Comment;
using TeddyCourseYT.Extensions;
using TeddyCourseYT.Interfaces;
using TeddyCourseYT.Models;

namespace TeddyCourseYT.Controllers;

[Route("api/baskets")]
[ApiController]
public class BasketController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    private readonly UserManager<AppUser> _userManager;
    private readonly IBasketRepository _basketRepository;
    
    public BasketController(UserManager<AppUser> userManager, IProductRepository productRepository, IBasketRepository basketRepository)
    {
        _userManager = userManager;
        _basketRepository = basketRepository;
        _productRepository = productRepository;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUserBasket()
    {
        var username = User.GetUsername();
        var appUser = await _userManager.FindByNameAsync(username);
        var userBasket = await _basketRepository.GetUserBasket(appUser);
        return Ok(userBasket);

    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateBasket(int ProductId)
    {
        var username = User.GetUsername();
        var appUser = await _userManager.FindByNameAsync(username);
        var product = await _productRepository.GetByIdAsync(ProductId);

        if (product is null) return NotFound();

        var userBasket = await _basketRepository.GetUserBasket(appUser);

        if (userBasket.Any(e => e.Id == ProductId)) return BadRequest("Can not add same product");

        var basketModel = new Basket
        {
            ProductId = product.Id,
            AppUserId = appUser.Id
        };
        if (basketModel is null) return BadRequest();
        
        await _basketRepository.CreateAsync(basketModel);

        return Created();
    }

    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeleteBasket(int ProductId)
    {
        var username = User.GetUsername();
        var appUser = await _userManager.FindByNameAsync(username);

        var userBasket = await _basketRepository.GetUserBasket(appUser);

        var filterBasket = userBasket.Where(p => p.Id == ProductId);
        if (filterBasket.Count() == 1)
        {
            await _basketRepository.DeleteBasketAsync(appUser, ProductId);
        }
        else
        {
            return BadRequest();
        }

        return Ok();
    }
}