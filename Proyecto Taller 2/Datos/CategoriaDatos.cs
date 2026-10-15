using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using Proyecto_Taller_2.Entidades;

namespace Proyecto_Taller_2.Datos
{
    internal class CategoriaDatos
    {
        // Usa el mismo connectionString que tienes en ProductoDatos
        private string connectionString = "Server=localhost;Port=33060;Database=esenciaretro2.0;Uid=root;Pwd=puntoybarraroot;";

        public List<Categoria> ObtenerTodasLasCategorias()
        {
            List<Categoria> lista = new List<Categoria>();

            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {
                // Solo traemos el ID y el Nombre
                string query = "SELECT id_categoria, nombre FROM categoria;";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    conexion.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Categoria cat = new Categoria
                            {
                                id_categoria = Convert.ToInt32(reader["id_categoria"]),
                                nombre = reader["nombre"].ToString()
                            };
                            lista.Add(cat);
                        }
                    }
                }
            }
            return lista;
        }
    }
}
