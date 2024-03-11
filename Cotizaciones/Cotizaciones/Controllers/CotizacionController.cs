using Cotizaciones.Datos;
using Cotizaciones.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using System;
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
        List<CotizacionVistaModel> lista = new List<CotizacionVistaModel>();
        CotizacionDatos _cotizacion = new CotizacionDatos();
        DetalleDatos _detalle = new DetalleDatos(); 
        ClienteDatos _clientes = new ClienteDatos();
        public IActionResult hacerCotizacion()
        {
            var familia = _familia.Listar();
            var productos = _productos.Listar();
            var clientes = _clientes.Listar();  
            _hacerCotizacion.Familia = familia;
            _hacerCotizacion.productos = productos;
            _hacerCotizacion.cliente = clientes;
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


        [HttpPost]
        public IActionResult InsertarCotizacion([FromBody] DatosCotizacionModel datos)
        {
            
            var respuesta = _cotizacion.Guardar(datos);
            if(respuesta != 0)
            {
                var urlRedireccion = Url.Action(nameof(ImprimirCotizacion), new { idCotizacion = respuesta });
                return Json(new { success = true, redirectUrl = urlRedireccion });
            }

            return View();
        }


        public IActionResult ImprimirCotizacion(int idCotizacion)
        {
            var header = _cotizacion.Obtener(idCotizacion);
            var detalle = _detalle.ObtenerP(idCotizacion);
            ImprimirModel model = new ImprimirModel();
            model.Header = header;
            model.Detalle = detalle;

            return new ViewAsPdf("ImprimirCotizacion", model)
            {
                FileName = $"Cotizacion {idCotizacion}.pdf",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A4,

            };
        }

        public IActionResult ReporteCotizaciones()
        {
            var lista = _cotizacion.Listar();
            return View(lista);

        }
    }
}