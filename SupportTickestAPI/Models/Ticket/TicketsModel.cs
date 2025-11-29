namespace SupportTickestAPI.Models;

public class TicketsModel
{
    public Guid Id { get; private init; } = Guid.NewGuid();
    public string Title { get; init; } = null!;
    public string Description { get; init; } = null!;
    public DateTime Created { get; private set; } = DateTime.UtcNow;
    public DateTime Closure { get; private set; }

    public Guid UserId { get; private set; }
    public UserModel UserModel { get; private set; } = null!;

    public StatusTicket StatusTicket { get; private set; } = StatusTicket.Aberto;


    private TicketsModel()
    {
    }

    public TicketsModel(string title, string description, UserModel userModel)
    {
        Title = title;
        Description = description;
        UserModel = userModel;
        UserId = userModel.Id;
    }

    public void SetUser(UserModel user)
    {
        UserModel = user;
        UserId = user.Id;
    }


    public void StartProgress()
    {
        if (StatusTicket != StatusTicket.Aberto)
            throw new InvalidOperationException("Apenas tickets abertos podem ser iniciados");
        
        StatusTicket = StatusTicket.EmAndamento;
    }

    public void CloseTicket()
    {
        if (StatusTicket == StatusTicket.Finalizado || StatusTicket == StatusTicket.Cancelado)
            throw new InvalidOperationException("Ticket já está fechado");

        StatusTicket = StatusTicket.Finalizado;
        Closure = DateTime.UtcNow;
    }

    public void CancelTicket()
    {
        if (StatusTicket == StatusTicket.Finalizado)
            throw new InvalidOperationException("Não é possível cancelar ticket finalizado");

        StatusTicket = StatusTicket.Cancelado;
        Closure = DateTime.UtcNow;
    }

    public void ReopenTicket()
    {
        if (StatusTicket == StatusTicket.Aberto)
            throw new InvalidOperationException("Ticket já está aberto");

        StatusTicket = StatusTicket.Aberto;
        Closure = new DateTime(0000-000);
    }
}