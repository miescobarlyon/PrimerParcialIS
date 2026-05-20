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
    public partial class AdministrarUnidadDeVenta : Form
    {
        private BLL.Lote loteService;
        private BLL.Articulo articuloService;

        public AdministrarUnidadDeVenta()
        {
            InitializeComponent();
            loteService = new BLL.Lote();
            articuloService = new BLL.Articulo();

            loteService.EnviarError += (mensaje) =>
                MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void AdministrarUnidadDeVenta_Load(object sender, EventArgs e)
        {
            CargarArbol();
            CargarLista();
        }

        private void CargarArbol()
        {
            treeViewUnidadesDeVenta.Nodes.Clear();

            List<BE.UnidadDeVenta> unidadesRaiz = ObtenerUnidadesRaiz();

            foreach (BE.UnidadDeVenta unidad in unidadesRaiz)
            {
                if (unidad is BE.Lote lote)
                {
                    TreeNode nodoLote = new TreeNode(lote.Nombre);
                    nodoLote.Tag = lote;
                    nodoLote.ImageIndex = 0;
                    treeViewUnidadesDeVenta.Nodes.Add(nodoLote);

                    AgregarHijos(nodoLote, lote.Articulos);
                }
                else if (unidad is BE.Articulo articulo)
                {
                    TreeNode nodoArticulo = new TreeNode(articulo.Nombre);
                    nodoArticulo.Tag = articulo;
                    nodoArticulo.ImageIndex = 1;
                    treeViewUnidadesDeVenta.Nodes.Add(nodoArticulo);
                }
            }
        }

        private List<BE.UnidadDeVenta> ObtenerUnidadesRaiz()
        {
            List<BE.UnidadDeVenta> resultado = new List<BE.UnidadDeVenta>();
            BLL.Lote loteService = new BLL.Lote();
            HashSet<int> articulosEnLotes = new HashSet<int>();

            List<BE.Lote> todosLosLotes = loteService.Listar();
            HashSet<int> lotesAnidados = new HashSet<int>();

            foreach (BE.Lote lote in todosLosLotes)
            {
                ObtenerLotesYArticulosAnidados(lote, lotesAnidados, articulosEnLotes);
            }

            foreach (BE.Lote lote in todosLosLotes)
            {
                if (!lotesAnidados.Contains(lote.Id))
                {
                    resultado.Add(lote);
                }
            }

            BLL.Articulo articuloService = new BLL.Articulo();
            List<BE.Articulo> todosLosArticulos = articuloService.Listar();

            foreach (BE.Articulo articulo in todosLosArticulos)
            {
                if (!articulosEnLotes.Contains(articulo.Id))
                {
                    resultado.Add(articulo);
                }
            }

            return resultado;
        }

        private void ObtenerLotesYArticulosAnidados(BE.Lote lote, HashSet<int> lotesAnidados, HashSet<int> articulosEnLotes)
        {
            if (lote.Articulos == null || lote.Articulos.Count == 0)
                return;

            foreach (BE.UnidadDeVenta unidad in lote.Articulos)
            {
                if (unidad is BE.Articulo articulo)
                {
                    articulosEnLotes.Add(articulo.Id);
                }
                else if (unidad is BE.Lote loteLote)
                {
                    lotesAnidados.Add(loteLote.Id);
                    ObtenerLotesYArticulosAnidados(loteLote, lotesAnidados, articulosEnLotes);
                }
            }
        }

        private void CargarLista()
        {
            List<BE.UnidadDeVenta> unidades = loteService.ListarCompleto();
            listBoxUnidadesDeVenta.DataSource = null;
            listBoxUnidadesDeVenta.DataSource = unidades;
        }

        private void CargarLista(BE.Lote lote)
        {
            if (lote?.Articulos == null || lote.Articulos.Count == 0)
            {
                listBoxLoteSeleccionado.DataSource = null;
                return;
            }

            List<BE.UnidadDeVenta> articulosActualizados = new List<BE.UnidadDeVenta>(lote.Articulos);
            listBoxLoteSeleccionado.DataSource = null;
            listBoxLoteSeleccionado.DataSource = articulosActualizados;
            labelMiembros.Text = $"Miembros dentro de '{lote.Nombre}'";
        }

        private void AgregarHijos(TreeNode nodoPadre, List<BE.UnidadDeVenta> hijos)
        {
            if (hijos == null || hijos.Count == 0)
                return;

            foreach (BE.UnidadDeVenta hijo in hijos)
            {
                TreeNode nodoHijo = new TreeNode(hijo.Nombre);
                nodoHijo.Tag = hijo;

                if (hijo is BE.Articulo articulo)
                {
                    nodoHijo.ImageIndex = 1;
                }
                else if (hijo is BE.Lote loteLote)
                {
                    nodoHijo.ImageIndex = 0;
                    AgregarHijos(nodoHijo, loteLote.Articulos);
                }

                nodoPadre.Nodes.Add(nodoHijo);
            }
        }

        private void buttonNuevoLote_Click(object sender, EventArgs e)
        {
            AgregarLote dialog = new AgregarLote();
            
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                CargarArbol();
                CargarLista();
            }
        }

        private void buttonNuevoArticulo_Click(object sender, EventArgs e)
        {
            AgregarArticulo dialog = new AgregarArticulo();
            
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                CargarArbol();
                CargarLista();
            }
        }

        private void buttonEliminarSeleccionado_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeViewUnidadesDeVenta.SelectedNode;
            
            if (selectedNode == null)
            {
                MessageBox.Show("Por favor, seleccione un elemento para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"¿Está seguro de que desea eliminar '{selectedNode.Text}'?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.No)
                return;

            try
            {
                object unidadDeVenta = selectedNode.Tag;

                if (unidadDeVenta is BE.Lote lote)
                {
                    loteService.Eliminar(lote);
                }
                else if (unidadDeVenta is BE.Articulo articulo)
                {
                    articuloService.Eliminar(articulo);
                }

                CargarArbol();
                CargarLista();
                MessageBox.Show("Elemento eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar el elemento: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAgregarArticuclo_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeViewUnidadesDeVenta.SelectedNode;

            if (selectedNode == null)
            {
                MessageBox.Show("Por favor, seleccione un lote.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!(selectedNode.Tag is BE.Lote loteSeleccionado))
            {
                MessageBox.Show("Por favor, seleccione un lote válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (listBoxUnidadesDeVenta.SelectedItem == null)
            {
                MessageBox.Show("Por favor, seleccione un artículo para agregar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            BE.UnidadDeVenta unidadSeleccionada = listBoxUnidadesDeVenta.SelectedItem as BE.UnidadDeVenta;

            if (unidadSeleccionada.Id == loteSeleccionado.Id)
            {
                MessageBox.Show("No se puede agregar un lote a sí mismo.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                bool ok = loteService.AgregarUnidad(loteSeleccionado, unidadSeleccionada);

                if (!ok)
                {
                    return;
                }

                loteService.Gruardar(loteSeleccionado);

                CargarArbol();
                CargarLista();
                
                TreeNode nodoActualizado = treeViewUnidadesDeVenta.SelectedNode;
                if (nodoActualizado?.Tag is BE.Lote loteActualizado)
                {
                    CargarLista(loteActualizado);
                }

                MessageBox.Show("Artículo agregado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar el artículo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void treeViewUnidadesDeVenta_AfterSelect(object sender, TreeViewEventArgs e)
        {
            CargarDetalles();

            if (e.Node.Tag is BE.Lote loteSeleccionado)
            {
                buttonAgregarArticuclo.Enabled = true;
                buttonQuitarArticulo.Enabled = true;
                CargarLista(loteSeleccionado);
            }
            else
            {
                buttonAgregarArticuclo.Enabled = false;
                buttonQuitarArticulo.Enabled = false;
                listBoxLoteSeleccionado.DataSource = null;
            }
        }

        private void buttonQuitarArticulo_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeViewUnidadesDeVenta.SelectedNode;

            if (selectedNode == null)
            {
                MessageBox.Show("Por favor, seleccione un lote.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!(selectedNode.Tag is BE.Lote loteSeleccionado))
            {
                MessageBox.Show("Por favor, seleccione un lote válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (listBoxLoteSeleccionado.SelectedItem == null)
            {
                MessageBox.Show("Por favor, seleccione un artículo para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            BE.UnidadDeVenta unidadSeleccionada = listBoxLoteSeleccionado.SelectedItem as BE.UnidadDeVenta;

            try
            {
                loteService.EliminarUnidad(loteSeleccionado, unidadSeleccionada);
                loteService.Gruardar(loteSeleccionado);

                CargarArbol();
                CargarLista();

                TreeNode nodoActualizado = treeViewUnidadesDeVenta.SelectedNode;
                if (nodoActualizado?.Tag is BE.Lote loteActualizado)
                {
                    CargarLista(loteActualizado);
                }

                MessageBox.Show("Artículo eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar el artículo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarDetalles()
        {
            TreeNode selectedNode = treeViewUnidadesDeVenta.SelectedNode;

            if (selectedNode != null)
            {
                if (selectedNode.Tag is BE.Articulo articulo)
                {
                    BLL.Articulo articuloService = new BLL.Articulo();
                    textBoxDetalles.Text = $"Artículo: {articulo.Nombre}\r\nDescripción: {articulo.Descripcion}\r\nPrecio Base: ${articulo.PrecioBase}";
                }
                else if (selectedNode.Tag is BE.Lote lote)
                {
                    BLL.Lote loteService = new BLL.Lote();
                    textBoxDetalles.Text = $"Lote: {lote.Nombre}\r\nPrecio: ${loteService.PrecioBase(lote)}\r\nDescripcion:{loteService.Descripcion(lote)}";
                }
            }
        }
    }
}
