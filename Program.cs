using Microsoft.EntityFrameworkCore;
using PaloGames.Data;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Conexión a PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql("Host=dpg-d7lealjbc2fs73be4nrg-a.oregon-postgres.render.com;Port=5432;Database=palogames;Username=palogames_user;Password=8oOY4i8zJq9XxGMnwHyhqzmisHG3EX7j"));

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