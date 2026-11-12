namespace backend.Models;

public class Ticket
{
    public int Id { get; set; }
    public string Number { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = "New";
    
    public int SiteId { get; set; }
    public Site? Site { get; set; } // Навигационное свойство

    public int CreatedByUserId { get; set; }
    public User? CreatedByUser { get; set; }

    public int? AssigneeUserId { get; set; }
    public User? AssigneeUser { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}