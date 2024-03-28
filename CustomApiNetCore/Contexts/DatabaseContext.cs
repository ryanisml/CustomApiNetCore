using Bogus;
using CustomApiNetCore.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomApiNetCore.Contexts
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() { }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CategoryModel>().HasMany(x => x.products).WithOne(x => x.category).HasForeignKey(x => x.category_id);
            var ids = 1;
            var categoryProduct = new Faker<CategoryModel>().RuleFor(x => x.id, f => ids++)
                .RuleFor(x => x.name, f => f.Commerce.Categories(1)[0])
                .RuleFor(x => x.description, f => f.Commerce.ProductDescription())
                .RuleFor(x => x.created_at, f => f.Date.Past())
                .RuleFor(x => x.created_by, f => f.Person.FullName);

            modelBuilder.Entity<CategoryModel>().HasData(categoryProduct.GenerateBetween(100, 100));
            
            var product = new Faker<ProductModel>().RuleFor(x => x.id, f => ids ++)
                .RuleFor(p => p.name, f => f.Commerce.ProductName())
                .RuleFor(p => p.description, f => f.Commerce.ProductDescription())
                .RuleFor(p => p.price, f => f.Random.Float(1000, 100000))
                .RuleFor(p => p.stock, f => f.Random.Int(1, 100))
                .RuleFor(p => p.created_at, f => f.Date.Past())
                .RuleFor(p => p.created_by, f => f.Person.FullName)
                .RuleFor(p => p.image_path, f => f.Image.PicsumUrl())
                .RuleFor(p => p.is_deleted, f => f.Random.Int(0, 1))
                .RuleFor(p => p.category_id, f => f.Random.Long(1, 100));

            var logs = new Faker<LogModel>().RuleFor(x => x.log_id, f => ids++)
                .RuleFor(x => x.created_by, f => f.Person.FullName)
                .RuleFor(x => x.created_at, f => f.Date.Past())
                .RuleFor(x => x.action, f => f.Lorem.Sentence())
                .RuleFor(x => x.description, f => f.Lorem.Sentence());

            modelBuilder.Entity<ProductModel>().HasData(product.GenerateBetween(1000, 1000));
            modelBuilder.Entity<LogModel>().HasData(logs.GenerateBetween(1000, 1000));
        }

        public virtual DbSet<ProductModel> Product { get; set; } = null!;
        public virtual DbSet<CategoryModel> Category { get; set; } = null!;
        public virtual DbSet<LogModel> Log { get; set; } = null!;
    }
}
