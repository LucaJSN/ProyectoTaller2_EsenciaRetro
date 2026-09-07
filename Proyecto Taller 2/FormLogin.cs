using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions; //Para validar con expresiones regulares

namespace Proyecto_Taller_2
{
    public partial class FormLogin : Form
    {
        public Usuario UsuarioAutenticado { get; private set; }
        public FormLogin()
        {
            InitializeComponent();
        }

        private void BtnIngresar_Click(object sender, EventArgs e)
        {
            string correo = TBCorreo.Text.Trim();
            string pass = TBPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(pass))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                UsuarioDAO usuarioDAO = new UsuarioDAO();
                Usuario usuarioLogueado = usuarioDAO.ValidarUsuario(correo, pass);

                if (usuarioLogueado != null)
                {
                    // Login correcto
                    UsuarioAutenticado = usuarioLogueado;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Correo o contraseña incorrectos.", "Error de Autenticación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TBCorreo_TextChanged(object sender, EventArgs e)
        {

        }

        private void TBCorreo_Leave(object sender, EventArgs e)
        {
            // Si está vacío, no hacemos la validación (puedes requerirlo en el botón Guardar)
            if (string.IsNullOrWhiteSpace(TBCorreo.Text)) return;

            // Patrón Regex para validar formato@dominio.algo
            string patron = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            if (!Regex.IsMatch(TBCorreo.Text, patron))
            {
                MessageBox.Show("El formato del correo es inválido. Debe ser ejemplo@dominio.com",
                                "Correo Inválido",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                TBCorreo.Focus(); // Opcional: Mantiene el cursor en el TextBox hasta que lo corrija
            }
        }
    }
}
