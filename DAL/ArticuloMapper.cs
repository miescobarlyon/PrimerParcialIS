using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ArticuloMapper : MAPPER<BE.Articulo>
    {
        public ArticuloMapper()
        {
            acceso = new ACCESO();
        }

        public override int Insertar(Articulo objeto)
        {
            try
            {
                acceso.Abrir();

                List<SqlParameter> parametros = new List<SqlParameter>
            {
                acceso.CrearParametro("@nombre",      objeto.Nombre),
                acceso.CrearParametro("@descripcion", objeto.Descripcion),
                acceso.CrearParametro("@precio_base", objeto.PrecioBase.ToString())
            };

                return acceso.Escribir("InsertarArticulo", parametros);
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

        public override int Editar(Articulo objeto)
        {
            try
            {
                acceso.Abrir();

                List<SqlParameter> parametros = new List<SqlParameter>
            {
                acceso.CrearParametro("@id_articulo",  objeto.Id),
                acceso.CrearParametro("@nombre",       objeto.Nombre),
                acceso.CrearParametro("@descripcion",  objeto.Descripcion),
                acceso.CrearParametro("@precio_base",  objeto.PrecioBase.ToString())
            };

                return acceso.Escribir("EditarArticulo", parametros);
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

        public override int Borrar(Articulo objeto)
        {
            try
            {
                acceso.Abrir();

                List<SqlParameter> parametros = new List<SqlParameter>
            {
                acceso.CrearParametro("@id_articulo", objeto.Id)
            };

                return acceso.Escribir("BorrarArticulo", parametros);
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

        public override List<Articulo> Listar()
        {
            List<Articulo> lista = new List<Articulo>();

            try
            {
                acceso.Abrir();
                DataTable tabla = acceso.Leer("ListarArticulos");

                foreach (DataRow fila in tabla.Rows)
                {
                    Articulo a = new Articulo();
                    a.Id = Convert.ToInt32(fila["id_articulo"]);
                    a.Nombre = fila["nombre"].ToString();
                    a.Descripcion = fila["descripcion"].ToString();
                    a.PrecioBase = Convert.ToSingle(fila["precio"]);
                    lista.Add(a);
                }
            }
            catch (Exception)
            {
                lista = new List<Articulo>();
            }
            finally
            {
                acceso.Cerrar();
            }

            return lista;
        }

        public Articulo ObtenerPorId(int id)
        {
            try
            {
                acceso.Abrir();

                List<SqlParameter> parametros = new List<SqlParameter>
            {
                acceso.CrearParametro("@id_articulo", id)
            };

                DataTable tabla = acceso.Leer("ObtenerArticuloPorId", parametros);

                if (tabla.Rows.Count == 0) return null;

                DataRow fila = tabla.Rows[0];

                Articulo a = new Articulo();
                a.Id = Convert.ToInt32(fila["id_articulo"]);
                a.Nombre = fila["nombre"].ToString();
                a.Descripcion = fila["descripcion"].ToString();
                a.PrecioBase = Convert.ToSingle(fila["precio"]);

                return a;
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
