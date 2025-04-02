using L02P02_2022CP602_2022HZ651.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L02P02_2022CP602_2022HZ651.Controllers
{
    public class DetalleVentaController : Controller
    {
        private static List<pedido_detalle> carrito = new List<pedido_detalle>();
        private readonly LibreriaContext _context;

        public DetalleVentaController(LibreriaContext context)
        {
            _context = context;
        }

        //public IActionResult DetalleVenta()
        //{
        //    var libros = _context.Libros.ToList();
        //    return View(libros);
        //}

        //[HttpPost]
        //public IActionResult AgregarAlCarrito(int idLibro)
        //{
        //    var libro = _context.Libros.Find(idLibro);
        //    if (libro == null)
        //    {
        //        return NotFound();
        //    }

        //    var pedidoDetalle = new pedido_detalle
        //    {
        //        IdLibro = libro.id,
        //        IdPedido = 1, // Se debe cambiar por un ID de pedido real
        //    };

        //    carrito.Add(pedidoDetalle);
        //    return Json(new { totalLibros = carrito.Count, totalPrecio = carrito.Sum(p => _context.Libros.Find(p.IdLibro).Precio) });
        //}

        // Acción para mostrar los detalles de la venta y el listado de libros
        public IActionResult DetalleVenta(int pedidoId)
        {
            var pedido = _context.pedido_encabezado.Find(pedidoId);
            if (pedido == null)
            {
                return NotFound();
            }

            var libros = _context.Libros.ToList();

            ViewData["PedidoId"] = pedidoId;
            return View(libros);
        }

        [HttpPost]
        public async Task<IActionResult> AgregarAlCarrito(int idLibro, int pedidoId)
        {
            var libro = await _context.Libros.FindAsync(idLibro);
            if (libro == null)
            {
                return NotFound();
            }

            var pedidoDetalle = new pedido_detalle
            {
                IdLibro = libro.id,
                IdPedido = pedidoId 
            };

            _context.pedido_detalle.Add(pedidoDetalle);
            await _context.SaveChangesAsync();

            var totalLibros = _context.pedido_detalle.Count(pd => pd.IdPedido == pedidoId);
            var totalPrecio = _context.pedido_detalle.Where(pd => pd.IdPedido == pedidoId)
                                                    .Sum(pd => _context.Libros.Find(pd.IdLibro).Precio);

            return Json(new { totalLibros, totalPrecio });
        }

        [HttpPost]
        public IActionResult CompletarCompra(int pedidoId)
        {
            var pedido = _context.pedido_encabezado.Find(pedidoId);
            if (pedido == null)
            {
                return NotFound();
            }


            var detalles = _context.pedido_detalle.Where(pd => pd.IdPedido == pedidoId).ToList();
            _context.pedido_detalle.RemoveRange(detalles);
            _context.SaveChanges();

            return RedirectToAction("ProcesoVenta", "Venta");
        }
        

            



        [HttpPost]
        public IActionResult CompletarCompra()
        {
            carrito.Clear(); // Vaciar el carrito después de la compra
            return RedirectToAction("ProcesoVenta", "Venta");
        }
    }
}
