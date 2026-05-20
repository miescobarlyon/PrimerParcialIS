using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public abstract class UnidadDeVenta<T> where T : BE.UnidadDeVenta
    {
        public event BE.delEnviarError EnviarError;
        public abstract float PrecioBase(T unidadDeVenta);
        public abstract string Descripcion(T unidadDeVenta);
        public abstract void Gruardar(T unidadDeVenta);
        public abstract void Eliminar(T unidadDeVenta);
        public virtual List<T> Listar() 
        {
            return new List<T>();
        }

        public List<BE.UnidadDeVenta> ListarCompleto()
        {
            List<BE.UnidadDeVenta> resultado = new List<BE.UnidadDeVenta>();

            BLL.Lote loteService = new Lote();

            List<BE.Lote> lotes = loteService.Listar();
            HashSet<int> articulosEnLotes = new HashSet<int>();

            foreach (BE.Lote lote in lotes)
            {
                resultado.Add(lote);
                ObtenerArticulosEnLote(lote, articulosEnLotes);
            }

            Articulo articuloService = new Articulo();
            List<BE.Articulo> artículos = articuloService.Listar();

            foreach (BE.Articulo articulo in artículos)
            {
                if (!articulosEnLotes.Contains(articulo.Id))
                {
                    resultado.Add(articulo);
                }
            }

            return resultado;
        }

        private void ObtenerArticulosEnLote(BE.Lote lote, HashSet<int> articulosEnLotes)
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
                    ObtenerArticulosEnLote(loteLote, articulosEnLotes);
                }
            }
        }

        protected void OnEnviarError(string mensaje)
        {
            EnviarError.Invoke(mensaje);
        }
    }
}
