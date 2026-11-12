using backend.Data;
using backend.Dtos;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services;

public class TicketService : ITicketService
{
    private readonly AppDbContext _db;

    public TicketService(AppDbContext db)
    {
        _db = db;
    }

    public List<TicketResponse> GetAll() =>
        _db.Tickets.Select(ToResponse).ToList();

    public TicketResponse? GetById(int id)
    {
        var t = _db.Tickets.Find(id);
        return t is null ? null : ToResponse(t);
    }

    public TicketResponse Create(CreateTicketRequest request)
    {
        // Получаем следующий ID для красивого номера (упрощенно)
        var nextId = (_db.Tickets.Max(t => (int?)t.Id) ?? 0) + 1;
        
        var ticket = new Ticket
        {
            Number = $"T-2026-{nextId:D4}",
            Title = request.Title.Trim(),
            Description = request.Description.Trim(),
            Status = "New",
            SiteId = request.SiteId,
            CreatedByUserId = 1, // Упрощенно: жестко задаем создателя
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        
        _db.Tickets.Add(ticket);
        _db.SaveChanges();
        return ToResponse(ticket);
    }

    public TicketResponse? Assign(int id, int assigneeUserId)
    {
        var t = _db.Tickets.Find(id);
        if (t is null) return null;
        
        t.AssigneeUserId = assigneeUserId;
        t.UpdatedAt = DateTime.UtcNow;
        _db.SaveChanges();
        return ToResponse(t);
    }

    public (TicketResponse? ticket, int? errorCode) ChangeStatus(int id, string status)
    {
        var t = _db.Tickets.Find(id);
        if (t is null) return (null, 404);

        var allowed = new[] { "New", "InProgress", "Closed", "Cancelled" };
        if (!allowed.Contains(status)) return (null, 400);

        if (status == "InProgress" && t.AssigneeUserId is null)
            return (null, 409);

        t.Status = status;
        t.UpdatedAt = DateTime.UtcNow;
        _db.SaveChanges();
        
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