using Cotizaciones.Datos;
using Cotizaciones.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cotizaciones.Controllers
{
    public class ProductosController : Controller
    {
        ProductosDatos _datos = new ProductosDatos();
        FamiliaProductosDatos _faamiliaProductosDatos = new FamiliaProductosDatos();
        GuardarProdcutosModel _guardarProdcutosModel = new GuardarProdcutosModel() ;

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
            var lista  =  _faamiliaProductosDatos.Listar();
            _guardarProdcutosModel.familiaProductos = lista;
            return View(_guardarProdcutosModel);
        }

        /// <summary>
        /// guarda la informacion en la base de datos
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Guardar(GuardarProdcutosModel productos)
        {
            if (!ModelState.IsValid)
                return View();

            var res = _datos.Guardar(productos.Producto);
            if (res)
                return RedirectToAction("Listar");
            else
                return View();
        }


        public IActionResult Editar(int IdProducto)
        {
            var producto = _datos.Obtener(IdProducto);
            var lista = _faamiliaProductosDatos.Listar();
            _guardarProdcutosModel.familiaProductos = lista;
            _guardarProdcutosModel.Producto = producto;
            return View(_guardarProdcutosModel);
        }


        [HttpPost]
        public IActionResult Editar(GuardarProdcutosModel productos)
        {
            if (!ModelState.IsValid)
                return View();

            var res = _datos.Editar(productos.Producto);
            if (res)
                return RedirectToAction("Listar");
            else
                return View();
        }

        public IActionResult Eliminar(int IdProducto)
        {
            var producto = _datos.Obtener(IdProducto);
            return View(producto);
        }


        [HttpPost]
        public IActionResult Eliminar(ProductosModel producto)
        {

            var res = _datos.Eliminar(producto.IdPrudcuto);
            if (res)
                return RedirectToAction("Listar");
            else
                return View();
        }

        

    }
}
