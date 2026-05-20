using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Error : EventArgs
    {
        private string mensaje;

        public string Mensaje
        {
            get { return mensaje; }
            set { mensaje = value; }
        }

        private EnumError tipo;

        public EnumError Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        private Exception excepcion;

        public Exception Excepcion
        {
            get { return excepcion; }
            set { excepcion = value; }
        }

        public Error()
        {
        }

        public Error(string mensaje, EnumError tipo, Exception excepcion = null)
        {
            this.mensaje = mensaje;
            this.tipo = tipo;
            this.excepcion = excepcion;
        }
    }
}
