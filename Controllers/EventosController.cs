using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaloGames.Data;
using PaloGames.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PaloGames.Controllers
{
    public class EventosController : Controller
    {
        private readonly AppDbContext _context;

        // Inyectamos TU AppDbContext
        public EventosController(AppDbContext context)
        {
            _context = context;
        }

        // Acción principal: Carga la cartelera y aplica el filtro del dropdown
        public async Task<IActionResult> Index(string tipoEvento)
        {
            var eventos = from e in _context.Eventos
                          select e;

            // Si el usuario elige un filtro en el dropdown, lo aplicamos
            if (!string.IsNullOrEmpty(tipoEvento))
            {
                eventos = eventos.Where(e => e.Categoria == tipoEvento);
            }

            // Guardamos el filtro para que el dropdown no se resetee visualmente
            ViewData["FiltroActual"] = tipoEvento;

            // Retornamos la lista de eventos ordenada por fecha
            return View(await eventos.OrderBy(e => e.Fecha).ToListAsync());
        }

        // Acción para ver el detalle de un evento individual
        public async Task<IActionResult> Detalles(int? id)
        {
            if (id == null) return NotFound();

            var evento = await _context.Eventos.FirstOrDefaultAsync(m => m.Id == id);
            if (evento == null) return NotFound();

            return View(evento);
        }
    }
}