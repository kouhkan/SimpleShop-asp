using System.Security.Principal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using TeddyCourseYT.Data;
using TeddyCourseYT.Dtos.Product;
using TeddyCourseYT.Helpers;
using TeddyCourseYT.Interfaces;
using TeddyCourseYT.Models;

namespace TeddyCourseYT.Repository;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDBContext _context;

    public ProductRepository(ApplicationDBContext context)
    {
        _context = context;
    }
    public async Task<List<Product>> GetAllAsync(QueryObject query)
    {
        // return _context.Products.Include(c => c.Comments).ToListAsync();
        var products = _context.Products.Include(c => c.Comments).ThenInclude(a => a.AppUser).AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.Title))
        {
            products = products.Where(t => t.Title.Contains(query.Title));
        }

        if (query.MinPrice > 0)
        {
            products = products.Where(m => m.Price > query.MinPrice);
        }
        
        if (query.MaxPrice > 0)
        {
            products = products.Where(m => m.Price < query.MaxPrice);
        }

        if (!string.IsNullOrWhiteSpace(query.SortBy))
        {
            if (query.SortBy.Equals("Title", StringComparison.OrdinalIgnoreCase))
            {
                products = query.IsDescending
                    ? products.OrderByDescending(o => o.Title)
                    : products.OrderBy(o => o.Title);
            }
            if (query.SortBy.Equals("Price", StringComparison.OrdinalIgnoreCase))
            {
                products = query.IsDescending
                    ? products.OrderByDescending(o => o.Price)
                    : products.OrderBy(o => o.Price);
            }
        }

        var skipNumber = (query.PageNumber - 1) * query.PageSize;

        return await products.Skip(skipNumber).Take(query.PageSize).ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products.Include(c => c.Comments).ThenInclude(a => a.AppUser).FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<Product> CreateAsync(Product productModel)
    {
        await _context.Products.AddAsync(productModel);
        await _context.SaveChangesAsync();
        return productModel;
    }

    public async Task<Product?> UpdateAsync(int id, UpdateProductRequestDto productRequestDto)
    {
        var existingProduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        if (existingProduct is null) return null;

        existingProduct.Title = productRequestDto.Title;
        existingProduct.Price = productRequestDto.Price;

        await _context.SaveChangesAsync();
        return existingProduct;
    }

    public async Task<Product?> DeleteAsync(int id)
    {
        var productModel = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        if (productModel is null) return null;
        _context.Products.Remove(productModel);
        await _context.SaveChangesAsync();
        return productModel;
    }

    public async Task<bool> ProductExistsAsync(int id)
    {
        return await _context.Products.AnyAsync(p => p.Id == id);
    }
}