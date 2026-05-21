namespace UI
{
    partial class FormSubasta
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.groupBoxSubasta = new System.Windows.Forms.GroupBox();
            this.labelUnidad = new System.Windows.Forms.Label();
            this.comboBoxUnidades = new System.Windows.Forms.ComboBox();
            this.buttonAbrirSubasta = new System.Windows.Forms.Button();
            this.labelPrecioLabel = new System.Windows.Forms.Label();
            this.labelPrecioActual = new System.Windows.Forms.Label();
            this.buttonCerrar = new System.Windows.Forms.Button();
            this.groupBoxNotificaciones = new System.Windows.Forms.GroupBox();
            this.listBoxNotificaciones = new System.Windows.Forms.ListBox();
            this.groupBoxSubasta.SuspendLayout();
            this.groupBoxNotificaciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxSubasta
            // 
            this.groupBoxSubasta.Controls.Add(this.labelUnidad);
            this.groupBoxSubasta.Controls.Add(this.comboBoxUnidades);
            this.groupBoxSubasta.Controls.Add(this.buttonAbrirSubasta);
            this.groupBoxSubasta.Controls.Add(this.labelPrecioLabel);
            this.groupBoxSubasta.Controls.Add(this.labelPrecioActual);
            this.groupBoxSubasta.Controls.Add(this.buttonCerrar);
            this.groupBoxSubasta.Location = new System.Drawing.Point(12, 12);
            this.groupBoxSubasta.Name = "groupBoxSubasta";
            this.groupBoxSubasta.Size = new System.Drawing.Size(460, 150);
            this.groupBoxSubasta.TabIndex = 0;
            this.groupBoxSubasta.TabStop = false;
            this.groupBoxSubasta.Text = "Subasta";
            // 
            // labelUnidad
            // 
            this.labelUnidad.AutoSize = true;
            this.labelUnidad.Location = new System.Drawing.Point(16, 32);
            this.labelUnidad.Name = "labelUnidad";
            this.labelUnidad.Size = new System.Drawing.Size(129, 20);
            this.labelUnidad.TabIndex = 0;
            this.labelUnidad.Text = "Unidad de venta:";
            // 
            // comboBoxUnidades
            // 
            this.comboBoxUnidades.FormattingEnabled = true;
            this.comboBoxUnidades.Location = new System.Drawing.Point(16, 55);
            this.comboBoxUnidades.Name = "comboBoxUnidades";
            this.comboBoxUnidades.Size = new System.Drawing.Size(300, 28);
            this.comboBoxUnidades.TabIndex = 0;
            this.comboBoxUnidades.SelectedValueChanged += new System.EventHandler(this.comboBoxUnidades_SelectedValueChanged);
            // 
            // buttonAbrirSubasta
            // 
            this.buttonAbrirSubasta.Location = new System.Drawing.Point(330, 52);
            this.buttonAbrirSubasta.Name = "buttonAbrirSubasta";
            this.buttonAbrirSubasta.Size = new System.Drawing.Size(110, 32);
            this.buttonAbrirSubasta.TabIndex = 1;
            this.buttonAbrirSubasta.Text = "Abrir subasta";
            this.buttonAbrirSubasta.UseVisualStyleBackColor = true;
            this.buttonAbrirSubasta.Click += new System.EventHandler(this.buttonAbrirSubasta_Click);
            // 
            // labelPrecioLabel
            // 
            this.labelPrecioLabel.AutoSize = true;
            this.labelPrecioLabel.Location = new System.Drawing.Point(16, 105);
            this.labelPrecioLabel.Name = "labelPrecioLabel";
            this.labelPrecioLabel.Size = new System.Drawing.Size(104, 20);
            this.labelPrecioLabel.TabIndex = 2;
            this.labelPrecioLabel.Text = "Precio actual:";
            // 
            // labelPrecioActual
            // 
            this.labelPrecioActual.AutoSize = true;
            this.labelPrecioActual.Location = new System.Drawing.Point(119, 105);
            this.labelPrecioActual.Name = "labelPrecioActual";
            this.labelPrecioActual.Size = new System.Drawing.Size(27, 20);
            this.labelPrecioActual.TabIndex = 3;
            this.labelPrecioActual.Text = "$0";
            // 
            // buttonCerrar
            // 
            this.buttonCerrar.Location = new System.Drawing.Point(330, 93);
            this.buttonCerrar.Name = "buttonCerrar";
            this.buttonCerrar.Size = new System.Drawing.Size(110, 32);
            this.buttonCerrar.TabIndex = 3;
            this.buttonCerrar.Text = "Cerrar subasta";
            this.buttonCerrar.UseVisualStyleBackColor = false;
            this.buttonCerrar.Click += new System.EventHandler(this.buttonCerrar_Click);
            // 
            // groupBoxNotificaciones
            // 
            this.groupBoxNotificaciones.Controls.Add(this.listBoxNotificaciones);
            this.groupBoxNotificaciones.Location = new System.Drawing.Point(490, 12);
            this.groupBoxNotificaciones.Name = "groupBoxNotificaciones";
            this.groupBoxNotificaciones.Size = new System.Drawing.Size(689, 323);
            this.groupBoxNotificaciones.TabIndex = 2;
            this.groupBoxNotificaciones.TabStop = false;
            this.groupBoxNotificaciones.Text = "Notificaciones en tiempo real";
            // 
            // listBoxNotificaciones
            // 
            this.listBoxNotificaciones.FormattingEnabled = true;
            this.listBoxNotificaciones.HorizontalScrollbar = true;
            this.listBoxNotificaciones.ItemHeight = 20;
            this.listBoxNotificaciones.Location = new System.Drawing.Point(12, 25);
            this.listBoxNotificaciones.Name = "listBoxNotificaciones";
            this.listBoxNotificaciones.Size = new System.Drawing.Size(661, 284);
            this.listBoxNotificaciones.TabIndex = 0;
            // 
            // FormSubasta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1281, 350);
            this.Controls.Add(this.groupBoxSubasta);
            this.Controls.Add(this.groupBoxNotificaciones);
            this.Name = "FormSubasta";
            this.Text = "Subastas";
            this.Load += new System.EventHandler(this.FormSubasta_Load);
            this.groupBoxSubasta.ResumeLayout(false);
            this.groupBoxSubasta.PerformLayout();
            this.groupBoxNotificaciones.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        // Controls
        private System.Windows.Forms.GroupBox groupBoxSubasta;
        private System.Windows.Forms.Label labelUnidad;
        private System.Windows.Forms.ComboBox comboBoxUnidades;
        private System.Windows.Forms.Button buttonAbrirSubasta;
        private System.Windows.Forms.Label labelPrecioLabel;
        private System.Windows.Forms.Label labelPrecioActual;
        private System.Windows.Forms.Button buttonCerrar;

        private System.Windows.Forms.GroupBox groupBoxNotificaciones;
        private System.Windows.Forms.ListBox listBoxNotificaciones;
    }
}