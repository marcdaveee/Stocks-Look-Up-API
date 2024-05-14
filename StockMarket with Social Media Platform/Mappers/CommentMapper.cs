using StockMarket_with_Social_Media_Platform.Dtos.Comment;
using StockMarket_with_Social_Media_Platform.Models;

namespace StockMarket_with_Social_Media_Platform.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto toCommentDto(this Comment commentModel)
        {            
            return new CommentDto
            {
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreatedOn = commentModel.CreatedOn,
                StockId = commentModel.StockId
            };
        }

        public static Comment toCommentFromCreateDto(this CreateCommentRequestDto createdComment, int stockId)
        {
            return new Comment
            {
                Title = createdComment.Title,
                Content = createdComment.Content,
                StockId = stockId,
            };
        }

        public static Comment toCommentFromUpdateDto(this UpdateCommentRequestDto updatedComment)
        {
            return new Comment
            {
                Title = updatedComment.Title,
                Content = updatedComment.Content,                
            };
        }
    }
}
