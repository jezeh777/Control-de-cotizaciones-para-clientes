namespace Cotizaciones.Models
{
    public class DetalleModel
    {
        public int IdDetalle { get; set; }
        public int IdCotizacion { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal precio { get; set; }
        public decimal Total { get; set; }
    }
}
