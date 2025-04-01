using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace L02P02_2022CP602_2022HZ651.Models
{
    public class ComentarioLibro
    {
        [Key]
        public int Id { get; set; }
        public int IdLibro { get; set; }
        public string Comentarios { get; set; }
        public string Usuario { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Libro Libro { get; set; }

    }
}
