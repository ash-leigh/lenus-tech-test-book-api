namespace BooksAPI.Controllers
{
    using BooksAPI.Data;
    using BooksAPI.Data.Domain;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly ILogger<BooksController> _logger;
        private readonly ApplicationDbContext db;

        public BooksController(ILogger<BooksController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            this.db = db;
        }

        [HttpGet(Name = "GET")]
        public IEnumerable<Book> Get()
        {
            return db.Set<Book>();
        }
    }
}