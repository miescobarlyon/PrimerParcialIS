using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Permiso : PermisoBase
    {

        internal string tipo;

        public string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        public override string ToString()
        {
            return $"[{tipo}] {nombre}";
        }
    }
}
