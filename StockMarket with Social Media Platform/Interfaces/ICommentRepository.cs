using StockMarket_with_Social_Media_Platform.Dtos.Comment;
using StockMarket_with_Social_Media_Platform.Models;

namespace StockMarket_with_Social_Media_Platform.Interfaces
{
    public interface ICommentRepository
    {
        Task <IEnumerable<Comment>> GetAllCommentsAsync();

        Task<Comment?> GetComment(int id);

        Task<Comment> CreateCommentAsync(Comment commentModel);

        Task<Comment?> UpdateCommentAsync(int id, UpdateCommentRequestDto updatedComment);

        Task<Comment?> DeleteCommentAsync(int id);
    }
}
