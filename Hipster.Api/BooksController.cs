using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hipster.Api;

[ApiController]
public class BooksController: ControllerBase
{
    private readonly DatabaseContext db;

    public BooksController(DatabaseContext db)
    {
        this.db = db;
    }

    [HttpGet("/")]
    [ProducesResponseType(typeof(IEnumerable<Book>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Get()
    {
        var books = await db.Books.ToArrayAsync();
        return Ok(books);
    }
}