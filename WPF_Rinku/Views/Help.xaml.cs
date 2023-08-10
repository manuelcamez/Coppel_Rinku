using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Lógica de interacción para Help.xaml
    /// </summary>
    public partial class Help : Window
    {
        public Help(string resJSON)
        {
            InicializarComponentes(resJSON);
        }
        private void HandleF3(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F3)
            {
                Close();
            }
        }

        private void InicializarComponentes(string resJSON)
        {
            InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(HandleF3);

            if (resJSON.Length > 0)
            {
                var info = JsonConvert.DeserializeObject<Info>(resJSON);
                this.txPathFile.Text = info.PathFiles;
                this.txArchivos.Text = string.Join("\n", info.Archivos);

            }
        }
    }
}
