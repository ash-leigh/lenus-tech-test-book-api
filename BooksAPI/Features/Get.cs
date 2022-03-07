namespace BooksAPI.Features
{
    using BooksAPI.Data;
    using BooksAPI.Data.Domain;
    using MediatR;
    public class Get
    {
        public class Command : IRequest<Response>
        {
            public Command(int? id)
            {
                Id = id;
            }

            public int? Id { get; set; }
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

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var query = db.Set<Book>();
                if (request.Id is object)
                {
                    query.Where(x => x.Id == request.Id);
                }

                var books = query.ToList();
                return new Response(books);
            }
        }
    }
}
