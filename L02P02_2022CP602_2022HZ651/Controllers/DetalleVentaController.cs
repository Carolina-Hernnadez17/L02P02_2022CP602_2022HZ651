using L02P02_2022CP602_2022HZ651.Models;
using Microsoft.AspNetCore.Mvc;

namespace L02P02_2022CP602_2022HZ651.Controllers
{
    public class DetalleVentaController : Controller
    {
        private static List<PedidoDetalle> carrito = new List<PedidoDetalle>();
        private readonly LibreriaContext _context;

        public DetalleVentaController(LibreriaContext context)
        {
            _context = context;
        }

        public IActionResult DetalleVenta()
        {
            var libros = _context.Libros.ToList();
            return View(libros);
        }

        [HttpPost]
        public IActionResult AgregarAlCarrito(int idLibro)
        {
            var libro = _context.Libros.Find(idLibro);
            if (libro == null)
            {
                return NotFound();
            }

            var pedidoDetalle = new PedidoDetalle
            {
                IdLibro = libro.id,
                IdPedido = 1, // Se debe cambiar por un ID de pedido real
            };

            carrito.Add(pedidoDetalle);
            return Json(new { totalLibros = carrito.Count, totalPrecio = carrito.Sum(p => _context.Libros.Find(p.IdLibro).Precio) });
        }

        [HttpPost]
        public IActionResult CompletarCompra()
        {
            // Aquí se puede procesar la compra y guardar en la base de datos
            carrito.Clear(); // Vaciar el carrito después de la compra
            return RedirectToAction("ProcesoVenta", "Venta");
        }
    }
}
