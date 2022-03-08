namespace BooksAPI.Features
{
    using BooksAPI.Data;
    using BooksAPI.Data.Domain;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using System.ComponentModel.DataAnnotations;

    public class UpdateBook
    {
        public class Command : IRequest<IActionResult>
        {
            public Command(int id, string author, string title, double price)
            {
                Id = id;
                Author = author;
                Title = title;
                Price = price;
            }

            public int Id { get; set; }

            [Required]
            public string Author { get; set; }

            [Required]
            public string Title { get; set; }

            [Required]
            public double Price { get; set; }
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
                    book.Author = request.Author;
                    book.Title = request.Title;
                    book.Price = request.Price;

                    await db.SaveChangesAsync(cancellationToken);

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
