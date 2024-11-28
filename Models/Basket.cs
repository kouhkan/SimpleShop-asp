using System.ComponentModel.DataAnnotations.Schema;

namespace TeddyCourseYT.Models;

[Table("Baskets")]
public class Basket
{
    public string AppUserId { get; set; }
    public int ProductId { get; set; }
    public AppUser AppUser { get; set; }
    public Product Product { get; set; }
}