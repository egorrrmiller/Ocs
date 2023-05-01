using Microsoft.EntityFrameworkCore;
using Ocs.Api.Extensions;
using Ocs.Api.Middlewares;
using Ocs.Database.Context;
using Ocs.Database.Services;
using Ocs.Database.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("Postgres");
builder.Services.AddDbContext<OcsContext>(opt => opt.UseNpgsql(connectionString));

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ILineService, LineService>();

builder.Services.AddControllers()
    .AddNewtonsoftJson();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorExceptionHandling>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.DatabaseMigrate();

app.Run();