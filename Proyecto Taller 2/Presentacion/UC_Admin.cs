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
        private List<Usuario> listaUsuariosGlobal = new List<Usuario>();
        private int idUsuarioSeleccionado = 0;
        private int idDireccionSeleccionada = 0;
        public UC_Admin()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //validamos que el clic no sea en la cabecera de la tabla (fila -1)
            if (e.RowIndex < 0) return;

            if (DGVUsuarios.Columns[e.ColumnIndex].Name == "CEliminar")
            {

                int idUsuario = Convert.ToInt32(DGVUsuarios.Rows[e.RowIndex].Cells["CID"].Value);
                Usuario usuarioSelec = DGVUsuarios.Rows[e.RowIndex].DataBoundItem as Usuario;

                if (usuarioSelec != null)
                {
                    if (usuarioSelec.FechaBaja.HasValue)
                    {
                        MessageBox.Show("El usuario que intenta eliminar ya ha sido eliminado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    DialogResult resultado = MessageBox.Show("¿Estás seguro de dar de baja a este usuario?",
                                                            "Confirmar baja",
                                                            MessageBoxButtons.YesNo,
                                                            MessageBoxIcon.Question);

                    if (resultado == DialogResult.Yes)
                    {
                        try
                        {
                            //llamamos a la capa de negocio para ejecutar la baja lógica
                            UsuarioNegocio negocio = new UsuarioNegocio();
                            bool exito = negocio.DarDeBajaUsuario(usuarioSelec.IdUsuario); //metodo de capa de negocio

                            if (exito)
                            {
                                MessageBox.Show("Usuario dado de baja correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                CargarUsuariosEnGrilla();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ocurrió un error al dar de baja: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            if (DGVUsuarios.Columns[e.ColumnIndex].Name == "CEditar")
            {
                LCuestionario.Text = "Editar Usuario";
                Usuario usuarioSelec = DGVUsuarios.Rows[e.RowIndex].DataBoundItem as Usuario;
                if (usuarioSelec != null)
                {
                    if (usuarioSelec.FechaBaja.HasValue)
                    {
                        MessageBox.Show("El usuario que intenta editar está dado de baja", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        LCuestionario.Text = "Crear Usuario";
                        return;
                    }

                    // 1. Guardamos el ID globalmente para saber que estamos editando
                    idUsuarioSeleccionado = usuarioSelec.IdUsuario;
                    idDireccionSeleccionada = usuarioSelec.Direccion.IdDireccion;

                    // 2. Cargamos los datos en los TextBox para que el usuario los modifique
                    TBNombre.Text = usuarioSelec.Nombre;
                    TBApellido.Text = usuarioSelec.Apellido;
                    TBCorreo.Text = usuarioSelec.Correo;
                    TBTelefono.Text = usuarioSelec.Telefono;
                    CBRol.SelectedValue = usuarioSelec.RolId;
                    TBContraseña.Text = usuarioSelec.Password;

                    // Validar que el objeto Dirección no sea nulo antes de leerlo
                    if (usuarioSelec.Direccion != null)
                    {
                        TBProvincia.Text = usuarioSelec.Direccion.Provincia;
                        TBCiudad.Text = usuarioSelec.Direccion.Ciudad;
                        TBCalle.Text = usuarioSelec.Direccion.Calle;
                        TBAltura.Text = usuarioSelec.Direccion.Altura.ToString();
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
            CargarUsuariosEnGrilla();
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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Capturamos lo que quedó escrito en los TextBox (con los cambios ya hechos)
                Usuario usuario = new Usuario
                {
                    Nombre = TBNombre.Text.Trim(),
                    Apellido = TBApellido.Text.Trim(),
                    Correo = TBCorreo.Text.Trim(),
                    Telefono = TBTelefono.Text.Trim(),
                    RolId = Convert.ToInt32(CBRol.SelectedValue),
                    Password = TBContraseña.Text.Trim()
                };

                Direccion direccion = new Direccion
                {
                    Provincia = TBProvincia.Text.Trim(),
                    Ciudad = TBCiudad.Text.Trim(),
                    Calle = TBCalle.Text.Trim(),
                    Altura = int.TryParse(TBAltura.Text.Trim(), out int alt) ? alt : 0
                };

                UsuarioNegocio negocio = new UsuarioNegocio();

                if (idUsuarioSeleccionado == 0)
                {
                    // --- REGISTRO NUEVO ---
                    bool exito = negocio.RegistrarUsuario(usuario, direccion);
                    if (exito)
                    {
                        MessageBox.Show("Usuario registrado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarUsuariosEnGrilla();
                        LimpiarControles();
                    }
                }
                else
                {
                    // --- EDICIÓN ---
                    usuario.IdUsuario = idUsuarioSeleccionado; // Asignamos el ID del usuario que estamos modificando
                    usuario.DireccionId = idDireccionSeleccionada; //Y también el ID de direccion

                    DialogResult resultado = MessageBox.Show("¿Estás seguro que deseas guardar los cambios?",
                                                            "Confirmar edición",
                                                            MessageBoxButtons.YesNo,
                                                            MessageBoxIcon.Question);

                    if (resultado == DialogResult.Yes)
                    {
                        try
                        {
                            bool exito = negocio.ActualizarUsuario(usuario, direccion);

                            if (exito)
                            {
                                MessageBox.Show("Usuario editado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                CargarUsuariosEnGrilla();
                                LimpiarControles();
                                idUsuarioSeleccionado = 0; // Reiniciamos la variable para volver al modo registro
                                idDireccionSeleccionada = 0;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ocurrió un error al editar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private void CargarUsuariosEnGrilla()
        {
            try
            {
                DGVUsuarios.AutoGenerateColumns = false; //Para que no cree columnas extras de las ya definidas
                UsuarioNegocio negocio = new UsuarioNegocio();

                List<Usuario> listaCompleta = negocio.ListarTodosLosUsuarios();
                List<Usuario> listaFiltrada = listaUsuariosGlobal.ToList();

                string filtro = CBFiltro.Text;

                if (filtro == "Activos")
                {
                    listaFiltrada = listaCompleta.Where(u => !u.FechaBaja.HasValue).ToList();
                }
                else if (filtro == "Inactivos")
                {
                    listaFiltrada = listaCompleta.Where(u => u.FechaBaja.HasValue).ToList();
                }
                else
                {
                    listaFiltrada = listaCompleta;
                }
                string textoBusqueda = TBBuscador.Text.Trim();
                if (!string.IsNullOrEmpty(textoBusqueda))
                {
                    listaFiltrada = listaFiltrada.Where(u =>
                        (u.Nombre != null && u.Nombre.IndexOf(textoBusqueda, StringComparison.OrdinalIgnoreCase) >= 0) ||
                        (u.Apellido != null && u.Apellido.IndexOf(textoBusqueda, StringComparison.OrdinalIgnoreCase) >= 0) ||
                        (u.Correo != null && u.Correo.IndexOf(textoBusqueda, StringComparison.OrdinalIgnoreCase) >= 0)
                    ).ToList(); // <-- ¡El secreto está aquí! Ejecuta el filtro de texto inmediatamente
                }
                DGVUsuarios.DataSource = null;
                DGVUsuarios.DataSource = listaFiltrada;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la grilla de usuarios: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarControles();
            LCuestionario.Text = "Crear Usuario";
        }
        private void LimpiarControles()
        {
            TBNombre.Clear();
            TBApellido.Clear();
            TBCorreo.Clear();
            TBTelefono.Clear();
            TBProvincia.Clear();
            TBCiudad.Clear();
            TBCalle.Clear();
            TBAltura.Clear();
            TBContraseña.Clear();

            // Si el ComboBox tiene elementos, lo devolvemos al inicio (índice 0 o -1)
            if (CBRol.Items.Count > 0)
            {
                CBRol.SelectedIndex = -1;
            }

            // Devolvemos la variable global a su estado inicial de registro
            idUsuarioSeleccionado = 0;
        }

        private void CBFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarUsuariosEnGrilla();
        }

        private void TBBuscador_TextChanged(object sender, EventArgs e)
        {
            CargarUsuariosEnGrilla();
        }
    }
}
