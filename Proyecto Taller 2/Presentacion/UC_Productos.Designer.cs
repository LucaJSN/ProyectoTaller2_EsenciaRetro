namespace Proyecto_Taller_2
{
    partial class UC_Productos
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.BtnAgregar = new System.Windows.Forms.Button();
            this.TBNombre = new System.Windows.Forms.TextBox();
            this.LNombre = new System.Windows.Forms.Label();
            this.LDescripcion = new System.Windows.Forms.Label();
            this.LVenta = new System.Windows.Forms.Label();
            this.LCantidad = new System.Windows.Forms.Label();
            this.TBDescripcion = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.LCosto = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.BtnCancelar = new System.Windows.Forms.Button();
            this.BtnImagen = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.NUDCosto = new System.Windows.Forms.NumericUpDown();
            this.NUDVenta = new System.Windows.Forms.NumericUpDown();
            this.NUDCantidad = new System.Windows.Forms.NumericUpDown();
            this.DGVProductos = new System.Windows.Forms.DataGridView();
            this.CNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CPrecio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CStock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CTalle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CImagen = new System.Windows.Forms.DataGridViewImageColumn();
            this.CEditar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.CEliminar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.CReactivar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.LTalle = new System.Windows.Forms.Label();
            this.CBTalle = new System.Windows.Forms.ComboBox();
            this.LClase = new System.Windows.Forms.Label();
            this.CBCategoria = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.TBBuscador = new System.Windows.Forms.TextBox();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUDCosto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUDVenta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUDCantidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGVProductos)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnAgregar
            // 
            this.BtnAgregar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnAgregar.BackColor = System.Drawing.Color.ForestGreen;
            this.BtnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnAgregar.Font = new System.Drawing.Font("Microsoft YaHei", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAgregar.Location = new System.Drawing.Point(514, 369);
            this.BtnAgregar.Name = "BtnAgregar";
            this.BtnAgregar.Size = new System.Drawing.Size(145, 76);
            this.BtnAgregar.TabIndex = 1;
            this.BtnAgregar.Text = "Agregar";
            this.BtnAgregar.UseVisualStyleBackColor = false;
            this.BtnAgregar.Click += new System.EventHandler(this.BtnAgregar_Click);
            // 
            // TBNombre
            // 
            this.TBNombre.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TBNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TBNombre.Location = new System.Drawing.Point(199, 30);
            this.TBNombre.Name = "TBNombre";
            this.TBNombre.Size = new System.Drawing.Size(222, 28);
            this.TBNombre.TabIndex = 2;
            this.TBNombre.TextChanged += new System.EventHandler(this.TBNombre_TextChanged);
            this.TBNombre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TBNombre_KeyPress);
            // 
            // LNombre
            // 
            this.LNombre.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LNombre.AutoSize = true;
            this.LNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LNombre.ForeColor = System.Drawing.Color.Black;
            this.LNombre.Location = new System.Drawing.Point(28, 29);
            this.LNombre.Name = "LNombre";
            this.LNombre.Size = new System.Drawing.Size(81, 25);
            this.LNombre.TabIndex = 6;
            this.LNombre.Text = "Nombre";
            this.LNombre.Click += new System.EventHandler(this.LNombre_Click);
            // 
            // LDescripcion
            // 
            this.LDescripcion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LDescripcion.AutoSize = true;
            this.LDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LDescripcion.ForeColor = System.Drawing.Color.Black;
            this.LDescripcion.Location = new System.Drawing.Point(28, 102);
            this.LDescripcion.Name = "LDescripcion";
            this.LDescripcion.Size = new System.Drawing.Size(114, 25);
            this.LDescripcion.TabIndex = 7;
            this.LDescripcion.Text = "Descripcion";
            this.LDescripcion.Click += new System.EventHandler(this.LDescripcion_Click);
            // 
            // LVenta
            // 
            this.LVenta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LVenta.AutoSize = true;
            this.LVenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LVenta.ForeColor = System.Drawing.Color.Black;
            this.LVenta.Location = new System.Drawing.Point(28, 331);
            this.LVenta.Name = "LVenta";
            this.LVenta.Size = new System.Drawing.Size(154, 25);
            this.LVenta.TabIndex = 8;
            this.LVenta.Text = "Precio Venta ($)";
            this.LVenta.Click += new System.EventHandler(this.LPrecio_Click);
            // 
            // LCantidad
            // 
            this.LCantidad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LCantidad.AutoSize = true;
            this.LCantidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LCantidad.ForeColor = System.Drawing.Color.Black;
            this.LCantidad.Location = new System.Drawing.Point(28, 382);
            this.LCantidad.Name = "LCantidad";
            this.LCantidad.Size = new System.Drawing.Size(91, 25);
            this.LCantidad.TabIndex = 9;
            this.LCantidad.Text = "Cantidad";
            // 
            // TBDescripcion
            // 
            this.TBDescripcion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TBDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TBDescripcion.Location = new System.Drawing.Point(199, 86);
            this.TBDescripcion.Multiline = true;
            this.TBDescripcion.Name = "TBDescripcion";
            this.TBDescripcion.Size = new System.Drawing.Size(222, 67);
            this.TBDescripcion.TabIndex = 10;
            this.TBDescripcion.TextChanged += new System.EventHandler(this.TBDescripcion_TextChanged);
            this.TBDescripcion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TBDescripcion_KeyPress);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // LCosto
            // 
            this.LCosto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LCosto.AutoSize = true;
            this.LCosto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LCosto.ForeColor = System.Drawing.Color.Black;
            this.LCosto.Location = new System.Drawing.Point(28, 274);
            this.LCosto.Name = "LCosto";
            this.LCosto.Size = new System.Drawing.Size(154, 25);
            this.LCosto.TabIndex = 14;
            this.LCosto.Text = "Precio Costo ($)";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox1.BackColor = System.Drawing.Color.DarkGray;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(21, 18);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(317, 314);
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // BtnCancelar
            // 
            this.BtnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnCancelar.BackColor = System.Drawing.Color.Red;
            this.BtnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnCancelar.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancelar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.BtnCancelar.Location = new System.Drawing.Point(709, 369);
            this.BtnCancelar.Name = "BtnCancelar";
            this.BtnCancelar.Size = new System.Drawing.Size(145, 76);
            this.BtnCancelar.TabIndex = 17;
            this.BtnCancelar.Text = "Cancelar";
            this.BtnCancelar.UseVisualStyleBackColor = false;
            this.BtnCancelar.Click += new System.EventHandler(this.BCancelar_Click);
            // 
            // BtnImagen
            // 
            this.BtnImagen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnImagen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.BtnImagen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnImagen.ForeColor = System.Drawing.Color.White;
            this.BtnImagen.Location = new System.Drawing.Point(43, 436);
            this.BtnImagen.Name = "BtnImagen";
            this.BtnImagen.Size = new System.Drawing.Size(391, 30);
            this.BtnImagen.TabIndex = 18;
            this.BtnImagen.Text = "Imagen";
            this.BtnImagen.UseVisualStyleBackColor = false;
            this.BtnImagen.Click += new System.EventHandler(this.BtnImagen_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // NUDCosto
            // 
            this.NUDCosto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.NUDCosto.DecimalPlaces = 2;
            this.NUDCosto.Location = new System.Drawing.Point(199, 279);
            this.NUDCosto.Maximum = new decimal(new int[] {
            -1981284353,
            -1966660860,
            0,
            0});
            this.NUDCosto.Name = "NUDCosto";
            this.NUDCosto.Size = new System.Drawing.Size(120, 22);
            this.NUDCosto.TabIndex = 20;
            // 
            // NUDVenta
            // 
            this.NUDVenta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.NUDVenta.DecimalPlaces = 2;
            this.NUDVenta.Location = new System.Drawing.Point(200, 331);
            this.NUDVenta.Maximum = new decimal(new int[] {
            -1981284353,
            -1966660860,
            0,
            0});
            this.NUDVenta.Name = "NUDVenta";
            this.NUDVenta.Size = new System.Drawing.Size(120, 22);
            this.NUDVenta.TabIndex = 22;
            // 
            // NUDCantidad
            // 
            this.NUDCantidad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.NUDCantidad.Location = new System.Drawing.Point(199, 382);
            this.NUDCantidad.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            0});
            this.NUDCantidad.Name = "NUDCantidad";
            this.NUDCantidad.Size = new System.Drawing.Size(120, 22);
            this.NUDCantidad.TabIndex = 23;
            // 
            // DGVProductos
            // 
            this.DGVProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVProductos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CNombre,
            this.CID,
            this.CPrecio,
            this.CStock,
            this.CTalle,
            this.CImagen,
            this.CEditar,
            this.CEliminar,
            this.CReactivar});
            this.DGVProductos.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.DGVProductos.Location = new System.Drawing.Point(0, 670);
            this.DGVProductos.Name = "DGVProductos";
            this.DGVProductos.ReadOnly = true;
            this.DGVProductos.RowHeadersWidth = 51;
            this.DGVProductos.RowTemplate.Height = 24;
            this.DGVProductos.Size = new System.Drawing.Size(1464, 155);
            this.DGVProductos.TabIndex = 25;
            this.DGVProductos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVProductos_CellContentClick);
            // 
            // CNombre
            // 
            this.CNombre.DataPropertyName = "nombre";
            this.CNombre.HeaderText = "Nombre";
            this.CNombre.MinimumWidth = 6;
            this.CNombre.Name = "CNombre";
            this.CNombre.ReadOnly = true;
            this.CNombre.Width = 125;
            // 
            // CID
            // 
            this.CID.DataPropertyName = "id_producto";
            this.CID.HeaderText = "Id";
            this.CID.MinimumWidth = 6;
            this.CID.Name = "CID";
            this.CID.ReadOnly = true;
            this.CID.Width = 125;
            // 
            // CPrecio
            // 
            this.CPrecio.DataPropertyName = "precio";
            this.CPrecio.HeaderText = "Precio";
            this.CPrecio.MinimumWidth = 6;
            this.CPrecio.Name = "CPrecio";
            this.CPrecio.ReadOnly = true;
            this.CPrecio.Width = 125;
            // 
            // CStock
            // 
            this.CStock.DataPropertyName = "stock";
            this.CStock.HeaderText = "Stock";
            this.CStock.MinimumWidth = 6;
            this.CStock.Name = "CStock";
            this.CStock.ReadOnly = true;
            this.CStock.Width = 125;
            // 
            // CTalle
            // 
            this.CTalle.DataPropertyName = "talle";
            this.CTalle.HeaderText = "Talle";
            this.CTalle.MinimumWidth = 6;
            this.CTalle.Name = "CTalle";
            this.CTalle.ReadOnly = true;
            this.CTalle.Width = 125;
            // 
            // CImagen
            // 
            this.CImagen.HeaderText = "Imagen";
            this.CImagen.MinimumWidth = 6;
            this.CImagen.Name = "CImagen";
            this.CImagen.ReadOnly = true;
            this.CImagen.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CImagen.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.CImagen.Width = 125;
            // 
            // CEditar
            // 
            this.CEditar.HeaderText = "Editar";
            this.CEditar.MinimumWidth = 6;
            this.CEditar.Name = "CEditar";
            this.CEditar.ReadOnly = true;
            this.CEditar.Width = 125;
            // 
            // CEliminar
            // 
            this.CEliminar.HeaderText = "Eliminar";
            this.CEliminar.MinimumWidth = 6;
            this.CEliminar.Name = "CEliminar";
            this.CEliminar.ReadOnly = true;
            this.CEliminar.Width = 125;
            // 
            // CReactivar
            // 
            this.CReactivar.HeaderText = "Reactivar";
            this.CReactivar.MinimumWidth = 6;
            this.CReactivar.Name = "CReactivar";
            this.CReactivar.ReadOnly = true;
            this.CReactivar.Width = 125;
            // 
            // LTalle
            // 
            this.LTalle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LTalle.AutoSize = true;
            this.LTalle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LTalle.ForeColor = System.Drawing.Color.Black;
            this.LTalle.Location = new System.Drawing.Point(28, 171);
            this.LTalle.Name = "LTalle";
            this.LTalle.Size = new System.Drawing.Size(55, 25);
            this.LTalle.TabIndex = 27;
            this.LTalle.Text = "Talle";
            // 
            // CBTalle
            // 
            this.CBTalle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CBTalle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBTalle.FormattingEnabled = true;
            this.CBTalle.Items.AddRange(new object[] {
            "S",
            "M",
            "L",
            "XL",
            "XXL"});
            this.CBTalle.Location = new System.Drawing.Point(198, 175);
            this.CBTalle.Name = "CBTalle";
            this.CBTalle.Size = new System.Drawing.Size(121, 24);
            this.CBTalle.TabIndex = 28;
            // 
            // LClase
            // 
            this.LClase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LClase.AutoSize = true;
            this.LClase.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LClase.ForeColor = System.Drawing.Color.Black;
            this.LClase.Location = new System.Drawing.Point(28, 220);
            this.LClase.Name = "LClase";
            this.LClase.Size = new System.Drawing.Size(97, 25);
            this.LClase.TabIndex = 29;
            this.LClase.Text = "Categoria";
            this.LClase.Click += new System.EventHandler(this.label1_Click);
            // 
            // CBCategoria
            // 
            this.CBCategoria.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CBCategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBCategoria.FormattingEnabled = true;
            this.CBCategoria.Items.AddRange(new object[] {
            "Normal",
            "Epica",
            "Legendaria"});
            this.CBCategoria.Location = new System.Drawing.Point(199, 224);
            this.CBCategoria.Name = "CBCategoria";
            this.CBCategoria.Size = new System.Drawing.Size(121, 24);
            this.CBCategoria.TabIndex = 30;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Lavender;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(912, 37);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(360, 348);
            this.panel1.TabIndex = 32;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Lavender;
            this.panel2.Controls.Add(this.LNombre);
            this.panel2.Controls.Add(this.TBNombre);
            this.panel2.Controls.Add(this.LDescripcion);
            this.panel2.Controls.Add(this.CBCategoria);
            this.panel2.Controls.Add(this.BtnCancelar);
            this.panel2.Controls.Add(this.BtnImagen);
            this.panel2.Controls.Add(this.BtnAgregar);
            this.panel2.Controls.Add(this.NUDCantidad);
            this.panel2.Controls.Add(this.TBDescripcion);
            this.panel2.Controls.Add(this.NUDVenta);
            this.panel2.Controls.Add(this.LClase);
            this.panel2.Controls.Add(this.LCantidad);
            this.panel2.Controls.Add(this.NUDCosto);
            this.panel2.Controls.Add(this.LTalle);
            this.panel2.Controls.Add(this.CBTalle);
            this.panel2.Controls.Add(this.LVenta);
            this.panel2.Controls.Add(this.LCosto);
            this.panel2.Location = new System.Drawing.Point(18, 37);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(860, 483);
            this.panel2.TabIndex = 33;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // TBBuscador
            // 
            this.TBBuscador.Location = new System.Drawing.Point(142, 625);
            this.TBBuscador.Name = "TBBuscador";
            this.TBBuscador.Size = new System.Drawing.Size(100, 22);
            this.TBBuscador.TabIndex = 35;
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(61, 4);
            // 
            // UC_Productos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(182)))), ((int)(((byte)(211)))));
            this.Controls.Add(this.TBBuscador);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.DGVProductos);
            this.MinimumSize = new System.Drawing.Size(400, 300);
            this.Name = "UC_Productos";
            this.Size = new System.Drawing.Size(1464, 825);
            this.Load += new System.EventHandler(this.UC_Productos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUDCosto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUDVenta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUDCantidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGVProductos)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button BtnAgregar;
        private System.Windows.Forms.TextBox TBNombre;
        private System.Windows.Forms.Label LNombre;
        private System.Windows.Forms.Label LDescripcion;
        private System.Windows.Forms.Label LVenta;
        private System.Windows.Forms.Label LCantidad;
        private System.Windows.Forms.TextBox TBDescripcion;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label LCosto;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button BtnCancelar;
        private System.Windows.Forms.Button BtnImagen;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.NumericUpDown NUDCosto;
        private System.Windows.Forms.NumericUpDown NUDVenta;
        private System.Windows.Forms.NumericUpDown NUDCantidad;
        private System.Windows.Forms.DataGridView DGVProductos;
        private System.Windows.Forms.Label LTalle;
        private System.Windows.Forms.ComboBox CBTalle;
        private System.Windows.Forms.Label LClase;
        private System.Windows.Forms.ComboBox CBCategoria;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox TBBuscador;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.DataGridViewTextBoxColumn CNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn CID;
        private System.Windows.Forms.DataGridViewTextBoxColumn CPrecio;
        private System.Windows.Forms.DataGridViewTextBoxColumn CStock;
        private System.Windows.Forms.DataGridViewTextBoxColumn CTalle;
        private System.Windows.Forms.DataGridViewImageColumn CImagen;
        private System.Windows.Forms.DataGridViewButtonColumn CEditar;
        private System.Windows.Forms.DataGridViewButtonColumn CEliminar;
        private System.Windows.Forms.DataGridViewButtonColumn CReactivar;
    }
}
