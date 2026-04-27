namespace PaloGames.Models
{
    public class Evento
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Categoria { get; set; } // Hackatons, Conciertos, Ferias, etc.
        public DateTime Fecha { get; set; }
        public string Ubicacion { get; set; }
        public decimal Costo { get; set; } 
        public string ImagenUrl { get; set; }
    }
}