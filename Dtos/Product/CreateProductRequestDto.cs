using System.ComponentModel.DataAnnotations;
using TeddyCourseYT.Models;

namespace TeddyCourseYT.Dtos.Product;

public class CreateProductRequestDto
{
    [Required]
    [MinLength(5, ErrorMessage = "5 chars at least")]
    [MaxLength(100, ErrorMessage = "100 chars is maximum")]
    public string Title { get; set; } = string.Empty;
    [Required]
    [Range(1, 10000D)]
    public decimal Price { get; set; }
}