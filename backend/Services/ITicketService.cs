using backend.Dtos;

namespace backend.Services;

public interface ITicketService
{
    List<TicketResponse> GetAll();
    TicketResponse? GetById(int id);
    TicketResponse Create(CreateTicketRequest request);
    TicketResponse? Assign(int id, int assigneeUserId);
    (TicketResponse? ticket, int? errorCode) ChangeStatus(int id, string status);
}