using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_Rinku.Interfaces
{
    public interface IViewModel
    {
        bool PuedeIniciarProceso { get; }
        void IniciarProceso();
        bool PuedeF5 { get; }
        void F5();
        bool PuedeF4 { get; }
        void F4(Window win);
        bool PuedeF1 { get; }
        void F1();
    }
}
