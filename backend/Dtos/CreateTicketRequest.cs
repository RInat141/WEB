namespace backend.Dtos;

public class CreateTicketRequest
{
    public string Title { get; set; } = string.Empty;
    public int SiteId { get; set; }
    public string Description { get; set; } = string.Empty;
}