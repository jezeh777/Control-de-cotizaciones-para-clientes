using System.Collections.Generic;

namespace Cotizaciones.Models
{
    public class ImprimirModel
    {
        public  CotizacionModel Header { get; set; }
        public List<DetalleProductoModel> Detalle { get; set; }
    }
}
