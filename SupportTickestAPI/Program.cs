using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using SupportTickestAPI.Data;
using SupportTickestAPI.Models.Mapper;
using SupportTickestAPI.Routes;

var builder = WebApplication.CreateBuilder(args);

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Configurar JSON para converter Enums em strings
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// DbContext
builder.Services.AddScoped<SupportContext>();

// Configurar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Support Tickets API",
        Version = "v1",
        Description = "API para gerenciamento de tickets de suporte"
    });

    // Organizar endpoints por tags
    options.TagActionsBy(api =>
    {
        var route = api.RelativePath ?? "Users";
        if (route.StartsWith("users"))
            return new[] { "Users" };
        if (route.StartsWith("tickets"))
            return new[] { "Tickets" };

        return new[] { "Users" };
    });

    // Ordenar endpoints
    options.OrderActionsBy(api => $"{api.HttpMethod}_{api.RelativePath}");
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Support Tickets API v1");
    options.DocumentTitle = "Support Tickets API - Documentation";
});

// Rota raiz redireciona para Swagger
app.MapGet("/", () => Results.Redirect("/swagger"))
    .ExcludeFromDescription();

// Rotas da aplicação
UserRoute.UserRoutes(app);
TicketRoute.TicketRoutes(app);

app.Run();