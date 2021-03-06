// <auto-generated />
using System;
using JustTradeIt.Software.API.Repositories.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JustTradeIt.Software.API.Migrations
{
    [DbContext(typeof(TradeDbContext))]
    partial class TradeDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.11");

            modelBuilder.Entity("JustTradeIt.Software.API.Models.Entities.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<int>("ItemConditionId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("OwnerId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PublicIdentifier")
                        .HasColumnType("TEXT");

                    b.Property<string>("ShortDescription")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("JustTradeIt.Software.API.Models.Entities.ItemCondition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConditionCode")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ItemConditions");
                });

            modelBuilder.Entity("JustTradeIt.Software.API.Models.Entities.ItemImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("TEXT");

                    b.Property<int>("ItemId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("ItemImages");
                });

            modelBuilder.Entity("JustTradeIt.Software.API.Models.Entities.JwtToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("blacklisted")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("JwtTokens");
                });

            modelBuilder.Entity("JustTradeIt.Software.API.Models.Entities.Trade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("IssueDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ModifiedByDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("PublicIdentifier")
                        .HasColumnType("TEXT");

                    b.Property<int>("ReceiverId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SenderId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TradeStatus")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Trades");
                });

            modelBuilder.Entity("JustTradeIt.Software.API.Models.Entities.TradeItem", b =>
                {
                    b.Property<int>("ItemId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TradeId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ItemId", "TradeId", "UserId");

                    b.ToTable("TradeItems");
                });

            modelBuilder.Entity("JustTradeIt.Software.API.Models.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("FullName")
                        .HasColumnType("TEXT");

                    b.Property<string>("HashedPassword")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProfileImageUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("PublicIdentifier")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
