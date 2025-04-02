using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace L02P02_2022CP602_2022HZ651.Models
{
    public class Libro
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string UrlImagen { get; set; }
        public decimal Precio { get; set; }
        public char Estado { get; set; }
        public int IdAutor { get; set; }
        public int Autor { get; set; }
        public int IdCategoria { get; set; }
        public int Categoria { get; set; }

    }
}
