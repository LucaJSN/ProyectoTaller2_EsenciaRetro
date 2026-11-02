using System;
using System.Data;
using MySqlConnector;

namespace Proyecto_Taller_2.Negocio
{
    public class VentaNegocio
    {
        public void RegistrarVentaBD(decimal totalVenta, int idMetodoPago, int idUsuario = 1)
        {
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();
                string query = @"INSERT INTO venta (fecha_venta, hora_venta, total_venta, id_metodo_pago, metodo_pago, id_usuario) 
                                VALUES (@fecha, @hora, @total, @metodo, @metodo, @usuario)";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@fecha", DateTime.Now.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@hora", DateTime.Now.ToString("HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@total", totalVenta);
                    cmd.Parameters.AddWithValue("@metodo", idMetodoPago);
                    cmd.Parameters.AddWithValue("@usuario", idUsuario);

                    cmd.ExecuteNonQuery();
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

        /// <summary>
        /// Verifica en la base de datos si una venta ya fue facturada previamente.
        /// </summary>
        /// <param name="idVenta">ID de la venta a consultar</param>
        /// <returns>True si ya posee factura, False en caso contrario.</returns>
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
    }
}