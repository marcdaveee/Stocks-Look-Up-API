using StockMarket_with_Social_Media_Platform.Models;

namespace StockMarket_with_Social_Media_Platform.Interfaces
{
    public interface ICommentRepository
    {
        Task <IEnumerable<Comment>> GetAllCommentsAsync();
    }
}
