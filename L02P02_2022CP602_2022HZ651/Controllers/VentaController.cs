using L02P02_2022CP602_2022HZ651.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace L02P02_2022CP602_2022HZ651.Controllers
{
    public class VentaController : Controller
    {
        private readonly LibreriaContext _context;

        public VentaController(LibreriaContext context)
        {
            _context = context;
        }

        public IActionResult IniciarVenta()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IniciarVenta(Clientes cliente)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Clientes.Add(cliente);
                    await _context.SaveChangesAsync();

                    if (cliente.Id <= 0)
                    {
                        ModelState.AddModelError("", "No se pudo guardar el cliente.");
                        return View(cliente);
                    }

                    var nuevoPedido = new pedido_encabezado
                    {
                        IdCliente = cliente.Id,
                        CantidadLibros = 0,
                        Total = 0,
                        Estado = 'P'
                    };

                    _context.pedido_encabezado.Add(nuevoPedido);
                    await _context.SaveChangesAsync();

                    TempData["Mensaje"] = "Venta iniciada exitosamente.";

                    return RedirectToAction("ListadoLibros", new { pedidoId = nuevoPedido.Id });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al iniciar la venta: " + ex.Message);
                }
            }
            return View(cliente);
        }

        public async Task<IActionResult> ListadoLibros(int pedidoId)
        {
            var libros = await _context.Libros.ToListAsync();
            ViewBag.PedidoId = pedidoId;
            return View(libros);
        }
    }
}
