using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PerfilMapper : MAPPER<BE.Perfil>
    {
        public PerfilMapper()
        {
            acceso = new ACCESO();
        }

        public override int Insertar(BE.Perfil objeto)
        {
            try
            {
                acceso.Abrir();

                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(acceso.CrearParametro("@nombre", objeto.Nombre));

                int filas = acceso.Escribir("InsertarPerfil", parametros);
                if (filas <= 0) return -1;

                DataTable tabla = acceso.Leer("ObtenerUltimoPerfil");
                if (tabla.Rows.Count == 0) return -1;

                int idPerfil = Convert.ToInt32(tabla.Rows[0]["PERFIL_ID"]);
                objeto.Id = idPerfil;

                if (objeto.Permisos != null)
                {
                    foreach (BE.Permiso item in objeto.Permisos)
                    {
                        List<SqlParameter> pItems = new List<SqlParameter>
                        {
                            acceso.CrearParametro("@id_perfil", idPerfil),
                            acceso.CrearParametro("@id_permiso", item.Id)
                        };

                        acceso.Escribir("InsertarPerfilPermiso", pItems);
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

        public override int Editar(BE.Perfil objeto)
        {
            try
            {
                acceso.Abrir();

                List<SqlParameter> parametros = new List<SqlParameter>
                {
                    acceso.CrearParametro("@id_perfil", objeto.Id),
                    acceso.CrearParametro("@nombre", objeto.Nombre)
                };

                int filas = acceso.Escribir("EditarPerfil", parametros);

                List<SqlParameter> pBorrar = new List<SqlParameter>
                {
                    acceso.CrearParametro("@id_perfil", objeto.Id)
                };

                acceso.Escribir("BorrarPerfilPermisos", pBorrar);

                if (objeto.Permisos != null)
                {
                    foreach (BE.Permiso item in objeto.Permisos)
                    {
                        List<SqlParameter> pItems = new List<SqlParameter>
                        {
                            acceso.CrearParametro("@id_perfil", objeto.Id),
                            acceso.CrearParametro("@id_permiso", item.Id)
                        };

                        acceso.Escribir("InsertarPerfilPermiso", pItems);
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

        public override int Borrar(BE.Perfil objeto)
        {
            try
            {
                acceso.Abrir();

                List<SqlParameter> pItems = new List<SqlParameter>
                {
                    acceso.CrearParametro("@id_perfil", objeto.Id)
                };

                acceso.Escribir("BorrarPerfilPermisos", pItems);

                List<SqlParameter> parametros = new List<SqlParameter>
                {
                    acceso.CrearParametro("@id_perfil", objeto.Id)
                };

                return acceso.Escribir("BorrarPerfil", parametros);
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

        public override List<BE.Perfil> Listar()
        {
            List<BE.Perfil> lista = new List<BE.Perfil>();

            try
            {
                acceso.Abrir();
                DataTable tabla = acceso.Leer("ListarPerfiles");

                foreach (DataRow fila in tabla.Rows)
                {
                    BE.Perfil p = new BE.Perfil();
                    p.Id = Convert.ToInt32(fila["PERFIL_ID"]);
                    p.Nombre = fila["NOMBRE"].ToString();
                    lista.Add(p);
                }

                acceso.Cerrar();

                foreach (BE.Perfil perfil in lista)
                {
                    perfil.Permisos = ObtenerPerfilPermisos(perfil.Id);
                }
            }
            catch (Exception)
            {
                lista = new List<BE.Perfil>();
            }

            return lista;
        }

        public List<BE.Permiso> ObtenerPerfilPermisos(int idPerfil)
        {
            List<BE.Permiso> permisos = new List<BE.Permiso>();

            try
            {
                acceso.Abrir();

                List<SqlParameter> parametros = new List<SqlParameter>
                {
                    acceso.CrearParametro("@id_perfil", idPerfil)
                };

                DataTable tabla = acceso.Leer("ObtenerPerfilPermisos", parametros);
                acceso.Cerrar();

                PermisoMapper permisoMapper = new PermisoMapper();

                foreach (DataRow fila in tabla.Rows)
                {
                    int idPermiso = Convert.ToInt32(fila["PERMISO_ID"]);
                    BE.Permiso p = permisoMapper.ObtenerPorId(idPermiso);
                    if (p != null) permisos.Add(p);
                }
            }
            catch (Exception)
            {
                permisos = new List<BE.Permiso>();
            }

            return permisos;
        }

        public BE.Perfil ObtenerPorId(int id)
        {
            try
            {
                acceso.Abrir();

                List<SqlParameter> parametros = new List<SqlParameter>
                {
                    acceso.CrearParametro("@id_perfil", id)
                };

                DataTable tabla = acceso.Leer("ObtenerPerfilPorId", parametros);

                if (tabla.Rows.Count == 0) return null;

                DataRow fila = tabla.Rows[0];

                BE.Perfil perfil = new BE.Perfil();
                perfil.Id = Convert.ToInt32(fila["PERFIL_ID"]);
                perfil.Nombre = fila["NOMBRE"].ToString();

                acceso.Cerrar();

                perfil.Permisos = ObtenerPerfilPermisos(perfil.Id);

                return perfil;
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

        public int AsignarPerfilAUsuario(int idUsuario, int idPerfil)
        {
            try
            {
                acceso.Abrir();

                List<SqlParameter> parametros = new List<SqlParameter>
                {
                    acceso.CrearParametro("@id_usuario", idUsuario),
                    acceso.CrearParametro("@id_perfil", idPerfil)
                };

                int resultado = acceso.Escribir("AsignarPerfilAUsuario", parametros);
                return resultado;
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

        public int RemoverPerfilDeUsuario(int idUsuario, int idPerfil)
        {
            try
            {
                acceso.Abrir();

                List<SqlParameter> parametros = new List<SqlParameter>
                {
                    acceso.CrearParametro("@id_usuario", idUsuario),
                    acceso.CrearParametro("@id_perfil", idPerfil)
                };

                int resultado = acceso.Escribir("RemoverPerfilDelUsuario", parametros);
                return resultado;
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

        public List<BE.Perfil> ObtenerPerfilesDeUsuario(int idUsuario)
        {
            List<BE.Perfil> perfiles = new List<BE.Perfil>();

            try
            {
                acceso.Abrir();

                List<SqlParameter> parametros = new List<SqlParameter>
                {
                    acceso.CrearParametro("@id_usuario", idUsuario)
                };

                DataTable tabla = acceso.Leer("ObtenerPerfilesDelUsuario", parametros);

                foreach (DataRow fila in tabla.Rows)
                {
                    BE.Perfil p = new BE.Perfil();
                    p.Id = Convert.ToInt32(fila["PERFIL_ID"]);
                    p.Nombre = fila["NOMBRE"].ToString();
                    perfiles.Add(p);
                }
            }
            catch (Exception)
            {
                perfiles = new List<BE.Perfil>();
            }
            finally
            {
                acceso.Cerrar();
            }

            return perfiles;
        }
    }
}
