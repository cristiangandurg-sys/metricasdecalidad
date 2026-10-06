using Application.Abstractions;
using Application.UseCases;
using Infrastructure.Data;
using Infrastructure.Logging;
using Infrastructure.Services;
using WebApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Sql")
    ?? throw new InvalidOperationException("Falta la cadena de conexion ConnectionStrings:Sql.");
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>()
    ?? Array.Empty<string>();

builder.Services.AddCors(options => options.AddPolicy("Default", policy =>
    policy.WithOrigins(allowedOrigins).AllowAnyHeader().WithMethods("GET", "POST")));
builder.Services.AddProblemDetails();

// Raiz de composicion: aqui se conectan las interfaces de Application con Infrastructure.
builder.Services.AddSingleton<IAppLogger, ConsoleAppLogger>();
builder.Services.AddSingleton<IIdGenerator, SecureIdGenerator>();
builder.Services.AddSingleton<IOrderWriter>(_ => new SqlOrderRepository(connectionString));
builder.Services.AddSingleton<InMemoryRecentOrders>();
builder.Services.AddSingleton<IRecentOrdersReader>(sp => sp.GetRequiredService<InMemoryRecentOrders>());
builder.Services.AddSingleton<IRecentOrdersWriter>(sp => sp.GetRequiredService<InMemoryRecentOrders>());
builder.Services.AddScoped<CreateOrderUseCase>();
builder.Services.AddScoped<GetLastOrdersUseCase>();

var app = builder.Build();

app.UseExceptionHandler();
app.UseCors("Default");
app.MapOrderEndpoints();

await app.RunAsync();
