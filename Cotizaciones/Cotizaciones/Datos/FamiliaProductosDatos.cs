using Cotizaciones.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System;

namespace Cotizaciones.Datos
{
    public class FamiliaProductosDatos
    {
        public List<FamiliaProductosModel> Listar()
        {
            var lista = new List<FamiliaProductosModel>();
            var conn = new Conexion();
            using (var conexion = new SqlConnection(conn.getCadenaSQL()))
            {
                conexion.Open();
                string sql = "SELECT * FROM FAMILIA_PRODUCTO";
                SqlCommand cmd = new SqlCommand(sql, conexion);
                cmd.CommandType = CommandType.Text;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new FamiliaProductosModel()
                        {
                            IdFamilia = Convert.ToInt32(reader["IDFAMILIA"]),
                            Nombre = reader["NOMBRE"].ToString(),
                        });
                    }
                }

            }
            return lista;
        }
    }
}
