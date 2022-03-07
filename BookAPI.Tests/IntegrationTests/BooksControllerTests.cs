namespace BookAPI.Tests.IntegrationTests
{
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.Data.Sqlite;
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Xunit;
    public class BooksControllerTests : WebApplicationFactory<Program>
    {
        private readonly SqliteConnection connection;

        [Fact]
        public async Task ShouldCreateBook()
        {
            var client = CreateClient();
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
