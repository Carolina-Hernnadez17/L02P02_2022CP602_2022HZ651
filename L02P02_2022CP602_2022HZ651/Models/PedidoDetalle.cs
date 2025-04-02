using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace L02P02_2022CP602_2022HZ651.Models
{
    public class PedidoDetalle
    {
        [Key]
        public int Id { get; set; }
        public int IdPedido { get; set; }
        public int IdLibro { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int PedidoEncabezado { get; set; }

        public int Libro { get; set; }

    }
}
