using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Perfil
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        private List<Permiso> permisos;

        public List<Permiso> Permisos
        {
            get { return permisos; }
            set { permisos = value; }
        }

        public Perfil()
        {
            permisos = new List<Permiso>();
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
