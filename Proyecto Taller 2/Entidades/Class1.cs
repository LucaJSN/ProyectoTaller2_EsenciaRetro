using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Taller_2
{
    public class Rol
    {
        public int id_rol { get; set; }
        public string tipo { get; set; }

        public Rol()
        {
            tipo = string.Empty;
        }
    }

    public class Usuario
    {
        public int id_usuario { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string correo { get; set; }
        public string password { get; set; }
        public string telefono { get; set; }
        public int id_direccion{ get; set; }
        public int id_rol { get; set; }
        public DateTime? fecha_baja { get; set; } // El signo '?' indica que puede ser nulo si el usuario está activo
        public DateTime fecha_alta { get; set; }
        public DateTime fecha_modificacion { get; set; }

        public Rol rol { get; set; }
        public Direccion direccion { get; set; }

        // El constructor debe ir ACÁ ADENTRO, antes de cerrar la clase Usuario
        public Usuario()
        {
            nombre = string.Empty;
            apellido = string.Empty;
            correo = string.Empty;
            password = string.Empty;
            telefono = string.Empty;
            rol = new Rol();
            direccion = new Direccion();
        }
    }

    public class Direccion
    {
        public int id_direccion { get; set; }
        public string provincia { get; set; }
        public string ciudad { get; set; }
        public string calle { get; set; }
        public int altura { get; set; }
    }
}