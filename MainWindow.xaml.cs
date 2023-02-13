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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Challenger_Launcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        enum LauncherStatus
        {
            readyToPlay
        }
        public MainWindow()
        {
            InitializeComponent();
            LauncherVersion.Text = AppSettings.Default.LauncherVersion;
        }

        private void CloseLauncher(object sender, RoutedEventArgs e)
        {
            this.Close();
            Application.Current.Shutdown();
        }

        private void CheckLauncherUpdates(object sender, RoutedEventArgs e)
        {
            LauncherUpdater launcherUpdater = new LauncherUpdater();
            launcherUpdater.InitializeComponent();
            launcherUpdater.Show();
            launcherUpdater.UpdateCaller(this);
            LauncherVersion.Text = AppSettings.Default.LauncherVersion;
        }
    }
}
