using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_Taller_2
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            bool mantenerAplicacionViva = true;
            while (mantenerAplicacionViva)
            {
                // Usamos 'using' para asegurar que la ventana de Login se destruya al terminar
                using (FormLogin login = new FormLogin())
                {
                    if (login.ShowDialog() == DialogResult.OK)
                    {
                        // Se crea el formulario principal pasándole el usuario autenticado
                        Form1 mainForm = new Form1(login.UsuarioAutenticado);

                        // Se ejecuta el formulario principal hasta que el usuario decida salir o cerrar sesión
                        Application.Run(mainForm);

                        // Evaluamos si el usuario solo cerró sesión o si cerró la app completa
                        mantenerAplicacionViva = mainForm.CerroSesionParaReingresar;
                    }
                    else
                    {
                        // Si el usuario cerró la ventana de Login con la "X", finaliza el programa
                        mantenerAplicacionViva = false;
                    }
                }
            }

            using (FormLogin login = new FormLogin())
            {
                if (login.ShowDialog() == DialogResult.OK)
                {
                    Application.Run(new Form1(login.UsuarioAutenticado));
                }
                else
                {
                    Application.Exit();
                }
            }
        }
    }
}
