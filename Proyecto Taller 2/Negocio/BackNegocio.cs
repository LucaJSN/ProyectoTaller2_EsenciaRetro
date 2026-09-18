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
        public bool GenerarBackup(string rutaArchivo)
        {
            BackDatos datos = new BackDatos();
            return datos.GenerarBackup(rutaArchivo);
        }
    }
}
