using L02P02_2022CP602_2022HZ651.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L02P02_2022CP602_2022HZ651.Controllers
{
    public class CerrarVentaController : Controller
    {
        private readonly LibreriaContext _context;

        public CerrarVentaController(LibreriaContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> CerrarVentaa()
        {
            int usuarioId = 2; // Esto puede cambiar según el cliente que necesites buscar.

            // Obtener los datos del cliente
            var cliente = await _context.Clientes
                .Where(c => c.id == usuarioId)
                .Select(c => new
                {
                    c.nombre,
                    c.apellido,
                    c.email,
                    c.direccion
                })
                .FirstOrDefaultAsync();

            if (cliente == null)
            {
                return NotFound();
            }

            // Obtener el encabezado del pedido para el cliente
            var pedidoEncabezado = await _context.pedido_encabezado
                .Where(p => p.id_Cliente == usuarioId)
                .FirstOrDefaultAsync();

            if (pedidoEncabezado == null)
            {
                return NotFound("No se encontró ningún pedido para este cliente.");
            }

            // Obtener todos los detalles del pedido (de todos los libros en el pedido)
            var pedidosDetalle = await _context.pedido_detalle
                .Where(pd => pd.id_pedido == pedidoEncabezado.id)
                .ToListAsync();

            // Asegúrate de que los datos que necesitas se cargan correctamente
            var resultado = new
            {
                cliente,
                pedidoEncabezado,
                pedidosDetalle
            };

            return View(resultado);
        }


    }
}
