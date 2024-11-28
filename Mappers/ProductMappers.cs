using TeddyCourseYT.Dtos.Product;
using TeddyCourseYT.Models;

namespace TeddyCourseYT.Mappers;

public static class ProductMappers
{
    public static ProductDto ToProductDto(this Product productModel)
    {
        return new ProductDto
        {
            Id = productModel.Id,
            Title = productModel.Title,
            Price = productModel.Price,
            Comments = productModel.Comments.Select(s => s.ToCommentDto()).ToList()
            
        };
    }

    public static Product ToProductFromCreateDto(this CreateProductRequestDto productDto)
    {
        return new Product
        {
            Title = productDto.Title,
            Price = productDto.Price
        };
    }
}