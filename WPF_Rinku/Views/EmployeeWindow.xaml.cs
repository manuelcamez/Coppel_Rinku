using System;
using System.Collections.Generic;
using System.Linq;
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

namespace WPF_Rinku.Views
{
    /// <summary>
    /// Lógica de interacción para EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : Window
    {
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
        }
    }
}
