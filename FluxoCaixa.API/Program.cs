using FluxoCaixa.Application.Services;
using FluxoCaixa.Infrastructure.Data;
using FluxoCaixa.Messaging.Interfaces;
using FluxoCaixa.Messaging.Rabbit;
using Microsoft.EntityFrameworkCore;
using Polly;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("FluxoCaixaDb"));

builder.Services.AddSingleton<IBusPublisher, RabbitMQPublisher>();

//Checagens de Segurança e Saúde do Software

builder.Services.AddHttpClient("ConsolidadoClient")
    .AddTransientHttpErrorPolicy(policy =>
        policy.WaitAndRetryAsync(3, retry => TimeSpan.FromSeconds(retry)));

builder.Services.AddHealthChecks()
    .AddRabbitMQ("amqp://guest:guest@localhost:5672", name: "RabbitMQ");

builder.Services.AddHealthChecksUI(setupSettings: setup =>
{
    setup.AddHealthCheckEndpoint("API", "/health");
}).AddInMemoryStorage();

builder.Services.AddScoped<LancamentoService>();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHealthChecksUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
