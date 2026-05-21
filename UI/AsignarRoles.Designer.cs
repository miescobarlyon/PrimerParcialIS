namespace UI
{
    partial class AsignarRoles
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
            this.groupBoxUsuarios = new System.Windows.Forms.GroupBox();
            this.listBoxUsuarios = new System.Windows.Forms.ListBox();
            this.groupBoxGestion = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxDetalles = new System.Windows.Forms.TextBox();
            this.labelPerfilesDelUsuario = new System.Windows.Forms.Label();
            this.listBoxPerfilesDelUsuario = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listBoxPerfilesDisponibles = new System.Windows.Forms.ListBox();
            this.buttonRemover = new System.Windows.Forms.Button();
            this.buttonAsignar = new System.Windows.Forms.Button();
            this.groupBoxUsuarios.SuspendLayout();
            this.groupBoxGestion.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxUsuarios
            // 
            this.groupBoxUsuarios.Controls.Add(this.listBoxUsuarios);
            this.groupBoxUsuarios.Location = new System.Drawing.Point(23, 12);
            this.groupBoxUsuarios.Name = "groupBoxUsuarios";
            this.groupBoxUsuarios.Size = new System.Drawing.Size(381, 623);
            this.groupBoxUsuarios.TabIndex = 0;
            this.groupBoxUsuarios.TabStop = false;
            this.groupBoxUsuarios.Text = "Usuarios";
            // 
            // listBoxUsuarios
            // 
            this.listBoxUsuarios.FormattingEnabled = true;
            this.listBoxUsuarios.ItemHeight = 20;
            this.listBoxUsuarios.Location = new System.Drawing.Point(24, 38);
            this.listBoxUsuarios.Name = "listBoxUsuarios";
            this.listBoxUsuarios.SelectionMode = System.Windows.Forms.SelectionMode.One;
            this.listBoxUsuarios.Size = new System.Drawing.Size(328, 564);
            this.listBoxUsuarios.TabIndex = 0;
            this.listBoxUsuarios.SelectedValueChanged += new System.EventHandler(this.listBoxUsuarios_SelectedValueChanged);
            // 
            // groupBoxGestion
            // 
            this.groupBoxGestion.Controls.Add(this.label2);
            this.groupBoxGestion.Controls.Add(this.textBoxDetalles);
            this.groupBoxGestion.Controls.Add(this.labelPerfilesDelUsuario);
            this.groupBoxGestion.Controls.Add(this.listBoxPerfilesDelUsuario);
            this.groupBoxGestion.Controls.Add(this.label1);
            this.groupBoxGestion.Controls.Add(this.listBoxPerfilesDisponibles);
            this.groupBoxGestion.Controls.Add(this.buttonRemover);
            this.groupBoxGestion.Controls.Add(this.buttonAsignar);
            this.groupBoxGestion.Location = new System.Drawing.Point(427, 12);
            this.groupBoxGestion.Name = "groupBoxGestion";
            this.groupBoxGestion.Size = new System.Drawing.Size(740, 623);
            this.groupBoxGestion.TabIndex = 1;
            this.groupBoxGestion.TabStop = false;
            this.groupBoxGestion.Text = "Gestión de Perfiles";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 363);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Detalles";
            // 
            // textBoxDetalles
            // 
            this.textBoxDetalles.Location = new System.Drawing.Point(24, 408);
            this.textBoxDetalles.Multiline = true;
            this.textBoxDetalles.Name = "textBoxDetalles";
            this.textBoxDetalles.ReadOnly = true;
            this.textBoxDetalles.Size = new System.Drawing.Size(695, 187);
            this.textBoxDetalles.TabIndex = 6;
            // 
            // labelPerfilesDelUsuario
            // 
            this.labelPerfilesDelUsuario.AutoSize = true;
            this.labelPerfilesDelUsuario.Location = new System.Drawing.Point(454, 38);
            this.labelPerfilesDelUsuario.Name = "labelPerfilesDelUsuario";
            this.labelPerfilesDelUsuario.Size = new System.Drawing.Size(115, 20);
            this.labelPerfilesDelUsuario.TabIndex = 5;
            this.labelPerfilesDelUsuario.Text = "Perfiles de \'\'";
            // 
            // listBoxPerfilesDelUsuario
            // 
            this.listBoxPerfilesDelUsuario.FormattingEnabled = true;
            this.listBoxPerfilesDelUsuario.ItemHeight = 20;
            this.listBoxPerfilesDelUsuario.Location = new System.Drawing.Point(458, 78);
            this.listBoxPerfilesDelUsuario.Name = "listBoxPerfilesDelUsuario";
            this.listBoxPerfilesDelUsuario.Size = new System.Drawing.Size(261, 264);
            this.listBoxPerfilesDelUsuario.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Perfiles disponibles";
            // 
            // listBoxPerfilesDisponibles
            // 
            this.listBoxPerfilesDisponibles.FormattingEnabled = true;
            this.listBoxPerfilesDisponibles.ItemHeight = 20;
            this.listBoxPerfilesDisponibles.Location = new System.Drawing.Point(24, 78);
            this.listBoxPerfilesDisponibles.Name = "listBoxPerfilesDisponibles";
            this.listBoxPerfilesDisponibles.Size = new System.Drawing.Size(261, 264);
            this.listBoxPerfilesDisponibles.TabIndex = 2;
            // 
            // buttonRemover
            // 
            this.buttonRemover.Location = new System.Drawing.Point(299, 216);
            this.buttonRemover.Name = "buttonRemover";
            this.buttonRemover.Size = new System.Drawing.Size(145, 32);
            this.buttonRemover.TabIndex = 1;
            this.buttonRemover.Text = "< Remover";
            this.buttonRemover.UseVisualStyleBackColor = true;
            this.buttonRemover.Click += new System.EventHandler(this.buttonRemover_Click);
            // 
            // buttonAsignar
            // 
            this.buttonAsignar.Location = new System.Drawing.Point(299, 172);
            this.buttonAsignar.Name = "buttonAsignar";
            this.buttonAsignar.Size = new System.Drawing.Size(145, 32);
            this.buttonAsignar.TabIndex = 0;
            this.buttonAsignar.Text = "Asignar >";
            this.buttonAsignar.UseVisualStyleBackColor = true;
            this.buttonAsignar.Click += new System.EventHandler(this.buttonAsignar_Click);
            // 
            // AsignarRoles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1192, 647);
            this.Controls.Add(this.groupBoxGestion);
            this.Controls.Add(this.groupBoxUsuarios);
            this.Name = "AsignarRoles";
            this.Text = "Asignar Roles a Usuarios";
            this.Load += new System.EventHandler(this.AsignarRoles_Load);
            this.groupBoxUsuarios.ResumeLayout(false);
            this.groupBoxGestion.ResumeLayout(false);
            this.groupBoxGestion.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxUsuarios;
        private System.Windows.Forms.ListBox listBoxUsuarios;
        private System.Windows.Forms.GroupBox groupBoxGestion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxDetalles;
        private System.Windows.Forms.Label labelPerfilesDelUsuario;
        private System.Windows.Forms.ListBox listBoxPerfilesDelUsuario;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBoxPerfilesDisponibles;
        private System.Windows.Forms.Button buttonRemover;
        private System.Windows.Forms.Button buttonAsignar;
    }
}
