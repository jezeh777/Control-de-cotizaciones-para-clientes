using Cotizaciones.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;

namespace Cotizaciones.Datos
{
    public class CotizacionDatos
    {
        DetalleDatos _detalle = new DetalleDatos();

        public List<CotizacionModel> Listar()
        {
            var lista = new List<CotizacionModel>();
            var conn = new Conexion();
            using (var conexion = new SqlConnection(conn.getCadenaSQL()))
            {
                conexion.Open();
                string sql = @$"SELECT 
                                    CO.IDCOTIZACION,
                                    CO.FECHA,
                                    CO.CANTIDAD,
                                    CO.TOTAL,
                                    CL.NOMBRE
                                FROM COTIZACION CO
                                INNER JOIN CLIENTES CL ON CO.IDCLIENTE = CL.IDCLIENTE";
                SqlCommand cmd = new SqlCommand(sql, conexion);
                cmd.CommandType = CommandType.Text;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new CotizacionModel()
                        {
                            IdCotizacion = Convert.ToInt32(reader["IDCOTIZACION"]),
                            Fecha = Convert.ToDateTime(reader["FECHA"]),
                            Cantidad = Convert.ToInt32(reader["CANTIDAD"]),
                            Total = Convert.ToDecimal(reader["TOTAL"]),
                            NombreCliente = reader["NOMBRE"].ToString(),
                        });
                    }
                }

            }
            return lista;
        }
        public CotizacionModel Obtener(int idCotizacion)
        {
            var coti = new CotizacionModel();
            var conn = new Conexion();
            using (var conexion = new SqlConnection(conn.getCadenaSQL()))
            {
                conexion.Open();
                string sql = @$"SELECT 
                                    CO.IDCOTIZACION,
                                    CO.FECHA,
                                    CO.CANTIDAD,
                                    CO.TOTAL,
                                    CL.NOMBRE
                                FROM COTIZACION CO
                                INNER JOIN CLIENTES CL ON CO.IDCLIENTE = CL.IDCLIENTE
                                WHERE IDCOTIZACION = @ID";
                SqlCommand cmd = new SqlCommand(sql, conexion);
                cmd.Parameters.AddWithValue("@ID", idCotizacion);
                cmd.CommandType = CommandType.Text;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        coti.IdCotizacion = Convert.ToInt32(reader["IDCOTIZACION"]);
                        coti.Fecha = Convert.ToDateTime(reader["FECHA"]);
                        coti.Cantidad = Convert.ToInt32(reader["CANTIDAD"]);
                        coti.Total = Convert.ToDecimal(reader["TOTAL"]);
                        coti.NombreCliente = reader["NOMBRE"].ToString();

                    }
                }

            }
            return coti;
        }
        public int Guardar(DatosCotizacionModel datos)
        {
     
            int nuevoId = 0;
            try
            {
                
                var conn = new Conexion();
                
                using (var conexion = new SqlConnection(conn.getCadenaSQL()))
                {
                    conexion.Open();
                    using (var transaccion = conexion.BeginTransaction())
                    {
                        try
                        {
                            string sql = "INSERT INTO COTIZACION(FECHA,CANTIDAD,TOTAL,IDCLIENTE) VALUES(GETDATE(),@CANTIDAD,@TOTAL,@IDCLIENTE); SELECT SCOPE_IDENTITY();";
                            SqlCommand cmd = new SqlCommand(sql, conexion,transaccion);
                            cmd.Parameters.AddWithValue("@CANTIDAD", datos.cantidad);
                            cmd.Parameters.AddWithValue("@TOTAL", datos.total);
                            cmd.Parameters.AddWithValue("@IDCLIENTE", datos.idCliente);
                            cmd.CommandType = CommandType.Text;
                            nuevoId = Convert.ToInt32(cmd.ExecuteScalar());

                            foreach (DetalleModel model in datos.lista)
                            {
                                model.IdCotizacion = nuevoId;
                                var respuesta = _detalle.Guardar(model);
                                if (!respuesta)
                                    throw new Exception("Error al guardar detalle");

                            }

                            transaccion.Commit();
                            
                        }
                        catch (Exception)
                        {
                            transaccion.Rollback();
                            throw;
                        }
                    }
                   
                    
                }
                return nuevoId;
          
            }
            catch (Exception)
            {
                throw;
            }
          

        }
    }
}
