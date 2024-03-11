using System.ComponentModel.DataAnnotations;

namespace Cotizaciones.Models
{
    public class ClienteModel
    {
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "El campo Nombre es Obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo Telefono es Obligatorio")]
        public string Telefono  { get; set; }
        [Required(ErrorMessage = "El campo Correo es Obligatorio")]
        public string Correo { get; set; }
    }
}
