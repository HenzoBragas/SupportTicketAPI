using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SupportTickestAPI.Data;
using SupportTickestAPI.Models;
using SupportTickestAPI.Models.DTO;
using SupportTickestAPI.Models.User;

namespace SupportTickestAPI.Routes;

public static class UserRoute
{
    public static void UserRoutes(this WebApplication app)
    {
        var route = app.MapGroup("user")
            .WithTags("Users");

        route.MapPost("", async (UserRequest req, SupportContext context, IMapper mapper ) =>
        {
            var user = mapper.Map<UserModel>(req); 
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();


            var response = mapper.Map<UserResponse>(user);
            return Results.Created($"/users/{user.Id}", response);
        })
        .WithName("CreateUser")
        .WithDescription("Cria um novo usuário no sistema de suporte")
        .Produces<UserResponse>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest);;

        route.MapGet("", async (SupportContext context) =>
        {
            var user = await context.Users.Where(u => u.Active == true).ToListAsync();
            return Results.Ok(user);
        });

        route.MapGet("/disabled", async (SupportContext context) =>
        {
            var user = await context.Users.Where(u => u.Active == false).ToListAsync();
            return Results.Ok(user);
        });
        
        route.MapPut("{id:guid}", async (Guid id, UserRequest req, SupportContext context) => 
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
            
            if(user == null)
                return Results.NotFound();
            
            if(!string.IsNullOrWhiteSpace(req.name))
                user.ChangeName(req.name);
            
            if(!string.IsNullOrWhiteSpace(req.email))
                user.ChangeEmail(req.email);
            
            await context.SaveChangesAsync();
            
            return Results.Ok();
        });
        
        route.MapDelete("{id:guid}", async (Guid id, SupportContext context) =>
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
                return Results.NotFound();

            user.SetInactive();
            await context.SaveChangesAsync();
            
            return Results.Ok();
        });
    }
}