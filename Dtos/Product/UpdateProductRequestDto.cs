using System.ComponentModel.DataAnnotations;
using TeddyCourseYT.Models;

namespace TeddyCourseYT.Dtos.Product;

public class UpdateProductRequestDto
{
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
}