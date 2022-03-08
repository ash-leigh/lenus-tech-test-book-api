namespace BookAPI.Tests.IntegrationTests
{
    using BooksAPI.Data.Domain;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Xunit;
    using Xunit.Abstractions;

    public class BooksControllerTests : ApiControllerTestsBase
    {
        public BooksControllerTests()
        {
        }

        [Fact]
        public async Task ShouldCreateBook()
        {
            await using var dbContext = await CreateDatabase();
            var books = dbContext.Set<Book>().FirstOrDefault();
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
