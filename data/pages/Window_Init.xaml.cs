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
using System.Threading;
using Spectral_Launcher.Localization;
using Spectral_Launcher.Themes;
using Spectral_Launcher.properties;
using System.Drawing;
namespace Spectral_Launcher.data.pages
{
    public partial class Window_Init : Window
    {
        Language desiredLanguage;
        UITheme desiredTheme;
        public Window_Init()
        {
            Settings.Default.defaultUITheme = new Default_Light();
            Settings.Default.defaultLanguage = new DefaultLanguage();

            if (desiredTheme == null)
            {
                if (Settings.Default.desiredUITheme == null)
                {
                    if (Settings.Default.defaultUITheme == null)
                    {
                        throw new MissingFieldException("Error: defaultUITheme must not be Null or otherwise Missing!");
                    }
                    else
                    {
                        desiredTheme = Settings.Default.defaultUITheme;
                    }
                }
                else
                {
                    desiredTheme = Settings.Default.desiredUITheme;
                }
            }
            else
            {
                desiredTheme = Settings.Default.desiredUITheme;
            }

            if (Settings.Default.desiredLanguage == null)
            {
                if (Settings.Default.desiredLanguage == null)
                {
                    desiredLanguage = new DefaultLanguage();
                }
                else
                {
                    desiredLanguage = Settings.Default.defaultLanguage;
                }
            }
            else
            {
                desiredLanguage = Settings.Default.desiredLanguage;
            }

            InitializeComponent();
            UpdateLocalization(); // Update is called after InitComp because otherwise 'StudioName_Text' and other values do not exist...
            UpdateGraphics();
        }
        private void UpdateLocalization()
        {
            if (desiredLanguage != null)
            {
                Close_Launcher_Button.Content = $"{desiredLanguage.CloseLauncher_Local()}";
                PlayLauncher_Button.Content = $"{desiredLanguage.OpenLauncher_Local()}";
                StudioName_Text.Text = $"{desiredLanguage.StudioName_Local()}";
                LauncherName_Text.Text = $"{desiredLanguage.LauncherName_Local()}";
            }
            else
            {
                Language _Lanugage = new DefaultLanguage();
                Close_Launcher_Button.Content = $"{_Lanugage.CloseLauncher_Local()}";
                PlayLauncher_Button.Content = $"{_Lanugage.OpenLauncher_Local()}";
                StudioName_Text.Text = $"{_Lanugage.StudioName_Local()}";
                LauncherName_Text.Text = $"{_Lanugage.LauncherName_Local()}";
            }
        }
        private void UpdateGraphics()
        {
            if (desiredTheme != null)
            {
                if (desiredTheme.IsInternal() == true)
                {
                    BitmapImage _primaryImage = new BitmapImage();
                    _primaryImage.BeginInit();
                    _primaryImage.UriSource = new Uri($"{desiredTheme.Primary_BackgroundPicture()}", UriKind.Relative);
                    _primaryImage.EndInit();
                    BackgroundImage.Source = _primaryImage;
                }
                else
                {
                    BitmapImage _primaryImage = new BitmapImage();
                    _primaryImage.BeginInit();
                    _primaryImage.UriSource = new Uri($"{System.IO.Directory.GetCurrentDirectory()}/{desiredTheme.RootFolder()}/{desiredTheme.ThemeFolder()}/{desiredTheme.Primary_BackgroundPicture()}");
                    _primaryImage.EndInit();
                    BackgroundImage.Source = _primaryImage;
                }
            }
            else
            {
                throw new NotImplementedException($"Error, Desired UI theme must exist! got {desiredTheme}!");
            }
        }
        private void StartLauncher(object sender, RoutedEventArgs e)
        {
            Window_Main mainWindow = new Window_Main();
            mainWindow.desiredLanguage = this.desiredLanguage;
            mainWindow.desiredTheme = this.desiredTheme;
            this.Close();
            mainWindow.Show();
        }

        private void CloseLauncher(object sender, RoutedEventArgs e)
        {

        }
    }
}

