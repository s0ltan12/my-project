using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Data
{

    public class AppDbContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-VO7PIM7\\SQLEXPRESS;Database=ProjectDb;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, FirstName = "Mohamed", LastName = "Soltan", Email = "mohamed@example.com", Password = "123456" },
                new User { UserId = 2, FirstName = "Sarah", LastName = "Ali", Email = "sarah@example.com", Password = "abcdef" }
            );

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Electronics", Description = "Electronic devices" },
                new Category { CategoryId = 2, Name = "Books", Description = "Reading materials" },
                new Category { CategoryId = 3, Name = "Clothing" ,Description = "Fashoin" }

            );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = 1,
                    Title = "Smartphone",
                    Price = 999.99M,
                    Description = "Latest Android smartphone",
                    Quantity = 10,
                    ImagePath = "/images/smartphone.jpg",
                    CategoryId = 1
                },
                new Product
                {
                    ProductId = 2,
                    Title = "C# Programming Book",
                    Price = 49.99M,
                    Description = "Learn C# from scratch",
                    Quantity = 20,
                    ImagePath = "/images/csharpbook.jpg",
                    CategoryId = 2
                }
            );
        }
    }

}
