using System;
using System.Data;
using MySqlConnector;

namespace Proyecto_Taller_2.Datos
{
    public class DashboardDatos
    {
        public decimal ObtenerRecaudacionDelDia()
        {
            decimal total = 0;
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();
                string query = "SELECT IFNULL(SUM(total_venta), 0) FROM venta WHERE DATE(fecha_venta) = CURDATE()";
                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                        total = Convert.ToDecimal(result);
                }
            }
            return total;
        }

        public int ObtenerCamisetasVendidasDelDia()
        {
            int unidades = 0;
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();
                string query = @"SELECT IFNULL(SUM(d.cantidad), 0) 
                                 FROM detalleventa d
                                 INNER JOIN venta v ON d.id_venta = v.id_venta
                                 WHERE DATE(v.fecha_venta) = CURDATE()";
                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                        unidades = Convert.ToInt32(result);
                }
            }
            return unidades;
        }

        public DataTable ObtenerVentasDelDiaParaGrilla()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();
                string query = @"SELECT v.id_venta AS 'ID Venta', 
                                        p.nombre AS 'Nombre', 
                                        d.cantidad AS 'Cantidad', 
                                        CONCAT(c.nombre, ' ', c.apellido) AS 'Cliente', 
                                        m.nombre AS 'Metodo de Pago', 
                                        d.subtotal AS 'Sub Total'
                                 FROM detalleventa d
                                 INNER JOIN venta v ON d.id_venta = v.id_venta
                                 INNER JOIN producto p ON d.id_producto = p.id_producto
                                 LEFT JOIN cliente c ON v.id_cliente = c.id_cliente
                                 LEFT JOIN metodo_pago m ON v.id_metodo_pago = m.id_metodo_pago
                                 WHERE DATE(v.fecha_venta) = CURDATE()
                                 ORDER BY v.id_venta DESC";

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