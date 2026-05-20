using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SubastaMapper : MAPPER<BE.Subasta>
    {
        private ArticuloMapper articuloMapper;
        private LoteMapper loteMapper;

        public SubastaMapper() 
        { 
            acceso = new ACCESO();
            articuloMapper = new ArticuloMapper();
            loteMapper = new LoteMapper();
        }

        public override int Insertar(BE.Subasta obj)
        {
            try
            {
                acceso.Abrir();
                var p = new List<SqlParameter>
                {
                    acceso.CrearParametro("@id_unidad_venta", obj.Articulo.Id),
                    acceso.CrearParametro("@precio_inicial", obj.PrecioActual.ToString())
                };

                DataTable t = acceso.Leer("InsertarSubasta", p);
                return t.Rows.Count > 0 ? Convert.ToInt32(t.Rows[0][0]) : -1;
            }
            finally { acceso.Cerrar(); }
        }

        public int RegistrarOferta(BE.Subasta subasta, BE.Oferta oferta)
        {
            try
            {
                acceso.Abrir();
                List<SqlParameter> p = new List<SqlParameter>
                {
                    acceso.CrearParametro("@id_subasta", subasta.Id),
                    acceso.CrearParametro("@id_interesado", oferta.Ofertante.Id),
                    acceso.CrearParametro("@monto", oferta.Monto),
                    acceso.CrearParametro("@fecha_hora", oferta.FechaHora)
                };
                return acceso.Escribir("RegistrarOferta", p);
            }
            finally { acceso.Cerrar(); }
        }

        public int CerrarSubasta(BE.Subasta subasta)
        {
            try
            {
                acceso.Abrir();
                var p = new List<SqlParameter>
                {
                    acceso.CrearParametro("@id_subasta", subasta.Id),
                    acceso.CrearParametro("@id_ganador", subasta.Ganador?.Id ?? 0),
                    acceso.CrearParametro("@precio_final", subasta.PrecioActual),
                    acceso.CrearParametro("@fecha_cierre", DateTime.Now)
                };
                return acceso.Escribir("CerrarSubasta", p);
            }
            finally { acceso.Cerrar(); }
        }

        public override int Editar(BE.Subasta obj) => -1;
        public override int Borrar(BE.Subasta obj) => -1;

        public override List<BE.Subasta> Listar()
        {
            List<BE.Subasta> subastas = new List<BE.Subasta>();

            try
            {
                acceso.Abrir();
                DataTable t = acceso.Leer("ListarSubastas");

                foreach (DataRow row in t.Rows)
                {
                    int idUnidadVenta = Convert.ToInt32(row["id_articulo"]);
                    BE.UnidadDeVenta unidad = ObtenerUnidadDeVenta(idUnidadVenta);

                    subastas.Add(new BE.Subasta
                    {
                        Id = Convert.ToInt32(row["id_subasta"]),
                        Articulo = unidad,
                        PrecioActual = Convert.ToSingle(row["precio_final"]),
                        Estado = (BE.EstadoSubasta)Enum.Parse(typeof(BE.EstadoSubasta), row["estado"].ToString()),
                        Ganador = row["id_ganador"] != DBNull.Value ? new BE.Usuario { Id = Convert.ToInt32(row["id_ganador"]) } : null
                    });
                }
            }
            finally { acceso.Cerrar(); }

            return subastas;
        }

        private BE.UnidadDeVenta ObtenerUnidadDeVenta(int idUnidad)
        {
            try
            {
                acceso.Abrir();
                List<SqlParameter> p = new List<SqlParameter>
                {
                    acceso.CrearParametro("@id_articulo", idUnidad)
                };

                DataTable t = acceso.Leer("ObtenerUnidadVentaPorId", p);

                if (t.Rows.Count == 0)
                    return null;

                string tipo = t.Rows[0]["tipo"]?.ToString() ?? "Articulo";

                if (tipo == "Lote")
                {
                    return loteMapper.ObtenerPorId(idUnidad);
                }
                else
                {
                    return articuloMapper.ObtenerPorId(idUnidad);
                }
            }
            finally { acceso.Cerrar(); }
        }

    }
}
