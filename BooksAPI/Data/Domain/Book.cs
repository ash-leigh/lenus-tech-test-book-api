namespace BooksAPI.Data.Domain
{
    public class Book
    {
        public Book(int id, string author, string title, double price)
        {
            Id = id;
            Author = author;
            Title = title;
            Price = price;
        }
        public int Id { get; set; }

        public string Author { get; set; }

        public string Title { get; set; }

        public double Price { get; set; }
    }
}
