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
    public partial class AdministrarRoles : Form
    {
        private BLL.PerfilService perfilService;
        private BLL.PermisoService permisoService;

        public AdministrarRoles()
        {
            InitializeComponent();
            perfilService = new BLL.PerfilService();
            permisoService = new BLL.PermisoService();

            perfilService.EnviarError += (mensaje) =>
                MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void AdministrarRoles_Load(object sender, EventArgs e)
        {
            CargarArbol();
            CargarLista();
        }

        private void CargarArbol()
        {
            treeViewPerfiles.Nodes.Clear();

            List<BE.Perfil> perfiles = perfilService.Listar();

            foreach (BE.Perfil perfil in perfiles)
            {
                TreeNode nodoPerfil = new TreeNode(perfil.Nombre);
                nodoPerfil.Tag = perfil;
                treeViewPerfiles.Nodes.Add(nodoPerfil);

                AgregarPermisos(nodoPerfil, perfil.Permisos);
            }

            treeViewPerfiles.ExpandAll();
        }

        private void AgregarPermisos(TreeNode nodoPadre, List<BE.Permiso> permisos)
        {
            if (permisos == null || permisos.Count == 0)
                return;

            foreach (BE.Permiso permiso in permisos)
            {
                TreeNode nodoPermiso = new TreeNode(permiso.ToString());
                nodoPermiso.Tag = permiso;
                nodoPadre.Nodes.Add(nodoPermiso);
            }
        }

        private void CargarLista()
        {
            List<BE.Permiso> permisos = permisoService.Listar();
            listBoxPermisos.DataSource = null;
            listBoxPermisos.DataSource = permisos;
        }

        private void CargarLista(BE.Perfil perfil)
        {
            if (perfil?.Permisos == null || perfil.Permisos.Count == 0)
            {
                listBoxPermisosDelPerfil.DataSource = null;
                labelMiembrosDelPerfil.Text = $"Permisos de '{perfil.Nombre}'";
                return;
            }

            List<BE.Permiso> permisosActualizados = new List<BE.Permiso>(perfil.Permisos);
            listBoxPermisosDelPerfil.DataSource = null;
            listBoxPermisosDelPerfil.DataSource = permisosActualizados;
            labelMiembrosDelPerfil.Text = $"Permisos de '{perfil.Nombre}'";
        }

        private void CargarDetalles()
        {
            TreeNode selectedNode = treeViewPerfiles.SelectedNode;

            if (selectedNode != null)
            {
                if (selectedNode.Tag is BE.Permiso permiso)
                {
                    textBoxDetalles.Text = $"Permiso: {permiso.Nombre}\r\nTipo: {permiso.Tipo}";
                }
                else if (selectedNode.Tag is BE.Perfil perfil)
                {
                    int cantidadPermisos = perfil.Permisos != null ? perfil.Permisos.Count : 0;
                    textBoxDetalles.Text = $"Perfil: {perfil.Nombre}\r\nPermisos: {cantidadPermisos}";
                }
            }
        }

        private void treeViewPerfiles_AfterSelect(object sender, TreeViewEventArgs e)
        {
            CargarDetalles();

            if (e.Node.Tag is BE.Perfil perfilSeleccionado)
            {
                buttonAgregarPermiso.Enabled = true;
                buttonQuitarPermiso.Enabled = true;
                CargarLista(perfilSeleccionado);
            }
            else
            {
                buttonAgregarPermiso.Enabled = false;
                buttonQuitarPermiso.Enabled = false;
                listBoxPermisosDelPerfil.DataSource = null;
                labelMiembrosDelPerfil.Text = "Permisos de...";
            }
        }

        private void buttonNuevoPerfil_Click(object sender, EventArgs e)
        {
            AgregarPerfil dialog = new AgregarPerfil();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                CargarArbol();
                CargarLista();
            }
        }

        private void buttonAgregarPermiso_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeViewPerfiles.SelectedNode;

            if (selectedNode == null)
            {
                MessageBox.Show("Por favor, seleccione un perfil.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!(selectedNode.Tag is BE.Perfil perfilSeleccionado))
            {
                MessageBox.Show("Por favor, seleccione un perfil válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (listBoxPermisos.SelectedItem == null)
            {
                MessageBox.Show("Por favor, seleccione un permiso para agregar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            BE.Permiso permisoSeleccionado = listBoxPermisos.SelectedItem as BE.Permiso;

            try
            {
                bool ok = perfilService.AgregarPermiso(perfilSeleccionado, permisoSeleccionado);

                if (!ok)
                {
                    return;
                }

                perfilService.Guardar(perfilSeleccionado);

                CargarArbol();
                CargarLista();

                TreeNode nodoActualizado = treeViewPerfiles.SelectedNode;
                if (nodoActualizado?.Tag is BE.Perfil perfilActualizado)
                {
                    CargarLista(perfilActualizado);
                }

                MessageBox.Show("Permiso agregado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar el permiso: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonQuitarPermiso_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeViewPerfiles.SelectedNode;

            if (selectedNode == null)
            {
                MessageBox.Show("Por favor, seleccione un perfil.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!(selectedNode.Tag is BE.Perfil perfilSeleccionado))
            {
                MessageBox.Show("Por favor, seleccione un perfil válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (listBoxPermisosDelPerfil.SelectedItem == null)
            {
                MessageBox.Show("Por favor, seleccione un permiso para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            BE.Permiso permisoSeleccionado = listBoxPermisosDelPerfil.SelectedItem as BE.Permiso;

            try
            {
                perfilService.QuitarPermiso(perfilSeleccionado, permisoSeleccionado);
                perfilService.Guardar(perfilSeleccionado);

                CargarArbol();
                CargarLista();

                TreeNode nodoActualizado = treeViewPerfiles.SelectedNode;
                if (nodoActualizado?.Tag is BE.Perfil perfilActualizado)
                {
                    CargarLista(perfilActualizado);
                }

                MessageBox.Show("Permiso eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar el permiso: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonEliminarPerfil_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeViewPerfiles.SelectedNode;

            if (selectedNode == null)
            {
                MessageBox.Show("Por favor, seleccione un perfil para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!(selectedNode.Tag is BE.Perfil perfilSeleccionado))
            {
                MessageBox.Show("Por favor, seleccione un perfil válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"¿Está seguro de que desea eliminar el perfil '{perfilSeleccionado.Nombre}'?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.No)
                return;

            try
            {
                perfilService.Eliminar(perfilSeleccionado);
                CargarArbol();
                CargarLista();
                MessageBox.Show("Perfil eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar el perfil: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
