namespace BookAPI.Tests
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.Extensions.Hosting;

    public class ApiControllerTestBase : WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddMediatR(typeof(Program).Assembly);
            });

            return base.CreateHost(builder);
        }
    }
}
