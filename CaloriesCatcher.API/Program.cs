using Auth.API.Service;
using AuthApi.API.Data;
using AuthApi.API.Models;
using AuthApi.API.Service.IService;
using AuthApi.API.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Google;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
builder.Services.AddAuthentication().AddGoogle("Google", options =>
{
    options.ClientId = builder.Configuration["ApiSettings:GoogleAuth:Client-Id"];
    options.ClientSecret = builder.Configuration["ApiSettings:GoogleAuth:Client-Secret"];
    options.Events.OnTicketReceived = context =>
    {
        // Do something when the user is authenticated, e.g., create a JWT token.
        // You can access the user info from context.Principal
        return Task.CompletedTask;
    };
    options.CallbackPath = new PathString("/signin-google");  // The URL to return the user to after Google authentication
});


builder.Services.AddControllers();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<IAuthService, AuthService>(); // For the IAuthService use the AuthService implemention
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

app.UseHttpsRedirection();
app.UseAuthentication(); // Authentication should always come before Authorization
app.UseAuthorization();

app.MapControllers();
ApplyMigration();
app.Run();
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