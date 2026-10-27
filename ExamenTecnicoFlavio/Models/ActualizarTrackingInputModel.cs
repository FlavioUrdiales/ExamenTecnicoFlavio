using System.ComponentModel.DataAnnotations;

namespace ExamenTecnicoFlavio.Models
{
    public class ActualizarTrackingInputModel
    {
        public int SalesOrderID { get; set; }

        public int SalesOrderDetailID { get; set; }

        [StringLength(25, ErrorMessage = "Máximo 25 caracteres.")]
        public string? CarrierTrackingNumber { get; set; }
    }
}
