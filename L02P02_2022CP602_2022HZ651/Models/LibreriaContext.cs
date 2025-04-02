using Microsoft.EntityFrameworkCore;

namespace L02P02_2022CP602_2022HZ651.Models
{
    public class LibreriaContext : DbContext
    {
        public LibreriaContext(DbContextOptions<LibreriaContext> options) : base(options)
        {
        }
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Autores> Autores { get; set; }
        public DbSet<Categorias> Categorias { get; set; }
        public DbSet<Libros> Libros { get; set; }
        public DbSet<pedido_encabezado> pedido_encabezado { get; set; }
        public DbSet<pedido_detalle> pedido_detalle { get; set; }
        public DbSet<comentarios_libros> comentarios_libros { get; set; }
    }
}
