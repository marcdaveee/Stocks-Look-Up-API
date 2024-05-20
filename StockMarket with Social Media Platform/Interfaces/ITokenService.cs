using StockMarket_with_Social_Media_Platform.Models;

namespace StockMarket_with_Social_Media_Platform.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
