using Microsoft.Data.SqlClient;
using System.Data;
using System;
using Cotizaciones.Models;
using System.Collections.Generic;
using System.Collections;

namespace Cotizaciones.Datos
{
    public class DetalleDatos
    {

        public List<DetalleProductoModel> ObtenerP(int idCotizacion)
        {
            var lista = new List<DetalleProductoModel>();
            var conn = new Conexion();
            using (var conexion = new SqlConnection(conn.getCadenaSQL()))
            {
                conexion.Open();
                string sql = @$"SELECT  
                                    P.NOMBRE 'NOMBREPRODUCTO',
                                    CAST(D.CANTIDAD AS INT) 'CANTIDAD',
                                    CAST(D.PRECIO AS DECIMAL(18,2)) 'PRECIO',
                                    CAST(D.TOTAL AS DECIMAL(18,2)) 'TOTAL'
                                FROM COTIZACION_DET D
                                    INNER JOIN PRODUCTO P ON D.IDPRODUCTO = P.IDPRODUCTO
                                WHERE IDCOTIZACION = @ID";
                SqlCommand cmd = new SqlCommand(sql, conexion);
                cmd.Parameters.AddWithValue("@ID", idCotizacion);
                cmd.CommandType = CommandType.Text;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new DetalleProductoModel()
                        {
                            nombreProducto = reader["NOMBREPRODUCTO"].ToString(),
                            Cantidad = Convert.ToInt32(reader["CANTIDAD"]),
                            precio = Convert.ToDecimal(reader["PRECIO"]),
                            Total = Convert.ToDecimal(reader["TOTAL"])
                        });

                    }
                }

            }
            return lista;
        }
        public bool Guardar(DetalleModel detalle)
        {
            bool result = false;
            try
            {
                
                var conn = new Conexion();
                using (var conexion = new SqlConnection(conn.getCadenaSQL()))
                {
                    conexion.Open();
                    string sql = @$"INSERT INTO [dbo].[COTIZACION_DET](IDCOTIZACION,IDPRODUCTO,CANTIDAD,PRECIO,TOTAL)
                                                        VALUES (@IDCOTIZACION,@IDPRODUCTO,@CANTIDAD,@PRECIO,@TOTAL)";
                    SqlCommand cmd = new SqlCommand(sql, conexion);
                    cmd.Parameters.AddWithValue("@IDCOTIZACION", detalle.IdCotizacion);
                    cmd.Parameters.AddWithValue("@IDPRODUCTO", detalle.IdProducto);
                    cmd.Parameters.AddWithValue("@CANTIDAD", detalle.Cantidad);
                    cmd.Parameters.AddWithValue("@PRECIO", detalle.precio);
                    cmd.Parameters.AddWithValue("@TOTAL", detalle.Total);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                result = true;
            }
            catch (Exception)
            {
                result = false;
                throw;
            }
            return result;

        }
    }
}
