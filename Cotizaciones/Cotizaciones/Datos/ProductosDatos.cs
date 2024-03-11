using Cotizaciones.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System;
using System.Collections;

namespace Cotizaciones.Datos
{
    public class ProductosDatos
    {
        public List<ProductosModel> Listar()
        {
            var lista = new List<ProductosModel>();
            var conn = new Conexion();
            using (var conexion = new SqlConnection(conn.getCadenaSQL()))
            {
                conexion.Open();
                string sql = "SELECT * FROM PRODUCTO";
                SqlCommand cmd = new SqlCommand(sql, conexion);
                cmd.CommandType = CommandType.Text;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new ProductosModel()
                        {
                            IdPrudcuto = Convert.ToInt32(reader["IDPRODUCTO"]),
                            Nombre = reader["NOMBRE"].ToString(),
                            Descripcion = reader["DESCRIPCION"].ToString(),
                            Stock = Convert.ToInt32(reader["STOCK"]),
                            precio = Convert.ToDecimal(reader["PRECIO"]),
                            IdFamilia = Convert.ToInt32(reader["IDFAMILIA"]),
                        });
                    }
                }

            }
            return lista;
        }

        public ProductosModel Obtener(int idProducto)
        {
            var Producto = new ProductosModel();
            var conn = new Conexion();
            using (var conexion = new SqlConnection(conn.getCadenaSQL()))
            {
                conexion.Open();
                string sql = "SELECT * FROM PRODUCTO WHERE IDPRODUCTO = @ID";
                SqlCommand cmd = new SqlCommand(sql, conexion);
                cmd.Parameters.AddWithValue("@ID", idProducto);
                cmd.CommandType = CommandType.Text;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Producto.IdPrudcuto = Convert.ToInt32(reader["IDPRODUCTO"]);
                            Producto.Nombre = reader["NOMBRE"].ToString();
                        Producto.Descripcion = reader["DESCRIPCION"].ToString();
                        Producto.Stock = Convert.ToInt32(reader["STOCK"]);
                        Producto.precio = Convert.ToDecimal(reader["PRECIO"]);
                        Producto.IdFamilia = Convert.ToInt32(reader["IDFAMILIA"]);

                    }
                }

            }
            return Producto;
        }


        public List<ProductosModel> ObtenerPorFamilia(int idFamilia)
        {
            //var Producto = new ProductosModel();
            var lista = new List<ProductosModel>();
            var conn = new Conexion();
            using (var conexion = new SqlConnection(conn.getCadenaSQL()))
            {
                conexion.Open();
                string sql = "SELECT * FROM PRODUCTO WHERE IDFAMILIA = @ID";
                SqlCommand cmd = new SqlCommand(sql, conexion);
                cmd.Parameters.AddWithValue("@ID", idFamilia);
                cmd.CommandType = CommandType.Text;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new ProductosModel()
                        {
                            IdPrudcuto = Convert.ToInt32(reader["IDPRODUCTO"]),
                            Nombre = reader["NOMBRE"].ToString(),
                            Descripcion = reader["DESCRIPCION"].ToString(),
                            Stock = Convert.ToInt32(reader["STOCK"]),
                            precio = Convert.ToDecimal(reader["PRECIO"]),
                            IdFamilia = Convert.ToInt32(reader["IDFAMILIA"]),
                        });
                    }
                }

            }
            return lista;
        }

        public bool Guardar(ProductosModel producto)
        {
            bool res;
            try
            {
                var conn = new Conexion();
                using (var conexion = new SqlConnection(conn.getCadenaSQL()))
                {
                    conexion.Open();
                    string sql = @$"INSERT INTO  PRODUCTO(NOMBRE,DESCRIPCION,STOCK,PRECIO,IDFAMILIA) 
                                                VALUES(@NOMBRE,@DESCRIPCION,@STOCK,@PRECIO,@IDFAMILIA)";
                    SqlCommand cmd = new SqlCommand(sql, conexion);
                    cmd.Parameters.AddWithValue("@NOMBRE", producto.Nombre);
                    cmd.Parameters.AddWithValue("@DESCRIPCION", producto.Descripcion);
                    cmd.Parameters.AddWithValue("@STOCK", producto.Stock);
                    cmd.Parameters.AddWithValue("@PRECIO", producto.precio);
                    cmd.Parameters.AddWithValue("@IDFAMILIA", producto.IdFamilia);
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

        public bool Editar(ProductosModel producto)
        {
            bool res;
            try
            {
                var conn = new Conexion();
                using (var conexion = new SqlConnection(conn.getCadenaSQL()))
                {
                    conexion.Open();
                    string sql = @$"UPDATE PRODUCTO SET NOMBRE =  @NOMBRE, 
                                                        DESCRIPCION = @DESCRIPCION, 
                                                        STOCK = @STOCK,
                                                        PRECIO = @PRECIO,
                                                        IDFAMILIA = @IDFAMILIA
                                                    WHERE IDPRODUCTO = @ID";
                    SqlCommand cmd = new SqlCommand(sql, conexion);
                    cmd.Parameters.AddWithValue("@NOMBRE", producto.Nombre);
                    cmd.Parameters.AddWithValue("@DESCRIPCION", producto.Descripcion);
                    cmd.Parameters.AddWithValue("@STOCK", producto.Stock);
                    cmd.Parameters.AddWithValue("@PRECIO", producto.precio);
                    cmd.Parameters.AddWithValue("@IDFAMILIA", producto.IdFamilia);
                    cmd.Parameters.AddWithValue("@ID", producto.IdPrudcuto);
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

        public bool Eliminar(int IdProducto)
        {
            bool res;
            try
            {
                var conn = new Conexion();
                using (var conexion = new SqlConnection(conn.getCadenaSQL()))
                {
                    conexion.Open();
                    string sql = @$"DELETE FROM PRODUCTO WHERE IDPRODUCTO = @ID";
                    SqlCommand cmd = new SqlCommand(sql, conexion);
                    cmd.Parameters.AddWithValue("@ID", IdProducto);
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
