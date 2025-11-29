using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SupportTickestAPI.Data;
using SupportTickestAPI.Models;
using SupportTickestAPI.Models.DTO;
using SupportTickestAPI.Models.Ticket;

namespace SupportTickestAPI.Routes;

public  static class TicketRoute
{
    public static void TicketRoutes(this WebApplication app)
    {
        var route = app.MapGroup("tickets");


        //Criar Tickets
        route.MapPost("{userId:guid}", async (Guid userId, TicketRequest req, SupportContext context, IMapper mapper) =>
        {
            var user = await context.Users.FindAsync(userId);
            if (user == null)
                return Results.NotFound("User not found");

            var ticket = mapper.Map<TicketsModel>(req);
            ticket.SetUser(user);

            await context.Tickets.AddAsync(ticket);
            await context.SaveChangesAsync();

            var response = mapper.Map<TicketResponse>(ticket);
            return Results.Created($"/tickets/{ticket.Id}", response);
        });

        //Retorna todos tickets
        route.MapGet("", async (SupportContext context) =>
        {
            var ticket = await context.Tickets.FirstOrDefaultAsync();
            if (ticket == null)
                return Results.NotFound("Tickets not found");
            
            return Results.Ok(ticket);
        });
        
        //Listar por ID
        route.MapGet("{userId:guid}", async (Guid userId, SupportContext context) =>
        {
           var user =  await context.Users.FindAsync(userId);
           if (user == null)
               Results.NotFound("User not found");

           var ticket = await context.Tickets.ToListAsync();
           
           return Results.Ok(ticket);
        });

        //Iniciar  Chamado
        route.MapPatch("{id:guid}/start", async (Guid id, SupportContext context, IMapper mapper) =>
        {
            var ticket = await context.Tickets.FindAsync(id);
            if (ticket == null)
               return Results.NotFound("Ticket not found");
            try
            {
                ticket.StartProgress();
                await context.SaveChangesAsync();

                var response = mapper.Map<TicketResponse>(ticket);
                return Results.Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return Results.BadRequest(ex.Message);

            }
        });
        
        
        // PATCH - Finalizar ticket
        route.MapPatch("{id:guid}/close", async (Guid id, SupportContext context, IMapper mapper) =>
        {
            var ticket = await context.Tickets.FindAsync(id);
            if (ticket == null)
                return Results.NotFound("Ticket not found");

            try
            {
                ticket.CloseTicket();
                await context.SaveChangesAsync();
            
                var response = mapper.Map<TicketResponse>(ticket);
                return Results.Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        // PATCH - Cancelar ticket
        route.MapPatch("{id:guid}/cancel", async (Guid id, SupportContext context, IMapper mapper) =>
        {
            var ticket = await context.Tickets.FindAsync(id);
            if (ticket == null)
                return Results.NotFound("Ticket not found");

            try
            {
                ticket.CancelTicket();
                await context.SaveChangesAsync();
            
                var response = mapper.Map<TicketResponse>(ticket);
                return Results.Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        // PATCH - Reabrir ticket
        route.MapPatch("{id:guid}/reopen", async (Guid id, SupportContext context, IMapper mapper) =>
        {
            var ticket = await context.Tickets.FindAsync(id);
            if (ticket == null)
                return Results.NotFound("Ticket not found");

            try
            {
                ticket.ReopenTicket();
                await context.SaveChangesAsync();
            
                var response = mapper.Map<TicketResponse>(ticket);
                return Results.Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

    }
}