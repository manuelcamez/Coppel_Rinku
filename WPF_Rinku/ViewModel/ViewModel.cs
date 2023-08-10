using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPF_Rinku.Commands;
using WPF_Rinku.Commants;
using WPF_Rinku.Interfaces;
using WPF_Rinku.Services;
using WPF_Rinku.Views;

namespace WPF_Rinku.ViewModel
{
    public class ViewModel : IViewModel, INotifyPropertyChanged
    {
        private string _horaInicio; 
        private string _horaFinal;
        private string _duracion;

        /*VistaEmployee*/
        private string _nameEmployee;
        private string _hourlyWage;
        

        private Services.Services _service;
        public bool PuedeIniciarProceso => true;
        public bool PuedeSaveEmployee => true;
        public bool PuedeF1 => true;
        public bool PuedeF4 => true;
        public bool PuedeF5 => true;
        public string HoraInicio { get { return _horaInicio; } }
        public string HoraFinal { get { return _horaFinal; } }
        public string Duracion { get { return _duracion; } }

        public string NameEmployee { get; set; }
        public string HourlyWage { get; set; }
        
        public ICommand IniciarProcesoCommand { get; set; }
        public ICommand SaveEmployeeCommand { get; set; }
        public ICommand F1Command { get; set; }
        public ICommand F5Command { get; set; }
        public ICommand F4Command { get; set; }

        public ViewModel()
        {
            this.InicializarObjetos();
        }
        public void InicializarObjetos()
        {
            IniciarProcesoCommand = new IniciarProcesoCommand(this);
            SaveEmployeeCommand = new SaveEmployeeCommand(this);
            F1Command = new F1Command(this);
            F5Command = new F5Command(this); 
            F4Command = new F4Command(this);
            _service = new Services.Services();
        }

        //Eventos
        public async void IniciarProceso()
        {
            await EjecutarComando();
            
        }
        public async void SaveEmployee()
        {
            await SaveEmployeeEject();
        }


        public void F5()
        {
            IniciarProceso();
        }
        public void F4(Window win)
        {
            win.Close();
        }
        public void F1()
        {
            string resJSON = _service.Info();
            if (resJSON.Equals("ErrorHelp"))
            {
                MessageBox.Show("");
            }
            else if (resJSON.Equals("ErrorEnRuta"))
            {
                MessageBox.Show("");
            }
            else
            {
                var wh = new Help(resJSON);
                wh.ShowDialog();
            }
        }

        public async Task EjecutarComando()
        {
            _horaInicio = ""; 
            OnPropertyChanged(new PropertyChangedEventArgs("HoraInicio"));
            _horaFinal = ""; 
            OnPropertyChanged(new PropertyChangedEventArgs("HoraFinal"));
            _duracion = "_"; 
            OnPropertyChanged(new PropertyChangedEventArgs("Duracion"));
            _nameEmployee = NameEmployee; 
            OnPropertyChanged(new PropertyChangedEventArgs("NameEmployee"));
            _hourlyWage = HourlyWage;
            OnPropertyChanged(new PropertyChangedEventArgs("HourlyWage"));

            /*validar campo hourlyWage es requerido*/
            if (string.IsNullOrWhiteSpace(_hourlyWage))
            {
                MessageBox.Show("Campo HourlyWage requerido.", "Error de validación", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            /*validar formato del campo hourlyWage*/
            if (!decimal.TryParse(_hourlyWage, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal numericValue))
            {
                MessageBox.Show("Formato numérico inválido.", "Error de validación", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            /*validar hourlyWage debe de ser positivo*/
            if (numericValue < 0)
            {
                MessageBox.Show("El valor debe ser positivo.", "Error de validación", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                string resultado = await this._service.EjecutarProceso();
                switch (resultado)
                {
                    case "OK":
                        MessageBox.Show("", "Finalizado", MessageBoxButton.OK); break;
                    case "ErrorEnDB":
                        MessageBox.Show("");
                        break;
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (TaskCanceledException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public async Task SaveEmployeeEject()
        {
            _horaInicio = "";
            OnPropertyChanged(new PropertyChangedEventArgs("HoraInicio"));
            _horaFinal = "";
            OnPropertyChanged(new PropertyChangedEventArgs("HoraFinal"));
            _duracion = "_";
            OnPropertyChanged(new PropertyChangedEventArgs("Duracion"));
            _nameEmployee = NameEmployee;
            OnPropertyChanged(new PropertyChangedEventArgs("NameEmployee"));
            _hourlyWage = HourlyWage;
            OnPropertyChanged(new PropertyChangedEventArgs("HourlyWage"));

            /*validar campo hourlyWage es requerido*/
            if (string.IsNullOrWhiteSpace(_hourlyWage))
            {
                MessageBox.Show("Campo HourlyWage requerido.", "Error de validación", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            /*validar formato del campo hourlyWage*/
            if (!decimal.TryParse(_hourlyWage, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal numericValue))
            {
                MessageBox.Show("Formato numérico inválido.", "Error de validación", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            /*validar hourlyWage debe de ser positivo*/
            if (numericValue < 0)
            {
                MessageBox.Show("El valor debe ser positivo.", "Error de validación", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                string resultado = await this._service.EjecutarProceso();
                switch (resultado)
                {
                    case "OK":
                        MessageBox.Show("", "Finalizado", MessageBoxButton.OK); break;
                    case "ErrorEnDB":
                        MessageBox.Show("");
                        break;
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (TaskCanceledException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null) PropertyChanged(this, e);
        }
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)] static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)] static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
    }
}
