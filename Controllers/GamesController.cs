using Microsoft.AspNetCore.Mvc;
using PaloGames.Models;
using System.Collections.Generic;
using System.Linq;

namespace PaloGames.Controllers
{
    public class GamesController : Controller
    {
        private static readonly List<Game> games = new List<Game>
        {
            new Game { Id = 1, Title = "The Legend of Zelda", Genre = "Aventura", Price = 59.99m, Description = "Un clásico juego de aventuras.", ImageUrl = "/images/zelda.jpg" },
            new Game { Id = 2, Title = "Super Mario Bros", Genre = "Plataformas", Price = 49.99m, Description = "Salta y recolecta monedas.", ImageUrl = "/images/mario.jpg" },
            new Game { Id = 3, Title = "FIFA 2023", Genre = "Deportes", Price = 69.99m, Description = "El mejor simulador de fútbol.", ImageUrl = "/images/fifa.jpg" },
            new Game { Id = 4, Title = "Call of Duty", Genre = "Acción", Price = 79.99m, Description = "Disparos intensos.", ImageUrl = "/images/cod.jpg" },
            new Game { Id = 5, Title = "Minecraft", Genre = "Sandbox", Price = 39.99m, Description = "Construye y explora.", ImageUrl = "/images/minecraft.jpg" },
            new Game { Id = 6, Title = "Fortnite", Genre = "Battle Royale", Price = 0.00m, Description = "Juego gratuito.", ImageUrl = "/images/fortnite.jpg" },
            new Game { Id = 7, Title = "The Witcher 3", Genre = "RPG", Price = 89.99m, Description = "Historia épica.", ImageUrl = "/images/witcher.jpg" },
            new Game { Id = 8, Title = "Grand Theft Auto V", Genre = "Acción", Price = 99.99m, Description = "Mundo abierto.", ImageUrl = "/images/gta.jpg" },
            new Game { Id = 9, Title = "League of Legends", Genre = "MOBA", Price = 0.00m, Description = "Estrategia en equipo.", ImageUrl = "/images/lol.jpg" },
            new Game { Id = 10, Title = "Among Us", Genre = "Party", Price = 4.99m, Description = "Sospecha y engaño.", ImageUrl = "/images/amongus.jpg" },
            new Game { Id = 11, Title = "Cyberpunk 2077", Genre = "RPG", Price = 59.99m, Description = "Futuro distópico.", ImageUrl = "/images/cyberpunk.jpg" },
            new Game { Id = 12, Title = "Assassin's Creed Valhalla", Genre = "Aventura", Price = 79.99m, Description = "Vikingos y mitos.", ImageUrl = "/images/acvalhalla.jpg" },
            new Game { Id = 13, Title = "Valorant", Genre = "FPS", Price = 0.00m, Description = "Táctico competitivo.", ImageUrl = "/images/valorant.jpg" },
            new Game { Id = 14, Title = "Rocket League", Genre = "Deportes", Price = 19.99m, Description = "Fútbol con autos.", ImageUrl = "/images/rocket.jpg" },
            new Game { Id = 15, Title = "Overwatch 2", Genre = "FPS", Price = 39.99m, Description = "Héroes y equipos.", ImageUrl = "/images/overwatch.jpg" },
            new Game { Id = 16, Title = "The Sims 4", Genre = "Simulación", Price = 49.99m, Description = "Crea vidas.", ImageUrl = "/images/sims.jpg" },
            new Game { Id = 17, Title = "Hades", Genre = "Roguelike", Price = 24.99m, Description = "Mitología griega.", ImageUrl = "/images/hades.jpg" },
            new Game { Id = 18, Title = "Stardew Valley", Genre = "Simulación", Price = 14.99m, Description = "Vida en la granja.", ImageUrl = "/images/stardew.jpg" },
            new Game { Id = 19, Title = "Apex Legends", Genre = "Battle Royale", Price = 0.00m, Description = "Lucha por la gloria.", ImageUrl = "/images/apex.jpg" },
            new Game { Id = 20, Title = "Resident Evil Village", Genre = "Terror", Price = 59.99m, Description = "Supervivencia horror.", ImageUrl = "/images/re8.jpg" },
            new Game { Id = 21, Title = "Spider-Man", Genre = "Acción", Price = 49.99m, Description = "Aventura de superhéroe.", ImageUrl = "/images/spiderman.jpg" },
            new Game { Id = 22, Title = "Halo Infinite", Genre = "FPS", Price = 59.99m, Description = "Guerra interestelar.", ImageUrl = "/images/halo.jpg" },
            new Game { Id = 23, Title = "Animal Crossing", Genre = "Simulación", Price = 59.99m, Description = "Vida relajada.", ImageUrl = "/images/animalcrossing.jpg" },
            new Game { Id = 24, Title = "Elden Ring", Genre = "RPG", Price = 69.99m, Description = "Mundo abierto épico.", ImageUrl = "/images/eldenring.jpg" },
            new Game { Id = 25, Title = "God of War Ragnarök", Genre = "Aventura", Price = 79.99m, Description = "Mitología nórdica.", ImageUrl = "/images/gow.jpg" },
            new Game { Id = 26, Title = "Horizon Zero Dawn", Genre = "Aventura", Price = 39.99m, Description = "Mundo post-apocalíptico.", ImageUrl = "/images/horizon.jpg" },
            new Game { Id = 27, Title = "Death Stranding", Genre = "Aventura", Price = 29.99m, Description = "Entrega y conexión.", ImageUrl = "/images/deathstranding.jpg" },
            new Game { Id = 28, Title = "Control", Genre = "Acción", Price = 39.99m, Description = "Agencia secreta.", ImageUrl = "/images/control.jpg" },
            new Game { Id = 29, Title = "Ghostwire: Tokyo", Genre = "Acción", Price = 59.99m, Description = "Terror sobrenatural.", ImageUrl = "/images/ghostwire.jpg" },
            new Game { Id = 30, Title = "Ratchet & Clank", Genre = "Plataformas", Price = 49.99m, Description = "Aventura espacial.", ImageUrl = "/images/ratchet.jpg" },
            new Game { Id = 31, Title = "Uncharted 4", Genre = "Aventura", Price = 39.99m, Description = "Tesoro y acción.", ImageUrl = "/images/uncharted.jpg" },
            new Game { Id = 32, Title = "The Last of Us Part II", Genre = "Aventura", Price = 59.99m, Description = "Historia emocional.", ImageUrl = "/images/tlou2.jpg" }
        };

        public IActionResult Index(string search = "", string genre = "", string priceOrder = "", int page = 1)
        {
agregando-funcionalidad-1
            const int pageSize = 20; // 20 juegos por página
=======
            const int pageSize = 12; // 12 juegos por página
main

            var filteredGames = games.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                filteredGames = filteredGames.Where(g => g.Title.Contains(search, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(genre))
            {
                filteredGames = filteredGames.Where(g => g.Genre == genre);
            }

            if (priceOrder == "asc")
            {
                filteredGames = filteredGames.OrderBy(g => g.Price);
            }
            else if (priceOrder == "desc")
            {
                filteredGames = filteredGames.OrderByDescending(g => g.Price);
            }

            var totalGames = filteredGames.Count();
            var totalPages = (int)Math.Ceiling((double)totalGames / pageSize);
            var gamesOnPage = filteredGames.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.Search = search;
            ViewBag.Genre = genre;
            ViewBag.PriceOrder = priceOrder;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.Genres = games.Select(g => g.Genre).Distinct().ToList();

            return View(gamesOnPage);
        }
    }
}
