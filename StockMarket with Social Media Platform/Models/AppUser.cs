using Microsoft.AspNetCore.Identity;

namespace StockMarket_with_Social_Media_Platform.Models
{
    public class AppUser : IdentityUser
    {
        public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
    }
}
