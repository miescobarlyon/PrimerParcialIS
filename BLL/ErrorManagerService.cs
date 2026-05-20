using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ErrorManagerService
    {
        private static ErrorManagerService instancia;
        private static object lockObject = new object();

        public event EventHandler<BE.Error> OnOcurrioError;

        private ErrorManagerService()
        {
        }

        public static ErrorManagerService GetInstancia()
        {
            if (instancia == null)
            {
                lock (lockObject)
                {
                    if (instancia == null)
                    {
                        instancia = new ErrorManagerService();
                    }
                }
            }
            return instancia;
        }

        public void ManejarError(string mensaje, BE.EnumError tipo)
        {
            BE.Error error = new BE.Error(mensaje, tipo, null);
            OnOcurrioError?.Invoke(this, error);
        }

        public void ManejarError(Exception excepcion, BE.EnumError tipo)
        {
            BE.Error error = new BE.Error(excepcion.Message, tipo, excepcion);
            OnOcurrioError?.Invoke(this, error);
        }
    }
}
