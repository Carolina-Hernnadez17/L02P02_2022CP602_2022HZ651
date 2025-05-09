﻿using L02P02_2022CP602_2022HZ651.Models;
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

        public IActionResult DetalleVenta()
        {
            var libros = _context.Libros.ToList();
            return View(libros);
        }

        [HttpPost]
        public IActionResult AgregarAlCarrito(int idLibro, int idPedido)
        {
            var libro = _context.Libros.Find(idLibro);
            if (libro == null)
            {
                return NotFound();
            }

            var pedidoDetalle = new pedido_detalle
            {
                id_libro = libro.id,
                id_pedido = idPedido,
            };

            carrito.Add(pedidoDetalle);
            return Json(new { totalLibros = carrito.Count, totalPrecio = carrito.Sum(p => _context.Libros.Find(p.id_libro).Precio) });
        }




        [HttpPost]
        public IActionResult CompletarCompra()
        {
            carrito.Clear();
            return RedirectToAction("CerrarVentana", "CerrarVentana");
        }
    }
}