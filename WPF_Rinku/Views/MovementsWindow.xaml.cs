using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Drawing;

namespace WPF_Rinku.Views
{
    /// <summary>
    /// Lógica de interacción para MovementsWindow.xaml
    /// </summary>
    public partial class MovementsWindow : Window
    {
        public List<MonthInfo> month { get; set; }
        private List<MovementInfo> movemenths = new List<MovementInfo>();

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
        public MovementsWindow()
        {
            InitializeComponent();
            _vm = new ViewModel.ViewModel();
            DataContext = _vm;
            CargarComboMonth();

        }

        private async void CargarComboMonth()
        {
            try
            {
                string url = ConfigurationManager.AppSettings["UrlService"].ToString();
                string controller = "Catalogue/";
                string parametros = 1.ToString();
                //string method = "GetEmployeeById/" + parametros;
                string method = "GetAll_Months";

                using (HttpClient client = new HttpClient())
                {
                    string apiUrl = url + controller + method;
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonContent = await response.Content.ReadAsStringAsync();
                        var data = JObject.Parse(jsonContent).SelectToken("Data");
                        month = data.ToObject<List<MonthInfo>>();
                        cmbMonth.ItemsSource = month;
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
            string idText = txtMovementId.Text;
            Int64 employeeId = 0;
            string idEmployeeText = txtEmployeeId.Text;
            string nombre = txtName.Text;
            string deliveryQuantityText = txtDeliveryQuantity.Text;
            //string hourlyWageText = txtHourlyWage.Text;
            //string rolId = (cmbRol.SelectedItem as ComboBoxItem)?.Content?.ToString();
            // string rolIdText = (cmbRol.SelectedValue != null) ? cmbRol.SelectedValue.ToString() : "";
            string monthIdText = (cmbMonth.SelectedValue != null) ? cmbMonth.SelectedValue.ToString() : "";

            if (!string.IsNullOrWhiteSpace(idEmployeeText))
            {
                if (!Int64.TryParse(idEmployeeText, out Int64 EmployeeIdReturn))
                {
                    MessageBox.Show("The employeeId is not valid.");
                    return;
                }
                employeeId = EmployeeIdReturn;

            }

            if (!string.IsNullOrWhiteSpace(idText))
            {
                if (!Int64.TryParse(idText, out Int64 IdReturn))
                {
                    MessageBox.Show("The Movement is not valid.");
                    return;
                }
               id = IdReturn;

            }


            if (!string.IsNullOrWhiteSpace(nombre) && !string.IsNullOrWhiteSpace(deliveryQuantityText) && !string.IsNullOrWhiteSpace(monthIdText))
            {
                if (int.TryParse(deliveryQuantityText, out int deliveryQuantity))
                {
                    if (int.TryParse(monthIdText, out int monthId))
                    {

                        MovementInfo newMovement = new MovementInfo(id, employeeId, deliveryQuantity, monthId);
                        movemenths.Add(newMovement);

                        MovementInfo.ItemsSource = null;
                        MovementInfo.ItemsSource = movemenths;

                        //txtName.Clear();

                        await EnviarDatosAPI(newMovement);
                    }
                    else
                    {
                        MessageBox.Show("Month wage must be a valid number.");
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
            string idText = txtMovementId.Text;

            try
            {

                if (!Int64.TryParse(idText, out Int64 IdReturn))
                {
                    MessageBox.Show("Select a registered Movement.");
                    return;
                }
                id = IdReturn;



                string url = ConfigurationManager.AppSettings["UrlService"].ToString();
                string controller = "Process/";
                string method = $"InactiveMovementById/{id}";
                string apiUrl = url + controller + method;

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.PutAsync(apiUrl, null);

                    if (response.IsSuccessStatusCode)
                    {
                        LimpiarCampos();
                        MessageBox.Show("The movement was deleted successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Error deleting movement.");
                    }
                }
            }
            catch (Exception ex)
            {
                txtName.Text = $"Error: {ex.Message}";
            }
        }

        private async Task EnviarDatosAPI(MovementInfo movement)
        {
            try
            {
                string url = ConfigurationManager.AppSettings["UrlService"].ToString();
                string controller = "Process/";
                //string parametros = 1.ToString();
                //string method = "GetEmployeeById/" + parametros;
                string method = "";
                if (movement.Id > 0)
                {
                    method = "UpdateMovement";
                }
                else
                {
                    method = "SaveMovements";
                }


                using (HttpClient client = new HttpClient())
                {
                    //string apiUrl = "https://reqres.in/api/users";
                    string apiUrl = url + controller + method;
                    var data = new
                    {
                        Id = movement.Id,
                        EmployeeId = movement.EmployeeId,
                        DeliveryQuantity = movement.DeliveryQuantity,
                        MonthId = movement.MonthId
                    };
                    string jsonData = JsonConvert.SerializeObject(data);
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = new HttpResponseMessage();
                    if (movement.Id > 0)
                    {
                        /*Si trae Id es Un Put*/
                        response = await client.PutAsync(apiUrl, content);
                    }
                    else
                    {
                        /*Si no trae Id es Un Post*/
                        response = await client.PostAsync(apiUrl, content);
                    }


                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                       

                        if (movement.Id > 0)
                        {
                            /*Si trae Id es Un Update*/
                            //LimpiarCampos();
                            MessageBox.Show("Movement Update to API successfully.");
                        }
                        else
                        {
                            var dataReturn = JObject.Parse(responseContent).SelectToken("Data");
                            if (dataReturn.Count() > 0)
                            {
                                var movementRet = dataReturn.ToObject<MovementInfoRequest>();
                                //EmpleadoInfo employee = JsonConvert.DeserializeObject<EmpleadoInfo>(data);
                                //txtName.Text = $"Nombre: {employee.Name}\nEmail: {employee.Email}\nTeléfono: {employee.Phone}";
                                txtMovementId.Text = movementRet.Id.ToString();
                                //txtEmployeeId.Text = movementRet.EmployeeId.ToString();
                                //txtName.Text = movementRet.EmployeeName;
                                //txtRol.Text = movementRet.Rol;
                                //cmbMonth.SelectedValue = movementRet.MonthId;
                                //txtDeliveryQuantity.Text = movementRet.DeliveryQuantity.ToString();
                            }
                            else
                            {
                                LimpiarCampos();
                                MessageBox.Show("Movement not found.");
                            }
                            /*Si no trae Id es Un Save*/
                            //LimpiarCampos();
                            MessageBox.Show("Movement saved to API successfully.");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Failed to save the movement in the API.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sending data to API: " + ex.Message);
            }
        }

        private async void SearchEmployee_Click(object sender, RoutedEventArgs e)
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
                    string apiUrl = url + controller + method;
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
                            txtRol.Text = employee.Rol;
                        }
                        else
                        {
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

        private async void SearchMovement_Click(object sender, RoutedEventArgs e)
        {
            Int64 Id;

            if (Int64.TryParse(txtMovementId.Text, out Id))
            {
                try
                {
                    string url = ConfigurationManager.AppSettings["UrlService"].ToString();
                    string controller = "Process/";
                    //string parametros = 1.ToString();
                    //string method = "GetEmployeeById/" + parametros;
                    string method = $"GetMovementById/{Id}";

                    //string apiUrl = $"https://jsonplaceholder.typicode.com/users/{EmployeeId}";
                    string apiUrl = url + controller + method;
                    string movementJson = await ObtenerUsuario(apiUrl);

                    if (!string.IsNullOrWhiteSpace(movementJson))
                    {
                        var data = JObject.Parse(movementJson).SelectToken("Data");
                        if (data.Count() > 0)
                        {
                            var movement = data.ToObject<MovementInfoRequest>();
                            //EmpleadoInfo employee = JsonConvert.DeserializeObject<EmpleadoInfo>(data);
                            //txtName.Text = $"Nombre: {employee.Name}\nEmail: {employee.Email}\nTeléfono: {employee.Phone}";
                            txtEmployeeId.Text = movement.EmployeeId.ToString();
                            txtName.Text = movement.EmployeeName;
                            txtRol.Text = movement.Rol;
                            cmbMonth.SelectedValue = movement.MonthId;
                            txtDeliveryQuantity.Text = movement.DeliveryQuantity.ToString();
                        }
                        else
                        {
                            LimpiarCampos();
                            MessageBox.Show("Movement not found.");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Movement not found.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid Movement ID.");
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
            txtMovementId.Text = string.Empty;
            txtEmployeeId.Text = string.Empty;
            txtName.Text = string.Empty;
            txtRol.Text = string.Empty;
            cmbMonth.SelectedItem = null;
            txtDeliveryQuantity.Text = string.Empty;
        }
    }
}
