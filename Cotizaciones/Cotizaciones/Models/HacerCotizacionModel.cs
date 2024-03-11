using System.Collections.Generic;

namespace Cotizaciones.Models
{
    public class HacerCotizacionModel
    {
        public List<FamiliaProductosModel> Familia { get; set; }

        public List<ProductosModel> productos { get; set; }

        public CotizacionModel header { get; set; }

        public List<DetalleModel> Detalle { get; set; }
    }
}
