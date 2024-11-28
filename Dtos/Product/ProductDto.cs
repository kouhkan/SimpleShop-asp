using TeddyCourseYT.Dtos.Comment;
using TeddyCourseYT.Models;

namespace TeddyCourseYT.Dtos.Product;

public class ProductDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    
    public List<CommentDto> Comments { get; set; }
}