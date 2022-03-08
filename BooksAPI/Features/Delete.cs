namespace BooksAPI.Features
{
    using BooksAPI.Data;
    using BooksAPI.Data.Domain;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    public class Delete
    {
        public class Command : IRequest<IActionResult>
        {
            public Command(int? id)
            {
                Id = id;
            }

            public int? Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, IActionResult>
        {
            private readonly ApplicationDbContext db;

            public Handler(ApplicationDbContext db)
            {
                this.db = db;
            }

            public async Task<IActionResult> Handle(Command request, CancellationToken cancellationToken)
            {
                var book = db.Set<Book>().Where(x => x.Id == request.Id).SingleOrDefault();
                if (book is object)
                {
                    db.Remove(book);
                    await db.SaveChangesAsync();
                    return new OkResult();
                }
                else
                {
                    return new NotFoundResult();
                }
            }
        }
    }
}
