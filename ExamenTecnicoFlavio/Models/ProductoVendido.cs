namespace ExamenTecnicoFlavio.Models
{
    public class ProductoVendido
    {

        public int ProductID { get; set; }
        public string NombreProducto { get; set; } = string.Empty;
        public int CantidadVendida { get; set; }

        public decimal MontoTotalVendido { get; set; }
    }
}
