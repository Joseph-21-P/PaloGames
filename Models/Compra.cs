using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PaloGames.Models
{
    public class Compra
    {
        [Key]
        public int Id { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;

        public decimal Total { get; set; }

        public List<DetalleCompra> Detalles { get; set; }
    }
}