using System.ComponentModel.DataAnnotations;

namespace L02P02_2022CP602_2022HZ651.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }

    }
}
