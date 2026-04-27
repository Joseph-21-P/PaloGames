using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaloGames.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        public int EventoId { get; set; }
        public Evento Evento { get; set; }

        public string Tipo { get; set; } = "";

        public decimal Precio { get; set; }

        public int Stock { get; set; }
    }
}