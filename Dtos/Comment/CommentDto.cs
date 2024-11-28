using TeddyCourseYT.Models;

namespace TeddyCourseYT.Dtos.Comment;

public class CommentDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = string.Empty;
}