using System.ComponentModel.DataAnnotations.Schema;

namespace StockMarket_with_Social_Media_Platform.Models
{
    [Table("Portfolio")]
    public class Portfolio
    {
        public int AppUserId { get; set; }

        public int StockId { get; set; }

        public AppUser AppUser { get; set; }

        public Stock Stock { get; set; }
    }
}
