using Microsoft.EntityFrameworkCore;
using PaloGames.Data;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Conexión a PostgreSQL (Se mantiene igual)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql("Host=dpg-d7lealjbc2fs73be4nrg-a.oregon-postgres.render.com;Port=5432;Database=palogames;Username=palogames_user;Password=8oOY4i8zJq9XxGMnwHyhqzmisHG3EX7j"));

// 🔹 BLINDAJE REDIS: Memoria local en Desarrollo, Redis en Producción
var redisConn = builder.Configuration.GetConnectionString("RedisConnection");

if (builder.Environment.IsProduction() && !string.IsNullOrEmpty(redisConn))
{
    // Solo intenta conectarse a Redis en la nube si el proyecto está subido a Render
    builder.Services.AddStackExchangeRedisCache(options =>
    {
        options.Configuration = redisConn;
        options.InstanceName = "PaloEvents_";
    });
}
else
{
    // Si estás probando en tu PC (localhost), usa la memoria RAM de tu computadora.
    // ¡Esto evita que el error de conexión a la nube tumbe la página!
    builder.Services.AddDistributedMemoryCache();
}

builder.Services.AddControllersWithViews();

// 🔹 Sesiones (Funcionarán perfecto sin importar si usa Redis o Memoria Local)
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