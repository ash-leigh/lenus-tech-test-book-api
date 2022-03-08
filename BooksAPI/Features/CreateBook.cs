namespace BooksAPI.Features
{
    using BooksAPI.Data;
    using BooksAPI.Data.Domain;
    using MediatR;
    using System.ComponentModel.DataAnnotations;

    public class CreateBook
    {
        public class Command : IRequest<Response>
        {
            public Command(string author, string title, double price)
            {
                Author = author;
                Title = title;
                Price = price;
            }

            [Required]
            public string Author { get; set; }

            [Required]
            public string Title { get; set; }

            [Required]
            public double Price { get; set; }
        }

        public class Response
        {
            public Response(int id)
            {
                Id = id;
            }

            public int Id { get; set; }
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
                var id = db.Set<Book>().Max(x => x.Id)+1;
                var newBook = new Book(id, request.Author, request.Title, request.Price);
                await db.AddAsync(newBook, cancellationToken);
                await db.SaveChangesAsync(cancellationToken);

                return new Response(id);
            }
        }
    }
}
