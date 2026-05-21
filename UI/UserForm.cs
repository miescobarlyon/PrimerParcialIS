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
    public partial class UserForm : Form
    {
        private BLL.ErrorManagerService errorManager;

        public UserForm()
        {
            InitializeComponent();
            errorManager = BLL.ErrorManagerService.GetInstancia();
        }

        private void UserForm_Load(object sender, EventArgs e)
        {
            // Load the default view on startup
            LoadForm(new AdministrarOferta());
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

        private void administrarOfertaToolStripMenuItem_Click(object sender, EventArgs e)
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
    }
}
