using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PermisoMapper : MAPPER<BE.Permiso>
    {
        public PermisoMapper()
        {
            acceso = new ACCESO();
        }

        public override int Insertar(BE.Permiso objeto)
        {
            try
            {
                acceso.Abrir();

                List<SqlParameter> parametros = new List<SqlParameter>
                {
                    acceso.CrearParametro("@nombre", objeto.Nombre),
                    acceso.CrearParametro("@tipo", objeto.Tipo)
                };

                return acceso.Escribir("InsertarPermiso", parametros);
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

        public override int Editar(BE.Permiso objeto)
        {
            try
            {
                acceso.Abrir();

                List<SqlParameter> parametros = new List<SqlParameter>
                {
                    acceso.CrearParametro("@id_permiso", objeto.Id),
                    acceso.CrearParametro("@nombre", objeto.Nombre),
                    acceso.CrearParametro("@tipo", objeto.Tipo)
                };

                return acceso.Escribir("EditarPermiso", parametros);
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

        public override int Borrar(BE.Permiso objeto)
        {
            try
            {
                acceso.Abrir();

                List<SqlParameter> parametros = new List<SqlParameter>
                {
                    acceso.CrearParametro("@id_permiso", objeto.Id)
                };

                return acceso.Escribir("BorrarPermiso", parametros);
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

        public override List<BE.Permiso> Listar()
        {
            List<BE.Permiso> lista = new List<BE.Permiso>();

            try
            {
                acceso.Abrir();
                DataTable tabla = acceso.Leer("ListarPermisos");

                foreach (DataRow fila in tabla.Rows)
                {
                    BE.Permiso p = new BE.Permiso();
                    p.Id = Convert.ToInt32(fila["PERMISO_ID"]);
                    p.Nombre = fila["NOMBRE"].ToString();
                    p.Tipo = fila["TIPO"].ToString();
                    lista.Add(p);
                }
            }
            catch (Exception)
            {
                lista = new List<BE.Permiso>();
            }
            finally
            {
                acceso.Cerrar();
            }

            return lista;
        }

        public BE.Permiso ObtenerPorId(int id)
        {
            try
            {
                acceso.Abrir();

                List<SqlParameter> parametros = new List<SqlParameter>
                {
                    acceso.CrearParametro("@id_permiso", id)
                };

                DataTable tabla = acceso.Leer("ObtenerPermisoPorId", parametros);

                if (tabla.Rows.Count == 0) return null;

                DataRow fila = tabla.Rows[0];

                BE.Permiso p = new BE.Permiso();
                p.Id = Convert.ToInt32(fila["PERMISO_ID"]);
                p.Nombre = fila["NOMBRE"].ToString();
                p.Tipo = fila["TIPO"].ToString();

                return p;
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
