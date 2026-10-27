
namespace ExamenTecnicoFlavio.Models
{
    public class OrdenProducto
    {
        public int SalesOrderID { get; set; }

        public int SalesOrderDetailID { get; set; }

        public DateTime FechaOrden { get; set; }

        public short Cantidad { get; set; }

        public decimal PrecioUnitario { get; set; }

        public string? CarrierTrackingNumber { get; set; }
    }
}
