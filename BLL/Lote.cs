using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Lote : UnidadDeVenta<BE.Lote>
    {
        private Articulo articuloBLL = new Articulo();

        public override string Descripcion(BE.Lote lote)
        {
            if (lote?.Articulos == null || lote.Articulos.Count == 0)
                return string.Empty;

            List<string> descripciones = lote.Articulos
                .Select(a =>
                {
                    if (a is BE.Articulo)
                        return articuloBLL.Descripcion((BE.Articulo)a);
                    else if (a is BE.Lote)
                        return this.Descripcion((BE.Lote)a);
                    return string.Empty;
                })
                .Where(d => !string.IsNullOrEmpty(d))
                .ToList();

            return string.Join("\n", descripciones);
        }

        public override float PrecioBase(BE.Lote lote)
        {
            if (lote?.Articulos == null || lote.Articulos.Count == 0)
                return 0;

            return lote.Articulos
                .Sum(a =>
                {
                    if (a is BE.Articulo)
                        return articuloBLL.PrecioBase((BE.Articulo)a);
                    else if (a is BE.Lote)
                        return this.PrecioBase((BE.Lote)a);
                    return 0;
                });
        }

        public bool AgregarUnidad(BE.Lote lote, BE.UnidadDeVenta unidadAAgregar)
        {
            bool ok = true;
            if (!lote.Articulos.Contains(unidadAAgregar))
            {
                if (unidadAAgregar is BE.Lote lotePorAgregar)
                {
                    if (VerificarReferenciaCircular(lote.Id, lotePorAgregar.Id))
                    {
                        OnEnviarError("No se puede agregar este lote porque ya contiene al lote actual. Evite referencias circulares.");
                        ok = false;
                    }
                    else
                    {
                        lote.Articulos.Add(lotePorAgregar);
                    }
                }
                else
                {
                    lote.Articulos.Add(unidadAAgregar);
                }
            }

            return ok;
        }

        private bool VerificarReferenciaCircular(int idLotePadre, int idLoteHijo)
        {
            DAL.LoteMapper loteMapper = new DAL.LoteMapper();
            
            List<BE.UnidadDeVenta> hijosDelLoteAgregar = loteMapper.ObtenerHijos(idLoteHijo);

            return ContieneLote(hijosDelLoteAgregar, idLotePadre);
        }

        private bool ContieneLote(List<BE.UnidadDeVenta> unidades, int idLoteBuscado)
        {
            foreach (BE.UnidadDeVenta unidad in unidades)
            {
                if (unidad is BE.Lote lote)
                {
                    if (lote.Id == idLoteBuscado)
                        return true;

                    DAL.LoteMapper loteMapper = new DAL.LoteMapper();
                    List<BE.UnidadDeVenta> hijosDelLote = loteMapper.ObtenerHijos(lote.Id);
                    
                    if (ContieneLote(hijosDelLote, idLoteBuscado))
                        return true;
                }
            }

            return false;
        }

        public void EliminarUnidad(BE.Lote lote, BE.UnidadDeVenta unidadAAgregar)
        {
            if (lote.Articulos.Contains(unidadAAgregar))
            {
                lote.Articulos.Remove(unidadAAgregar);
            }
        }

        public override void Gruardar(BE.Lote unidadDeVenta)
        {
            DAL.LoteMapper mapper = new DAL.LoteMapper();

            if (unidadDeVenta.Id == 0)
            {
                mapper.Insertar(unidadDeVenta);
            }
            else
            {
                mapper.Editar(unidadDeVenta);
            }
        }

        public override void Eliminar(BE.Lote unidadDeVenta)
        {
            DAL.LoteMapper mapper = new DAL.LoteMapper();
            mapper.Borrar(unidadDeVenta);
        }

        public override List<BE.Lote> Listar()
        {
            DAL.LoteMapper mapper = new DAL.LoteMapper();
            return mapper.Listar();
        }
    }
}
