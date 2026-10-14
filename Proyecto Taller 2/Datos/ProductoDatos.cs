using System;
using System.Collections.Generic;
using MySqlConnector;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto_Taller_2.Entidades;

namespace Proyecto_Taller_2.Datos
{
    internal class ProductoDatos
    {
        private string connectionString = "Server=localhost;Port=33060;Database=esenciaretro2.0;Uid=root;Pwd=puntoybarraroot;";
        public List<Producto> ObtenerTodosLosProductos()
        {
            List<Producto> lista = new List<Producto>();

            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {
                // 1. Agregamos 'u.password' al SELECT y usamos LEFT JOIN por mayor seguridad
                string query = @"SELECT p.id_producto, p.nombre, p.descripcion, p.stock, p.precio, p.talle, p.edicion, p.url_imagen, p.fecha_baja, p.id_categoria FROM producto AS p";

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
                                precio = Convert.ToDecimal(reader["precio"]),
                                talle = reader["talle"].ToString(),
                                edicion = reader["edicion"].ToString(),
                                url_imagen = reader["telefono"].ToString(),
                                fecha_baja = reader["fecha_baja"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["fecha_baja"])
                            };
                            lista.Add(producto);
                        }
                    }
                }
            }
            return lista;
        }
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

                    // ---INSERTAR  producto ---
                    string query = @"INSERT INTO producto (nombre, descripcion, precio, stock, talle, edicion, url_imagen, id_categoria) 
                                            VALUES (@nom, @desc, @pre, @stock, @tal, @edic, @img, @idCat);";
                    -
                    using (MySqlCommand cmdProd = new MySqlCommand(query, conexion, transaccion))
                    {
                        cmdProd.Parameters.AddWithValue("@nom", producto.nombre);
                        cmdProd.Parameters.AddWithValue("@desc", producto.descripcion);
                        cmdProd.Parameters.AddWithValue("@pre", producto.precio);
                        cmdProd.Parameters.AddWithValue("@stock", producto.stock);
                        cmdProd.Parameters.AddWithValue("@tal", producto.talle);
                        cmdProd.Parameters.AddWithValue("@edic", producto.edicion);
                        cmdProd.Parameters.AddWithValue("@img", producto.url_imagen);
                        cmdProd.Parameters.AddWithValue("@idCat", producto.id_categoria)

                        cmdProd.ExecuteNonQuery();
                    }

                    transaccion.Commit();
                    return true; // Todo salió bien
                }
                catch (Exception)
                {
                    transaccion.Rollback();
                    throw; // ¡Importante! Lanzamos el error para que la capa de Presentación lo atrape y muestre el MessageBox
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


                    // --- ACTUALIZAR  producto ---
                    string queryProd = @"UPDATE producto 
                                    SET nombre = @nom, descripcion = @desc, stock = @stock, precio = @prec, talle = @tal, edicion = @edi, url_imagen = @img, id_categoria = @idCat";

                    using (MySqlCommand cmdProd = new MySqlCommand(queryProd, conexion, transaccion))
                    {
                        cmdProd.Parameters.AddWithValue("@nom", producto.nombre);
                        cmdProd.Parameters.AddWithValue("@desc", producto.descripcion);
                        cmdProd.Parameters.AddWithValue("@stock", producto.stock);
                        cmdProd.Parameters.AddWithValue("prec", producto.precio);
                        cmdProd.Parameters.AddWithValue("@tal", producto.talle);
                        cmdProd.Parameters.AddWithValue("@edi", producto.edicion);
                        cmdProd.Parameters.AddWithValue("@img", producto.url_imagen);
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
}
