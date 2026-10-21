using Proyecto_Taller_2.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Taller_2.Negocio
{
    internal class BackNegocio
    {
        private BackDatos datos = new BackDatos();

        // Agregamos el List<string> aquí
        public bool GenerarBackup(string rutaArchivo, List<string> tablasSeleccionadas)
        {
            // Le pasamos ambos parámetros a la capa de Datos
            return datos.GenerarBackup(rutaArchivo, tablasSeleccionadas);
        }

        // Ya que estamos, agregamos los otros dos puentes que te van a hacer falta:
        public List<string> ObtenerTablas()
        {
            return datos.ObtenerTablas();
        }

        public bool RestaurarBackup(string rutaArchivo)
        {
            return datos.RestaurarBackup(rutaArchivo);
        }
    }
}
