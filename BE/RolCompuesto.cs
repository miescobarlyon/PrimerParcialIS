using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class RolCompuesto : Permiso
    {
        private List<Permiso> permisos;

        public List<Permiso> Permisos
        {
            get { return permisos; }
            set { permisos = value; }
        }

        public override string ToString()
        {
            return $"[ROL COMPUESTO] {nombre}";
        }

    }
}
