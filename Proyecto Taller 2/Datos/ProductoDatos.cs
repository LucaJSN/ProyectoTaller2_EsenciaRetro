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
                string query = @"SELECT p.id_producto, p.nombre, p.precio, p.talle, p.edicion, p.url_imagen, p.fecha_baja FROM producto AS p";

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
                                precio = Convert.ToDecimal(reader["id_rol"]),
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
}
