using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ACCESO
    {
        private SqlConnection conexion;

        public void Abrir()
        {
            conexion = new SqlConnection("Initial Catalog=PRIMER_PARCIAL_IS; Integrated Security=SSPI; Data Source=.");
            conexion.Open();
        }

        public void Cerrar()
        {
            if (conexion == null) return;
            conexion.Close();
            conexion = null;
            GC.Collect();
        }

        public SqlCommand CrearComando(string sql, List<SqlParameter> parametros = null)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = sql;
            cmd.Connection = conexion;

            if (parametros != null)
            {
                cmd.Parameters.AddRange(parametros.ToArray());
            }

            return cmd;
        }

        public SqlParameter CrearParametro(string nombre, string valor)
        {
            SqlParameter p = new SqlParameter();
            p.ParameterName = nombre;
            p.Value = valor;
            p.DbType = DbType.String;

            return p;
        }

        public SqlParameter CrearParametro(string nombre, int valor)
        {
            SqlParameter p = new SqlParameter();
            p.ParameterName = nombre;
            p.Value = valor;
            p.DbType = DbType.Int32;

            return p;
        }

        public SqlParameter CrearParametro(string nombre, float valor)
        {
            SqlParameter p = new SqlParameter();
            p.ParameterName = nombre;
            p.Value = valor;
            p.DbType = DbType.Single;

            return p;
        }

        public SqlParameter CrearParametro(string nombre, DateTime valor)
        {
            SqlParameter p = new SqlParameter();
            p.ParameterName = nombre;
            p.Value = valor;
            p.DbType = DbType.DateTime;

            return p;
        }

        public int Escribir(string sql, List<SqlParameter> parametros = null)
        {
            SqlCommand cmd = CrearComando(sql, parametros);
            int filas = 0;

            try
            {
                filas = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                filas = -1;
            }

            cmd.Parameters.Clear();

            return filas;
        }

        public DataTable Leer(string sql, List<SqlParameter> parametros = null)
        {
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = CrearComando(sql, parametros);

            DataTable tabla = new DataTable();

            adaptador.Fill(tabla);

            adaptador.Dispose();
            adaptador = null;

            return tabla;
        }
    }
}
