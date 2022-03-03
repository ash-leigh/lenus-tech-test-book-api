namespace BooksAPI.Controllers
{
    using BooksAPI.Data;
    using BooksAPI.Data.Domain;
    using BooksAPI.Features;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly ILogger<BooksController> logger;
        private readonly ApplicationDbContext db;
        protected IMediator mediator { get; }

        public BooksController(ILogger<BooksController> logger, ApplicationDbContext db, IMediator mediator)
        {
            this.logger = logger;
            this.db = db;
            this.mediator = mediator;
        }

        [HttpPost(Name = "POST")]
        public async Task<CreateBook.Response> Post(CreateBook.Command request, CancellationToken cancellationToken)
        {
            return await mediator.Send(request, cancellationToken);
        }

        [HttpGet(Name = "GET")]
        public IEnumerable<Book> Get()
        {
            return db.Set<Book>();
        }
    }
}