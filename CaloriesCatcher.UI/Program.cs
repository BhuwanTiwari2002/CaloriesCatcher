using CaloriesCatcher.UI.Model;
using CaloriesCatcher.UI.Service;
using CaloriesCatcher.UI.Service.IService;
using KitchenComfort.Web.Models.Utility;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MudBlazor.Services;
using RecipeCatcher.UI.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ApiTokenOptions>(builder.Configuration.GetSection("APIToken:Token"));
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor(); /* We need this for cookies */
builder.Services.AddHttpClient<IAuthService, AuthService>();
builder.Services.AddHttpClient<ICalories, CaloriesService>();
builder.Services.AddHttpClient<IRecipe, RecipeService>();
builder.Services.AddHttpClient<IEdamamAPI, EdamamAPI>();
builder.Services.AddHttpClient<IUserDetailService, UserDetailService>();

builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICalories, CaloriesService>();
builder.Services.AddScoped<IRecipe, RecipeService>();
builder.Services.AddScoped<ITokenProvider, TokenProvider>();
builder.Services.AddScoped<IUserDetailService, UserDetailService>();
builder.Services.AddScoped<IEdamamAPI, EdamamAPI>();
builder.Services.AddScoped<IJsInteropService, JsInteropService>();
builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<StripePaymentsService>();

builder.Services.AddMudServices();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
