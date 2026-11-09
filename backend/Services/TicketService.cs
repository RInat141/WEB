using backend.Dtos;
using backend.Models;

namespace backend.Services;

public class TicketService : ITicketService
{
    private readonly List<Ticket> _tickets = new();
    private int _nextId = 1;

    public TicketService()
    {
        // Тестовые записи для наглядности
        _tickets.Add(new Ticket
        {
            Id = _nextId++,
            Number = $"T-2026-{_nextId:D4}",
            Title = "Не запускается сервер",
            Description = "При включении нет POST, вентиляторы не вращаются",
            Status = "New",
            SiteId = 2,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });
        _tickets.Add(new Ticket
        {
            Id = _nextId++,
            Number = $"T-2026-{_nextId:D4}",
            Title = "Замена SSD в рабочей станции",
            Description = "Диск сыпется, нужна замена",
            Status = "InProgress",
            SiteId = 1,
            AssigneeUserId = 5,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });
    }

    public List<TicketResponse> GetAll() =>
        _tickets.Select(ToResponse).ToList();

    public TicketResponse? GetById(int id)
    {
        var t = _tickets.FirstOrDefault(x => x.Id == id);
        return t is null ? null : ToResponse(t);
    }

    public TicketResponse Create(CreateTicketRequest request)
    {
        var ticket = new Ticket
        {
            Id = _nextId,
            Number = $"T-2026-{_nextId:D4}",
            Title = request.Title.Trim(),
            Description = request.Description.Trim(),
            Status = "New",
            SiteId = request.SiteId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        _nextId++;
        _tickets.Add(ticket);
        return ToResponse(ticket);
    }

    public TicketResponse? Assign(int id, int assigneeUserId)
    {
        var t = _tickets.FirstOrDefault(x => x.Id == id);
        if (t is null) return null;
        t.AssigneeUserId = assigneeUserId;
        t.UpdatedAt = DateTime.UtcNow;
        return ToResponse(t);
    }

    public (TicketResponse? ticket, int? errorCode) ChangeStatus(int id, string status)
    {
        var t = _tickets.FirstOrDefault(x => x.Id == id);
        if (t is null) return (null, 404);

        var allowed = new[] { "New", "InProgress", "Closed", "Cancelled" };
        if (!allowed.Contains(status)) return (null, 400);

        // Правило процесса: нельзя InProgress без исполнителя
        if (status == "InProgress" && t.AssigneeUserId is null)
            return (null, 409);

        t.Status = status;
        t.UpdatedAt = DateTime.UtcNow;
        return (ToResponse(t), null);
    }

    private static TicketResponse ToResponse(Ticket t) => new()
    {
        Id = t.Id,
        Number = t.Number,
        Title = t.Title,
        Description = t.Description,
        Status = t.Status,
        SiteId = t.SiteId,
        AssigneeUserId = t.AssigneeUserId,
        CreatedAt = t.CreatedAt,
        UpdatedAt = t.UpdatedAt
    };
}