using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TechLibrary.Domain;

namespace TechLibrary.Data
{
    public class BooksContext : DbContext
    {
        ILogger _logger;
        string _connectionString = "Data Source=techLibrary.db";

        public BooksContext(ILogger<BooksContext> logger)
        {
            _logger = logger;
        }

        public BooksContext(ILogger<BooksContext> logger, string connectionString)
        {
            _logger = logger;
            _connectionString = connectionString;
        }

        public BooksContext(ILogger<BooksContext> logger, DbContextOptions<BooksContext> options) : base(options)
        {
            _logger = logger;
            // NoOp
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(_connectionString);
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Book>().ToTable("Books");
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.BookId);
                //entity.Property(e => e.BookId).HasColumnName("BookId");
                entity.Property(e => e.PublishedDate).HasColumnName("PublishedDate");

                entity.Property(e => e.Title).IsRequired();


            });

            modelBuilder.Entity<Category>().ToTable("Categories");
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.CategoryName).IsRequired();

            });


            modelBuilder.Entity<BookCategory>().ToTable("BookCategories");
            modelBuilder.Entity<BookCategory>(entity =>
            {
                entity.HasKey(e => new { e.CategoryId, e.BookId });

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.BookCategories)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.BookCategories)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade);


            });

            modelBuilder.Entity<Book>().HasQueryFilter(t => t.Deleted == 0);
            modelBuilder.Entity<Category>().HasQueryFilter(t => t.Deleted == 0);

            base.OnModelCreating(modelBuilder);
        }
    }
}
