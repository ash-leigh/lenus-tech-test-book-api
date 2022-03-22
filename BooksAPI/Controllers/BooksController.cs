namespace BooksAPI.Controllers
{
    using BooksAPI.Data;
    using BooksAPI.Features;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly ILogger<BooksController> logger;
        private readonly ApplicationDbContext db;
        protected IMediator mediator { get; }

        public BooksController(ILogger<BooksController> logger, ApplicationDbContext db, IMediator mediator)
        {
            this.logger = logger;
            this.db = db;
            this.mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateBook.Response>> CreateBook(CreateBook.Command request, CancellationToken cancellationToken)
        {
            var response = await mediator.Send(request, cancellationToken);
            return CreatedAtAction(nameof(CreateBook), new { id = response.Id }, response);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Get.Response>> Get(CancellationToken cancellationToken)
        {
            return await mediator.Send(new Get.Command(), cancellationToken);
        }


        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateBook(int id, UpdateBook.Command request, CancellationToken cancellationToken)
        {
            request.Id = id;
            return await mediator.Send(request, cancellationToken);
        }


        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetById.Response>> GetById(int id, CancellationToken cancellationToken)
        {
            return await mediator.Send(new GetById.Command(id), cancellationToken);
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            return await mediator.Send(new Delete.Command(id), cancellationToken);
        }

    }
}