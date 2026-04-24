namespace PaloGames.Models
{
    public class CartItem
    {
        public int JuegoId { get; set; }
        public string? Nombre { get; set; } // Agrega el ?
        public decimal Precio { get; set; }
        public string? ImagenUrl { get; set; } // Agrega el ?
        public int Cantidad { get; set; }
    }
}