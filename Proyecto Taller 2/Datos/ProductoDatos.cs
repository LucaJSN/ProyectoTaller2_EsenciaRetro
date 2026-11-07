using MySqlConnector;
using Proyecto_Taller_2.Entidades;
using System;
using System.Collections.Generic;

namespace Proyecto_Taller_2.Datos
{
    internal class ProductoDatos
    {
        private string connectionString = "Server=localhost;Port=33060;Database=esenciaretro2.0;Uid=root;Pwd=puntoybarraroot;"; //Luca
        //private string connectionString = "Server=localhost;Port=33060;Database=esenciaretro2.0;Uid=root;Pwd=puntoybarraroot;";//Samu

        public List<Producto> ObtenerTodosLosProductos()
        {
            List<Producto> lista = new List<Producto>();
            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {
                string query = @"SELECT id_producto, nombre, descripcion, stock, precio_costo, precio_venta, talle, edicion, url_imagen, fecha_baja, id_categoria FROM producto";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    conexion.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Producto producto = new Producto
                            {
                                id_producto = Convert.ToInt32(reader["id_producto"]),
                                nombre = reader["nombre"].ToString(),
                                descripcion = reader["descripcion"].ToString(),
                                stock = Convert.ToInt32(reader["stock"]),
                                precio_costo = Convert.ToDecimal(reader["precio_costo"]),
                                precio_venta = Convert.ToDecimal(reader["precio_venta"]),
                                talle = reader["talle"].ToString(),
                                edicion = reader["edicion"].ToString(),
                                url_imagen = reader["url_imagen"] == DBNull.Value ? null : reader["url_imagen"].ToString(),
                                fecha_baja = reader["fecha_baja"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["fecha_baja"]),
                                id_categoria = Convert.ToInt32(reader["id_categoria"])
                            };
                            lista.Add(producto);
                        }
                    }
                }
            }
            return lista;
        }

        public bool InsertarProducto(Producto producto)
        {
            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {
                conexion.Open();
                using (MySqlTransaction transaccion = conexion.BeginTransaction())
                {
                    try
                    {
                        string query = @"INSERT INTO producto (nombre, descripcion, precio_costo, precio_venta, stock, talle, edicion, url_imagen, id_categoria) 
                                         VALUES (@nom, @desc, @preCos, @preVen, @stock, @tal, @edic, @img, @idCat);";

                        using (MySqlCommand cmdProd = new MySqlCommand(query, conexion, transaccion))
                        {
                            cmdProd.Parameters.AddWithValue("@nom", producto.nombre);
                            cmdProd.Parameters.AddWithValue("@desc", producto.descripcion);
                            cmdProd.Parameters.AddWithValue("@preCos", producto.precio_costo);
                            cmdProd.Parameters.AddWithValue("@preVen", producto.precio_venta);
                            cmdProd.Parameters.AddWithValue("@stock", producto.stock);
                            cmdProd.Parameters.AddWithValue("@tal", producto.talle);
                            cmdProd.Parameters.AddWithValue("@edic", producto.edicion);
                            cmdProd.Parameters.AddWithValue("@img", producto.url_imagen ?? (object)DBNull.Value);
                            cmdProd.Parameters.AddWithValue("@idCat", producto.id_categoria);

                            cmdProd.ExecuteNonQuery();
                        }
                        transaccion.Commit();
                        return true;
                    }
                    catch (Exception)
                    {
                        transaccion.Rollback();
                        throw;
                    }
                }
            }
        }

        public bool ActualizarProducto(Producto producto)
        {
            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {
                conexion.Open();
                using (MySqlTransaction transaccion = conexion.BeginTransaction())
                {
                    try
                    {
                        // CORRECCIÓN CRÍTICA: Se agregó el WHERE id_producto = @idProd
                        string queryProd = @"UPDATE producto 
                                             SET nombre = @nom, descripcion = @desc, stock = @stock, 
                                                 precio_costo = @preCos, precio_venta = @preVen, talle = @tal, edicion = @edi, 
                                                 url_imagen = @img, id_categoria = @idCat 
                                             WHERE id_producto = @idProd";

                        using (MySqlCommand cmdProd = new MySqlCommand(queryProd, conexion, transaccion))
                        {
                            cmdProd.Parameters.AddWithValue("@nom", producto.nombre);
                            cmdProd.Parameters.AddWithValue("@desc", producto.descripcion);
                            cmdProd.Parameters.AddWithValue("@stock", producto.stock);
                            cmdProd.Parameters.AddWithValue("@preCos", producto.precio_costo);
                            cmdProd.Parameters.AddWithValue("@preVen", producto.precio_venta);
                            cmdProd.Parameters.AddWithValue("@tal", producto.talle);
                            cmdProd.Parameters.AddWithValue("@edi", producto.edicion);
                            cmdProd.Parameters.AddWithValue("@img", producto.url_imagen ?? (object)DBNull.Value);
                            cmdProd.Parameters.AddWithValue("@idCat", producto.id_categoria);
                            cmdProd.Parameters.AddWithValue("@idProd", producto.id_producto);

                            cmdProd.ExecuteNonQuery();
                        }
                        transaccion.Commit();
                        return true;
                    }
                    catch (Exception)
                    {
                        transaccion.Rollback();
                        throw;
                    }
                }
            }
        }

        // --- MÉTODOS NUEVOS PARA BAJA Y REACTIVACIÓN LÓGICA ---
        public bool BajaProducto(int idProducto)
        {
            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {
                string query = "UPDATE producto SET fecha_baja = NOW() WHERE id_producto = @id;";
                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@id", idProducto);
                    conexion.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
        }

        public void ActivarProducto(int idProducto)
        {
            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {
                string query = "UPDATE producto SET fecha_baja = NULL WHERE id_producto = @id;";
                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@id", idProducto);
                    conexion.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}