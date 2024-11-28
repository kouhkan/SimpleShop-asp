using System.Security.Principal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using TeddyCourseYT.Data;
using TeddyCourseYT.Dtos.Comment;
using TeddyCourseYT.Dtos.Product;
using TeddyCourseYT.Interfaces;
using TeddyCourseYT.Models;

namespace TeddyCourseYT.Repository;

public class CommentRepository : ICommentRepository
{
    private readonly ApplicationDBContext _context;

    public CommentRepository(ApplicationDBContext context)
    {
        _context = context;
    }
    public Task<List<Comment>> GetAllAsync()
    {
        return _context.Comments.Include(c => c.AppUser).ToListAsync();
    }

    public async Task<Comment?> GetByIdAsync(int id)
    {
        return await _context.Comments.Include(c => c.AppUser).FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<Comment> CreateAsync(Comment commentModel)
    {
        await _context.Comments.AddAsync(commentModel);
        await _context.SaveChangesAsync();
        return commentModel;
    }

    public async Task<Comment?> UpdateAsync(int id, UpdateCommentRequestDto commentRequestDto)
    {
        var existingComment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
        if (existingComment is null) return null;

        existingComment.Title = commentRequestDto.Title;
        existingComment.Content = commentRequestDto.Content;

        await _context.SaveChangesAsync();
        return existingComment;
    }

    public async Task<Comment?> DeleteAsync(int id)
    {
        var commentModel = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
        if (commentModel is null) return null;
        _context.Comments.Remove(commentModel);
        await _context.SaveChangesAsync();
        return commentModel;
    }
}