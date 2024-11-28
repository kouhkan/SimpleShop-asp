using Microsoft.EntityFrameworkCore;
using TeddyCourseYT.Data;
using TeddyCourseYT.Interfaces;
using TeddyCourseYT.Models;

namespace TeddyCourseYT.Repository;

public class BasketRepository : IBasketRepository
{
    private readonly ApplicationDBContext _context;
    
    public BasketRepository(ApplicationDBContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetUserBasket(AppUser user)
    {
        return await _context.Baskets.Where(u => u.AppUserId == user.Id)
            .Select(product => new Product
            {
                Id = product.ProductId,
                Title = product.Product.Title,
                Price = product.Product.Price
            }).ToListAsync();
    }

    public async Task<Basket> CreateAsync(Basket basket)
    {
        await _context.Baskets.AddAsync(basket);
        await _context.SaveChangesAsync();
        return basket;
    }

    public async Task<Basket> DeleteBasketAsync(AppUser appUser, int ProductId)
    {
        var basketModel =
            await _context.Baskets.FirstOrDefaultAsync(f => f.AppUserId == appUser.Id && f.ProductId == ProductId);
        if (basketModel is null) return null;

        _context.Baskets.Remove(basketModel);
        await _context.SaveChangesAsync();
        return basketModel;
    }
}