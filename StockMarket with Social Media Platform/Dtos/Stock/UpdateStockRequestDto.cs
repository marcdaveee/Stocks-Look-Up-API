﻿using System.ComponentModel.DataAnnotations.Schema;

namespace StockMarket_with_Social_Media_Platform.Dtos.Stock
{
    public class UpdateStockRequestDto
    {
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18, 2")]
        public decimal Purchase { get; set; }

        [Column(TypeName = "decimal(18, 2")]
        public decimal LastDiv { get; set; }
        public string Industry { get; set; } = string.Empty;
        public long MarketCap { get; set; }
    }
}
