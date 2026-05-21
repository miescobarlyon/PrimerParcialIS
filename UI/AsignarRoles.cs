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
    public partial class AsignarRoles : Form
    {
        private BLL.UsuarioPerfilService _service;

        public AsignarRoles()
        {
            InitializeComponent();
            _service = new BLL.UsuarioPerfilService();
            _service.EnviarError += (msg) => MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            buttonAsignar.Enabled = false;
            buttonRemover.Enabled = false;
        }

        private void AsignarRoles_Load(object sender, EventArgs e)
        {
            CargarUsuarios();
        }

        private void CargarUsuarios()
        {
            listBoxUsuarios.DataSource = null;
            listBoxUsuarios.DataSource = _service.ListarUsuarios();
            listBoxUsuarios.DisplayMember = "User";
        }

        private void CargarPerfilesDisponibles()
        {
            if (listBoxUsuarios.SelectedItem is null)
            {
                listBoxPerfilesDisponibles.DataSource = null;
                return;
            }

            BE.Usuario usuario = (BE.Usuario)listBoxUsuarios.SelectedItem;
            List<BE.Perfil> todosLosPerfiles = _service.ListarPerfiles();
            List<BE.Perfil> perfilesDelUsuario = _service.ListarPerfilesDeUsuario(usuario);

            // Filter: show only profiles NOT already assigned to this user
            List<BE.Perfil> perfilesDisponibles = todosLosPerfiles
                .Where(p => !perfilesDelUsuario.Any(pu => pu.Id == p.Id))
                .ToList();

            listBoxPerfilesDisponibles.DataSource = null;
            listBoxPerfilesDisponibles.DataSource = perfilesDisponibles;
            listBoxPerfilesDisponibles.DisplayMember = "Nombre";
        }

        private void CargarPerfilesDeUsuario()
        {
            if (listBoxUsuarios.SelectedItem is null)
            {
                listBoxPerfilesDelUsuario.DataSource = null;
                return;
            }

            BE.Usuario usuario = (BE.Usuario)listBoxUsuarios.SelectedItem;
            List<BE.Perfil> perfilesDelUsuario = _service.ListarPerfilesDeUsuario(usuario);
            
            listBoxPerfilesDelUsuario.DataSource = null;
            listBoxPerfilesDelUsuario.DataSource = perfilesDelUsuario;
            listBoxPerfilesDelUsuario.DisplayMember = "Nombre";
            
            labelPerfilesDelUsuario.Text = $"Perfiles de '{usuario.User}'";
        }

        private void CargarDetalles(List<BE.Perfil> perfilesDelUsuario = null)
        {
            if (listBoxUsuarios.SelectedItem is null)
            {
                textBoxDetalles.Text = "";
                return;
            }

            BE.Usuario u = (BE.Usuario)listBoxUsuarios.SelectedItem;
            
            if (perfilesDelUsuario == null)
            {
                perfilesDelUsuario = _service.ListarPerfilesDeUsuario(u);
            }

            textBoxDetalles.Text = $"Usuario: {u.User}\r\nNombre: {u.Nombre} {u.Apellido}\r\nPerfiles asignados: {perfilesDelUsuario.Count}";
        }

        private void listBoxUsuarios_SelectedValueChanged(object sender, EventArgs e)
        {
            bool hayUsuario = listBoxUsuarios.SelectedItem != null;
            buttonAsignar.Enabled = hayUsuario;
            buttonRemover.Enabled = hayUsuario;
            CargarPerfilesDeUsuario();
            CargarPerfilesDisponibles();
            CargarDetalles();
        }

        private void buttonAsignar_Click(object sender, EventArgs e)
        {
            if (listBoxUsuarios.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecciona un usuario.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (listBoxPerfilesDisponibles.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecciona un perfil.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            BE.Usuario usuario = (BE.Usuario)listBoxUsuarios.SelectedItem;
            BE.Perfil perfil = (BE.Perfil)listBoxPerfilesDisponibles.SelectedItem;

            bool ok = _service.AsignarPerfil(usuario, perfil);

            if (ok)
            {
                List<BE.Perfil> perfilesActualizados = _service.ListarPerfilesDeUsuario(usuario);
                CargarPerfilesDeUsuario();
                CargarPerfilesDisponibles();
                CargarDetalles(perfilesActualizados);
                MessageBox.Show("Perfil asignado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonRemover_Click(object sender, EventArgs e)
        {
            if (listBoxUsuarios.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecciona un usuario.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (listBoxPerfilesDelUsuario.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecciona un perfil para remover.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                "¿Desea remover este perfil del usuario?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.No)
                return;

            BE.Usuario usuario = (BE.Usuario)listBoxUsuarios.SelectedItem;
            BE.Perfil perfil = (BE.Perfil)listBoxPerfilesDelUsuario.SelectedItem;

            _service.RemoverPerfil(usuario, perfil);

            List<BE.Perfil> perfilesActualizados = _service.ListarPerfilesDeUsuario(usuario);
            CargarPerfilesDeUsuario();
            CargarPerfilesDisponibles();
            CargarDetalles(perfilesActualizados);
            MessageBox.Show("Perfil removido exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
