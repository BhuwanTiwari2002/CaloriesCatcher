using Auth.API.Service;
using AuthApi.API.Data;
using AuthApi.API.Models;
using AuthApi.API.Service.IService;
using AuthApi.API.Service;
using CaloriesCatcher.UI.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));
builder.Services.Configure<EmailAuthOptions>(builder.Configuration.GetSection("EmailCredentials"));
builder.Services.Configure<ApiTokenOptions>(builder.Configuration.GetSection("APIToken:Token"));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie()
.AddGoogle("Google", options =>
{
    options.ClientId = builder.Configuration["ApiSettings:GoogleAuth:Client-Id"];
    options.ClientSecret = builder.Configuration["ApiSettings:GoogleAuth:Client-Secret"];
    options.Events.OnTicketReceived = context =>
    {
        return Task.CompletedTask;
    };
});


builder.Services.AddControllers();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<IAuthService, AuthService>(); // For the IAuthService use the AuthService implemention
builder.Services.AddTransient<IEmailSender, EmailSender>();
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