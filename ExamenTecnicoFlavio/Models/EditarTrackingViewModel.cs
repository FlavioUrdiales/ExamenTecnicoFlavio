
using System.ComponentModel.DataAnnotations;

namespace ExamenTecnicoFlavio.Models
{
    public class EditarTrackingViewModel
    {
        public int SalesOrderID { get; set; }

        public int SalesOrderDetailID { get; set; }

        public int ProductID { get; set; }

        public string NombreProducto { get; set; } = string.Empty;

        public DateTime FechaOrden { get; set; }
        public short Cantidad { get; set; }

        public decimal PrecioUnitario { get; set; }

        [StringLength( 25,ErrorMessage = "El número de seguimiento no puede superar 25 caracteres.")]
        [Display(Name = "Número de seguimiento")]
        public string? CarrierTrackingNumber { get; set; }
    }
}
