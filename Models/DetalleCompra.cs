using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaloGames.Models
{
    public class DetalleCompra
    {
        public int Id { get; set; }

        public int CompraId { get; set; }
        public Compra Compra { get; set; }

        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }

        public int Cantidad { get; set; }

        public decimal PrecioUnitario { get; set; }
    }
}