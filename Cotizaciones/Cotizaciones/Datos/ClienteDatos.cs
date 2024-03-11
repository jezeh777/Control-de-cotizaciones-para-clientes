using Cotizaciones.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Cotizaciones.Datos
{
    public class ClienteDatos
    {
        public ClienteDatos() { }

        public List<ClienteModel> Listar()
        {
            var lista = new List<ClienteModel>();
            var conn = new Conexion();
            using (var conexion = new SqlConnection(conn.getCadenaSQL()))
            {
                conexion.Open();
                string sql = "SELECT * FROM CLIENTES";
                SqlCommand cmd = new SqlCommand(sql, conexion);
                cmd.CommandType = CommandType.Text;
                using(var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new ClienteModel()
                        {
                            IdCliente = Convert.ToInt32(reader["IDCLIENTE"]),
                            Nombre = reader["NOMBRE"].ToString(),
                            Telefono = reader["TELEFONO"].ToString(),
                            Correo = reader["CORREO"].ToString(),
                        });
                    }
                }

            }
            return lista;
        }

        public ClienteModel Obtener(int idCliente)
        {
            var cliente = new ClienteModel();
            var conn = new Conexion();
            using (var conexion = new SqlConnection(conn.getCadenaSQL()))
            {
                conexion.Open();
                string sql = "SELECT * FROM CLIENTES WHERE IDCLIENTE = @ID";
                SqlCommand cmd = new SqlCommand(sql, conexion);
                cmd.Parameters.AddWithValue("@ID",idCliente);
                cmd.CommandType = CommandType.Text;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cliente.IdCliente = Convert.ToInt32(reader["IDCLIENTE"]);
                        cliente.Nombre = reader["NOMBRE"].ToString();
                        cliente.Telefono = reader["TELEFONO"].ToString();
                        cliente.Correo = reader["CORREO"].ToString();
                        
                    }
                }

            }
            return cliente;
        }


        public bool Guardar(ClienteModel cliente) 
        {
            bool res;
            try
            {
                var conn = new Conexion();
                using (var conexion = new SqlConnection(conn.getCadenaSQL()))
                {
                    conexion.Open();
                    string sql = "INSERT INTO CLIENTES (NOMBRE,TELEFONO,CORREO) VALUES(@NOMBRE,@TELEFONO,@CORREO)";
                    SqlCommand cmd = new SqlCommand(sql, conexion);
                    cmd.Parameters.AddWithValue("@NOMBRE", cliente.Nombre);
                    cmd.Parameters.AddWithValue("@TELEFONO", cliente.Telefono);
                    cmd.Parameters.AddWithValue("@CORREO", cliente.Correo);
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

        public bool Editar(ClienteModel cliente)
        {
            bool res;
            try
            {
                var conn = new Conexion();
                using (var conexion = new SqlConnection(conn.getCadenaSQL()))
                {
                    conexion.Open();
                    string sql = @$"UPDATE CLIENTES SET NOMBRE = @NOMBRE,                    
                                                        TELEFONO = @TELEFONO,                    
                                                        CORREO = @CORREO                    
                                                    WHERE IDCLIENTE = @ID";
                    SqlCommand cmd = new SqlCommand(sql, conexion);
                    cmd.Parameters.AddWithValue("@NOMBRE", cliente.Nombre);
                    cmd.Parameters.AddWithValue("@TELEFONO", cliente.Telefono);
                    cmd.Parameters.AddWithValue("@CORREO", cliente.Correo);
                    cmd.Parameters.AddWithValue("@ID", cliente.IdCliente);
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

        public bool Eliminar(int IdCliente)
        {
            bool res;
            try
            {
                var conn = new Conexion();
                using (var conexion = new SqlConnection(conn.getCadenaSQL()))
                {
                    conexion.Open();
                    string sql = @$"DELETE FROM CLIENTES WHERE IDCLIENTE = @ID";
                    SqlCommand cmd = new SqlCommand(sql, conexion);
                    cmd.Parameters.AddWithValue("@ID", IdCliente);
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
