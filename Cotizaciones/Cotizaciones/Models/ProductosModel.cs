using System.ComponentModel.DataAnnotations;

namespace Cotizaciones.Models
{
    public class ProductosModel
    {
        public int IdPrudcuto { get; set; }

        [Required(ErrorMessage = "El campo Nombre es Obligatorio")]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public int Stock { get; set; }

        public  decimal precio { get; set; }

        public int IdFamilia { get; set; }

    }
}
