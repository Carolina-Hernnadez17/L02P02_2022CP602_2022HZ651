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
                using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        // Guardar cliente en la BD
                        _context.Clientes.Add(cliente);
                        await _context.SaveChangesAsync();

                        // Crear pedido con estado 'p' (En Proceso)
                        var nuevoPedido = new pedido_encabezado
                        {
                            id_Cliente = cliente.Id,
                            cantidad_libros = 0,
                            total = 0,
                            Estado = 'p' // Tipo char
                        };

                        _context.pedido_encabezado.Add(nuevoPedido);
                        await _context.SaveChangesAsync();

                        // Confirmar la transacción
                        await transaction.CommitAsync();

                        TempData["Mensaje"] = "Venta iniciada exitosamente.";
                        return RedirectToAction("DetalleVenta", "DetalleVenta");
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        ModelState.AddModelError("", "Error al iniciar la venta: " + ex.Message);
                    }
                }
            }

            return View(cliente);
        }


        public async Task<IActionResult> DetalleVenta(int pedidoId)
        {
            var libros = await _context.Libros.ToListAsync();
            ViewBag.PedidoId = pedidoId;
            return View(libros);
        }
    }
}
