using MySqlConnector;
using Proyecto_Taller_2.Entidades;
using Proyecto_Taller_2.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Mysqlx.Crud.Order.Types;

namespace Proyecto_Taller_2
{
    public partial class UC_Productos : UserControl
    {
        private string rutaImagen = null;
        private List<Producto> listaProductosGlobal = new List<Producto>();
        private int idProductoSeleccionado = 0;
        public UC_Productos()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DGVProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //validamos que el clic no sea en la cabecera de la tabla (fila -1)
            if (e.RowIndex < 0) return;

            if (DGVProductos.Columns[e.ColumnIndex].Name == "CEliminar")
            {

                int idProducto = Convert.ToInt32(DGVProductos.Rows[e.RowIndex].Cells["CID"].Value);
                Producto productoSelec = DGVProductos.Rows[e.RowIndex].DataBoundItem as Producto;
                
                if (productoSelec != null)
                {
                    if (productoSelec.fecha_baja.HasValue)
                    {
                        MessageBox.Show("El producto que intenta eliminar ya ha sido eliminado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    DialogResult resultado = MessageBox.Show("¿Estás seguro de dar de baja a este producto?",
                                                            "Confirmar baja",
                                                            MessageBoxButtons.YesNo,
                                                            MessageBoxIcon.Question);

                    if (resultado == DialogResult.Yes)
                    {
                        try
                        {
                            //llamamos a la capa de negocio para ejecutar la baja lógica
                            ProductoNegocio negocio = new ProductoNegocio();
                            bool exito = negocio.BajaProducto(productoSelec.id_producto); //metodo de capa de negocio

                            if (exito)
                            {
                                MessageBox.Show("Producto dado de baja correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                CargarProductosEnGrilla();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ocurrió un error al dar de baja: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            if (DGVProductos.Columns[e.ColumnIndex].Name == "CEditar")
            {
                //LCuestionario.Text = "Editar Producto";
                Producto productoSelec = DGVProductos.Rows[e.RowIndex].DataBoundItem as Producto;
                if (productoSelec != null)
                {
                    if (productoSelec.fecha_baja.HasValue)
                    {
                        MessageBox.Show("El Producto que intenta editar está dado de baja", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //LCuestionario.Text = "Crear Producto";
                        return;
                    }

                    // 1. Guardamos el ID globalmente para saber que estamos editando
                    idProductoSeleccionado = productoSelec.id_producto;

                    // 2. Cargamos los datos en los TextBox para que el producto los modifique
                    TBNombre.Text = productoSelec.nombre;
                    TBDescripcion.Text = productoSelec.descripcion;
                    NUDCosto.Value = productoSelec.precio; //Fijate si este o el de precioVenta
                    NUDCantidad.Value = productoSelec.stock;
                    CBTalle.SelectedValue = productoSelec.talle;
                    if (!string.IsNullOrEmpty(productoSelec.url_imagen))
                    {
                        try
                        {
                            // 2. Transformamos la ruta de texto en una imagen gráfica de forma segura
                            using (var stream = new System.IO.FileStream(productoSelec.url_imagen, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                            {
                                pictureBox1.Image = System.Drawing.Image.FromStream(stream);
                            }
                        }
                        catch (Exception)
                        {
                            // Si la imagen fue movida o borrada de tu PC accidentalmente, dejamos el cuadro vacío
                            pictureBox1.Image = null;
                        }
                    }
                    else
                    {
                        // Si el producto no tiene foto en la base de datos, limpiamos el PictureBox
                        pictureBox1.Image = null;
                    }
                }
            }
            if (DGVProductos.Columns[e.ColumnIndex].Name == "CReactivar") // Asumiendo que el botón se llama CActivar
            {
                Producto productoSelec = DGVProductos.Rows[e.RowIndex].DataBoundItem as Producto;

                if (productoSelec != null)
                {
                    // Verificamos si realmente está inactivo antes de hacer el viaje a la BD
                    if (productoSelec.fecha_baja.HasValue)
                    {
                        DialogResult respuesta = MessageBox.Show($"¿Deseas reactivar al Producto {productoSelec.nombre}?", "Confirmar activación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (respuesta == DialogResult.Yes)
                        {
                            ProductoNegocio negocio = new ProductoNegocio();
                            negocio.ActivarProducto(productoSelec.id_producto);

                            MessageBox.Show("Producto reactivado con éxito.");

                            // Recargamos la grilla para que el Producto vuelva a aparecer como Activo
                            CargarProductosEnGrilla();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Este Producto ya se encuentra activo.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }


        private void LimpiarFormulario()
        {
            TBNombre.Clear();
            TBDescripcion.Clear();
            NUDCosto.Value = 0;
            NUDVenta.Value = 0;
            NUDCantidad.Value = 0;
            pictureBox1.Image = null;
            pictureBox1.ImageLocation = null;
            TBNombre.Focus();
        }

        private void UC_Productos_Load(object sender, EventArgs e)
        {
            try
            {

                CategoriaNegocio negocioCat = new CategoriaNegocio();

                // Asignamos la lista de la BD al ComboBox
                CBCategoria.DataSource = negocioCat.ObtenerTodasLasCategorias();

                // Le decimos qué propiedad de la clase Categoria debe mostrar visualmente
                CBCategoria.DisplayMember = "nombre";

                // Le decimos qué propiedad usar como valor interno (el que falla ahora)
                CBCategoria.ValueMember = "id_categoria";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las categorías: " + ex.Message);
            }

            DGVProductos.DefaultCellStyle.ForeColor = Color.Black;
            CargarProductosEnGrilla();

            // Le decimos qué propiedad de la clase Categoria debe mostrar visualmente
            CBCategoria.DisplayMember = "nombre";

            // Le decimos qué propiedad usar como valor interno (el que falla ahora)
            CBCategoria.ValueMember = "id_categoria";

            
            // Configuración de estilos y colores para asegurar legibilidad y uniformidad con las demás vistas
            DGVProductos.EnableHeadersVisualStyles = false;
            DGVProductos.BackgroundColor = Color.White;
            DGVProductos.ForeColor = Color.FromArgb(44, 62, 80); // Gris oscuro corporativo

            // Estilo para las celdas de datos
            DGVProductos.DefaultCellStyle.BackColor = Color.White;
            DGVProductos.DefaultCellStyle.ForeColor = Color.FromArgb(44, 62, 80);
            DGVProductos.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219); // Azul moderno de selección
            DGVProductos.DefaultCellStyle.SelectionForeColor = Color.White;

            // Estilo para la cabecera (Headers)
            DGVProductos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94); // Azul oscuro institucional
            DGVProductos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            DGVProductos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);

            // Filas alternadas para mayor prolijidad visual
            DGVProductos.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
        }

        private void CargarProductosEnGrilla()
        {
            try
            {
                ProductoNegocio negocio = new ProductoNegocio();
                listaProductosGlobal = negocio.ListarTodosLosProductos();
                AplicarFiltros();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos: " + ex.Message);
            }
        }
        private void AplicarFiltros()
        {
            if (listaProductosGlobal == null || listaProductosGlobal.Count == 0) return;

            // Forzamos la memoria creando una lista real
            List<Producto> listaFiltrada = listaProductosGlobal.ToList();

            // B. FILTRO POR TEXTO DINÁMICO
            string textoBusqueda = TBBuscador.Text.Trim();
            if (!string.IsNullOrEmpty(textoBusqueda))
            {
                listaFiltrada = listaFiltrada.Where(p =>
                    (p.nombre != null && p.nombre.IndexOf(textoBusqueda, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (p.descripcion != null && p.descripcion.IndexOf(textoBusqueda, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (p.talle != null && p.talle.IndexOf(textoBusqueda, StringComparison.OrdinalIgnoreCase) >= 0)
                ).ToList();
            }

            // C. ASIGNAR A LA GRILLA
            DGVProductos.DataSource = null;
            DGVProductos.DataSource = listaFiltrada;
        }
        private void TBBuscador_TextChanged(object sender, EventArgs e)
        {
            AplicarFiltros();
        }
        private void CBFiltroEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        private void LPrecio_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void TBNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void TBNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Space)
            {
                e.Handled = true;
            }
        }

        private void TBDescripcion_TextChanged(object sender, EventArgs e)
        {

        }

        private void TBDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Space)
            {
                e.Handled = true;
            }
        }

        private void TBCosto_TextChanged(object sender, EventArgs e)
        {

        }

        private void TBCosto_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Delete)
            {
                if (e.KeyChar == ',' || e.KeyChar == '.')
                {
                    e.KeyChar = ',';

                    if (textBox.Text.Contains(",") || textBox.SelectionStart == 0)
                    {
                        e.Handled = true;
                    }
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void TBVenta_TextChanged(object sender, EventArgs e)
        {

        }

        private string rutaImagenSeleccionada = string.Empty;
        private void BtnImagen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.bmp";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    rutaImagen = ofd.FileName;

                    try
                    {
                        // Usar FileStream es más seguro y no bloquea el archivo original en el disco
                        using (var stream = new System.IO.FileStream(rutaImagen, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                        {
                            pictureBox1.Image = System.Drawing.Image.FromStream(stream);
                            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                        }
                    }
                    catch (OutOfMemoryException)
                    {
                        // Si el formato es inválido o no soportado, atrapamos el error engañoso aquí
                        MessageBox.Show("El archivo seleccionado no es una imagen válida o el formato no es compatible con el sistema.", "Formato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        rutaImagen = null; // Limpiamos la variable para que no guarde basura en la BD
                        pictureBox1.Image = null;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocurrió un error al cargar la imagen: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        rutaImagen = null;
                    }
                }
            }
        }

        private void BCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }



        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                Producto producto = new Producto
                {
                    nombre = TBNombre.Text.Trim(),

                    descripcion = TBDescripcion.Text.Trim(),

                    precio = NUDCosto.Value,
                    stock = (int)NUDCantidad.Value,


                    talle = CBTalle.Text,

                    edicion = "",

                    id_categoria = Convert.ToInt32(CBCategoria.SelectedValue),

                
                    url_imagen = rutaImagen
                };

                // Instanciamos la capa de negocio y guardamos
                ProductoNegocio negocio = new ProductoNegocio();
                if(idProductoSeleccionado == 0)
                {
                    if (negocio.InsertarProducto(producto))
                    {
                        MessageBox.Show("Producto agregado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Recargamos la grilla para ver el nuevo producto
                        CargarProductosEnGrilla();

                        // Opcional: Crear un método LimpiarCampos() para vaciar los TextBox después de guardar
                    }
                }
                else
                {
                    // --- EDICIÓN ---
                    producto.id_producto = idProductoSeleccionado; // Asignamos el ID del producto que estamos modificando

                    DialogResult resultado = MessageBox.Show("¿Estás seguro que deseas guardar los cambios?",
                                                            "Confirmar edición",
                                                            MessageBoxButtons.YesNo,
                                                            MessageBoxIcon.Question);

                    if (resultado == DialogResult.Yes)
                    {
                        try
                        {
                            bool exito = negocio.ActualizarProducto(producto);

                            if (exito)
                            {
                                MessageBox.Show("producto editado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                CargarProductosEnGrilla();
                                LimpiarFormulario();
                                idProductoSeleccionado = 0; // Reiniciamos la variable para volver al modo registro
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
                MessageBox.Show("Error al intentar guardar el producto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DGVProductos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // 1. Verificamos que estemos dibujando la columna de la imagen
            // REEMPLAZA "NombreDeTuColumnaImagen" por el Name real de tu columna en el DataGridView
            if (DGVProductos.Columns[e.ColumnIndex].Name == "CImagen")
                    {
                        // 2. Obtenemos el producto de la fila que se está dibujando en este momento
                        Producto prod = (Producto)DGVProductos.Rows[e.RowIndex].DataBoundItem;

                        // 3. Verificamos que el producto exista y tenga una URL guardada
                        if (prod != null && !string.IsNullOrEmpty(prod.url_imagen))
                        {
                            try
                            {
                                // Cargamos la imagen usando FileStream (igual que hicimos en el botón) 
                                // para evitar bloquear el archivo y prevenir errores de memoria
                                using (var stream = new System.IO.FileStream(prod.url_imagen, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                                {
                                    // Asignamos la imagen real al valor de la celda
                                    e.Value = System.Drawing.Image.FromStream(stream);
                                }
                            }
                            catch (Exception)
                            {
                                // Si la imagen fue borrada del disco o hay un error, dejamos la celda vacía
                                // (Opcional: aquí podrías poner una imagen genérica de "Imagen no encontrada")
                                e.Value = null;
                            }
                        }
                        else
                        {
                            // Si el producto no tiene foto, no mostramos nada
                            e.Value = null;
                        }
                    }
                }
            }
}
