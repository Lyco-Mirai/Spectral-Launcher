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

namespace Challenger_Launcher
{
    /// <summary>
    /// Interaction logic for InitLoadScreen.xaml
    /// </summary>
    public partial class InitLoadScreen : Window
    {
        MainWindow mainMenuWindow = new MainWindow();
        public InitLoadScreen()
        {
            InitializeComponent();
        }

        private void WindowActivated(object sender, EventArgs e)
        {
            PreLoadData();
        }
        private void PreLoadData()
        {
            FinishLoading();
        }
        private void FinishLoading()
        {
            mainMenuWindow.InitializeComponent();
            this.Hide();
            mainMenuWindow.Show();
        }
    }
}
