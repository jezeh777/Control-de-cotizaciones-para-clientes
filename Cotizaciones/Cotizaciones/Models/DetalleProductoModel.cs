namespace Cotizaciones.Models
{
    public class DetalleProductoModel
    {
        public string nombreProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal precio { get; set; }
        public decimal Total { get; set; }
    }
}
