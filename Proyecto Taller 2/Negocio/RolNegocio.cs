using Proyecto_Taller_2.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Taller_2.Negocio
{
    internal class RolNegocio
    {
        private RolDatos datosRol = new RolDatos();

        public List<Rol> ListarRoles()
        {
            // Aquí podríamos poner lógica extra si fuera necesario, 
            // por ahora solo pasamos la lista a la vista.
            return datosRol.ObtenerRoles();
        }
    }
}
