using BlazorWebAppMovies.Components;
using Microsoft.EntityFrameworkCore;
using BlazorWebAppMovies.Data;

var builder = WebApplication.CreateBuilder(args);

// Correct connection string format (remove "Data Source=" from configuration)
builder.Services.AddDbContext<BlazorWebAppMoviesContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("BlazorWebAppMoviesContext")),
    ServiceLifetime.Scoped);

// Register DbContextFactory with matching lifetime
builder.Services.AddDbContextFactory<BlazorWebAppMoviesContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("BlazorWebAppMoviesContext")),
    ServiceLifetime.Scoped); // Match the lifetime

builder.Services.AddQuickGridEntityFrameworkAdapter();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}
else
{
    app.UseMigrationsEndPoint(); // Only in development
}

app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
