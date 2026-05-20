using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class RolCompuestoMapper : MAPPER<BE.RolCompuesto>
    {
        private RolIndividualMapper rolIndividualMapper;

        public RolCompuestoMapper()
        {
            acceso = new ACCESO();
            rolIndividualMapper = new RolIndividualMapper();
        }

        public override int Insertar(BE.RolCompuesto objeto)
        {
            try
            {
                acceso.Abrir();

                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(acceso.CrearParametro("@nombre", objeto.Nombre));
                

                int filas = acceso.Escribir("InsertarRolCompuesto", parametros);
                if (filas <= 0) return -1;

                DataTable tabla = acceso.Leer("ObtenerUltimoRolCompuesto");
                if (tabla.Rows.Count == 0) return -1;

                int idRolCompuesto = Convert.ToInt32(tabla.Rows[0]["id_permiso"]);
                objeto.Id = idRolCompuesto;

                if (objeto.Permisos != null)
                {
                    foreach (BE.Permiso item in objeto.Permisos)
                    {
                        List<SqlParameter> pItems = new List<SqlParameter>
                    {
                        acceso.CrearParametro("@id_rol_compuesto",  idRolCompuesto),
                        acceso.CrearParametro("@id_permiso",  item.Id)
                    };

                        acceso.Escribir("InsertarRolCompuestoItem", pItems);
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

        public override int Editar(BE.RolCompuesto objeto)
        {
            try
            {
                acceso.Abrir();

                List<SqlParameter> parametros = new List<SqlParameter>
                {
                    acceso.CrearParametro("@id_permiso", objeto.Id),
                    acceso.CrearParametro("@nombre",      objeto.Nombre)
                };

                int filas = acceso.Escribir("EditarRolCompuesto", parametros);

                List<SqlParameter> pBorrar = new List<SqlParameter>
                {
                    acceso.CrearParametro("@id_rol_compuesto", objeto.Id)
                };

                acceso.Escribir("BorrarRolCompuestoItems", pBorrar);

                if (objeto.Permisos != null)
                {
                    foreach (BE.Permiso item in objeto.Permisos)
                    {
                        List<SqlParameter> pItems = new List<SqlParameter>
                    {
                        acceso.CrearParametro("@id_rol_compuesto", objeto.Id),
                        acceso.CrearParametro("@id_permiso", item.Id)
                    };

                        acceso.Escribir("InsertarRolCompuestoItem", pItems);
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

        public override int Borrar(BE.RolCompuesto objeto)
        {
            try
            {
                acceso.Abrir();

                List<SqlParameter> pItems = new List<SqlParameter>
            {
                acceso.CrearParametro("@id_rol_compuesto", objeto.Id)
            };

                acceso.Escribir("BorrarRolCompuestoItems", pItems);

                List<SqlParameter> parametros = new List<SqlParameter>
            {
                acceso.CrearParametro("@id_permiso", objeto.Id)
            };

                return acceso.Escribir("BorrarRolCompuesto", parametros);
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

        public override List<BE.RolCompuesto> Listar()
        {
            List<BE.RolCompuesto> lista = new List<BE.RolCompuesto>();

            try
            {
                acceso.Abrir();
                DataTable tabla = acceso.Leer("ListarRolesCompuestos");

                foreach (DataRow fila in tabla.Rows)
                {
                    BE.RolCompuesto rc = new BE.RolCompuesto();
                    rc.Id = Convert.ToInt32(fila["id_permiso"]);
                    rc.Nombre = fila["nombre"].ToString();
                    lista.Add(rc);
                }

                acceso.Cerrar();

                foreach (BE.RolCompuesto rolCompuesto in lista)
                {
                    rolCompuesto.Permisos = ObtenerHijos(rolCompuesto.Id);
                }
            }
            catch (Exception)
            {
                lista = new List<BE.RolCompuesto>();
            }

            return lista;
        }


        public List<BE.Permiso> ObtenerHijos(int idRolCompuesto)
        {
            List<BE.Permiso> hijos = new List<BE.Permiso>();

            try
            {
                acceso.Abrir();

                List<SqlParameter> parametros = new List<SqlParameter>
            {
                acceso.CrearParametro("@id_rol_compuesto", idRolCompuesto)
            };

                DataTable tabla = acceso.Leer("ObtenerRolCompuestoItems", parametros);
                acceso.Cerrar();

                foreach (DataRow fila in tabla.Rows)
                {
                    string tipo = fila["tipo"].ToString();
                    int idItem = Convert.ToInt32(fila["id_permiso"]);

                    if (tipo == "RolIndividual")
                    {
                        BE.RolIndividual r = rolIndividualMapper.ObtenerPorId(idItem);
                        if (r != null) hijos.Add(r);
                    }
                    else if (tipo == "RolCompuesto")
                    {
                        BE.RolCompuesto subRolCompuesto = new BE.RolCompuesto();
                        subRolCompuesto.Id = idItem;
                        subRolCompuesto.Nombre = fila["nombre"].ToString();
                        subRolCompuesto.Permisos = ObtenerHijos(idItem);
                        hijos.Add(subRolCompuesto);
                    }
                }
            }
            catch (Exception)
            {
                hijos = new List<BE.Permiso>();
            }

            return hijos;
        }

        public BE.RolCompuesto ObtenerPorId(int id)
        {
            try
            {
                acceso.Abrir();

                List<SqlParameter> parametros = new List<SqlParameter>
            {
                acceso.CrearParametro("@id_permiso", id)
            };

                DataTable tabla = acceso.Leer("ObtenerRolCompuestoPorId", parametros);

                if (tabla.Rows.Count == 0) return null;

                DataRow fila = tabla.Rows[0];

                BE.RolCompuesto rolCompuesto = new BE.RolCompuesto();
                rolCompuesto.Id = Convert.ToInt32(fila["id_permiso"]);
                rolCompuesto.Nombre = fila["nombre"].ToString();
                
                acceso.Cerrar();
                
                rolCompuesto.Permisos = ObtenerHijos(rolCompuesto.Id);

                return rolCompuesto;
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
