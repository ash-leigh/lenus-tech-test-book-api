namespace BooksAPI
{
    public class Book
    {
        public Book (Guid id, string author, string title, double price)
        {
            Id = id;    
            Author = author;
            Title = title;
            Price = price;
        }
        public Guid Id { get; set; }

        public string Author { get; set; }

        public string Title { get; set; }

        public double Price { get; set; }
    }
}