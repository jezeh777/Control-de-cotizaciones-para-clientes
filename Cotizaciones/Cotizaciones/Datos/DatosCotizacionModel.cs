using Cotizaciones.Models;
using System.Collections.Generic;

namespace Cotizaciones.Datos
{
    public class DatosCotizacionModel
    {
        public int cantidad { get; set; }

        public decimal total { get; set; }

        public int idCliente { get; set; }

        public List<DetalleModel> lista { get; set; }
    }
}
