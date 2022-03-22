namespace BooksAPI.Features
{
    using BooksAPI.Data;
    using BooksAPI.Data.Domain;
    using MediatR;
    public class Get
    {
        public class Command : IRequest<Response>
        {
        }

        public class Response
        {
            public Response(IList<Book> books)
            {
                Books = books;
            }

            public IList<Book> Books { get; set; }
        }

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly ApplicationDbContext db;

            public Handler(ApplicationDbContext db)
            {
                this.db = db;
            }

            public Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var query = db.Set<Book>().AsQueryable();
                var books = query.ToList();
                return Task.FromResult(new Response(books));
            }
        }
    }
}
