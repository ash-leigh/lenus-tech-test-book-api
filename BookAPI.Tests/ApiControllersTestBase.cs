namespace BookAPI.Tests
{
    using BooksAPI.Data;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.Data.Sqlite;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System.Threading.Tasks;

    public abstract class ApiControllerTestsBase : WebApplicationFactory<Program>
    {
        private readonly SqliteConnection connection;

        protected ApiControllerTestsBase()
        {
            connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
        }

        public async Task<ApplicationDbContext> CreateDatabase()
        {
            var db = new ApplicationDbContext(DbContextOptions());
            await db.Database.EnsureCreatedAsync();
            return db;
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.UseEnvironment("Integration");
            return base.CreateHost(builder);
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                ConfigureDatabase(services);
            });
        }

        protected void ConfigureDatabase(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options
                    .UseSqlite(connection);
            });

        }
        protected DbContextOptions DbContextOptions()
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(connection)
                .EnableSensitiveDataLogging()
                .Options;
        }
    }
}
