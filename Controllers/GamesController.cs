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
            new Game { Id = 3, Title = "FIFA 2023", Genre = "Deportes", Price = 69.99m, Description = "El mejor simulador de fútbol.", ImageUrl = "/images/fifa.jpg" }
        };

        public IActionResult Index()
        {
            return View(games);
        }
    }
}
