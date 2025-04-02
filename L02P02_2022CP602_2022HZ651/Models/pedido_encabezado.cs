using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace L02P02_2022CP602_2022HZ651.Models
{
    public class pedido_encabezado
    {
        [Key]
        public int id { get; set; }
        public int id_Cliente { get; set; }
        public int cantidad_libros { get; set; }
        public decimal total { get; set; }
        public char Estado { get; set; } = 'P';

    }
}
