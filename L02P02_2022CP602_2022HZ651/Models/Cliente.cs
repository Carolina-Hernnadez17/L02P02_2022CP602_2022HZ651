using System.ComponentModel.DataAnnotations;

namespace L02P02_2022CP602_2022HZ651.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        public string Direccion { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
