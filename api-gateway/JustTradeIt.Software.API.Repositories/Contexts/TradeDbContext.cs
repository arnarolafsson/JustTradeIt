using JustTradeIt.Software.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace JustTradeIt.Software.API.Repositories.Contexts
{
    public class TradeDbContext : DbContext
    {
        public TradeDbContext(DbContextOptions<TradeDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TradeItem>().HasKey(k => new
            {
                k.ItemId, k.TradeId, k.UserId
            });
        }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemCondition> ItemConditions { get; set; }
        public DbSet<ItemImage> ItemImages { get; set; }
        public DbSet<JwtToken> JwtTokens { get; set; }
        public DbSet<Trade> Trades { get; set; }
        public DbSet<TradeItem> TradeItems { get; set; }
        public DbSet<User> Users { get; set; }
    }
}