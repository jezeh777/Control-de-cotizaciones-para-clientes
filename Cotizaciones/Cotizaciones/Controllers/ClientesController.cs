using Cotizaciones.Datos;
using Cotizaciones.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cotizaciones.Controllers
{
    public class ClientesController : Controller
    {
        ClienteDatos _datos = new ClienteDatos();

        /// <summary>
        /// sirve para mostrar la lista de clientes
        /// </summary>
        /// <returns></returns>
        public IActionResult Listar()
        {
            var lista = _datos.Listar();
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
        public IActionResult Guardar(ClienteModel cliente)
        {
            if (!ModelState.IsValid)
                return View();

            var res = _datos.Guardar(cliente);
            if (res)
                return RedirectToAction("Listar");
            else
                return View();
        }


        public IActionResult Editar(int IdCliente)
        {
            var contacto = _datos.Obtener(IdCliente);
            return View(contacto);
        }


        [HttpPost]
        public IActionResult Editar(ClienteModel cliente)
        {
            if (!ModelState.IsValid)
                return View();

            var res = _datos.Editar(cliente);
            if (res)
                return RedirectToAction("Listar");
            else
                return View();
        }

        public IActionResult Eliminar(int IdCliente)
        {
            var contacto = _datos.Obtener(IdCliente);
            return View(contacto);
        }


        [HttpPost]
        public IActionResult Eliminar(ClienteModel cliente)
        {

            var res = _datos.Eliminar(cliente.IdCliente);
            if (res)
                return RedirectToAction("Listar");
            else
                return View();
        }
    }
}
