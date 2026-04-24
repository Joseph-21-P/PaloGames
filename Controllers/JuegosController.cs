using Microsoft.AspNetCore.Mvc;
using PaloGames.Models;
using PaloGames.Data;
using System.Collections.Generic;
using System.Linq;

namespace PaloGames.Controllers
{
    public class JuegosController : Controller
    {
        private readonly AppDbContext _context;

        public JuegosController(AppDbContext context)
        {
            _context = context;
        }

        // Ahora recibimos más parámetros desde la vista
        public IActionResult Index(string searchString, decimal? minPrice, decimal? maxPrice, List<string> categorias)
        {
            var juegosFalsos = new List<Juego>
            {
                new Juego { Id=1, Nombre="Elden Ring", Precio=159.90m, Categoria="RPG", ImagenUrl="https://placehold.co/300x400/1a1a24/9b51e0?text=Elden+Ring" },
                new Juego { Id=2, Nombre="Cyberpunk 2077", Precio=120.00m, Categoria="Acción", ImagenUrl="https://placehold.co/300x400/1a1a24/9b51e0?text=Cyberpunk" },
                new Juego { Id=3, Nombre="Resident Evil 4", Precio=199.99m, Categoria="Terror", ImagenUrl="https://placehold.co/300x400/1a1a24/9b51e0?text=RE4" },
                new Juego { Id=4, Nombre="Red Dead Redemption 2", Precio=99.50m, Categoria="Aventura", ImagenUrl="https://placehold.co/300x400/1a1a24/9b51e0?text=RDR2" },
                new Juego { Id=5, Nombre="The Witcher 3", Precio=39.90m, Categoria="RPG", ImagenUrl="https://placehold.co/300x400/1a1a24/9b51e0?text=Witcher+3" },
                new Juego { Id=6, Nombre="GTA V", Precio=59.90m, Categoria="Acción", ImagenUrl="https://placehold.co/300x400/1a1a24/9b51e0?text=GTA+V" },
                new Juego { Id=7, Nombre="God of War", Precio=149.00m, Categoria="Acción", ImagenUrl="https://placehold.co/300x400/1a1a24/9b51e0?text=God+of+War" },
                new Juego { Id=8, Nombre="Spider-Man Remastered", Precio=169.00m, Categoria="Aventura", ImagenUrl="https://placehold.co/300x400/1a1a24/9b51e0?text=Spider-Man" },
                new Juego { Id=9, Nombre="Horizon Zero Dawn", Precio=89.90m, Categoria="Aventura", ImagenUrl="https://placehold.co/300x400/1a1a24/9b51e0?text=Horizon" },
                new Juego { Id=10, Nombre="Hogwarts Legacy", Precio=189.99m, Categoria="RPG", ImagenUrl="https://placehold.co/300x400/1a1a24/9b51e0?text=Hogwarts" },
                new Juego { Id=11, Nombre="Doom Eternal", Precio=79.90m, Categoria="Acción", ImagenUrl="https://placehold.co/300x400/1a1a24/9b51e0?text=Doom" },
                new Juego { Id=12, Nombre="Minecraft", Precio=115.00m, Categoria="Supervivencia", ImagenUrl="https://placehold.co/300x400/1a1a24/9b51e0?text=Minecraft" },
                new Juego { Id=13, Nombre="Terraria", Precio=19.90m, Categoria="Indie", ImagenUrl="https://placehold.co/300x400/1a1a24/9b51e0?text=Terraria" },
                new Juego { Id=14, Nombre="Stardew Valley", Precio=29.90m, Categoria="Indie", ImagenUrl="https://placehold.co/300x400/1a1a24/9b51e0?text=Stardew" },
                new Juego { Id=15, Nombre="Hollow Knight", Precio=34.90m, Categoria="Indie", ImagenUrl="https://placehold.co/300x400/1a1a24/9b51e0?text=Hollow+Knight" },
                new Juego { Id=16, Nombre="Dark Souls III", Precio=129.90m, Categoria="RPG", ImagenUrl="https://placehold.co/300x400/1a1a24/9b51e0?text=Dark+Souls" },
                new Juego { Id=17, Nombre="Persona 5 Royal", Precio=179.90m, Categoria="RPG", ImagenUrl="https://placehold.co/300x400/1a1a24/9b51e0?text=Persona+5" },
                new Juego { Id=18, Nombre="Baldur's Gate 3", Precio=199.90m, Categoria="RPG", ImagenUrl="https://placehold.co/300x400/1a1a24/9b51e0?text=Baldurs+Gate" },
                new Juego { Id=19, Nombre="EA Sports FC 24", Precio=219.00m, Categoria="Deportes", ImagenUrl="https://placehold.co/300x400/1a1a24/9b51e0?text=FC+24" },
                new Juego { Id=20, Nombre="Call of Duty: MW3", Precio=249.90m, Categoria="Acción", ImagenUrl="https://placehold.co/300x400/1a1a24/9b51e0?text=COD+MW3" }
            };

            var juegos = juegosFalsos.AsQueryable();

            // 1. Filtro por Nombre
            if (!string.IsNullOrEmpty(searchString))
                juegos = juegos.Where(s => s.Nombre.ToLower().Contains(searchString.ToLower()));

            // 2. Filtro por Precio Mínimo
            if (minPrice.HasValue)
                juegos = juegos.Where(s => s.Precio >= minPrice.Value);

            // 3. Filtro por Precio Máximo
            if (maxPrice.HasValue)
                juegos = juegos.Where(s => s.Precio <= maxPrice.Value);

            // 4. Filtro por Categorías seleccionadas
            if (categorias != null && categorias.Any())
                juegos = juegos.Where(s => categorias.Contains(s.Categoria));

            // Guardamos los valores en el ViewBag para mantener los filtros visualmente en la pantalla
            ViewBag.SearchString = searchString;
            ViewBag.MinPrice = minPrice;
            ViewBag.MaxPrice = maxPrice;
            ViewBag.CategoriasSeleccionadas = categorias ?? new List<string>();

            return View(juegos.ToList());
        }
    }
}