﻿namespace BooksAPI.Features
{
    using BooksAPI.Data;
    using BooksAPI.Data.Domain;
    using MediatR;
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

            public string Author { get; set; }

            public string Title { get; set; }

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
                await db.AddAsync(newBook);
                await db.SaveChangesAsync();

                return new Response(id);
            }
        }
    }
}
