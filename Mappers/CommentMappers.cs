using TeddyCourseYT.Dtos.Comment;
using TeddyCourseYT.Models;

namespace TeddyCourseYT.Mappers;

public static class CommentMappers
{
    public static CommentDto ToCommentDto(this Comment commentModel)
    {
        return new CommentDto
        {
            Id = commentModel.Id,
            Title = commentModel.Title,
            Content = commentModel.Content,
            CreatedAt = commentModel.CreatedAt,
            CreatedBy = commentModel.AppUser.UserName
        };
    }

    public static Comment ToCommentFromRequest(this CreateCommentRequestDto commentDto, int productId)
    {
        return new Comment
        {
            Title = commentDto.Title,
            Content = commentDto.Content,
            ProductId = productId
        };
    }
}