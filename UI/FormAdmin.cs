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
        public FormAdmin()
        {
            InitializeComponent();
        }

        private void FormAdmin_Load(object sender, EventArgs e)
        {
            // Load the default view on startup
            LoadForm(new FormSubasta());
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

        private void rolesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadForm(new AdministrarRoles());
        }
    }
}
