using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaloGames.Models
{
    public class Evento
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = "";
        public string Descripcion { get; set; } = "";

        public DateTime Fecha { get; set; }
        public string Lugar { get; set; } = "";

        public string ImagenUrl { get; set; } = "";

        public decimal PrecioBase { get; set; }

        public int CapacidadTotal { get; set; }
    }
}