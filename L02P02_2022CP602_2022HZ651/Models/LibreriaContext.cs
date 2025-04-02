using Microsoft.EntityFrameworkCore;

namespace L02P02_2022CP602_2022HZ651.Models
{
    public class LibreriaContext : DbContext
    {
        public LibreriaContext(DbContextOptions<LibreriaContext> options) : base(options)
        {
        }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Libros> Libros { get; set; }
        public DbSet<PedidoEncabezado> PedidosEncabezado { get; set; }
        public DbSet<PedidoDetalle> PedidosDetalle { get; set; }
        public DbSet<ComentarioLibro> ComentariosLibros { get; set; }
    }
}
