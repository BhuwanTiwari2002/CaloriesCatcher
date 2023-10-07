using AutoMapper;
using Calories.API;
using Calories.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
}, ServiceLifetime.Transient);

/* Auto Mapper */
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper); /* One Instance is created */
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
/* Check if there pending Migration, if so update the database with the Migration */
void ApplyMigration()
{
    using (var scope = app.Services.CreateScope()) // Gets all the server
    {
        var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>(); // Give us the AppDbContext
        if (_db.Database.GetPendingMigrations().Count() > 0)
        {
            _db.Database.Migrate(); // Adds the Migration
        }
    }
}