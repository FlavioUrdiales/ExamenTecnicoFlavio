
using Microsoft.AspNetCore.Mvc;
using ExamenTecnicoFlavio.Services;
using ExamenTecnicoFlavio.Models;

namespace ExamenTecnicoFlavio.Controllers
{
    public class ProductosController : Controller
    {
        private readonly ProductoService _productoService;

        public ProductosController(ProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var lstProductos = await _productoService.ObtenerTop5Productos();

            return View(lstProductos);
        }

        [HttpGet]
        public async Task<IActionResult> Detalle(int id, int pagina = 1)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var producto = await _productoService.ObtenerOrdenesProducto(id, pagina);

            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        [HttpGet]
        public async Task<IActionResult> EditarTracking(int salesOrderId, int salesOrderDetailId)
        {
            var modelo = await _productoService.ObtenerTracking(salesOrderId, salesOrderDetailId);

            if (modelo == null)
            {
                return NotFound();
            }

            return View(modelo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarTracking( ActualizarTrackingInputModel modelo)
        {
            var detalleActual = await _productoService.ObtenerTracking(modelo.SalesOrderID,modelo.SalesOrderDetailID);

            if (detalleActual == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                detalleActual.CarrierTrackingNumber = modelo.CarrierTrackingNumber;

                return View(detalleActual);
            }

            var actualizado = await _productoService.ActualizarTracking( modelo.SalesOrderID,modelo.SalesOrderDetailID, modelo.CarrierTrackingNumber);

            if (!actualizado)
            {
                return NotFound();
            }

            TempData["MensajeExito"] =  "El número de seguimiento se actualizó correctamente.";

            return RedirectToAction(
                nameof(Detalle),
                new { id = detalleActual.ProductID });
        }
    }
}
