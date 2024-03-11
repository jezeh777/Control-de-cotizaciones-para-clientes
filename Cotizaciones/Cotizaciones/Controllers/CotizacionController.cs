using Cotizaciones.Datos;
using Cotizaciones.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

namespace Cotizaciones.Controllers
{
    public class CotizacionController : Controller
    {
        FamiliaProductosDatos _familia = new FamiliaProductosDatos();
        ProductosDatos _productos = new ProductosDatos();    
        HacerCotizacionModel _hacerCotizacion = new HacerCotizacionModel();
        List<CotizacionVistaModel> lista =  new List<CotizacionVistaModel>();
        public IActionResult hacerCotizacion()
        {
            var familia = _familia.Listar();
            var productos = _productos.Listar();
            _hacerCotizacion.Familia = familia;
            _hacerCotizacion.productos = productos;
            return View(_hacerCotizacion);
        }


        [HttpPost]
        public JsonResult ProductosPorFamilia(int idFamilia)
        {
            var productos = new List<ProductosModel>();

           if (idFamilia == -1)
            {
                productos = _productos.Listar();
            }
            else
            {
                productos = _productos.ObtenerPorFamilia(idFamilia);

            }
            return Json(productos);
        }

    }
}
