using System.ComponentModel.DataAnnotations.Schema;

namespace TeddyCourseYT.Models;

[Table("Products")]
public class Product
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    [Column(TypeName = "numeric(18,2)")]
    public decimal Price { get; set; }
    public List<Comment> Comments { get; set; } = new List<Comment>();
    public List<Basket> Baskets { get; set; } = new List<Basket>();

}