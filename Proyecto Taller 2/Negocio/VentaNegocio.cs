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
                // Completamos id_metodo_pago, metodo_pago e id_usuario para satisfacer MySQL
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

        public DataTable ObtenerHistorialVentas()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();
                string query = @"SELECT v.id_venta AS IdVenta, 
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
    }
}