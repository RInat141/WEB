using Microsoft.AspNetCore.Mvc;
using backend.Services;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PingController : ControllerBase
{
    private readonly ITicketService _tickets;

    public PingController(ITicketService tickets)
    {
        _tickets = tickets;
    }

    [HttpGet]
    public IActionResult Get() => Ok(new { message = _tickets.Ping() });
}