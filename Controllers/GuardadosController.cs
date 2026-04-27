using Microsoft.AspNetCore.Mvc;
using PaloGames.Data;
using PaloGames.Models;
using System.Text.Json;

namespace PaloGames.Controllers
{
    public class GuardadosController : Controller
    {
        private readonly AppDbContext _context;

        public GuardadosController(AppDbContext context)
        {
            _context = context;
        }

        // 1. Ver la lista de eventos que me interesan
        public IActionResult Index()
        {
            List<Evento> misGuardados = new List<Evento>();
            var sessionGuardados = HttpContext.Session.GetString("MisGuardadosSesion");

            if (!string.IsNullOrEmpty(sessionGuardados))
            {
                misGuardados = JsonSerializer.Deserialize<List<Evento>>(sessionGuardados);
            }

            return View(misGuardados);
        }

        // 2. Agregar un evento a "Me Interesa"
        public async Task<IActionResult> Agregar(int id)
        {
            var evento = await _context.Eventos.FindAsync(id);
            if (evento == null) return NotFound();

            List<Evento> misGuardados = new List<Evento>();
            var sessionGuardados = HttpContext.Session.GetString("MisGuardadosSesion");

            if (!string.IsNullOrEmpty(sessionGuardados))
            {
                misGuardados = JsonSerializer.Deserialize<List<Evento>>(sessionGuardados);
            }

            // Validar que no se agregue dos veces
            if (!misGuardados.Any(e => e.Id == id))
            {
                misGuardados.Add(evento);
                HttpContext.Session.SetString("MisGuardadosSesion", JsonSerializer.Serialize(misGuardados));
            }

            return RedirectToAction("Index"); 
        }

        // 3. Eliminar de la lista de "Me Interesa"
        public IActionResult Eliminar(int id)
        {
            var sessionGuardados = HttpContext.Session.GetString("MisGuardadosSesion");
            if (!string.IsNullOrEmpty(sessionGuardados))
            {
                var misGuardados = JsonSerializer.Deserialize<List<Evento>>(sessionGuardados);
                var eventoAEliminar = misGuardados.FirstOrDefault(e => e.Id == id);
                
                if (eventoAEliminar != null)
                {
                    misGuardados.Remove(eventoAEliminar);
                    HttpContext.Session.SetString("MisGuardadosSesion", JsonSerializer.Serialize(misGuardados));
                }
            }
            return RedirectToAction("Index");
        }

        // 4. LA MAGIA: Mover de "Me Interesa" a "Mi Agenda"
        public IActionResult MoverAAgenda(int id)
        {
            // Primero lo eliminamos de Guardados
            Eliminar(id);

            // Luego llamamos al controlador de la Agenda para que lo agregue allá
            return RedirectToAction("Agregar", "Agenda", new { id = id });
        }
    }
}