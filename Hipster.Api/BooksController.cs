using Microsoft.AspNetCore.Mvc;

namespace Hipster.Api;

[ApiController]
public class BooksController: ControllerBase
{
    [HttpGet("/")]
    public IActionResult Get()
    {
        return Ok(new 
        {
            Title = "Clean Code",
            Author = "Uncle Bob",
            Year = "2000"
        });
    }
}