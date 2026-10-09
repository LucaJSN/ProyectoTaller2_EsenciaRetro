using Proyecto_Taller_2.Datos;
using Proyecto_Taller_2.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Taller_2.Negocio
{
    internal class ProductoNegocio
    {
        public List<Producto> ListarTodosLosProductos()
        {
            ProductoDatos datos = new ProductoDatos();
            return datos.ObtenerTodosLosProductos();
        }
    }
}
