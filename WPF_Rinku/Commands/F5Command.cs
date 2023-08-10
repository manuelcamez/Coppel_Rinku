using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_Rinku.Interfaces;

namespace WPF_Rinku.Commands
{
    public class F5Command : ICommand
    {
        public readonly IViewModel _view;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested += value; }
        }
        public F5Command(IViewModel view)
        {
            _view = view;
        }
        public bool CanExecute(object parameter)
        {
            return _view.PuedeF5;
        }
        public void Execute(object parameter)
        {
            _view.F5();
        }
    }
}
