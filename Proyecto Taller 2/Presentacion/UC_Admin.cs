using MySqlConnector;
using Proyecto_Taller_2.Datos;
using Proyecto_Taller_2.Entidades;
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
            // Evitar clics en encabezados o fuera de las columnas
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            string columna = DGVUsuarios.Columns[e.ColumnIndex].Name;
            Usuario usuarioSelec = DGVUsuarios.Rows[e.RowIndex].DataBoundItem as Usuario;

            if (usuarioSelec == null) return;

            // =========================
            // ELIMINAR USUARIO
            // =========================
            if (columna == "CEliminar")
            {
                if (usuarioSelec.fecha_baja.HasValue)
                {
                    MessageBox.Show("El usuario que intenta eliminar ya ha sido eliminado.",
                        "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DialogResult resultado = MessageBox.Show(
                    "¿Estás seguro de dar de baja a este usuario?",
                    "Confirmar baja",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    try
                    {
                        UsuarioNegocio negocio = new UsuarioNegocio();
                        bool exito = negocio.DarDeBajaUsuario(usuarioSelec.id_usuario);

                        if (exito)
                        {
                            MessageBox.Show("Usuario dado de baja correctamente.",
                                "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarUsuariosEnGrilla();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocurrió un error al dar de baja: " + ex.Message,
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            // =========================
            // EDITAR USUARIO
            // =========================
            if (columna == "CEditar")
            {
                if (usuarioSelec.fecha_baja.HasValue)
                {
                    MessageBox.Show("El usuario que intenta editar está dado de baja.",
                        "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    LCuestionario.Text = "Crear Usuario";
                    return;
                }

                LCuestionario.Text = "Editar Usuario";

                // Guardamos los identificadores
                idUsuarioSeleccionado = usuarioSelec.id_usuario;

                // Cargamos los datos personales
                TBNombre.Text = usuarioSelec.nombre;
                TBApellido.Text = usuarioSelec.apellido;
                TBCorreo.Text = usuarioSelec.correo;
                TBTelefono.Text = usuarioSelec.telefono;
                CBRol.SelectedValue = usuarioSelec.id_rol;
                TBContraseña.Text = usuarioSelec.password;

                // Cargamos la dirección y sus relaciones
                if (usuarioSelec.direccion != null)
                {
                    CiudadNegocio ciudadNegocio = new CiudadNegocio();
                    idDireccionSeleccionada = usuarioSelec.direccion.id_direccion;
                    TBCalle.Text = usuarioSelec.direccion.calle;
                    TBAltura.Text = usuarioSelec.direccion.altura.ToString();

                    // Consultamos la ciudad para obtener también su provincia
                    Ciudad ciudad = ciudadNegocio.obtenerCiudad(
                        usuarioSelec.direccion.id_ciudad
                    );

                    if (ciudad != null)
                    {
                        // 1. Seleccionamos la provincia
                        CBProvincia.SelectedValue = ciudad.id_provincia;

                        // 2. Cargamos las ciudades de esa provincia
                        CargarCiudades(ciudad.id_provincia);

                        // 3. Seleccionamos la ciudad correspondiente
                        CBCiudad.SelectedValue = ciudad.id_ciudad;
                    }
                }
                else
                {
                    idDireccionSeleccionada = 0;

                    // if (CBProvincia != null) CBProvincia.SelectedIndex = -1;
                    CBCiudad.SelectedIndex = -1;

                    TBCalle.Clear();
                    TBAltura.Clear();
                }
            }

            // =========================
            // REACTIVAR USUARIO
            // =========================
            if (columna == "CReactivar")
            {
                if (usuarioSelec.fecha_baja.HasValue)
                {
                    DialogResult respuesta = MessageBox.Show(
                        $"¿Deseas reactivar al usuario {usuarioSelec.nombre}?",
                        "Confirmar activación",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (respuesta == DialogResult.Yes)
                    {
                        try
                        {
                            UsuarioNegocio negocio = new UsuarioNegocio();
                            negocio.ActivarUsuario(usuarioSelec.id_usuario);

                            MessageBox.Show("Usuario reactivado con éxito.",
                                "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            CargarUsuariosEnGrilla();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ocurrió un error al reactivar: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Este usuario ya se encuentra activo.",
                        "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            // --- CONFIGURACIÓN DE ESTILOS DEL DATAGRIDVIEW ---
            DGVUsuarios.EnableHeadersVisualStyles = false;
            DGVUsuarios.BackgroundColor = Color.White;
            DGVUsuarios.ForeColor = Color.FromArgb(44, 62, 80); // Gris oscuro corporativo

            // Estilo para las celdas de datos
            DGVUsuarios.DefaultCellStyle.BackColor = Color.White;
            DGVUsuarios.DefaultCellStyle.ForeColor = Color.FromArgb(44, 62, 80);
            DGVUsuarios.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219); // Azul moderno al seleccionar
            DGVUsuarios.DefaultCellStyle.SelectionForeColor = Color.White;

            // Estilo para la cabecera (Headers)
            DGVUsuarios.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94); // Azul oscuro institucional
            DGVUsuarios.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            DGVUsuarios.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);

            // Filas alternadas para mayor prolijidad visual
            DGVUsuarios.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            // -------------------------------------------------

            CargarRolesEnComboBox();
            CargarUsuariosEnGrilla();
            CargarProvinciasEnComboBox();
        }
        private void CargarProvinciasEnComboBox()
        {
            try
            {
                // 1. Instanciamos la capa de negocio para traer las provincias
                ProvinciaNegocio negocio = new ProvinciaNegocio();
                List<Provincia> listaProvincias = negocio.ObtenerProvincias();

                // 2. Mapeamos las propiedades de la entidad Provincia al ComboBox
                CBProvincia.DisplayMember = "nombre";      // Lo que ve el usuario en la lista
                CBProvincia.ValueMember = "id_provincia";  // El ID que se guarda en la base de datos

                // 3. Asignamos la fuente de datos
                CBProvincia.DataSource = listaProvincias;

                // 4. Iniciamos el ComboBox sin ninguna provincia seleccionada por defecto
                CBProvincia.SelectedIndex = -1;
            }
            catch (Exception ex) // Se agrega (Exception ex) para que funcione ex.Message
            {
                MessageBox.Show("Error al cargar las provincias: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CargarRolesEnComboBox()
        {
            try
            {
                string connectionString = "Server=localhost;Port=3306;Database=esenciaretro2.0;Uid=root;Pwd=puntoybarraroot;";

                using (MySqlConnection conexion = new MySqlConnection(connectionString))
                {
                    string query = "SELECT id_rol, tipo FROM rol";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        conexion.Open();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            DataTable dt = new DataTable();
                            dt.Load(reader);

                            CBRol.DataSource = dt;
                            CBRol.DisplayMember = "tipo";
                            CBRol.ValueMember = "id_rol";
                            CBRol.SelectedIndex = -1;
                        }
                    }
                }
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
                    nombre = TBNombre.Text.Trim(),
                    apellido = TBApellido.Text.Trim(),
                    correo = TBCorreo.Text.Trim(),
                    telefono = TBTelefono.Text.Trim(),
                    id_rol = Convert.ToInt32(CBRol.SelectedValue),
                    password = TBContraseña.Text.Trim()
                };

                Direccion direccion = new Direccion
                {
                    //provincia = CBProvincia.Text.Trim(),
                    id_ciudad = Convert.ToInt32(CBCiudad.SelectedValue),
                    calle = TBCalle.Text.Trim(),
                    altura = int.TryParse(TBAltura.Text.Trim(), out int alt) ? alt : 0
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
                    usuario.id_usuario = idUsuarioSeleccionado; // Asignamos el ID del usuario que estamos modificando
                    usuario.id_direccion = idDireccionSeleccionada; //Y también el ID de direccion

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
            catch (Exception ex)
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
                    listaFiltrada = listaCompleta.Where(u => !u.fecha_baja.HasValue).ToList();
                }
                else if (filtro == "Inactivos")
                {
                    listaFiltrada = listaCompleta.Where(u => u.fecha_baja.HasValue).ToList();
                }
                else
                {
                    listaFiltrada = listaCompleta;
                }
                string textoBusqueda = TBBuscador.Text.Trim();
                if (!string.IsNullOrEmpty(textoBusqueda))
                {
                    listaFiltrada = listaFiltrada.Where(u =>
                        (u.nombre != null && u.nombre.IndexOf(textoBusqueda, StringComparison.OrdinalIgnoreCase) >= 0) ||
                        (u.apellido != null && u.apellido.IndexOf(textoBusqueda, StringComparison.OrdinalIgnoreCase) >= 0) ||
                        (u.correo != null && u.correo.IndexOf(textoBusqueda, StringComparison.OrdinalIgnoreCase) >= 0)
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
            // Datos personales
            TBNombre.Clear();
            TBApellido.Clear();
            TBCorreo.Clear();
            TBTelefono.Clear();
            TBContraseña.Clear();

            // Dirección
            TBCalle.Clear();
            TBAltura.Clear();

            // Provincia
            if (CBProvincia.Items.Count > 0)
            {
                CBProvincia.SelectedIndex = -1;
            }

            // Ciudad
            CBCiudad.DataSource = null;
            CBCiudad.Items.Clear();
            CBCiudad.SelectedIndex = -1;

            // Rol
            if (CBRol.Items.Count > 0)
            {
                CBRol.SelectedIndex = -1;
            }

            // Reiniciamos los identificadores
            idUsuarioSeleccionado = 0;
            idDireccionSeleccionada = 0;
        }

        private void CBFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarUsuariosEnGrilla();
        }

        private void TBBuscador_TextChanged(object sender, EventArgs e)
        {
            CargarUsuariosEnGrilla();
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void CBCiudad_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CBProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBProvincia.SelectedValue == null)
                return;

            if (int.TryParse(CBProvincia.SelectedValue.ToString(), out int idProvincia))
            {
                CargarCiudades(idProvincia);
            }
        }
        private void CargarCiudades(int idProvincia)
        {
            try
            {
                CiudadNegocio negocio = new CiudadNegocio();

                List<Ciudad> ciudades = negocio.ListarCiudadesPorProvincia(idProvincia);

                CBCiudad.DataSource = null;
                CBCiudad.DataSource = ciudades;

                CBCiudad.DisplayMember = "nombre";
                CBCiudad.ValueMember = "id_ciudad";

                CBCiudad.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar ciudades: " + ex.Message);
            }
        }


    }
}