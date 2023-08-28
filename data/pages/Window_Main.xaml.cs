using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
using Spectral_Launcher.data.Users;
using Spectral_Launcher.Localization;
using Spectral_Launcher.Themes;

namespace Spectral_Launcher.data.pages
{

    public partial class Window_Main : Window
    {
        internal Page CurrentPage;
        //internal string CurrentPagePath;
        internal Page _HomePage;
        internal Page _UserInfoPage;
        internal Page _UserAccountPage;
        internal Page _UserSettingsPage;
        internal Page _LibaryPage;
        internal Page _StorePage;

        internal Language desiredLanguage;
        internal UITheme desiredTheme;

        public Window_Main()
        {
            if (desiredLanguage == null)
            {
                desiredLanguage = new DefaultLanguage();
            }
            if (desiredTheme == null)
            {
                desiredTheme = new Default_Dark();
            }
            _HomePage = new Page_Home();
            _UserInfoPage = new Page_UserSettings();
            _UserAccountPage = new Page_UserSettings();
            _UserSettingsPage = new Page_UserSettings();
            _LibaryPage = new Page_UserSettings();
            _StorePage = new Page_UserSettings();
            InitializeComponent();
            CurrentPage = _HomePage;
            UpdateTextToLocalization();
        }
        private void UpdateTextToLocalization()
        {
            if (desiredLanguage != null)
            {
                CloseButton.Content = $"{desiredLanguage.CloseLauncherShort_Local()}";
            }
            else
            {
                Language _Lanugage = new DefaultLanguage();
                CloseButton.Content = $"{_Lanugage.CloseLauncherShort_Local()}";
            }
        }
        private void GotoHome(object sender, RoutedEventArgs e)
        {
            CurrentPage = _StorePage;
            UpdatePage("Page_Home");
        }
        private void GotoStore(object sender, RoutedEventArgs e)
        {
            CurrentPage = _HomePage;
            UpdatePage("Page_Home");
        }
        private void GotoLibary(object sender, RoutedEventArgs e)
        {
            CurrentPage = _LibaryPage;
            UpdatePage("Page_Home");
        }
        private void GotoUserSettings(object sender, RoutedEventArgs e)
        {
            CurrentPage = _UserSettingsPage;
            UpdatePage("Page_UserSettings");
        }
        private void GotoUserAccountSettings(object sender, RoutedEventArgs e)
        {
            CurrentPage = _UserSettingsPage;
            UpdatePage("Page_UserAccountSettings");
        }
        private void UpdatePage(string _page)
        {
            PageSource.Source = new Uri($"{_page}.xaml", UriKind.Relative);
            UpdateTextToLocalization();
        }
        private void SaveSettings(object sender, RoutedEventArgs e)
        {
            
        }
        private void CloseLauncher(object sender, RoutedEventArgs e)
        {
            this.Close();
            System.Windows.Application.Current.Shutdown();
        }
    }
}
