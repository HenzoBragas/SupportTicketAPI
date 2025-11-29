namespace SupportTickestAPI.Models.Ticket;

public record TicketResponse(
    Guid Id, 
    string Title, 
    string Description, 
    DateTime Created, 
    DateTime? Closure, 
    Guid UserId, 
    StatusTicket  StatusTicket
    );