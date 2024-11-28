using Microsoft.AspNetCore.Mvc;
using TeddyCourseYT.Dtos.Comment;
using TeddyCourseYT.Dtos.Product;
using TeddyCourseYT.Models;

namespace TeddyCourseYT.Interfaces;

public interface ICommentRepository
{
    Task<List<Comment>> GetAllAsync();
    Task<Comment?> GetByIdAsync(int id);
    Task<Comment> CreateAsync(Comment commentModel);
    Task<Comment?> UpdateAsync(int id, UpdateCommentRequestDto commentRequestDto);
    Task<Comment?> DeleteAsync(int id);

}