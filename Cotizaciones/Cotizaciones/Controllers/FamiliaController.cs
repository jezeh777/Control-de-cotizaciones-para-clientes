using Cotizaciones.Datos;
using Cotizaciones.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cotizaciones.Controllers
{
    public class FamiliaController : Controller
    {
        FamiliaProductosDatos _datos = new FamiliaProductosDatos();
        public IActionResult Listado()
        {
            var lista  = _datos.Listar();
            return View(lista);
        }

        /// <summary>
        /// Solo devuelve la vista para guardar los datos del cliente
        /// </summary>
        /// <returns></returns>
        public IActionResult Guardar()
        {

            return View();
        }

        /// <summary>
        /// guarda la informacion en la base de datos
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Guardar(FamiliaProductosModel familia)
        {
            if (!ModelState.IsValid)
                return View();

            var res = _datos.Guardar(familia);
            if (res)
                return RedirectToAction("Listado");
            else
                return View();
        }

        public IActionResult Eliminar(int IdCliente)
        {
            var contacto = _datos.Obtener(IdCliente);
            return View(contacto);
        }


        [HttpPost]
        public IActionResult Eliminar(FamiliaProductosModel familia)
        {

            var res = _datos.Eliminar(familia.IdFamilia);
            if (res)
                return RedirectToAction("Listado");
            else
                return View();
        }
    }
}
