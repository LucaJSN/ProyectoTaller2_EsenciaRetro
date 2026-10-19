using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto_Taller_2.Datos;
using Proyecto_Taller_2.Entidades;

namespace Proyecto_Taller_2.Negocio
{
    internal class CategoriaNegocio
    {
        private CategoriaDatos datos = new CategoriaDatos();

        public List<Categoria> ObtenerTodasLasCategorias()
        {
            return datos.ObtenerTodasLasCategorias();
        }
    }
}
