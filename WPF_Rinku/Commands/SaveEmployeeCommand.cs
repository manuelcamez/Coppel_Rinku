using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_Rinku.Interfaces;

namespace WPF_Rinku.Commands
{
    public class SaveEmployeeCommand : ICommand
    {
        public readonly IViewModel _view;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested += value; }
        }
        public SaveEmployeeCommand(IViewModel view)
        {
            _view = view;
        }
        public bool CanExecute(object parameter)
        {
            return _view.PuedeSaveEmployee;
        }
        public void Execute(object parameter)
        {
            _view.SaveEmployee();
        }
    }
}
