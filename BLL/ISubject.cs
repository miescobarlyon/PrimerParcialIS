using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface ISubject
    {
        void Suscribir(BE.Subasta subasta, IObserver observer);
        void Desuscribir(BE.Subasta subasta, IObserver observer);
        void Notificar(BE.Subasta subasta, string evento);
    }
}
