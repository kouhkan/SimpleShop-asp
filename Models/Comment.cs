using System.ComponentModel.DataAnnotations.Schema;

namespace TeddyCourseYT.Models;

[Table("Comments")]
public class Comment
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public int? ProductId { get; set; }
    // Navigation property
    public Product? Product { get; set; }

    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }
    
}