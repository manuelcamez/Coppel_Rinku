using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPF_Rinku.Models;

namespace WPF_Rinku.Views
{
    /// <summary>
    /// Lógica de interacción para PaymentsWindow.xaml
    /// </summary>
    public partial class PaymentsWindow : Window
    {
        public List<MonthInfo> month { get; set; }
        public PaymentsWindow()
        {
            InitializeComponent();
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

        private async void CalculateSalaryAndCompensation_Click(object sender, RoutedEventArgs e)
        {
            string monthIdText = (cmbMonth.SelectedValue != null) ? cmbMonth.SelectedValue.ToString() : "";

            Int64 EmployeeId;

            if (Int64.TryParse(txtEmployeeId.Text, out EmployeeId))
            {
                if(!string.IsNullOrWhiteSpace(monthIdText))
                {
                    if (int.TryParse(monthIdText, out int monthId))
                    {
                        try
                        {
                            string url = ConfigurationManager.AppSettings["UrlService"].ToString();
                            string controller = "Process/";
                            //string parametros = 1.ToString();
                            //string method = "GetEmployeeById/" + parametros;
                            //string method = "CalculateSalaryAndCompensation/" + EmployeeId + "/" + monthId;
                            string method = "CalculateSalaryAndCompensation";

                            //string apiUrl = $"https://jsonplaceholder.typicode.com/users/{EmployeeId}";
                            //string apiUrl = url + controller + method;


                            string apiUrl = $"{url}/{controller}/{method}?EmployeeId={EmployeeId}&MonthId={monthId}";




                            string paymentJson = await GetPayment(apiUrl);

                            if (!string.IsNullOrWhiteSpace(paymentJson))
                            {
                                var data = JObject.Parse(paymentJson).SelectToken("Data");
                                if (data.Count() > 0)
                                {
                                    var paymendata = data.ToObject<CalculationsInfoRequest>();
                                    //EmpleadoInfo employee = JsonConvert.DeserializeObject<EmpleadoInfo>(data);
                                    //txtName.Text = $"Nombre: {employee.Name}\nEmail: {employee.Email}\nTeléfono: {employee.Phone}";
                                    txtName.Text = paymendata.EmployeeName;
                                    txtRol.Text = paymendata.RolName;
                                    txtDeliveryQuantity.Text = paymendata.DeliveryQuantity.ToString();
                                    txtMonthlySalary.Text = paymendata.MonthlySalary.ToString();
                                    txtAditionalBonus.Text = paymendata.AditionalBonus.ToString();
                                    txtBonusByHoursByMonth.Text = paymendata.BonusByHoursByMonth.ToString();
                                    txtGroceryVouchers.Text = paymendata.GroceryVouchers.ToString();
                                    txtMonthlyNetSalary.Text = paymendata?.MonthlyNetSalary.ToString();
                                    txtIsr.Text = paymendata?.Isr.ToString();
                                }
                                else
                                {
                                    LimpiarCampos();
                                    MessageBox.Show("Payment not found.");
                                }

                            }
                            else
                            {
                                MessageBox.Show("Payment not found.");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error: {ex.Message}");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Month wage must be a valid number.");
                    }
                }
                else
                {
                    MessageBox.Show("Complete all the fields.");
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid User ID.");
            }
        }
        private async Task<string> GetPayment(string url)
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
            txtRol.Text = string.Empty;
            txtEmployeeId.Text = string.Empty;
            txtName.Text = string.Empty;
            cmbMonth.SelectedItem = null;
            txtDeliveryQuantity.Text = string.Empty;

            
            
            txtMonthlySalary.Text = string.Empty;
            txtAditionalBonus.Text = string.Empty;
            txtBonusByHoursByMonth.Text = string.Empty;
            txtGroceryVouchers.Text = string.Empty;
            txtMonthlyNetSalary.Text = string.Empty;
            txtIsr.Text = string.Empty;
        }
    }

   
}
