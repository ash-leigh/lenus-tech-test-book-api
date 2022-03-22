namespace BookAPI.Tests.IntegrationTests
{
    using BooksAPI.Data;
    using BooksAPI.Data.Domain;
    using FluentAssertions;
    using Microsoft.Extensions.DependencyInjection;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Xunit;

    public class BooksControllerTests 
    {
        [Fact]
        public async Task CreateBook_Success_ShouldCreateBook_And_Return201()
        {
            await using var application = new ApiControllerTestBase();

            using (var scope = application.Services.CreateScope())
            {
                var provider = scope.ServiceProvider;
                using (var dbContext = provider.GetRequiredService<ApplicationDbContext>())
                {
                    await dbContext.Database.EnsureCreatedAsync();
                    await dbContext.SaveChangesAsync();

                    var client = application.CreateClient();
                    var data = new
                    {
                        Author = "Author",
                        Title = "Title",
                        Price = 2.20
                    };

                    var json = JsonSerializer.Serialize(data);

                    var existingBookCount = dbContext.Set<Book>().Count();
                    var lastId = dbContext.Set<Book>().Max(x => x.Id);

                    var request = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync($"/Books", request);

                    response.StatusCode.Should().Be(HttpStatusCode.Created);

                    var books = dbContext.Set<Book>().ToList();
                    books.Count.Should().Be(existingBookCount + 1);
                    var book = books.SingleOrDefault(x => x.Id == lastId + 1);
                    book.Should().NotBeNull();
                    book!.Author.Should().Be("Author");
                    book!.Title.Should().Be("Title");
                    book!.Price.Should().Be(2.20);
                }
            }           
            
        }

        [Fact]
        public async Task CreateBook_InvalidBody_ShouldReturn400()
        {
            await using var application = new ApiControllerTestBase();

            using (var scope = application.Services.CreateScope())
            {
                var provider = scope.ServiceProvider;
                using (var dbContext = provider.GetRequiredService<ApplicationDbContext>())
                {
                    await dbContext.Database.EnsureCreatedAsync();
                    await dbContext.SaveChangesAsync();

                    var client = application.CreateClient();
                    var data = new
                    {
                        Title = "Title",
                        Price = 2.20
                    };

                    var existingBookCount = dbContext.Set<Book>().Count();

                    var json = JsonSerializer.Serialize(data);
                    var request = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync($"/Books", request);

                    response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
                    dbContext.Set<Book>().Count().Should().Be(existingBookCount);
                }
            }

        }

        [Fact]
        public async Task GetBook_Success_ShouldGetBooks_And_Return200()
        {
            await using var application = new ApiControllerTestBase();
            using (var scope = application.Services.CreateScope())
            {
                var provider = scope.ServiceProvider;
                using (var dbContext = provider.GetRequiredService<ApplicationDbContext>())
                {
                    await dbContext.Database.EnsureCreatedAsync(); 
                    await dbContext.SaveChangesAsync();

                    var client = application.CreateClient();
                    var response = await client.GetAsync($"/Books");
                    var responseString = await response.Content.ReadAsStringAsync();
                    var deseralizedResponse = JsonSerializer.Deserialize<List<Book>>(responseString);

                    deseralizedResponse!.Count().Should().Be(3);
                    response.EnsureSuccessStatusCode();
                }
            }            
        }

        [Fact]
        public async Task UpdateBook_Success_ShouldUpdateBooks_And_Return200()
        {
            await using var application = new ApiControllerTestBase();
            using (var scope = application.Services.CreateScope())
            {
                var provider = scope.ServiceProvider;
                using (var dbContext = provider.GetRequiredService<ApplicationDbContext>())
                {
                    await dbContext.Database.EnsureCreatedAsync();
                    await dbContext.SaveChangesAsync();

                    var client = application.CreateClient();

                    var data = new
                    {
                        Author = "Author",
                        Title = "Title",
                        Price = 2.20
                    };

                    var json = JsonSerializer.Serialize(data);
                    var request = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PutAsync($"/Books/{1}", request);

                    response.EnsureSuccessStatusCode();
                }
            }
        }

        [Fact]
        public async Task UpdateBook_InvalidBody_ShouldReturn400()
        {
            await using var application = new ApiControllerTestBase();
            using (var scope = application.Services.CreateScope())
            {
                var provider = scope.ServiceProvider;
                using (var dbContext = provider.GetRequiredService<ApplicationDbContext>())
                {
                    await dbContext.Database.EnsureCreatedAsync();
                    await dbContext.SaveChangesAsync();

                    var client = application.CreateClient();

                    var data = new
                    {
                        Title = "Title",
                        Price = 2.20
                    };

                    var json = JsonSerializer.Serialize(data);
                    var request = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PutAsync($"/Books/{1}", request);

                    response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
                }
            }
        }

        [Fact]
        public async Task UpdateBook_BookNotFound_ShouldReturn404()
        {
            await using var application = new ApiControllerTestBase();
            using (var scope = application.Services.CreateScope())
            {
                var provider = scope.ServiceProvider;
                using (var dbContext = provider.GetRequiredService<ApplicationDbContext>())
                {
                    await dbContext.Database.EnsureCreatedAsync();
                    await dbContext.SaveChangesAsync();

                    var client = application.CreateClient();

                    var data = new
                    {
                        Author = "Author",
                        Title = "Title",
                        Price = 2.20
                    };

                    var json = JsonSerializer.Serialize(data);
                    var request = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PutAsync($"/Books/{100}", request);

                    response.StatusCode.Should().Be(HttpStatusCode.NotFound);
                }
            }
        }
    }
}
