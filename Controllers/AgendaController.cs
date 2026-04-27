using Microsoft.AspNetCore.Mvc;
using PaloGames.Data;
using PaloGames.Models;
using System.Text.Json;

namespace PaloGames.Controllers
{
    public class AgendaController : Controller
    {
        private readonly AppDbContext _context;

        public AgendaController(AppDbContext context)
        {
            _context = context;
        }

        // Muestra los eventos que el joven ha guardado en su agenda
        public IActionResult Index()
        {
            List<Evento> miAgenda = new List<Evento>();
            var sessionAgenda = HttpContext.Session.GetString("MiAgendaSesion");

            if (!string.IsNullOrEmpty(sessionAgenda))
            {
                miAgenda = JsonSerializer.Deserialize<List<Evento>>(sessionAgenda);
            }

            return View(miAgenda);
        }

        // Acción que se ejecuta al darle clic a "¡Me apunto!"
        public async Task<IActionResult> Agregar(int id)
        {
            var evento = await _context.Eventos.FindAsync(id);
            if (evento == null) return NotFound();

            List<Evento> miAgenda = new List<Evento>();
            var sessionAgenda = HttpContext.Session.GetString("MiAgendaSesion");

            if (!string.IsNullOrEmpty(sessionAgenda))
            {
                miAgenda = JsonSerializer.Deserialize<List<Evento>>(sessionAgenda);
            }

            // Validamos que no se agregue el mismo evento dos veces
            if (!miAgenda.Any(e => e.Id == id))
            {
                miAgenda.Add(evento);
                // Guardamos la lista actualizada en Redis/Memoria
                HttpContext.Session.SetString("MiAgendaSesion", JsonSerializer.Serialize(miAgenda));
            }

            return RedirectToAction("Index"); // Nos manda a ver la agenda
        }

        // Para que el usuario pueda cancelar su asistencia
        public IActionResult Eliminar(int id)
        {
            var sessionAgenda = HttpContext.Session.GetString("MiAgendaSesion");
            if (!string.IsNullOrEmpty(sessionAgenda))
            {
                var miAgenda = JsonSerializer.Deserialize<List<Evento>>(sessionAgenda);
                var eventoAEliminar = miAgenda.FirstOrDefault(e => e.Id == id);
                
                if (eventoAEliminar != null)
                {
                    miAgenda.Remove(eventoAEliminar);
                    HttpContext.Session.SetString("MiAgendaSesion", JsonSerializer.Serialize(miAgenda));
                }
            }
            return RedirectToAction("Index");
        }
    }
}