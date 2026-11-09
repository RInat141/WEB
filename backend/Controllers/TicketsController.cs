using Microsoft.AspNetCore.Mvc;
using backend.Dtos;
using backend.Services;

namespace backend.Controllers;

[ApiController]
[Route("api/tickets")]
public class TicketsController : ControllerBase
{
    private readonly ITicketService _service;
    private readonly ILogger<TicketsController> _logger;

    public TicketsController(ITicketService service, ILogger<TicketsController> logger)
    {
        _service = service;
        _logger = logger;
    }

    // GET /api/tickets
    [HttpGet]
    public IActionResult GetAll() => Ok(_service.GetAll());

    // GET /api/tickets/{id}
    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var t = _service.GetById(id);
        return t is null ? NotFound() : Ok(t);
    }

    // POST /api/tickets
    [HttpPost]
    public IActionResult Create([FromBody] CreateTicketRequest request)
    {
        // Валидация
        var title = (request.Title ?? "").Trim();
        var desc = (request.Description ?? "").Trim();

        if (title.Length < 5 || title.Length > 80)
            return BadRequest(new { error = "title: 5–80 символов" });
        if (desc.Length < 10 || desc.Length > 500)
            return BadRequest(new { error = "description: 10–500 символов" });
        if (request.SiteId <= 0)
            return BadRequest(new { error = "siteId: целое > 0" });

        try
        {
            var created = _service.Create(request);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "CreateTicket failed; siteId={SiteId}; error={Error}",
                request.SiteId, ex.GetType().Name);
            return Problem("Не удалось создать заявку");
        }
    }

    // PATCH /api/tickets/{id}/assignee
    [HttpPatch("{id:int}/assignee")]
    public IActionResult Assign(int id, [FromBody] AssignTicketRequest request)
    {
        if (request.AssigneeUserId <= 0)
            return BadRequest(new { error = "assigneeUserId: целое > 0" });

        var updated = _service.Assign(id, request.AssigneeUserId);
        return updated is null ? NotFound() : Ok(updated);
    }

    // PATCH /api/tickets/{id}/status
    [HttpPatch("{id:int}/status")]
    public IActionResult ChangeStatus(int id, [FromBody] ChangeStatusRequest request)
    {
        var (ticket, errorCode) = _service.ChangeStatus(id, request.Status ?? "");
        if (errorCode == 404) return NotFound();
        if (errorCode == 400) return BadRequest(new { error = "status: New | InProgress | Closed | Cancelled" });
        if (errorCode == 409) return Conflict(new { error = "Нельзя перевести в InProgress без исполнителя" });
        return Ok(ticket);
    }
}