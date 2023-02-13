using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Windows;

namespace Challenger_Launcher
{
    /// <summary>
    /// Interaction logic for LauncherUpdater.xaml
    /// </summary>
    public partial class LauncherUpdater : Window
    {
        private MainWindow mainMenuWindow;
        public LauncherUpdater()
        {
            InitializeComponent();
            LauncherVersion.Text = AppSettings.Default.LauncherVersion;
        }
        private void CheckLauncherForUpdates(object sender, EventArgs e)
        {
            UpdateLauncher updateLauncher= new UpdateLauncher();
            updateLauncher.CheckLauncherForUpdates();
            LauncherVersion.Text = AppSettings.Default.LauncherVersion;
        }
        public void UpdateCaller(MainWindow _mainWindow)
        {
            mainMenuWindow = _mainWindow;
        }

        private void CancelUpdates(object sender, RoutedEventArgs e)
        {
            mainMenuWindow.Show();
            this.Close();
        }
    }
}
