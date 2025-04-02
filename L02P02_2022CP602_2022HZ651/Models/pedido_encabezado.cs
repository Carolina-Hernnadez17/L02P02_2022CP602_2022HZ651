using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace L02P02_2022CP602_2022HZ651.Models
{
    public class pedido_encabezado
    {
        [Key]
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public int CantidadLibros { get; set; }
        public decimal Total { get; set; }
        public char Estado { get; set; } = 'P';
        public Clientes Cliente { get; set; }

    }
}
