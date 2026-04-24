using Microsoft.AspNetCore.Mvc;
using PaloGames.Models;
using PaloGames.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace PaloGames.Controllers
{
    public class CartController : Controller
    {
        // Ver el contenido del carrito
        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            return View(cart);
        }

        // Agregar al carrito
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
            }
    
            return RedirectToAction("Index");
        }
    }
}