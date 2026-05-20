namespace UI
{
    partial class AdministrarUnidadDeVenta
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.treeViewUnidadesDeVenta = new System.Windows.Forms.TreeView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonEliminarSeleccionado = new System.Windows.Forms.Button();
            this.buttonNuevoArticulo = new System.Windows.Forms.Button();
            this.buttonNuevoLote = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelMiembros = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonQuitarArticulo = new System.Windows.Forms.Button();
            this.listBoxLoteSeleccionado = new System.Windows.Forms.ListBox();
            this.listBoxUnidadesDeVenta = new System.Windows.Forms.ListBox();
            this.buttonAgregarArticuclo = new System.Windows.Forms.Button();
            this.textBoxDetalles = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeViewUnidadesDeVenta
            // 
            this.treeViewUnidadesDeVenta.Location = new System.Drawing.Point(24, 38);
            this.treeViewUnidadesDeVenta.Name = "treeViewUnidadesDeVenta";
            this.treeViewUnidadesDeVenta.Size = new System.Drawing.Size(328, 417);
            this.treeViewUnidadesDeVenta.TabIndex = 0;
            this.treeViewUnidadesDeVenta.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewUnidadesDeVenta_AfterSelect);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonEliminarSeleccionado);
            this.groupBox1.Controls.Add(this.buttonNuevoArticulo);
            this.groupBox1.Controls.Add(this.buttonNuevoLote);
            this.groupBox1.Controls.Add(this.treeViewUnidadesDeVenta);
            this.groupBox1.Location = new System.Drawing.Point(23, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(381, 623);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Unidades de venta";
            // 
            // buttonEliminarSeleccionado
            // 
            this.buttonEliminarSeleccionado.Location = new System.Drawing.Point(24, 563);
            this.buttonEliminarSeleccionado.Name = "buttonEliminarSeleccionado";
            this.buttonEliminarSeleccionado.Size = new System.Drawing.Size(328, 32);
            this.buttonEliminarSeleccionado.TabIndex = 3;
            this.buttonEliminarSeleccionado.Text = "Eliminar seleccionado";
            this.buttonEliminarSeleccionado.UseVisualStyleBackColor = true;
            this.buttonEliminarSeleccionado.Click += new System.EventHandler(this.buttonEliminarSeleccionado_Click);
            // 
            // buttonNuevoArticulo
            // 
            this.buttonNuevoArticulo.Location = new System.Drawing.Point(24, 515);
            this.buttonNuevoArticulo.Name = "buttonNuevoArticulo";
            this.buttonNuevoArticulo.Size = new System.Drawing.Size(328, 32);
            this.buttonNuevoArticulo.TabIndex = 2;
            this.buttonNuevoArticulo.Text = "Nuevo articulo";
            this.buttonNuevoArticulo.UseVisualStyleBackColor = true;
            this.buttonNuevoArticulo.Click += new System.EventHandler(this.buttonNuevoArticulo_Click);
            // 
            // buttonNuevoLote
            // 
            this.buttonNuevoLote.Location = new System.Drawing.Point(24, 470);
            this.buttonNuevoLote.Name = "buttonNuevoLote";
            this.buttonNuevoLote.Size = new System.Drawing.Size(328, 32);
            this.buttonNuevoLote.TabIndex = 1;
            this.buttonNuevoLote.Text = "Nuevo lote";
            this.buttonNuevoLote.UseVisualStyleBackColor = true;
            this.buttonNuevoLote.Click += new System.EventHandler(this.buttonNuevoLote_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.textBoxDetalles);
            this.groupBox2.Controls.Add(this.labelMiembros);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.buttonQuitarArticulo);
            this.groupBox2.Controls.Add(this.listBoxLoteSeleccionado);
            this.groupBox2.Controls.Add(this.listBoxUnidadesDeVenta);
            this.groupBox2.Controls.Add(this.buttonAgregarArticuclo);
            this.groupBox2.Location = new System.Drawing.Point(427, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(740, 623);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Unidades de venta";
            // 
            // labelMiembros
            // 
            this.labelMiembros.AutoSize = true;
            this.labelMiembros.Location = new System.Drawing.Point(454, 38);
            this.labelMiembros.Name = "labelMiembros";
            this.labelMiembros.Size = new System.Drawing.Size(150, 20);
            this.labelMiembros.TabIndex = 7;
            this.labelMiembros.Text = "Miembros dentro de";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(197, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Lotes/Articulos disponibles";
            // 
            // buttonQuitarArticulo
            // 
            this.buttonQuitarArticulo.Location = new System.Drawing.Point(299, 216);
            this.buttonQuitarArticulo.Name = "buttonQuitarArticulo";
            this.buttonQuitarArticulo.Size = new System.Drawing.Size(145, 32);
            this.buttonQuitarArticulo.TabIndex = 5;
            this.buttonQuitarArticulo.Text = "< Quitar";
            this.buttonQuitarArticulo.UseVisualStyleBackColor = true;
            this.buttonQuitarArticulo.Click += new System.EventHandler(this.buttonQuitarArticulo_Click);
            // 
            // listBoxLoteSeleccionado
            // 
            this.listBoxLoteSeleccionado.FormattingEnabled = true;
            this.listBoxLoteSeleccionado.ItemHeight = 20;
            this.listBoxLoteSeleccionado.Location = new System.Drawing.Point(458, 78);
            this.listBoxLoteSeleccionado.Name = "listBoxLoteSeleccionado";
            this.listBoxLoteSeleccionado.Size = new System.Drawing.Size(261, 264);
            this.listBoxLoteSeleccionado.TabIndex = 4;
            // 
            // listBoxUnidadesDeVenta
            // 
            this.listBoxUnidadesDeVenta.FormattingEnabled = true;
            this.listBoxUnidadesDeVenta.ItemHeight = 20;
            this.listBoxUnidadesDeVenta.Location = new System.Drawing.Point(24, 78);
            this.listBoxUnidadesDeVenta.Name = "listBoxUnidadesDeVenta";
            this.listBoxUnidadesDeVenta.Size = new System.Drawing.Size(261, 264);
            this.listBoxUnidadesDeVenta.TabIndex = 3;
            // 
            // buttonAgregarArticuclo
            // 
            this.buttonAgregarArticuclo.Location = new System.Drawing.Point(299, 172);
            this.buttonAgregarArticuclo.Name = "buttonAgregarArticuclo";
            this.buttonAgregarArticuclo.Size = new System.Drawing.Size(145, 32);
            this.buttonAgregarArticuclo.TabIndex = 1;
            this.buttonAgregarArticuclo.Text = "Agregar >";
            this.buttonAgregarArticuclo.UseVisualStyleBackColor = true;
            this.buttonAgregarArticuclo.Click += new System.EventHandler(this.buttonAgregarArticuclo_Click);
            // 
            // textBoxDetalles
            // 
            this.textBoxDetalles.Location = new System.Drawing.Point(24, 408);
            this.textBoxDetalles.Multiline = true;
            this.textBoxDetalles.Name = "textBoxDetalles";
            this.textBoxDetalles.ReadOnly = true;
            this.textBoxDetalles.Size = new System.Drawing.Size(695, 187);
            this.textBoxDetalles.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 363);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 20);
            this.label2.TabIndex = 9;
            this.label2.Text = "Detalles";
            // 
            // AdministrarUnidadDeVenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1192, 647);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "AdministrarUnidadDeVenta";
            this.Text = "AdministrarUnidadDeVenta";
            this.Load += new System.EventHandler(this.AdministrarUnidadDeVenta_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewUnidadesDeVenta;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonEliminarSeleccionado;
        private System.Windows.Forms.Button buttonNuevoArticulo;
        private System.Windows.Forms.Button buttonNuevoLote;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonQuitarArticulo;
        private System.Windows.Forms.ListBox listBoxLoteSeleccionado;
        private System.Windows.Forms.ListBox listBoxUnidadesDeVenta;
        private System.Windows.Forms.Button buttonAgregarArticuclo;
        private System.Windows.Forms.Label labelMiembros;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxDetalles;
        private System.Windows.Forms.Label label2;
    }
}