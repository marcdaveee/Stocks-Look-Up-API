using System.ComponentModel.DataAnnotations;

namespace StockMarket_with_Social_Media_Platform.Dtos.Account
{
    public class LoginDto
    {
        [Required]
        public string? UserName { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
