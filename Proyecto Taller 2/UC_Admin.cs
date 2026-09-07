using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySqlConnector;
using System.Text.RegularExpressions; //Para validar correo

namespace Proyecto_Taller_2
{
    public partial class UC_Admin : UserControl
    {
        public UC_Admin()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void TBNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo letras, la tecla de borrado (BackSpace) y la barra espaciadora
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true; // Bloquea el caracter
                return;
            }

            // Lógica para permitir solo 1 espacio (máximo 2 palabras)
            if (char.IsWhiteSpace(e.KeyChar))
            {
                TextBox txt = sender as TextBox;

                // Si ya hay un espacio en el texto, o si es el primer caracter, bloqueamos el nuevo espacio
                if (txt.Text.Contains(" ") || txt.Text.Length == 0)
                {
                    e.Handled = true;
                }
            }
        }

        private void UC_Admin_Load(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void TBAltura_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo números y la tecla de borrado
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Bloquea letras y símbolos
            }
        }

        private void TBCalle_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo letras, la tecla de borrado (BackSpace) y la barra espaciadora
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true; // Bloquea el caracter
                return;
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {

        }

        private void TBApellido_TextChanged(object sender, EventArgs e)
        {

        }

        private void TBApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo letras, la tecla de borrado (BackSpace) y la barra espaciadora
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true; // Bloquea el caracter
                return;
            }

            // Lógica para permitir solo 1 espacio (máximo 2 palabras)
            if (char.IsWhiteSpace(e.KeyChar))
            {
                TextBox txt = sender as TextBox;

                // Si ya hay un espacio en el texto, o si es el primer caracter, bloqueamos el nuevo espacio
                if (txt.Text.Contains(" ") || txt.Text.Length == 0)
                {
                    e.Handled = true;
                }
            }
        }

        private void TBTelefono_TextChanged(object sender, EventArgs e)
        {

        }

        private void TBTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo números y la tecla de borrado
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Bloquea letras y símbolos
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

        private void TBProvincia_TextChanged(object sender, EventArgs e)
        {
            
        }
        private void TBProvincia_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo letras, la tecla de borrado (BackSpace) y la barra espaciadora
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true; // Bloquea el caracter
                return;
            }

            // Lógica para permitir solo 1 espacio (máximo 2 palabras)
            if (char.IsWhiteSpace(e.KeyChar))
            {
                TextBox txt = sender as TextBox;

                // Si ya hay un espacio en el texto, o si es el primer caracter, bloqueamos el nuevo espacio
                if (txt.Text.Contains(" ") || txt.Text.Length == 0)
                {
                    e.Handled = true;
                }
            }
        }

        private void TBCiudad_TextChanged(object sender, EventArgs e)
        {

        }

        private void TBCiudad_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo letras, la tecla de borrado (BackSpace) y la barra espaciadora
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true; // Bloquea el caracter
                return;
            }
        }

        private void TBCalle_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
