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
            int usuarioId = 2; 

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

            var pedidoEncabezado = await _context.pedido_encabezado
                .Where(p => p.id_Cliente == usuarioId)
                .FirstOrDefaultAsync();

            if (pedidoEncabezado == null)
            {
                return NotFound("No se encontró ningún pedido para este cliente.");
            }

            var pedidosDetalle = await _context.pedido_detalle
                .Where(pd => pd.id_pedido == pedidoEncabezado.id)
                .ToListAsync();

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
