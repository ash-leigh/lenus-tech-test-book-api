namespace BooksAPI.Data
{
    using BooksAPI.Data.Domain;
    using Microsoft.Data.Sqlite;
    using Microsoft.EntityFrameworkCore;
    public class ApplicationDbContext : DbContext
    {
        private readonly object[] BookSeedData = new[]
        {
            new {Id = 1, Author = "A. A. Milne", Title = "Winnie-the-Pooh", Price = 19.25 },
            new {Id = 2, Author = "Jane Austen", Title = "Pride and Prejudice", Price = 5.49 },
            new {Id = 3, Author = "William Shakespeare", Title = "Romeo and Juliet", Price = 6.95 }
        };

        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(new SqliteConnection("DataSource=:memory:"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Book>().HasData(BookSeedData);
        }
    }
}
