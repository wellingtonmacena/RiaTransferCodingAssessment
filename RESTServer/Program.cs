using Microsoft.EntityFrameworkCore;
using RESTServer.Data;
using RESTServer.Repositories;
using RESTServer.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ICustomerRepository, SQLiteCustomerRepository>();


var connection = builder.Configuration["ConnectionSqlite:SqliteConnectionString"];
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connection)
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();