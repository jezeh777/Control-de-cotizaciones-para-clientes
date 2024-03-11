using System;

namespace Cotizaciones.Models
{
    public class CotizacionModel
    {
        public int IdCotizacion { get; set; }

        public DateTime Fecha { get; set; }

        public int Cantidad { get; set; }

        public decimal  Total { get; set; }

        public string NombreCliente { get; set; }
    }


}
