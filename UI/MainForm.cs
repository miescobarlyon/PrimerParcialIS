using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public partial class MainForm : Form
    {
        private BLL.ErrorManagerService errorManager;
        private static bool isErrorSubscribed = false;

        public MainForm()
        {
            InitializeComponent();
            errorManager = BLL.ErrorManagerService.GetInstancia();

            if (!isErrorSubscribed)
            {
                errorManager.OnOcurrioError += MostrarError;
                isErrorSubscribed = true;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ConfigureMenuItemsBasedOnRoles();
            LoadDefaultForm();
        }

        private void ConfigureMenuItemsBasedOnRoles()
        {
            BLL.SessionManager sessionManager = BLL.SessionManager.GetInstancia();
            BE.Usuario usuarioActual = sessionManager.GetUsuario();

            if (usuarioActual == null)
            {
                MessageBox.Show("No user authenticated.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            subastasToolStripMenuItem.Visible = false;
            rolesToolStripMenuItem.Visible = false;
            ofertasToolStripMenuItem.Visible = false;
            unidadesDeVentaToolStripMenuItem.Visible = false;

            if (BLL.UsuarioService.TieneRol(usuarioActual, "Administrador"))
           {
                subastasToolStripMenuItem.Visible = true;
                rolesToolStripMenuItem.Visible = true;
           }

           if (BLL.UsuarioService.TienePermiso(usuarioActual, "Acceso FormOfertas"))
           {
                ofertasToolStripMenuItem.Visible = true;
           }

           if (BLL.UsuarioService.TienePermiso(usuarioActual, "Acceso FormUnidadesDeVenta"))
           {
                unidadesDeVentaToolStripMenuItem.Visible = true;
            }

        }

        private void LoadDefaultForm()
        {
            BLL.SessionManager sessionManager = BLL.SessionManager.GetInstancia();
            BE.Usuario usuarioActual = sessionManager.GetUsuario();

            bool esAdministrador = BLL.UsuarioService.TieneRol(usuarioActual, "Administrador");

            if (esAdministrador)
            {
                LoadForm(new FormSubasta());
            }
            else
            {
                LoadForm(new AdministrarOferta());
            }
        }

        public void LoadForm(Form form)
        {
            panelContenido.Controls.Clear();
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            panelContenido.Controls.Add(form);
            form.Show();
        }

        private void MostrarError(object sender, BE.Error e)
        {
            MessageBoxIcon icono = MessageBoxIcon.Information;

            switch (e.Tipo)
            {
                case BE.EnumError.Info:
                    icono = MessageBoxIcon.Information;
                    break;
                case BE.EnumError.Advertencia:
                    icono = MessageBoxIcon.Warning;
                    break;
                case BE.EnumError.Error:
                    icono = MessageBoxIcon.Error;
                    break;
                case BE.EnumError.Critico:
                    icono = MessageBoxIcon.Stop;
                    break;
            }

            MessageBox.Show(e.Mensaje, "Información del Sistema", MessageBoxButtons.OK, icono);
        }

        private void subastasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadForm(new FormSubasta());
        }

        private void administrarRolesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadForm(new AdministrarRoles());
        }

        private void asignarRolesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadForm(new AsignarRoles());
        }

        private void ofertasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadForm(new AdministrarOferta());
        }

        private void cerrarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BLL.SessionManager sessionManager = BLL.SessionManager.GetInstancia();
            sessionManager.Logout();

            IniciarSesion form = new IniciarSesion();
            form.Show();
            form.BringToFront();
            this.Close();
        }

        private void unidadesDeVentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadForm(new AdministrarUnidadDeVenta());
        }
    }
}
