using MySqlConnector;
using Proyecto_Taller_2.Datos;
using Proyecto_Taller_2.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions; //Para validar correo
using System.Threading.Tasks;
using System.Windows.Forms;

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
            // 1. Validamos que el clic no sea en la cabecera de la tabla (fila -1)
            if (e.RowIndex < 0) return;

            // 2. Verificamos si la columna donde hizo clic es la de eliminar (cambia "eliminar" por el Name de tu columna)
            if (DGVUsuarios.Columns[e.ColumnIndex].Name == "CEliminar")
            {
                // 3. Obtenemos el ID del usuario de esa fila específica
                // (Asegúrate de que la celda 0 o la columna del ID se llame o corresponda al IdUsuario)
                int idUsuario = Convert.ToInt32(DGVUsuarios.Rows[e.RowIndex].Cells["IdUsuario"].Value);

                // 4. Confirmación antes de dar de baja
                DialogResult resultado = MessageBox.Show("¿Estás seguro de dar de baja a este usuario?",
                                                        "Confirmar baja",
                                                        MessageBoxButtons.YesNo,
                                                        MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    try
                    {
                        // 5. Llamamos a la Capa de Negocio para ejecutar la baja lógica
                        UsuarioNegocio negocio = new UsuarioNegocio();
                        bool exito = negocio.DarDeBajaUsuario(idUsuario); // (Este método debes declararlo en tu Negocio pasando al Datos)

                        if (exito)
                        {
                            MessageBox.Show("Usuario dado de baja correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // 6. Refrescamos la grilla para que desaparezca de la vista
                            // CargarUsuariosEnGrilla(); 
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocurrió un error al dar de baja: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
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
            CargarRolesEnComboBox();
        }

        private void CargarRolesEnComboBox()
        {
            try
            {
                RolNegocio negocioRol = new RolNegocio();
                CBRol.DataSource = negocioRol.ListarRoles();
                CBRol.DisplayMember = "tipo";
                CBRol.ValueMember = "id_rol";
                CBRol.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los roles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            // === 1. VALIDACIONES DE LA VISTA (PRIMERO QUE NADA) ===
            if (string.IsNullOrWhiteSpace(TBNombre.Text) || string.IsNullOrWhiteSpace(TBCorreo.Text))
            {
                MessageBox.Show("El nombre y el correo son obligatorios.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Corta acá y no deja seguir
            }

            if (CBRol.SelectedValue == null)
            {
                MessageBox.Show("Debes seleccionar un rol.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 1. Creamos y llenamos la entidad Dirección con los datos de la vista
                Direccion nuevaDireccion = new Direccion
                {
                    Provincia = TBProvincia.Text.Trim(),
                    Ciudad = TBCiudad.Text.Trim(),
                    Calle = TBCalle.Text.Trim(),
                    // Convertimos la altura a entero de forma segura
                    Altura = int.TryParse(TBAltura.Text.Trim(), out int alt) ? alt : 0
                };

                // 2. Creamos y llenamos la entidad Usuario
                Usuario nuevoUsuario = new Usuario
                {
                    Nombre = TBNombre.Text.Trim(),
                    Apellido = TBApellido.Text.Trim(),
                    Correo = TBCorreo.Text.Trim(),
                    Password = TBContraseña.Text, // Asegúrate de que el TextBox de la contraseña se llame así
                    Telefono = TBTelefono.Text.Trim(),
                    // Obtenemos el ID del rol seleccionado en el ComboBox
                    rol_id = Convert.ToInt32(CBRol.SelectedValue)
                };

                // 3. Instanciamos la Capa de Negocio
                // (Asegúrate de agregar el using Proyecto_Taller_2.Negocio; arriba si es necesario)
                UsuarioNegocio negocio = new UsuarioNegocio();

                // 4. Invocamos al método que orquestará el guardado
                bool resultado = negocio.RegistrarUsuario(nuevoUsuario, nuevaDireccion);

                if (resultado)
                {
                    MessageBox.Show("¡Usuario registrado con éxito mediante arquitectura de 4 capas!",
                                    "Éxito",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                    // Opcional: Limpiar los campos y refrescar la grilla de abajo
                    // LimpiarFormulario();
                    // CargarUsuariosEnGrilla();
                }
            }
            catch (Exception ex)
            {
                // Si la capa de Negocio o Datos lanza una excepción (por ejemplo, validaciones vacías o error de MySQL), lo atrapamos acá
                MessageBox.Show("Ocurrió un error: " + ex.Message,
                                "Error de validación",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
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

        private void CBRol_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TBContraseña_TextChanged(object sender, EventArgs e)
        {

        }

        public List<Usuario> ListarTodosLosUsuarios()
        {
            UsuarioDatos datos = new UsuarioDatos();
            return datos.ObtenerTodosLosUsuarios();
        }
    }
}
