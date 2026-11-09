using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using MySqlConnector;

namespace Proyecto_Taller_2.Negocio
{
    public class VentaNegocio
    {
        
        public void RegistrarVentaConDetalles(decimal totalVenta, int idMetodoPago, int idCliente, int idUsuario, BindingList<UC_Ventas.DetalleVentaItem> carrito)
        {
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();
                using (MySqlTransaction transaccion = conexion.BeginTransaction())
                {
                    try
                    {
                        // 1. Insertar cabecera en la tabla 'venta'
                        string queryVenta = @"INSERT INTO venta (fecha_venta, hora_venta, total_venta, id_metodo_pago, metodo_pago, id_usuario) 
                                    VALUES (@fecha, @hora, @total, @metodo, @metodo, @usuario)";

                        long idVentaGenerada = 0;
                        using (MySqlCommand cmdVenta = new MySqlCommand(queryVenta, conexion, transaccion))
                        {
                            cmdVenta.Parameters.AddWithValue("@fecha", DateTime.Now.ToString("yyyy-MM-dd"));
                            cmdVenta.Parameters.AddWithValue("@hora", DateTime.Now.ToString("HH:mm:ss"));
                            cmdVenta.Parameters.AddWithValue("@total", totalVenta);
                            cmdVenta.Parameters.AddWithValue("@metodo", idMetodoPago);
                            cmdVenta.Parameters.AddWithValue("@usuario", idUsuario);

                            cmdVenta.ExecuteNonQuery();
                            idVentaGenerada = cmdVenta.LastInsertedId;
                        }

                        // 2. Insertar cada producto del carrito en 'detalleventa' y descontar stock
                        foreach (var item in carrito)
                        {
                            string queryDetalle = @"INSERT INTO detalleventa (id_venta, id_producto, descripcion, cantidad, precio_unitario, subtotal) 
                                          VALUES (@idVenta, @idProducto, @descripcion, @cantidad, @precioUnitario, @subtotal)";

                            using (MySqlCommand cmdDetalle = new MySqlCommand(queryDetalle, conexion, transaccion))
                            {
                                cmdDetalle.Parameters.AddWithValue("@idVenta", idVentaGenerada);
                                cmdDetalle.Parameters.AddWithValue("@idProducto", Convert.ToInt32(item.IdProducto));
                                cmdDetalle.Parameters.AddWithValue("@descripcion", item.Descripcion);
                                cmdDetalle.Parameters.AddWithValue("@cantidad", item.Cantidad);
                                cmdDetalle.Parameters.AddWithValue("@precioUnitario", item.PrecioUnitario);
                                cmdDetalle.Parameters.AddWithValue("@subtotal", item.SubTotal);

                                cmdDetalle.ExecuteNonQuery();
                            }

                            // 3. Descontar stock del producto en la tabla 'producto'
                            string queryStock = "UPDATE producto SET stock = stock - @cantidad WHERE id_producto = @idProducto";
                            using (MySqlCommand cmdStock = new MySqlCommand(queryStock, conexion, transaccion))
                            {
                                cmdStock.Parameters.AddWithValue("@cantidad", item.Cantidad);
                                cmdStock.Parameters.AddWithValue("@idProducto", Convert.ToInt32(item.IdProducto));
                                cmdStock.ExecuteNonQuery();
                            }
                        }

                        transaccion.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaccion.Rollback();
                        throw new Exception("Error en la transacción de venta: " + ex.Message);
                    }
                }
            }
        }

        public void EliminarVentaBD(int idVenta)
        {
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();
                string query = "DELETE FROM venta WHERE id_venta = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@id", idVenta);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable ObtenerHistorialVentas()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();
                string query = @"SELECT v.id_venta AS IdVenta, 
                                        'Indumentaria Deportiva Retro' AS Descripcion,
                                        CONCAT(v.fecha_venta, ' ', v.hora_venta) AS Fecha, 
                                        'Cliente General' AS Cliente, 
                                        v.total_venta AS Total, 
                                        m.nombre AS MetodoPago 
                                 FROM venta v
                                 INNER JOIN metodo_pago m ON v.id_metodo_pago = m.id_metodo_pago
                                 ORDER BY v.id_venta DESC";

                using (MySqlDataAdapter da = new MySqlDataAdapter(query, conexion))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public void RegistrarFacturaBD(int idVenta, string numeroFactura, string tipoFactura, decimal montoTotal)
        {
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();
                string query = @"INSERT INTO factura (IdVenta, NumeroFactura, TipoFactura, MontoTotal, Fecha) 
                                VALUES (@idVenta, @numero, @tipo, @monto, CURRENT_TIMESTAMP)";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idVenta", idVenta);
                    cmd.Parameters.AddWithValue("@numero", numeroFactura);
                    cmd.Parameters.AddWithValue("@tipo", tipoFactura);
                    cmd.Parameters.AddWithValue("@monto", montoTotal);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable ObtenerHistorialFacturas()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();
                string query = @"SELECT NumeroFactura AS Numero, 
                                        TipoFactura,
                                        Fecha, 
                                        '26000000' AS DNI, 
                                        'Consumidor Final' AS Cliente, 
                                        MontoTotal 
                                 FROM factura 
                                 ORDER BY IdFactura DESC";

                using (MySqlDataAdapter da = new MySqlDataAdapter(query, conexion))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public bool VentaYaFacturada(int idVenta)
        {
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();
                string query = "SELECT COUNT(*) FROM factura WHERE IdVenta = @idVenta";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idVenta", idVenta);
                    long count = Convert.ToInt64(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        public DataTable ObtenerClientePorId(int idCliente)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();
                string query = "SELECT nombre, apellido, dni FROM cliente WHERE id_cliente = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@id", idCliente);
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public DataTable ObtenerProductoPorId(int idProducto)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();
                string query = "SELECT nombre, descripcion, stock, precio FROM producto WHERE id_producto = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@id", idProducto);
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }
    }
}