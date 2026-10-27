
namespace ExamenTecnicoFlavio.Models
{
    public class DetalleProductoViewModel
    {
        public int ProductID { get; set; }

        public string NombreProducto { get; set; } = string.Empty;

        public List<OrdenProducto> Ordenes { get; set; } = new();

        public int PaginaActual { get; set; } = 1;

        public int TamanoPagina { get; set; } = 10;

        public int TotalRegistros { get; set; }

        public int TotalPaginas => (int)Math.Ceiling( (double)TotalRegistros / TamanoPagina);
    }
}
