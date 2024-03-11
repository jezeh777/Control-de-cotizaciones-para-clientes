namespace Cotizaciones.Models
{
    public class CotizacionVistaModel
    {
        public int IdProducto { get; set; }
        public string  Nombre { get; set; }
        public int cantidad { get; set; }
        public decimal precio  { get; set; }
        public decimal Total { get; set; }
    }
}
