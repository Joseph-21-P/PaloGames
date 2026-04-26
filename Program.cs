using Microsoft.EntityFrameworkCore;
using PaloGames.Data;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Conexión a PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🔹 Conectar a Redis 
builder.Services.AddStackExchangeRedisCache(options =>
{
    // conexión  en el appsettings.json
    options.Configuration = builder.Configuration.GetConnectionString("RedisConnection");
    options.InstanceName = "PaloEvents_";
});

builder.Services.AddControllersWithViews();

// 🔹 Sesiones (Ahora usarán Redis automáticamente en el fondo)
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); 
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor(); 

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession(); // Debe estar antes de Authorization
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();