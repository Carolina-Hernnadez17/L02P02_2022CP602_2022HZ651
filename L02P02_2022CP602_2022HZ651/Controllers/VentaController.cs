using L02P02_2022CP602_2022HZ651.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L02P02_2022CP602_2022HZ651.Controllers
{
    public class VentaController : Controller
    {

        private readonly LibreriaContext _context;

        public VentaController(LibreriaContext context)
        {
            _context = context;
        }

        // Iniciar una nueva venta con datos del cliente
        public IActionResult IniciarVenta()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IniciarVenta(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.Clientes.Add(cliente);
                await _context.SaveChangesAsync();

                var nuevoPedido = new PedidoEncabezado
                {
                    IdCliente = cliente.Id,
                    CantidadLibros = 0,
                    Total = 0,
                    Estado = 'P'
                };

                _context.PedidosEncabezado.Add(nuevoPedido);
                await _context.SaveChangesAsync();

                return RedirectToAction("ListadoLibros", new { pedidoId = nuevoPedido.Id });
            }

            return View(cliente);
        }

        // Mostrar lista de libros disponibles
        public async Task<IActionResult> ListadoLibros(int pedidoId)
        {
            var libros = await _context.Libros.Include(l => l.Autor).Include(l => l.Categoria).ToListAsync();
            ViewBag.PedidoId = pedidoId;
            return View(libros);
        }

        // Agregar un libro a un pedido
        [HttpPost]
        public async Task<IActionResult> AgregarLibro(int pedidoId, int libroId)
        {
            var pedido = await _context.PedidosEncabezado.FindAsync(pedidoId);
            var libro = await _context.Libros.FindAsync(libroId);

            if (pedido != null && libro != null)
            {
                var detalle = new PedidoDetalle
                {
                    IdPedido = pedidoId,
                    IdLibro = libroId,
                    CreatedAt = DateTime.Now
                };

                _context.PedidosDetalle.Add(detalle);
                pedido.CantidadLibros++;
                pedido.Total += libro.Precio;
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("ListadoLibros", new { pedidoId });
        }

        // Mostrar resumen de la venta antes de cerrar
        public async Task<IActionResult> CierreVenta(int pedidoId)
        {
            var pedido = await _context.PedidosEncabezado
                .Include(p => p.Cliente)
                .FirstOrDefaultAsync(p => p.Id == pedidoId);

            if (pedido == null)
                return NotFound();

            return View(pedido);
        }

        // Cerrar la venta
        [HttpPost]
        public async Task<IActionResult> CerrarVenta(int pedidoId)
        {
            var pedido = await _context.PedidosEncabezado.FindAsync(pedidoId);

            if (pedido == null)
                return NotFound();

            pedido.Estado = 'C'; 
            await _context.SaveChangesAsync();
            ViewBag.Mensaje = "Venta cerrada con éxito.";

            return View("CierreVenta", pedido);
        }
    }
}

