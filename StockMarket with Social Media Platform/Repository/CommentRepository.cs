using Microsoft.EntityFrameworkCore;
using StockMarket_with_Social_Media_Platform.Data;
using StockMarket_with_Social_Media_Platform.Interfaces;
using StockMarket_with_Social_Media_Platform.Models;

namespace StockMarket_with_Social_Media_Platform.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Comment>> GetAllCommentsAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment?> GetComment(int id)
        {
            var commentModel = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);

            if(commentModel == null)
            {
                return null;
            }

            return commentModel;
        }

        public async Task<Comment> CreateCommentAsync( Comment commentModel)
        {
            await _context.Comments.AddAsync(commentModel);
            await _context.SaveChangesAsync();

            return commentModel;
        } 
    }
}
