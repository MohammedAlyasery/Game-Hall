using Game_Hall.Persistance;
using Game_Hall.web_ui.Components;
using GameHall.Services;
using GameHall.Servicesinterfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContextFactory<Game_Hall_Context>(options =>
      options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));






// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();



builder.Services.AddScoped<IGameServices,GameServices>();
builder.Services.AddScoped<IPlayerServices, PlayerServices>();






var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
