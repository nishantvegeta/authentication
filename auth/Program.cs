using Microsoft.AspNetCore.Authentication.Cookies;
using auth.Data;
using Microsoft.EntityFrameworkCore;
using auth.Provider;
using auth.Provider.Interfaces;
using auth.Manager;
using auth.Manager.Interfaces;
using auth.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<FirstRunDbContext>(builder =>
    builder.UseNpgsql("Host=localhost;Database=authen;Username=postgres;Password=5744"));

builder.Services.AddScoped<IAuthManager, AuthManager>();
builder.Services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/auth/Login";
    });


builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}").RequireAuthorization()
    .WithStaticAssets();


app.Run();
