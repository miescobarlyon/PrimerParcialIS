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
    public partial class FormAdmin : Form
    {
        private BLL.ErrorManagerService errorManager;

        public FormAdmin()
        {
            InitializeComponent();
            errorManager = BLL.ErrorManagerService.GetInstancia();
        }

        private void FormAdmin_Load(object sender, EventArgs e)
        {
            if (!VerificarAutorizacion())
            {
                MessageBox.Show("No tiene permisos para acceder al panel administrativo.", "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            LoadForm(new FormSubasta());
        }

        private bool VerificarAutorizacion()
        {
            BLL.SessionManager sessionManager = BLL.SessionManager.GetInstancia();
            BE.Usuario usuarioActual = sessionManager.GetUsuario();

            if (usuarioActual == null)
                return false;

            bool tieneAcceso = BLL.UsuarioService.TieneRol(usuarioActual, "Administrador") ||
                               BLL.UsuarioService.TienePermiso(usuarioActual, "Acceso FormAdmin");

            return tieneAcceso;
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

        private void subastasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadForm(new FormSubasta());
        }

        private void asignarRolesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadForm(new AsignarRoles());
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

        private void administrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadForm(new AdministrarRoles());
        }

        private void asignarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadForm(new AsignarRoles());
        }
    }
}
