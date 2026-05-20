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
    public class RolIndividualMapper : MAPPER<BE.RolIndividual>
    {
        public RolIndividualMapper()
        {
            acceso = new ACCESO();
        }

        public override int Insertar(RolIndividual objeto)
        {
            try
            {
                acceso.Abrir();

                List<SqlParameter> parametros = new List<SqlParameter>
            {
                acceso.CrearParametro("@nombre",      objeto.Nombre),
                acceso.CrearParametro("@descripcion", objeto.Descripcion)
            };

                return acceso.Escribir("InsertarRolIndividual", parametros);
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

        public override int Editar(RolIndividual objeto)
        {
            try
            {
                acceso.Abrir();

                List<SqlParameter> parametros = new List<SqlParameter>
            {
                acceso.CrearParametro("@id_permiso",   objeto.Id),
                acceso.CrearParametro("@nombre",       objeto.Nombre),
                acceso.CrearParametro("@descripcion",  objeto.Descripcion)
            };

                return acceso.Escribir("EditarRolIndividual", parametros);
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

        public override int Borrar(RolIndividual objeto)
        {
            try
            {
                acceso.Abrir();

                List<SqlParameter> parametros = new List<SqlParameter>
            {
                acceso.CrearParametro("@id_permiso", objeto.Id)
            };

                return acceso.Escribir("BorrarRolIndividual", parametros);
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

        public override List<RolIndividual> Listar()
        {
            List<RolIndividual> lista = new List<RolIndividual>();

            try
            {
                acceso.Abrir();
                DataTable tabla = acceso.Leer("ListarRolesIndividuales");

                foreach (DataRow fila in tabla.Rows)
                {
                    RolIndividual r = new RolIndividual();
                    r.Id = Convert.ToInt32(fila["id_permiso"]);
                    r.Nombre = fila["nombre"].ToString();
                    r.Descripcion = fila["descripcion"].ToString();
                    lista.Add(r);
                }
            }
            catch (Exception)
            {
                lista = new List<RolIndividual>();
            }
            finally
            {
                acceso.Cerrar();
            }

            return lista;
        }

        public RolIndividual ObtenerPorId(int id)
        {
            try
            {
                acceso.Abrir();

                List<SqlParameter> parametros = new List<SqlParameter>
            {
                acceso.CrearParametro("@id_permiso", id)
            };

                DataTable tabla = acceso.Leer("ObtenerRolIndividualPorId", parametros);

                if (tabla.Rows.Count == 0) return null;

                DataRow fila = tabla.Rows[0];

                RolIndividual r = new RolIndividual();
                r.Id = Convert.ToInt32(fila["id_permiso"]);
                r.Nombre = fila["nombre"].ToString();
                r.Descripcion = fila["descripcion"].ToString();

                return r;
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
