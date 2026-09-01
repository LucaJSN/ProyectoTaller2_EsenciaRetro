using Proyecto_Taller_2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Taller_2
{
    public class Rol
    {
        public int IdRol { get; set; }
        public string Tipo { get; set; }

        public Rol()
        {
            Tipo = string.Empty;
        }
    }

    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Password{ get; set; }
        public string Telefono { get; set; }
        public int DireccionId { get; set; }
        public int RolId { get; set; }

        public Rol Rol { get; set; }

        public Usuario()
        {
            Nombre = string.Empty;
            Apellido = string.Empty;
            Correo = string.Empty;
            Password = string.Empty;
            Telefono = string.Empty;
            Rol = new Rol();
        }
    }
}
