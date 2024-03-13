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

        public FamiliaProductosModel Obtener(int idFamilia)
        {
            var fam = new FamiliaProductosModel();
            var conn = new Conexion();
            using (var conexion = new SqlConnection(conn.getCadenaSQL()))
            {
                conexion.Open();
                string sql = "SELECT * FROM FAMILIA_PRODUCTO WHERE IDFAMILIA = @ID";
                SqlCommand cmd = new SqlCommand(sql, conexion);
                cmd.Parameters.AddWithValue("@ID", idFamilia);
                cmd.CommandType = CommandType.Text;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        fam.IdFamilia = Convert.ToInt32(reader["IDFAMILIA"]);
                        fam.Nombre = reader["NOMBRE"].ToString();
                        

                    }
                }

            }   
            return fam;
        }

        public bool Guardar(FamiliaProductosModel familia)
        {
            bool res;
            try
            {
                var conn = new Conexion();
                using (var conexion = new SqlConnection(conn.getCadenaSQL()))
                {
                    conexion.Open();
                    string sql = "INSERT INTO FAMILIA_PRODUCTO(NOMBRE) VALUES(@NOMBRE)";
                    SqlCommand cmd = new SqlCommand(sql, conexion);
                    cmd.Parameters.AddWithValue("@NOMBRE", familia.Nombre);
                    
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();

                }
                res = true;
            }
            catch (Exception)
            {
                res = false;
                throw;
            }

            return res;
        }

        public bool Eliminar(int IdFamilia)
        {
            bool res;
            try
            {
                var conn = new Conexion();
                using (var conexion = new SqlConnection(conn.getCadenaSQL()))
                {
                    conexion.Open();
                    string sql = @$"DELETE FROM FAMILIA_PRODUCTO WHERE IDFAMILIA = @ID";
                    SqlCommand cmd = new SqlCommand(sql, conexion);
                    cmd.Parameters.AddWithValue("@ID", IdFamilia);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();

                }
                res = true;
            }
            catch (Exception)
            {
                res = false;
                throw;
            }

            return res;
        }
    }
}
