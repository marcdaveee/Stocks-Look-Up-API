﻿using Microsoft.EntityFrameworkCore;
using StockMarket_with_Social_Media_Platform.Models;

namespace StockMarket_with_Social_Media_Platform.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        { 
            
        }

        public DbSet<Stock>  Stocks{ get; set; }

        public DbSet<Comment> Comments { get; set; }

    }
}