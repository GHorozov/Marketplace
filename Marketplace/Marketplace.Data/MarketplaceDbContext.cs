using Marketplace.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Data
{
    public class MarketplaceDbContext : IdentityDbContext<MarketplaceUser, IdentityRole, string>
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Color> Colors { get; set; }

        public DbSet<Picture> Pictures { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        public DbSet<WishProduct> WishProducts { get; set; }

        public DbSet<ProductOrder> ProductOrder { get; set; }

        public DbSet<CategoryProduct> CategoryProduct { get; set; }

        public DbSet<ShoppingCartProduct> ShoppingCartProduct { get; set; }

        public MarketplaceDbContext(DbContextOptions<MarketplaceDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<MarketplaceUser>()
                .HasMany(x => x.Products)
                .WithOne(x => x.MarketplaceUser)
                .HasForeignKey(x => x.MarketplaceUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<MarketplaceUser>()
                .HasMany(x => x.Orders)
                .WithOne(x => x.MarketplaceUser)
                .HasForeignKey(x => x.MarketplaceUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
               .Entity<MarketplaceUser>()
               .HasMany(x => x.WishProducts)
               .WithOne(x => x.MarketplaceUser)
               .HasForeignKey(x => x.MarketplaceUserId)
               .OnDelete(DeleteBehavior.Restrict);

            builder
               .Entity<MarketplaceUser>()
               .HasMany(x => x.Messages)
               .WithOne(x => x.MarketplaceUser)
               .HasForeignKey(x => x.MarketplaceUserId)
               .OnDelete(DeleteBehavior.Restrict);

            builder
              .Entity<MarketplaceUser>()
              .HasMany(x => x.Comments)
              .WithOne(x => x.MarketplaceUser)
              .HasForeignKey(x => x.MarketplaceUserId)
              .OnDelete(DeleteBehavior.Restrict);

            builder
              .Entity<Product>()
              .HasOne(x => x.Color)
              .WithOne()
              .OnDelete(DeleteBehavior.Restrict);

            builder
              .Entity<Product>()
              .HasMany(x => x.Pictures)
              .WithOne(x => x.Product)
              .HasForeignKey(x => x.ProductId)
              .OnDelete(DeleteBehavior.Restrict);

            builder
              .Entity<Product>()
              .HasMany(x => x.Comments)
              .WithOne(x => x.Product)
              .HasForeignKey(x => x.ProductId)
              .OnDelete(DeleteBehavior.Restrict);

            builder
              .Entity<Product>()
              .HasMany(x => x.Ratings)
              .WithOne(x => x.Product)
              .HasForeignKey(x => x.ProductId)
              .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<ProductOrder>()
                .HasKey(x => new { x.ProductId, x.OrderId });

            builder
                .Entity<Product>()
                .HasMany(x => x.Orders)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Order>()
                .HasMany(x => x.Products)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
               .Entity<CategoryProduct>()
               .HasKey(x => new { x.CategoryId, x.ProductId });

            builder
               .Entity<Category>()
               .HasMany(x => x.Products)
               .WithOne(x => x.Category)
               .HasForeignKey(x => x.CategoryId)
               .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Product>()
                .HasMany(x => x.Categories)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
               .Entity<ShoppingCartProduct>()
               .HasKey(x => new { x.ShoppingCartId, x.ProductId });

            builder
               .Entity<ShoppingCart>()
               .HasMany(x => x.Products)
               .WithOne(x => x.ShoppingCart)
               .HasForeignKey(x => x.ShoppingCartId)
               .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Product>()
                .HasMany(x => x.ShoppingCarts)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<ShoppingCart>()
                .HasOne(x => x.User)
                .WithOne(x => x.ShoppingCart)
                .HasForeignKey<MarketplaceUser>(x => x.ShoppingCartId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
