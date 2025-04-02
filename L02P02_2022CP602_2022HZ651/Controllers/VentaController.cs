using L02P02_2022CP602_2022HZ651.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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
                        var connection = _context.Database.GetDbConnection();
                        await connection.OpenAsync();

                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = @"
                                INSERT INTO Clientes (Nombre, Apellido, Email, Direccion, CreatedAt) 
                                OUTPUT INSERTED.Id
                                VALUES (@Nombre, @Apellido, @Email, @Direccion, GETDATE())";

                            command.Parameters.Add(new SqlParameter("@Nombre", cliente.nombre));
                            command.Parameters.Add(new SqlParameter("@Apellido", cliente.apellido));
                            command.Parameters.Add(new SqlParameter("@Email", cliente.email));
                            command.Parameters.Add(new SqlParameter("@Direccion", cliente.direccion));

                            var clienteId = (int)await command.ExecuteScalarAsync();

                            command.CommandText = @"
                                INSERT INTO pedido_encabezado (id_Cliente, cantidad_libros, total, Estado) 
                                VALUES (@IdCliente, 0, 0, 'p')";

                            command.Parameters.Clear();
                            command.Parameters.Add(new SqlParameter("@IdCliente", clienteId));

                            await command.ExecuteNonQueryAsync();
                        }

                        await transaction.CommitAsync();

                        TempData["Mensaje"] = "Venta iniciada exitosamente.";
                        return RedirectToAction("ListadoLibros");
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
