using TeddyCourseYT.Models;

namespace TeddyCourseYT.Interfaces;

public interface IBasketRepository
{
    Task<List<Product>> GetUserBasket(AppUser user);
    Task<Basket> CreateAsync(Basket basket);
    Task<Basket> DeleteBasketAsync(AppUser appUser, int ProductId);
}