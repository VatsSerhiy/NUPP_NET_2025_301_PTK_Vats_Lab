using Character.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();


builder.Services.AddOpenApi();
var connectionString = "Data Source=characters.db";
builder.Services.AddDbContext<CharacterContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddScoped(typeof(IRepository<>), typeof(CharacterRepository<>));

builder.Services.AddScoped(typeof(ICrudServiceAsync<>), typeof(CrudServiceAsync<>));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
