namespace BookAPI.Tests.IntegrationTests
{
    using BooksAPI.Data;
    using BooksAPI.Data.Domain;
    using Microsoft.Extensions.DependencyInjection;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Xunit;
    using Xunit.Abstractions;

    public class BooksControllerTests 
    {
        [Fact]
        public async Task ShouldGetBooks()
        {
            await using var application = new ApiControllerTestBase();
            using (var scope = application.Services.CreateScope())
            {
                var provider = scope.ServiceProvider;
                using (var dbContext = provider.GetRequiredService<ApplicationDbContext>())
                {
                    await dbContext.Database.EnsureCreatedAsync(); 
                    await dbContext.SaveChangesAsync();
                }
            }

            var client = application.CreateClient();
            var response = await client.GetAsync($"/Books");

            response.EnsureSuccessStatusCode();
        }
    

        [Fact]
        public async Task ShouldCreateBook()
        {
            await using var application = new ApiControllerTestBase();

            using (var scope = application.Services.CreateScope())
            {
                var provider = scope.ServiceProvider;
                using (var dbContext = provider.GetRequiredService<ApplicationDbContext>())
                {
                    await dbContext.Database.EnsureCreatedAsync();
                    await dbContext.SaveChangesAsync();
                }
            }

            var client = application.CreateClient();
            var data = new
            {
                Author = "Author",
                Title = "Title",
                Price = 2.20
            };

            var json = JsonSerializer.Serialize(data);
            var request = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"/Books", request);

            response.EnsureSuccessStatusCode();              
        }
    }
}
