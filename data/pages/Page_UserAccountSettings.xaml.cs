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
using Spectral_Launcher.Core.Common;
using Spectral_Launcher.data.Users;
using Spectral_Launcher.Localization;
using Spectral_Launcher.Themes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Spectral_Launcher.data.Users
{
    public class User
    {
        public string UserName { get; set; }
        internal string UserID { get; set; }
        internal string UserCreation_Date { get; set; }
        internal string UserCreation_Time { get; set; }
        internal string UserLocalCulture { get; set; }
    }
}

namespace Spectral_Launcher.data.pages
{
    public partial class Page_UserAccountSettings : Page
    {
        public User? currentUser;
        public User[]? userList;
        public int UserCount = 0;
        internal Language desiredLanguage;
        internal UITheme desiredTheme;
        public Page_UserAccountSettings()
        {
            if (desiredLanguage == null)
            {
                desiredLanguage = new DefaultLanguage();
            }
            if (desiredTheme == null)
            {
                desiredTheme = new Default_Dark();
            }
            InitializeComponent();
            UpdateTextToLocalization();
        }
        private void UpdateTextToLocalization()
        {
            if (desiredLanguage != null)
            {
            }
            if (currentUser != null)
            {
                Username_Text.Text = currentUser.UserName;
                Username_Background.Visibility = Visibility.Visible;
            }
            else
            {
                Username_Text.Text = "";
                Username_Background.Visibility = Visibility.Hidden;
            }
        }
        private void LoginAsUser()
        {

        }
        private void UpdatePreferances()
        {
            App_EventHandler operations = new App_EventHandler();
            if (userList != null)
            {
                operations.SaveProgramData(this.userList);
                MessageBox.Show($"Saving Data with Users in UserList. ({userList.Length}.List and {UserCount}.Count Users)");
            }
            else
            {
                operations.SaveProgramData(this.userList);
                MessageBox.Show($"Saving Data with No Users in UserList. (No Users)");
            }
            UpdateTextToLocalization();
        }
        private void CreateNewUser(object sender, RoutedEventArgs e)
        {
            var confirmResult = MessageBox.Show($"Are you sure you want to create a new account called: '{NewAccount_TextBox.Text}'", $"Confirm Account Creation, '{NewAccount_TextBox.Text}'", MessageBoxButton.OKCancel);
            if (confirmResult == MessageBoxResult.OK)
            {
                string _userName = NewAccount_TextBox.Text;
                CreateUser(_userName, out currentUser);
            }
            UpdatePreferances();
        }
        void CreateUser(string userName, out User? _user)
        {
            int _minLength = 4;
            int _maxLength = 24;
            string[] _forbiddenChars = 
            {
                ".",
                "/",
                ",",
                "[",
                "]",
                "{",
                "}",
                "$",
                "£",
                "'",
                "%",
                "^" 
            };
            string userID = "";
            string userFileDirectory = "";
            string userIDFileDirectory = "";
            string userCreationDate = "";
            string userCreationTime = "";
            string userLocalCulture = "";
            _user = null;
            if (userName == null)
            {
                MessageBox.Show($"Please enter a Username!", $"Username Invalid!", MessageBoxButton.OK);
            }
            else
            {
                if (userName.Length < _minLength)
                {
                    MessageBox.Show($"Username must be longer than {_minLength} characters! (Had {userName.Length}).", $"Username Invalid! Must be longer than {_minLength} Characters", MessageBoxButton.OK);
                }
                else
                {
                    if (userName.Length > _maxLength)
                    {
                        MessageBox.Show($"Username must be shorter than {_maxLength} characters! (Had {userName.Length}).", $"Username Invalid! Must be shorter than {_maxLength} Characters", MessageBoxButton.OK);
                    }
                    else
                    {
                        if (App_StaticEventHandler.hasChar(_forbiddenChars, userName))
                        {
                            MessageBox.Show($"Username must not contain: {string.Join("  ,  ", _forbiddenChars)}.", $"Username Invalid! Must not contain detremental Characters", MessageBoxButton.OK);
                        }
                        else
                        {
                            Random _random = new Random();
                            string _characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                            string _userUID = new string(Enumerable.Repeat(_characters, 24).Select(s => s[_random.Next(s.Length)]).ToArray());
                            userID = _userUID;
                            string _userFileDirectory = $"{System.IO.Directory.GetCurrentDirectory()}/Users/{userName}";
                            userFileDirectory = _userFileDirectory;
                            // If directory does not exist, create it
                            if (!Directory.Exists(_userFileDirectory))
                            {
                                Directory.CreateDirectory(_userFileDirectory);
                            }
                            // If directory does exist, create a new one with a (#) index
                            else
                            {

                            }

                            // User ID File, userName.playerURI
                            string _UIDFileDirectory = $"{_userFileDirectory}/{userName}.playerURI";
                            userIDFileDirectory = _UIDFileDirectory;
                            if (!File.Exists($"{_UIDFileDirectory}"))
                            {
                                FileStream _UIDFile = File.Create($"{_UIDFileDirectory}");
                                userCreationDate = $"{DateTime.Now.ToString("dd/MM/yyyy")}";
                                userCreationTime = $"{DateTime.Now.ToString("HH:mm:ss")}";
                                userLocalCulture = $"{CultureInfo.CurrentCulture.Name}";
                                string[] _FileLines =
                                {
                                    $"#Name={userName};",
                                    $"\r#UID={_userUID};",
                                    $"\r#CreationDate={userCreationDate};",
                                    $"\r#CreationTime={userCreationTime};",
                                    $"\r#Nationality={userLocalCulture};"
                                };
                                int UIDFileLine = 0;
                                foreach (string line in _FileLines)
                                {
                                    Byte[] lineByte = new UTF8Encoding(true).GetBytes($"{line}");
                                    _UIDFile.WriteAsync(lineByte, 0, lineByte.Length);
                                    UIDFileLine += 1;
                                }
                                _UIDFile.Close();
                            }

                            // User Preferances File, userName.playerPrefs
                            string _PreferancesFileDirectory = $"{_userFileDirectory}/{userName}.playerPrefs";
                            if (!File.Exists($"{_PreferancesFileDirectory}"))
                            {
                                FileStream _PreferancesFile = File.Create($"{_PreferancesFileDirectory}");
                                string[] _FileLines =
                                {
                                    //Common
                                    $"//Preferances File",
                                    $"\r//Common Preferances",
                                    $"\r#AutoLogin=false;",
                                    $"\r#SavePassword=false;",
                                    $"\r#AutoStart=false;",
                                    $"\r#PreferedTheme='Default_Light';",
                                    $"\r#PreferedLanguage='{CultureInfo.CurrentCulture.Name}';",
                                    $"\r#ExperienceLevel='Green';",
                                    $"\r#PreferedExperience='Arcade'+'Multiplayer';",
                                    $"\r#Interact_Button='Fnc'||'LeftWin';",
                                    $"\r#SelfInteract_Button='Ctrl'+'Fnc'||'Ctrl'+'LeftWin';",
                                    $"\r#Inventory_Button='I';",
                                    $"\r#GlobalChat_Button='U';",
                                    $"\r#SideChat_Button='T';",
                                    $"\r#SquadChat_Button='K';",
                                    $"\r#CommandMenu_Button=0*'E';",
                                    //Infantry
                                    $"\r//Infantry Preferances",
                                    $"\r#Invert_Vertical_Look=false;",
                                    $"\r#Invert_Horizontal_Look=false;",
                                    $"\r#FieldOfView=100;",
                                    $"\r#Sensitivity_Vertical=100;",
                                    $"\r#Sensitivity_Horizontal=100;",
                                    $"\r#Forward_Button='W';",
                                    $"\r#Backward_Button='S';",
                                    $"\r#StrafeLeft_Button='A';",
                                    $"\r#StrafeRight_Button='D';",
                                    $"\r#LeanLeft_Button='Q';",
                                    $"\r#LeanRight_Button='E';",
                                    $"\r#Jump_Button='SPACE';",
                                    $"\r#Climb_Button='X';",
                                    $"\r#Aim_Button='Mouse2';",
                                    $"\r#Fire_Button='Mouse1';",
                                    $"\r#Reload_Button=;'R';",
                                    $"\r#ChangeMode_Button='F';",
                                    $"\r#LowerWeapon_Button=2*'LeftCtrl';",
                                    $"\r#Unjam_Button='Ctrl'+'R';",
                                    $"\r#CheckAmmo_Button='Ctrl'+'R';",
                                    $"\r#DropWeapon_Button='Ctrl'+'F';",
                                    $"\r#Grenade_Button=2*'G';",
                                    $"\r#Melee_Button='V'||'Mouse0';",
                                    $"\r#Ragdoll_Button='LeftShift'+'SPACE'||'LeftCtrl'+'I';",
                                    //Land Vehicles
                                    $"\r//Land Vehicle Preferances",
                                    $"\r#Land_EngineIgnition_Button='LeftCtrl'+'I';",
                                    $"\r#Land_ChangeWeapon_Button='F';",
                                    $"\r#Land_Reload_Button=;'R';",
                                    //Air Vehicles
                                    $"\r//Air Vehicle Preferances",
                                    $"\r#Air_EngineIgnition_Button='LeftCtrl'+'I';",
                                    $"\r#Air_ChangeWeapon_Button='F';",
                                    $"\r#Air_Reload_Button=;'R';",
                                    //Naval Vehicles
                                    $"\r//Naval Vehicle Preferances",
                                    $"\r#Naval_EngineIgnition_Button='LeftCtrl'+'I';",
                                    $"\r#Naval_ChangeWeapon_Button='F';",
                                    $"\r#Naval_Reload_Button=;'R';",
                                };
                                int PreferancesFileLine = 0;
                                foreach (string line in _FileLines)
                                {
                                    Byte[] lineByte = new UTF8Encoding(true).GetBytes($"{line}");
                                    _PreferancesFile.WriteAsync(lineByte, 0, lineByte.Length);
                                    PreferancesFileLine += 1;
                                }
                                _PreferancesFile.Close();
                            }
                            UserCount++;
                            _user = new User();
                            _user.UserName = userName;
                            _user.UserID = userID;
                            _user.UserCreation_Date = userCreationDate;
                            _user.UserCreation_Time = userCreationTime;
                            _user.UserLocalCulture = userLocalCulture;
                            Username_List.Items.Add($"{userName}");

                            if (userList != null)
                            {
                                if (UserCount > 0)
                                {
                                    Array.Resize(ref userList, UserCount);
                                    userList[UserCount-1] = _user;
                                }
                                foreach (User userInList in userList)
                                {
                                    if (!Username_List.Items.Contains(userInList.UserName))
                                    {
                                        Username_List.Items.Add($"{userInList.UserName}");
                                    }
                                }
                            }
                            else
                            {
                                userList = new User[1];
                                userList[0] = _user;
                            }

                            MessageBox.Show($"User Sucessfully Created!", $"User Created", MessageBoxButton.OK);
                        }
                    }
                }
            }
        }
        void DeleteUser(string _userName, string _userDirectory)
        {
            Directory.Delete(_userDirectory);
        }

        private void SwitchUser(object sender, SelectionChangedEventArgs e)
        {
            if (userList != null && userList[0] != null)
            {
                foreach (User userInList in userList)
                {
                    if (userInList.UserName == Username_List.Text)
                    {
                        currentUser = userInList;
                    }
                }
            }
        }
    }
}
