using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPF_Rinku.Models;

namespace WPF_Rinku.Views
{
    /// <summary>
    /// Lógica de interacción para EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : Window
    {
        public List<RolInfo> Rol { get; set; }
        private List<EmpleadoInfo> empleados = new List<EmpleadoInfo>();

        private ViewModel.ViewModel _vm; private const uint WS_EX_CONTEXTHELP = 0x00000400;
        private const uint WS_MINIMIZEBOX = 0x00020000; private const uint WS_MAXIMIZEBOX = 0x00010000;
        private const int GWL_STYLE = -16; private const int GWL_EXSTYLE = -20;
        private const int SWP_NOSIZE = 0x0001; private const int SWP_NOMOVE = 0x0002;
        private const int SWP_NOZORDER = 0x0004; private const int SWP_FRAMECHANGED = 0x0020;
        private const int WM_SYSCOMMAND = 0x0112; private const int SC_CONTEXTHELP = 0xF180;
        [DllImport("user32.dll")]
        private static extern uint GetWindowLong(IntPtr hwnd, int index);
        [DllImport("user32.dll")] private static extern int SetWindowLong(IntPtr hwnd, int index, uint newStyle);
        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hwnd, IntPtr hwndInsertAfter, int x, int y, int width, int height, uint flags);
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e); IntPtr hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
            uint styles = GetWindowLong(hwnd, GWL_STYLE); styles &= 0xFFFFFFFF ^ (WS_MINIMIZEBOX | WS_MAXIMIZEBOX);
            SetWindowLong(hwnd, GWL_STYLE, styles); styles = GetWindowLong(hwnd, GWL_EXSTYLE);
            styles |= WS_EX_CONTEXTHELP; SetWindowLong(hwnd, GWL_EXSTYLE, styles);
            SetWindowPos(hwnd, IntPtr.Zero, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER | SWP_FRAMECHANGED); ((HwndSource)PresentationSource.FromVisual(this)).AddHook(HelpHook);
        }
        private IntPtr HelpHook(IntPtr hwnd, int msg,
               IntPtr wParam,
               IntPtr lParam, ref bool handled)
        {
            if (msg == WM_SYSCOMMAND &&
                    ((int)wParam & 0xFFF0) == SC_CONTEXTHELP)
            {
                this.WindowHelp();
                handled = true;
            }
            return IntPtr.Zero;
        }
        public void WindowHelp()
        {
            Services.Services _service = new Services.Services(); string resJSON = _service.Info();
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
                var wh = new Help(resJSON); wh.ShowDialog();
            }
        }
        public EmployeeWindow()
        {
            InitializeComponent();
            _vm = new ViewModel.ViewModel();
            DataContext = _vm;
            CargarComboRol();

        }

        private async void CargarComboRol()
        {
            try
            {
                string url = ConfigurationManager.AppSettings["UrlService"].ToString();
                string controller = "Catalogue/";
                string parametros = 1.ToString();
                //string method = "GetEmployeeById/" + parametros;
                string method = "GetAll_Rol";

                using (HttpClient client = new HttpClient())
                {
                    string apiUrl = url + controller + method;
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonContent = await response.Content.ReadAsStringAsync();
                        var data = JObject.Parse(jsonContent).SelectToken("Data");
                        Rol = data.ToObject<List<RolInfo>>();
                        cmbRol.ItemsSource = Rol;
                    }
                    else
                    {
                        MessageBox.Show("Error loading API data.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private async void Guardar_Click(object sender, RoutedEventArgs e)
        {
            Int64 id = 0;
            string idText = txtEmployeeId.Text;
            string nombre = txtName.Text;
            string hourlyWageText = txtHourlyWage.Text;
            //string rolId = (cmbRol.SelectedItem as ComboBoxItem)?.Content?.ToString();
            string rolIdText = (cmbRol.SelectedValue != null)? cmbRol.SelectedValue.ToString() : "";

            if (!string.IsNullOrWhiteSpace(idText))
            {
                if (!Int64.TryParse(idText, out Int64 IdReturn))
                {
                    MessageBox.Show("The employeeId is not valid.");
                    return;
                }
                id = IdReturn;
             
            }
            

            if (!string.IsNullOrWhiteSpace(nombre) && !string.IsNullOrWhiteSpace(hourlyWageText) && !string.IsNullOrWhiteSpace(rolIdText))
            {
                if (decimal.TryParse(hourlyWageText, out decimal hourlyWage))
                {
                    if (Int64.TryParse(rolIdText, out Int64 rolId))
                    {
                        EmpleadoInfo nuevoEmpleado = new EmpleadoInfo(id, nombre, hourlyWage, rolId);
                        empleados.Add(nuevoEmpleado);

                        EmpleadoInfo.ItemsSource = null;
                        EmpleadoInfo.ItemsSource = empleados;

                        //txtName.Clear();
                        //txtHourlyWage.Clear();
                        //cmbRol.SelectedIndex = -1;

                        await EnviarDatosAPI(nuevoEmpleado);
                    }
                    else
                    {
                        MessageBox.Show("The RoleId is not valid.");
                    }
                }
                else
                {
                    MessageBox.Show("Hourly wage must be a valid number.");
                }
            }
            else
            {
                MessageBox.Show("Complete all the fields.");
            }
        }

        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            Int64 id = 0;
            string idText = txtEmployeeId.Text;

            try
            {
               
                if (!Int64.TryParse(idText, out Int64 IdReturn))
                {
                    MessageBox.Show("Select a registered employee.");
                    return;
                }
                id = IdReturn;
                

            
                string url = ConfigurationManager.AppSettings["UrlService"].ToString();
                string controller = "Employee/";
                //string parametros = 1.ToString();
                //string method = "GetEmployeeById/" + parametros;
                string method = $"InactiveEmployeeById/{id}";
                string apiUrl = url + controller + method;

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.PutAsync(apiUrl, null);

                    if (response.IsSuccessStatusCode)
                    {
                        LimpiarCampos();
                        MessageBox.Show("The employee was deleted successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Error deleting employee.");
                    }
                }
            }
            catch (Exception ex)
            {
                txtName.Text = $"Error: {ex.Message}";
            }
        }

        private async Task EnviarDatosAPI(EmpleadoInfo empleado)
        {
            try
            {
                string url = ConfigurationManager.AppSettings["UrlService"].ToString();
                string controller = "Employee/";
                //string parametros = 1.ToString();
                //string method = "GetEmployeeById/" + parametros;
                string method = "";
                if (empleado.Id > 0)
                {
                    method = "UpdateEmployeeById";
                }
                else
                {
                    method = "SaveEmployee";
                }
                

                using (HttpClient client = new HttpClient())
                {
                    //string apiUrl = "https://reqres.in/api/users";
                    string apiUrl = url + controller + method;
                    var data = new
                    {   Id = empleado.Id,
                        Name = empleado.Name,
                        HourlyWage = empleado.HourlyWage,
                        RolId = empleado.RolId
                    };
                    string jsonData = JsonConvert.SerializeObject(data);
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = new HttpResponseMessage();
                    if (empleado.Id > 0)
                    {
                        /*Si trae Id es Un Put*/
                        response = await client.PutAsync(apiUrl, content);
                    }
                    else {
                        /*Si no trae Id es Un Post*/
                        response = await client.PostAsync(apiUrl, content);
                    }
                        

                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                      
                        if (empleado.Id > 0)
                        {
                            /*Si trae Id es Un Update*/
                            MessageBox.Show("Employee Update to API successfully.");
                        }
                        else
                        {
                            var dataReturn = JObject.Parse(responseContent).SelectToken("Data");
                            if (dataReturn.Count() > 0)
                            {
                                var employee = dataReturn.ToObject<EmpleadoInfoRequest>();
                                //EmpleadoInfo employee = JsonConvert.DeserializeObject<EmpleadoInfo>(data);
                                //txtName.Text = $"Nombre: {employee.Name}\nEmail: {employee.Email}\nTeléfono: {employee.Phone}";
                                txtEmployeeId.Text = employee.Id.ToString();
                                txtName.Text = employee.Name;
                                txtHourlyWage.Text = employee.HourlyWage.ToString();
                                cmbRol.SelectedValue = employee.RolId;
                            }
                            else
                            {
                                LimpiarCampos();
                                MessageBox.Show("user not found.");
                            }
                            /*Si no trae Id es Un Save*/
                            MessageBox.Show("Employee saved to API successfully.");
                        }
                            
                    }
                    else
                    {
                        MessageBox.Show("Failed to save the employee in the API.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sending data to API: " + ex.Message);
            }
        }

        private async void Buscar_Click(object sender, RoutedEventArgs e)
        {
            Int64 EmployeeId;

            if (Int64.TryParse(txtEmployeeId.Text, out EmployeeId))
            {
                try
                {
                    string url = ConfigurationManager.AppSettings["UrlService"].ToString();
                    string controller = "Employee/";
                    //string parametros = 1.ToString();
                    //string method = "GetEmployeeById/" + parametros;
                    string method = $"GetEmployeeById/{EmployeeId}";

                    //string apiUrl = $"https://jsonplaceholder.typicode.com/users/{EmployeeId}";
                    string apiUrl = url + controller + method ;
                    string employeeJson = await ObtenerUsuario(apiUrl);

                    if (!string.IsNullOrWhiteSpace(employeeJson))
                    {
                        var data = JObject.Parse(employeeJson).SelectToken("Data");
                        if (data.Count() > 0)
                        {
                            var employee = data.ToObject<EmpleadoInfoRequest>();
                            //EmpleadoInfo employee = JsonConvert.DeserializeObject<EmpleadoInfo>(data);
                            //txtName.Text = $"Nombre: {employee.Name}\nEmail: {employee.Email}\nTeléfono: {employee.Phone}";
                            txtName.Text = employee.Name;
                            txtHourlyWage.Text = employee.HourlyWage.ToString();
                            cmbRol.SelectedValue = employee.RolId;
                        }
                        else {
                            LimpiarCampos();
                            MessageBox.Show("user not found.");
                        }
                      
                    }
                    else
                    {
                        MessageBox.Show("user not found.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid User ID.");
            }
        }
        private async Task<string> ObtenerUsuario(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                return await response.Content.ReadAsStringAsync();
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            txtName.Text = string.Empty;
            txtHourlyWage.Text = string.Empty;
            txtEmployeeId.Text = string.Empty;
            cmbRol.SelectedItem = null;
            cmbRol.Text = string.Empty;
        }
    }
}

