using ExamenTecnicoFlavio.Data;
using ExamenTecnicoFlavio.Models;
using Microsoft.EntityFrameworkCore;


namespace ExamenTecnicoFlavio.Services
{
    public class ProductoService
    {
        private readonly AdventureWorksContext _context;

        public ProductoService(AdventureWorksContext context)
        {
            _context = context;
        }
        public async Task<List<ProductoVendido>> ObtenerTop5Productos()
        {
            return await _context.Products.AsNoTracking().Join(
                    _context.SalesOrderDetails.AsNoTracking(),
                    producto => producto.ProductId,
                    detalle => detalle.ProductId,
                    (producto, detalle) => new
                    {
                        producto.ProductId,
                        producto.Name,
                        detalle.OrderQty,
                        detalle.LineTotal
                    }
                ).GroupBy(x => new
                {
                    x.ProductId,
                    x.Name
                }).Select(grupo => new ProductoVendido
                {
                    ProductID = grupo.Key.ProductId,
                    NombreProducto = grupo.Key.Name,
                    CantidadVendida = grupo.Sum(x => (int)x.OrderQty),
                    MontoTotalVendido = grupo.Sum(x => x.LineTotal)
                }).OrderByDescending(x => x.CantidadVendida).ThenBy(x => x.ProductID).Take(5).ToListAsync();
        }




        public async Task<DetalleProductoViewModel?> ObtenerOrdenesProducto(int productId,int pagina = 1)
        {
            const int tamanoPagina = 15;

            pagina = Math.Max(1, pagina);

            var producto = await _context.Products
                .AsNoTracking()
                .Where(p => p.ProductId == productId)
                .Select(p => new
                {
                    p.ProductId,
                    p.Name
                })
                .FirstOrDefaultAsync();

            if (producto == null)
            {
                return null;
            }

            var consulta = _context.SalesOrderDetails.AsNoTracking().Where(d => d.ProductId == productId);

            var totalRegistros = await consulta.CountAsync();
             
            var totalPaginas = (int)Math.Ceiling((double)totalRegistros / tamanoPagina);

            if (totalPaginas > 0)
            {
                pagina = Math.Min(pagina, totalPaginas);
            }

            var lstOrdenes = await consulta
                .Join(
                    _context.SalesOrderHeaders.AsNoTracking(),
                    detalle => detalle.SalesOrderId,
                    orden => orden.SalesOrderId,
                    (detalle, orden) => new OrdenProducto
                    {
                        SalesOrderID = detalle.SalesOrderId,
                        SalesOrderDetailID = detalle.SalesOrderDetailId,
                        FechaOrden = orden.OrderDate,
                        Cantidad = detalle.OrderQty,
                        PrecioUnitario = detalle.UnitPrice,
                        CarrierTrackingNumber = detalle.CarrierTrackingNumber
                    })
                .OrderByDescending(o => o.FechaOrden)
                .ThenByDescending(o => o.SalesOrderID)
                .ThenBy(o => o.SalesOrderDetailID)
                .Skip((pagina - 1) * tamanoPagina)
                .Take(tamanoPagina)
                .ToListAsync();

            return new DetalleProductoViewModel
            {
                ProductID = producto.ProductId,
                NombreProducto = producto.Name,
                Ordenes = lstOrdenes,
                PaginaActual = pagina,
                TamanoPagina = tamanoPagina,
                TotalRegistros = totalRegistros
            };
        }



        public async Task<EditarTrackingViewModel?> ObtenerTracking(int salesOrderId, int salesOrderDetailId)
        {
            return await _context.SalesOrderDetails
                .AsNoTracking()
                .Where(d =>
                    d.SalesOrderId == salesOrderId &&
                    d.SalesOrderDetailId == salesOrderDetailId)
                .Join(
                    _context.Products.AsNoTracking(),
                    detalle => detalle.ProductId,
                    producto => producto.ProductId,
                    (detalle, producto) => new
                    {
                        detalle,
                        producto
                    })
                .Join(
                    _context.SalesOrderHeaders.AsNoTracking(),
                    datos => datos.detalle.SalesOrderId,
                    orden => orden.SalesOrderId,
                    (datos, orden) => new EditarTrackingViewModel
                    {
                        SalesOrderID = datos.detalle.SalesOrderId,
                        SalesOrderDetailID = datos.detalle.SalesOrderDetailId,
                        ProductID = datos.detalle.ProductId,
                        NombreProducto = datos.producto.Name,
                        FechaOrden = orden.OrderDate,
                        Cantidad = datos.detalle.OrderQty,
                        PrecioUnitario = datos.detalle.UnitPrice,
                        CarrierTrackingNumber = datos.detalle.CarrierTrackingNumber
                    })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> ActualizarTracking(int salesOrderId, int salesOrderDetailId,string? carrierTrackingNumber)
        {
            var detalle = await _context.SalesOrderDetails
                .FirstOrDefaultAsync(d =>
                    d.SalesOrderId == salesOrderId &&
                    d.SalesOrderDetailId == salesOrderDetailId);

            if (detalle == null)
            {
                return false;
            }

            detalle.CarrierTrackingNumber =
                string.IsNullOrWhiteSpace(carrierTrackingNumber)
                    ? null
                    : carrierTrackingNumber.Trim();

            await _context.SaveChangesAsync();

            return true;
        }



    }
}
