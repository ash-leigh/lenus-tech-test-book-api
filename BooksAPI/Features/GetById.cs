namespace BooksAPI.Features
{
    using BooksAPI.Data;
    using BooksAPI.Data.Domain;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    public class GetById
    {
        public class Command : IRequest<ActionResult<Response>>
        {
            public Command(int? id)
            {
                Id = id;
            }

            public int? Id { get; set; }
        }

        public class Response
        {
            public Response(Book book)
            {
                Book = book;
            }

            public Book Book { get; set; }
        }

        public class Handler : IRequestHandler<Command, ActionResult<Response>>
        {
            private readonly ApplicationDbContext db;

            public Handler(ApplicationDbContext db)
            {
                this.db = db;
            }

            public async Task<ActionResult<Response>> Handle(Command request, CancellationToken cancellationToken)
            {
                var book = db.Set<Book>().SingleOrDefault(x => x.Id == request.Id);

                if (book is object)
                {
                    return new OkObjectResult(new Response(book));
                }
                else
                {
                    return new NotFoundResult();
                }
            }
        }
    }
}
