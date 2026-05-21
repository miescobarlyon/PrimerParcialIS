namespace UI
{
    partial class AdministrarRoles
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
            this.treeViewPerfiles = new System.Windows.Forms.TreeView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonEliminarPerfil = new System.Windows.Forms.Button();
            this.buttonNuevoPerfil = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelMiembrosDelPerfil = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonQuitarPermiso = new System.Windows.Forms.Button();
            this.listBoxPermisosDelPerfil = new System.Windows.Forms.ListBox();
            this.listBoxPermisos = new System.Windows.Forms.ListBox();
            this.buttonAgregarPermiso = new System.Windows.Forms.Button();
            this.textBoxDetalles = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeViewPerfiles
            // 
            this.treeViewPerfiles.Location = new System.Drawing.Point(24, 38);
            this.treeViewPerfiles.Name = "treeViewPerfiles";
            this.treeViewPerfiles.Size = new System.Drawing.Size(328, 417);
            this.treeViewPerfiles.TabIndex = 0;
            this.treeViewPerfiles.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewPerfiles_AfterSelect);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonEliminarPerfil);
            this.groupBox1.Controls.Add(this.buttonNuevoPerfil);
            this.groupBox1.Controls.Add(this.treeViewPerfiles);
            this.groupBox1.Location = new System.Drawing.Point(23, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(381, 623);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Perfiles";
            // 
            // buttonEliminarPerfil
            // 
            this.buttonEliminarPerfil.Location = new System.Drawing.Point(24, 563);
            this.buttonEliminarPerfil.Name = "buttonEliminarPerfil";
            this.buttonEliminarPerfil.Size = new System.Drawing.Size(328, 32);
            this.buttonEliminarPerfil.TabIndex = 3;
            this.buttonEliminarPerfil.Text = "Eliminar seleccionado";
            this.buttonEliminarPerfil.UseVisualStyleBackColor = true;
            this.buttonEliminarPerfil.Click += new System.EventHandler(this.buttonEliminarPerfil_Click);
            // 
            // buttonNuevoPerfil
            // 
            this.buttonNuevoPerfil.Location = new System.Drawing.Point(24, 470);
            this.buttonNuevoPerfil.Name = "buttonNuevoPerfil";
            this.buttonNuevoPerfil.Size = new System.Drawing.Size(328, 32);
            this.buttonNuevoPerfil.TabIndex = 1;
            this.buttonNuevoPerfil.Text = "Nuevo Perfil";
            this.buttonNuevoPerfil.UseVisualStyleBackColor = true;
            this.buttonNuevoPerfil.Click += new System.EventHandler(this.buttonNuevoPerfil_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.textBoxDetalles);
            this.groupBox2.Controls.Add(this.labelMiembrosDelPerfil);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.buttonQuitarPermiso);
            this.groupBox2.Controls.Add(this.listBoxPermisosDelPerfil);
            this.groupBox2.Controls.Add(this.listBoxPermisos);
            this.groupBox2.Controls.Add(this.buttonAgregarPermiso);
            this.groupBox2.Location = new System.Drawing.Point(427, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(740, 623);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Gestión de Permisos";
            // 
            // labelMiembrosDelPerfil
            // 
            this.labelMiembrosDelPerfil.AutoSize = true;
            this.labelMiembrosDelPerfil.Location = new System.Drawing.Point(454, 38);
            this.labelMiembrosDelPerfil.Name = "labelMiembrosDelPerfil";
            this.labelMiembrosDelPerfil.Size = new System.Drawing.Size(155, 20);
            this.labelMiembrosDelPerfil.TabIndex = 7;
            this.labelMiembrosDelPerfil.Text = "Permisos de \'Perfil\'";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Permisos disponibles";
            // 
            // buttonQuitarPermiso
            // 
            this.buttonQuitarPermiso.Location = new System.Drawing.Point(299, 216);
            this.buttonQuitarPermiso.Name = "buttonQuitarPermiso";
            this.buttonQuitarPermiso.Size = new System.Drawing.Size(145, 32);
            this.buttonQuitarPermiso.TabIndex = 5;
            this.buttonQuitarPermiso.Text = "< Quitar";
            this.buttonQuitarPermiso.UseVisualStyleBackColor = true;
            this.buttonQuitarPermiso.Click += new System.EventHandler(this.buttonQuitarPermiso_Click);
            // 
            // listBoxPermisosDelPerfil
            // 
            this.listBoxPermisosDelPerfil.FormattingEnabled = true;
            this.listBoxPermisosDelPerfil.ItemHeight = 20;
            this.listBoxPermisosDelPerfil.Location = new System.Drawing.Point(458, 78);
            this.listBoxPermisosDelPerfil.Name = "listBoxPermisosDelPerfil";
            this.listBoxPermisosDelPerfil.Size = new System.Drawing.Size(261, 264);
            this.listBoxPermisosDelPerfil.TabIndex = 4;
            // 
            // listBoxPermisos
            // 
            this.listBoxPermisos.FormattingEnabled = true;
            this.listBoxPermisos.ItemHeight = 20;
            this.listBoxPermisos.Location = new System.Drawing.Point(24, 78);
            this.listBoxPermisos.Name = "listBoxPermisos";
            this.listBoxPermisos.Size = new System.Drawing.Size(261, 264);
            this.listBoxPermisos.TabIndex = 3;
            // 
            // buttonAgregarPermiso
            // 
            this.buttonAgregarPermiso.Location = new System.Drawing.Point(299, 172);
            this.buttonAgregarPermiso.Name = "buttonAgregarPermiso";
            this.buttonAgregarPermiso.Size = new System.Drawing.Size(145, 32);
            this.buttonAgregarPermiso.TabIndex = 1;
            this.buttonAgregarPermiso.Text = "Agregar >";
            this.buttonAgregarPermiso.UseVisualStyleBackColor = true;
            this.buttonAgregarPermiso.Click += new System.EventHandler(this.buttonAgregarPermiso_Click);
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
            // AdministrarRoles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1192, 647);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "AdministrarRoles";
            this.Text = "Administrar Roles";
            this.Load += new System.EventHandler(this.AdministrarRoles_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewPerfiles;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonEliminarPerfil;
        private System.Windows.Forms.Button buttonNuevoPerfil;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonQuitarPermiso;
        private System.Windows.Forms.ListBox listBoxPermisosDelPerfil;
        private System.Windows.Forms.ListBox listBoxPermisos;
        private System.Windows.Forms.Button buttonAgregarPermiso;
        private System.Windows.Forms.Label labelMiembrosDelPerfil;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxDetalles;
        private System.Windows.Forms.Label label2;
    }
}
