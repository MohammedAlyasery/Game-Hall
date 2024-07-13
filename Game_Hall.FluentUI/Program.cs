using Game_Hall.FluentUI.Components;
using Game_Hall.Persistance;
using Microsoft.EntityFrameworkCore;
using GameHall.Services;
using GameHall.Servicesinterfaces;
using Microsoft.FluentUI.AspNetCore.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextFactory<Game_Hall_Context>(options =>
      options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection"), opt =>
    opt.EnableRetryOnFailure(
        maxRetryCount: 5,
        maxRetryDelay: System.TimeSpan.FromSeconds(30),
        errorNumbersToAdd: null)
    ));


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddFluentUIComponents();

builder.Services.AddScoped<IGameServices, GameServices>();
builder.Services.AddScoped<IPlayerServices, PlayerServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
