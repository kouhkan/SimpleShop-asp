using System.ComponentModel.DataAnnotations;
using TeddyCourseYT.Models;

namespace TeddyCourseYT.Dtos.Comment;

public class CreateCommentRequestDto
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}