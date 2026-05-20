using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public abstract class UnidadDeVenta
    {
        internal int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        internal string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

    }
}
