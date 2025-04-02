using System.ComponentModel.DataAnnotations;

namespace L02P02_2022CP602_2022HZ651.Models
{
    public class Clientes
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string nombre { get; set; }
        [Required]
        public string apellido { get; set; }
        [Required, EmailAddress]
        public string email { get; set; }
        public string direccion { get; set; }
        public DateTime createdAt { get; set; } = DateTime.Now;

    }
}
