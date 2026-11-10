using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using MySqlConnector;
using Proyecto_Taller_2.Entidades;

namespace Proyecto_Taller_2.Datos
{
    public class VentaDatos
    {
        public bool RegistrarVentaSP(int idUsuario, int idCliente, int idMetodoPago, string tipoFactura, decimal totalVenta, List<DetalleVenta> listaDetalle, out string mensaje)
        {
            mensaje = string.Empty;
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                try
                {
                    conexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("sp_RegistrarVenta", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("p_id_usuario", idUsuario);
                        cmd.Parameters.AddWithValue("p_id_cliente", idCliente > 0 ? (object)idCliente : DBNull.Value);
                        cmd.Parameters.AddWithValue("p_id_metodo_pago", idMetodoPago);
                        cmd.Parameters.AddWithValue("p_tipo_factura", tipoFactura);
                        cmd.Parameters.AddWithValue("p_total_venta", totalVenta);

                        var jsonItems = listaDetalle.Select(x => string.Format(System.Globalization.CultureInfo.InvariantCulture, "{{\"id_producto\":{0},\"cantidad\":{1},\"precio_unitario\":{2}}}", x.IdProducto, x.Cantidad, x.PrecioUnitario));
                        string jsonProductos = "[" + string.Join(",", jsonItems) + "]";
                        cmd.Parameters.AddWithValue("p_productos_json", jsonProductos);

                        MySqlParameter outIdVenta = new MySqlParameter("p_id_venta_generado", MySqlDbType.Int32) { Direction = ParameterDirection.Output };
                        MySqlParameter outResultado = new MySqlParameter("p_resultado", MySqlDbType.Int32) { Direction = ParameterDirection.Output };
                        MySqlParameter outMensaje = new MySqlParameter("p_mensaje", MySqlDbType.VarChar, 255) { Direction = ParameterDirection.Output };

                        cmd.Parameters.Add(outIdVenta);
                        cmd.Parameters.Add(outResultado);
                        cmd.Parameters.Add(outMensaje);

                        cmd.ExecuteNonQuery();

                        int resultado = Convert.ToInt32(outResultado.Value);
                        mensaje = outMensaje.Value.ToString();

                        return resultado == 1;
                    }
                }
                catch (Exception ex)
                {
                    mensaje = "Error en Datos: " + ex.Message;
                    return false;
                }
            }
        }

        public DataTable ObtenerClientePorId(int idCliente)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();
                string query = "SELECT id_cliente, nombre, apellido, documento FROM cliente WHERE id_cliente = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@id", idCliente);
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd)) { da.Fill(dt); }
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
                string query = "SELECT id_producto, nombre, descripcion, stock, precio_venta FROM producto WHERE id_producto = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@id", idProducto);
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd)) { da.Fill(dt); }
                }
            }
            return dt;
        }

        public DataTable ObtenerHistorialVentas()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();
                string query = @"SELECT v.id_venta AS IdVenta, 
                                        COALESCE((SELECT GROUP_CONCAT(p.nombre SEPARATOR ', ') FROM detalleventa d INNER JOIN producto p ON d.id_producto = p.id_producto WHERE d.id_venta = v.id_venta), 'Sin detalle') AS Descripcion,
                                        CONCAT(v.fecha_venta, ' ', v.hora_venta) AS Fecha, 
                                        COALESCE(CONCAT(c.nombre, ' ', c.apellido), 'Cliente General') AS Cliente, 
                                        v.total_venta AS Total, 
                                        m.nombre AS MetodoPago 
                                 FROM venta v
                                 LEFT JOIN metodo_pago m ON v.id_metodo_pago = m.id_metodo_pago
                                 LEFT JOIN cliente c ON v.id_cliente = c.id_cliente
                                 ORDER BY v.id_venta DESC";

                using (MySqlDataAdapter da = new MySqlDataAdapter(query, conexion))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }

        // MÉTODO NUEVO PARA CONSULTAR CADA PRODUCTO INDEPENDIENTE DE LA VENTA SELECCIONADA
        public DataTable ObtenerDetalleVentaPorId(int idVenta)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();
                string query = @"SELECT p.nombre AS Nombre, 
                                        d.cantidad AS Cantidad, 
                                        d.precio_unitario AS PrecioUnitario, 
                                        (d.cantidad * d.precio_unitario) AS SubTotal 
                                 FROM detalleventa d 
                                 INNER JOIN producto p ON d.id_producto = p.id_producto 
                                 WHERE d.id_venta = @idVenta";
                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idVenta", idVenta);
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public DataTable ListarClientes()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();
                string query = "SELECT id_cliente, nombre, apellido, documento FROM cliente";
                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public DataTable ListarProductos()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();
                string query = "SELECT id_producto, nombre, descripcion, stock, precio_venta FROM producto";
                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
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