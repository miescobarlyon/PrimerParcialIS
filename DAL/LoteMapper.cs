using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class LoteMapper : MAPPER<BE.Lote>
    {
        private ArticuloMapper articuloMapper;

        public LoteMapper()
        {
            acceso = new ACCESO();
            articuloMapper = new ArticuloMapper();
        }

        public override int Insertar(BE.Lote objeto)
        {
            try
            {
                acceso.Abrir();

                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(acceso.CrearParametro("@nombre", objeto.Nombre));
                

                int filas = acceso.Escribir("InsertarLote", parametros);
                if (filas <= 0) return -1;

                DataTable tabla = acceso.Leer("ObtenerUltimoLote");
                if (tabla.Rows.Count == 0) return -1;

                int idLote = Convert.ToInt32(tabla.Rows[0]["id_articulo"]);
                objeto.Id = idLote;

                if (objeto.Articulos != null)
                {
                    foreach (BE.UnidadDeVenta item in objeto.Articulos)
                    {
                        List<SqlParameter> pItems = new List<SqlParameter>
                    {
                        acceso.CrearParametro("@id_lote",  idLote),
                        acceso.CrearParametro("@id_articulo",  item.Id)
                    };

                        acceso.Escribir("InsertarLoteItem", pItems);
                    }
                }

                return filas;
            }
            catch (Exception)
            {
                return -1;
            }
            finally
            {
                acceso.Cerrar();
            }
        }

        public override int Editar(BE.Lote objeto)
        {
            try
            {
                acceso.Abrir();

                List<SqlParameter> parametros = new List<SqlParameter>
                {
                    acceso.CrearParametro("@id_articulo", objeto.Id),
                    acceso.CrearParametro("@nombre",      objeto.Nombre)
                };

                int filas = acceso.Escribir("EditarLote", parametros);

                List<SqlParameter> pBorrar = new List<SqlParameter>
                {
                    acceso.CrearParametro("@id_lote", objeto.Id)
                };

                acceso.Escribir("BorrarLoteItems", pBorrar);

                if (objeto.Articulos != null)
                {
                    foreach (BE.UnidadDeVenta item in objeto.Articulos)
                    {
                        List<SqlParameter> pItems = new List<SqlParameter>
                    {
                        acceso.CrearParametro("@id_lote", objeto.Id),
                        acceso.CrearParametro("@id_articulo", item.Id)
                    };

                        acceso.Escribir("InsertarLoteItem", pItems);
                    }
                }

                return filas;
            }
            catch (Exception)
            {
                return -1;
            }
            finally
            {
                acceso.Cerrar();
            }
        }

        public override int Borrar(BE.Lote objeto)
        {
            try
            {
                acceso.Abrir();

                List<SqlParameter> pItems = new List<SqlParameter>
            {
                acceso.CrearParametro("@id_lote", objeto.Id)
            };

                acceso.Escribir("BorrarLoteItems", pItems);

                List<SqlParameter> parametros = new List<SqlParameter>
            {
                acceso.CrearParametro("@id_articulo", objeto.Id)
            };

                return acceso.Escribir("BorrarLote", parametros);
            }
            catch (Exception)
            {
                return -1;
            }
            finally
            {
                acceso.Cerrar();
            }
        }

        public override List<BE.Lote> Listar()
        {
            List<BE.Lote> lista = new List<BE.Lote>();

            try
            {
                acceso.Abrir();
                DataTable tabla = acceso.Leer("ListarLotes");

                foreach (DataRow fila in tabla.Rows)
                {
                    BE.Lote l = new BE.Lote();
                    l.Id = Convert.ToInt32(fila["id_articulo"]);
                    l.Nombre = fila["nombre"].ToString();
                    lista.Add(l);
                }

                acceso.Cerrar();

                foreach (BE.Lote lote in lista)
                {
                    lote.Articulos = ObtenerHijos(lote.Id);
                }
            }
            catch (Exception)
            {
                lista = new List<BE.Lote>();
            }

            return lista;
        }


        public List<BE.UnidadDeVenta> ObtenerHijos(int idLote)
        {
            List<BE.UnidadDeVenta> hijos = new List<BE.UnidadDeVenta>();

            try
            {
                acceso.Abrir();

                List<SqlParameter> parametros = new List<SqlParameter>
            {
                acceso.CrearParametro("@id_lote", idLote)
            };

                DataTable tabla = acceso.Leer("ObtenerLoteItems", parametros);
                acceso.Cerrar();

                foreach (DataRow fila in tabla.Rows)
                {
                    string tipo = fila["tipo"].ToString();
                    int idItem = Convert.ToInt32(fila["id_articulo"]);

                    if (tipo == "Articulo")
                    {
                        BE.Articulo a = articuloMapper.ObtenerPorId(idItem);
                        if (a != null) hijos.Add(a);
                    }
                    else if (tipo == "Lote")
                    {
                        BE.Lote subLote = new BE.Lote();
                        subLote.Id = idItem;
                        subLote.Nombre = fila["nombre"].ToString();
                        subLote.Articulos = ObtenerHijos(idItem);
                        hijos.Add(subLote);
                    }
                }
            }
            catch (Exception)
            {
                hijos = new List<BE.UnidadDeVenta>();
            }

            return hijos;
        }

        public BE.Lote ObtenerPorId(int id)
        {
            try
            {
                acceso.Abrir();

                List<SqlParameter> parametros = new List<SqlParameter>
            {
                acceso.CrearParametro("@id_articulo", id)
            };

                DataTable tabla = acceso.Leer("ObtenerLotePorId", parametros);

                if (tabla.Rows.Count == 0) return null;

                DataRow fila = tabla.Rows[0];

                BE.Lote lote = new BE.Lote();
                lote.Id = Convert.ToInt32(fila["id_articulo"]);
                lote.Nombre = fila["nombre"].ToString();
                
                acceso.Cerrar();
                
                lote.Articulos = ObtenerHijos(lote.Id);

                return lote;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                acceso.Cerrar();
            }
        }
    }
}
