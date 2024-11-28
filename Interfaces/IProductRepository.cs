using Microsoft.AspNetCore.Mvc;
using TeddyCourseYT.Dtos.Product;
using TeddyCourseYT.Helpers;
using TeddyCourseYT.Models;

namespace TeddyCourseYT.Interfaces;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync(QueryObject query);
    Task<Product?> GetByIdAsync(int id);
    Task<Product> CreateAsync(Product productModel);
    Task<Product?> UpdateAsync(int id, UpdateProductRequestDto productRequestDto);
    Task<Product?> DeleteAsync(int id);

    Task<bool> ProductExistsAsync(int id);

}