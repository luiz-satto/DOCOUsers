using BuildingBlocks.Exceptions.Handler;
using DOCOUsers;
using DOCOUsers.Application;
using DOCOUsers.Infrastructure;
using DOCOUsers.Infrastructure.Data.Extensions;

// Add services to the container.
var assembly = typeof(Program).Assembly;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services
    .AddExceptionHandler<CustomExceptionHandler>()
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices();

// Configure the HTTP request pipeline.
var app = builder.Build();
app.UseExceptionHandler(options => { });

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.InitialiseDatabase();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
