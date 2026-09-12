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
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Password { get; set; }
        public string Telefono { get; set; }
        public int DireccionId { get; set; }
        public int rol_id { get; set; }
        public DateTime? FechaBaja { get; set; } // El signo '?' indica que puede ser nulo si el usuario está activo

        public Rol Rol { get; set; }
        public Direccion Direccion { get; set; }

        // El constructor debe ir ACÁ ADENTRO, antes de cerrar la clase Usuario
        public Usuario()
        {
            Nombre = string.Empty;
            Apellido = string.Empty;
            Correo = string.Empty;
            Password = string.Empty;
            Telefono = string.Empty;
            Rol = new Rol();
            Direccion = new Direccion();
        }
    }

    public class Direccion
    {
        public int IdDireccion { get; set; }
        public string Provincia { get; set; }
        public string Ciudad { get; set; }
        public string Calle { get; set; }
        public int Altura { get; set; }
    }
}