using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UsuarioMapper : MAPPER<BE.Usuario>
    {
        public override int Insertar(BE.Usuario objeto)
        {
            acceso = new ACCESO();
            acceso.Abrir();

            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();
            parametros.Add(acceso.CrearParametro("@nombre", objeto.Nombre));
            parametros.Add(acceso.CrearParametro("@apellido", objeto.Apellido));
            parametros.Add(acceso.CrearParametro("@user", objeto.User));
            parametros.Add(acceso.CrearParametro("@pass", objeto.PasswordHash));

            int resultado = acceso.Escribir("InsertarUsuario", parametros);

            acceso.Cerrar();
            return resultado;
        }

        public override int Editar(BE.Usuario objeto)
        {
            throw new NotImplementedException();
        }

        public override int Borrar(BE.Usuario objeto)
        {
            throw new NotImplementedException();
        }

        public override List<BE.Usuario> Listar()
        {
            acceso = new ACCESO();
            acceso.Abrir();

            DataTable tabla = acceso.Leer("ListarUsuario");

            acceso.Cerrar();

            List<BE.Usuario> usuarios = new List<BE.Usuario>();

            foreach (DataRow fila in tabla.Rows)
            {
                BE.Usuario usuario = new BE.Usuario();
                usuario.Id = (int)fila["USUARIO_ID"];
                usuario.Nombre = (string)fila["NOMBRE"];
                usuario.Apellido = (string)fila["APELLIDO"];
                usuario.User = (string)fila["USUARIO"];
                usuario.PasswordHash = (string)fila["PASS_HASH"];
                usuario.Salt = (string)fila["SALT"];
                usuario.Bloqueado = (int)fila["BLOQUEADO"];
                usuario.Borrado = (int)fila["BORRADO"];

                usuarios.Add(usuario);
            }

            return usuarios;
        }

        public Tuple<string, string> TraerPass(string user)
        {
            acceso = new ACCESO();
            acceso.Abrir();

            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();
            parametros.Add(acceso.CrearParametro("@user", user));

            DataTable tabla = acceso.Leer("TraerPass", parametros);

            acceso.Cerrar();

            if (tabla.Rows.Count == 0)
                return null;

            string hash = (string)tabla.Rows[0]["PASS_HASH"];
            string salt = (string)tabla.Rows[0]["SALT"];

            return new Tuple<string, string>(hash, salt);
        }

        public BE.Usuario TraerUsuario(string user)
        {
            acceso = new ACCESO();
            acceso.Abrir();

            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();
            parametros.Add(acceso.CrearParametro("@user", user));

            DataTable tabla = acceso.Leer("TraerUsuario", parametros);

            acceso.Cerrar();

            if (tabla.Rows.Count == 0)
                return null;

            BE.Usuario usuario = new BE.Usuario();
            usuario.Id = (int)tabla.Rows[0]["USUARIO_ID"];
            usuario.Nombre = (string)tabla.Rows[0]["NOMBRE"];
            usuario.Apellido = (string)tabla.Rows[0]["APELLIDO"];
            usuario.User = (string)tabla.Rows[0]["USUARIO"];
            usuario.Bloqueado = (int)tabla.Rows[0]["BLOQUEADO"];

            // Load user profiles/roles
            usuario.Perfiles = ObtenerPerfilesDelUsuario(usuario.Id);

            return usuario;
        }

        public List<BE.Perfil> ObtenerPerfilesDelUsuario(int idUsuario)
        {
            List<BE.Perfil> perfiles = new List<BE.Perfil>();

            try
            {
                acceso = new ACCESO();
                acceso.Abrir();

                List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();
                parametros.Add(acceso.CrearParametro("@id_usuario", idUsuario));

                DataTable tabla = acceso.Leer("ObtenerPerfilesDelUsuario", parametros);
                acceso.Cerrar();

                PerfilMapper perfilMapper = new PerfilMapper();

                foreach (DataRow fila in tabla.Rows)
                {
                    int idPerfil = Convert.ToInt32(fila["PERFIL_ID"]);
                    BE.Perfil perfil = perfilMapper.ObtenerPorId(idPerfil);
                    if (perfil != null)
                        perfiles.Add(perfil);
                }
            }
            catch (Exception)
            {
                perfiles = new List<BE.Perfil>();
            }

            return perfiles;
        }

        public int AsignarPerfilAUsuario(int idUsuario, int idPerfil)
        {
            try
            {
                acceso = new ACCESO();
                acceso.Abrir();

                List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();
                parametros.Add(acceso.CrearParametro("@id_usuario", idUsuario));
                parametros.Add(acceso.CrearParametro("@id_perfil", idPerfil));

                int resultado = acceso.Escribir("AsignarPerfilAUsuario", parametros);
                acceso.Cerrar();
                return resultado;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int RemoverPerfilDelUsuario(int idUsuario, int idPerfil)
        {
            try
            {
                acceso = new ACCESO();
                acceso.Abrir();

                List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();
                parametros.Add(acceso.CrearParametro("@id_usuario", idUsuario));
                parametros.Add(acceso.CrearParametro("@id_perfil", idPerfil));

                int resultado = acceso.Escribir("RemoverPerfilDelUsuario", parametros);
                acceso.Cerrar();
                return resultado;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int TraerIntentos(BE.Usuario usuario)
        {
            acceso = new ACCESO();
            acceso.Abrir();

            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();
            parametros.Add(acceso.CrearParametro("@id", usuario.Id));

            DataTable tabla = acceso.Leer("TraerIntentos", parametros);

            acceso.Cerrar();

            if (tabla.Rows.Count == 0)
                return 0;

            return (int)tabla.Rows[0]["INTENTOS"];
        }

        public int AgregarIntento(BE.Usuario usuario)
        {
            acceso = new ACCESO();
            acceso.Abrir();

            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();
            parametros.Add(acceso.CrearParametro("@id", usuario.Id));

            int resultado = acceso.Escribir("AgregarIntento", parametros);

            acceso.Cerrar();
            return resultado;
        }

        public int ReestablecerIntentos(BE.Usuario usuario)
        {
            acceso = new ACCESO();
            acceso.Abrir();

            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();
            parametros.Add(acceso.CrearParametro("@id", usuario.Id));

            int resultado = acceso.Escribir("ReestablecerIntentos", parametros);

            acceso.Cerrar();
            return resultado;
        }

        public int BloquearUsuario(BE.Usuario usuario)
        {
            acceso = new ACCESO();
            acceso.Abrir();

            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();
            parametros.Add(acceso.CrearParametro("@id", usuario.Id));

            int resultado = acceso.Escribir("BloquearUsuario", parametros);

            acceso.Cerrar();
            return resultado;
        }
    }
}
