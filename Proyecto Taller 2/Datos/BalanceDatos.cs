using System;
using System.Data;
using MySqlConnector;

namespace Proyecto_Taller_2.Datos
{
    public class BalanceDatos
    {
        // 1. Obtener ventas agrupadas por categoría o colección (Ej: por categoría del producto)
        public DataTable ObtenerVentasPorColeccion(DateTime desde, DateTime hasta)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();
                string query = @"SELECT IFNULL(cat.nombre, 'General') AS Coleccion, SUM(d.cantidad) AS Unidades
                                 FROM detalleventa d
                                 INNER JOIN producto p ON d.id_producto = p.id_producto
                                 LEFT JOIN categoria cat ON p.id_categoria = cat.id_categoria
                                 INNER JOIN venta v ON d.id_venta = v.id_venta
                                 WHERE v.fecha_venta BETWEEN @desde AND @hasta
                                 GROUP BY Coleccion
                                 ORDER BY Unidades DESC";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@desde", desde.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@hasta", hasta.ToString("yyyy-MM-dd"));
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd)) { da.Fill(dt); }
                }
            }
            return dt;
        }

        // 2. Obtener el Top 5 de diseños/productos más vendidos
        public DataTable ObtenerTop5Productos(DateTime desde, DateTime hasta)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();
                string query = @"SELECT p.nombre AS Producto, SUM(d.cantidad) AS TotalCantidad
                                 FROM detalleventa d
                                 INNER JOIN producto p ON d.id_producto = p.id_producto
                                 INNER JOIN venta v ON d.id_venta = v.id_venta
                                 WHERE v.fecha_venta BETWEEN @desde AND @hasta
                                 GROUP BY p.id_producto, p.nombre
                                 ORDER BY TotalCantidad ASC
                                 LIMIT 5"; // ASC para que el gráfico de barras horizontales los ordene bien abajo hacia arriba

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@desde", desde.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@hasta", hasta.ToString("yyyy-MM-dd"));
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd)) { da.Fill(dt); }
                }
            }
            return dt;
        }

        
        public DataTable ObtenerVentasPorMetodoPago(DateTime desde, DateTime hasta)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();
                string query = @"SELECT m.nombre AS Metodo, SUM(v.total_venta) AS TotalRecaudado
                         FROM venta v
                         INNER JOIN metodo_pago m ON v.id_metodo_pago = m.id_metodo_pago
                         WHERE v.fecha_venta BETWEEN @desde AND @hasta
                         GROUP BY m.id_metodo_pago, m.nombre";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@desde", desde.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@hasta", hasta.ToString("yyyy-MM-dd"));
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd)) { da.Fill(dt); }
                }
            }
            return dt;
        }
        public DataTable ObtenerVentasPorMes(DateTime desde, DateTime hasta)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();
                string query = @"SELECT DATE_FORMAT(v.fecha_venta, '%Y-%m') AS AnioMes, 
                                MONTHNAME(v.fecha_venta) AS NombreMes, 
                                SUM(v.total_venta) AS TotalVentas
                         FROM venta v
                         WHERE v.fecha_venta BETWEEN @desde AND @hasta
                         GROUP BY AnioMes, NombreMes
                         ORDER BY AnioMes ASC";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@desde", desde.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@hasta", hasta.ToString("yyyy-MM-dd"));
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd)) { da.Fill(dt); }
                }
            }
            return dt;
        }
    }
}