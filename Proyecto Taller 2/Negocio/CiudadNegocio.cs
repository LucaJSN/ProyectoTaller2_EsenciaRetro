using Proyecto_Taller_2.Datos;
using Proyecto_Taller_2.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Taller_2.Negocio
{
    internal class CiudadNegocio
    {
        private CiudadDatos datos = new CiudadDatos();

        public List<Ciudad> ListarCiudadesPorProvincia(int idProvincia)
        {
            if (idProvincia <= 0)
                throw new Exception("Debe seleccionar una provincia válida.");

            return datos.ListarCiudadesPorProvincia(idProvincia);
        }

        public Ciudad obtenerCiudad(int idCiud)
        {
            return datos.consultarCiudad(idCiud);
        }
    }
}
