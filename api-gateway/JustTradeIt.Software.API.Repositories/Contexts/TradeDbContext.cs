using System.Threading;
using JustTradeIt.Software.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace JustTradeIt.Software.API.Repositories.Contexts
{
    public class TradeDbContext : DbContext
    {
        public TradeDbContext(DbContextOptions<TradeDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Item>().Property<bool>("isDeleted");
            modelBuilder.Entity<Item>().HasQueryFilter(m => EF.Property<bool>(m, "isDeleted") == false);
            modelBuilder.Entity<TradeItem>().HasKey(k => new
            {
                k.ItemId, k.TradeId, k.UserId
            });
        }
        
        /*
         * Borrowed from: https://spin.atomicobject.com/2019/01/29/entity-framework-core-soft-delete/
         * To implement soft delete
         */
        public override int SaveChanges()
        {
            UpdateSoftDeleteStatuses();
            return base.SaveChanges();
        }
        private void UpdateSoftDeleteStatuses()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity.Equals(Items))
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.CurrentValues["isDeleted"] = false;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Modified;
                            entry.CurrentValues["isDeleted"] = true;
                            break;
                    }
                }
                
            }
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