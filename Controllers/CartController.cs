using Microsoft.AspNetCore.Mvc;
using PaloGames.Models;
using PaloGames.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Caching.Distributed; // Necesario para Redis

namespace PaloGames.Controllers
{
    public class CartController : Controller
    {
        private readonly IDistributedCache _cache;

        public CartController(IDistributedCache cache)
        {
            _cache = cache;
        }

        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            return View(cart);
        }

        public IActionResult AddToCart(int id, string nombre, decimal precio, string imagen)
        {
            var cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            var item = cart.FirstOrDefault(x => x.JuegoId == id);

            if (item == null)
            {
                cart.Add(new CartItem { JuegoId = id, Nombre = nombre, Precio = precio, ImagenUrl = imagen, Cantidad = 1 });
            }
            else
            {
                item.Cantidad += 1;
            }

            HttpContext.Session.SetJson("Cart", cart);

            // Guardar monto en Sesión (Redis-backed)
            HttpContext.Session.SetString("UltimoMonto", precio.ToString("0.00"));

            // Invalidar el Caché al registrar nueva solicitud
            _cache.Remove("listado_solicitudes_cache");

            return RedirectToAction("Index");
        }

        public IActionResult Remove(int id)
        {
            var cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            var itemToRemove = cart.FirstOrDefault(x => x.JuegoId == id);
            
            if (itemToRemove != null)
            {
                cart.Remove(itemToRemove);
                HttpContext.Session.SetJson("Cart", cart);
                
                // Invalidar caché al actualizar estado
                _cache.Remove("listado_solicitudes_cache");
            }
    
            return RedirectToAction("Index");
        }
    }
}