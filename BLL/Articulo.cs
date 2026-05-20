using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Articulo : UnidadDeVenta<BE.Articulo>
    {
        public override string Descripcion(BE.Articulo articulo)
        {
            return $"\r\n- {articulo.Nombre}: {articulo.Descripcion}" ?? "";
        }

        public override float PrecioBase(BE.Articulo articulo)
        {
            return articulo.PrecioBase;
        }

        public override void Gruardar(BE.Articulo unidadDeVenta)
        {
            DAL.ArticuloMapper mapper = new DAL.ArticuloMapper();

            if (unidadDeVenta.Id == 0)
            {
                mapper.Insertar(unidadDeVenta);
            }
            else
            {
                mapper.Editar(unidadDeVenta);
            }
        }

        public override void Eliminar(BE.Articulo unidadDeVenta)
        {
            DAL.ArticuloMapper mapper = new DAL.ArticuloMapper();
            mapper.Borrar(unidadDeVenta);
        }

        public override List<BE.Articulo> Listar()
        {
            DAL.ArticuloMapper mapper = new DAL.ArticuloMapper();
            return mapper.Listar();
        }

    }
}
