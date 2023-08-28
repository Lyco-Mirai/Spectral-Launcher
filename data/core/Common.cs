using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Spectral_Launcher.data.Users;
using Spectral_Launcher.Localization;
using Spectral_Launcher.properties;
using Spectral_Launcher.Themes;

namespace Spectral_Launcher.Core.Common
{
    public struct Version
    {
        internal static Version zero = new Version("NA", 0, 00, 0);
        internal static Version none = new Version("Null");
        // Main V0.00.0
        private string build; // N X.XX.X
        private short major; // X N.XX.X
        private short minor; // X X.NN.X
        private short subminor; // X X.XX.N
        internal Version(string _build, short _major, short _minor, short _subminor)
        {
            build = _build;
            major = _major;
            minor = _minor;
            subminor = _subminor;
        }

        internal Version(string _version)
        {
            string[] _versionStrings = _version.Split('.');
            if (_versionStrings.Length != 3)
            {
                build = "NA";
                major = 0;
                minor = 0;
                subminor = 0;
                MessageBox.Show($"Version in wrong format! Got: {_version} Expected: Build VX.XX.X");
            }
            else
            {
                string[] _versionBuildStrings = _versionStrings[0].Split('V');
                build = _versionBuildStrings[0];
                major = short.Parse(_versionBuildStrings[1]);
                minor = short.Parse(_versionStrings[1]);
                subminor = short.Parse(_versionStrings[2]);
            }
        }

        internal bool IsDifferentThan(Version _otherVersion)
        {
            if (build != _otherVersion.build)
            {
                return true;
            }
            else
            {
                if (major != _otherVersion.major)
                {
                    return true;
                }
                else
                {
                    if (minor != _otherVersion.minor)
                    {
                        return true;
                    }
                    else
                    {
                        if (subminor != _otherVersion.subminor)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
        }
        /*public override string ToString
        {
            return $"{build}V{major}.{minor}.{subminor}";
        }*/
    }
    public class App_EventHandler // Seperate Handler Class to de-cluter other files
    {
        // Define what is where and is called what...
        static string UserListFileName = $"users.userList";
        static string UserListFileDir = $"{Directory.GetCurrentDirectory()}/Users/";
        static string UserListFile = $"{UserListFileDir}{UserListFileName}";
        static string ProgramDataFileName = $"appConfig.bin";
        static string ProgramDataFileDir = $"{Directory.GetCurrentDirectory()}/data/Config/";
        static string ProgramDataFile = $"{ProgramDataFileDir}{ProgramDataFileName}";
        // these are all static as they shouldn't be changed, and should be kept read-only

        public void SaveProgramData(User[]? userList)
        {
            // Create and/or read to Userlist file
            FileStream _UserListFile = File.Create($"{UserListFile}");
            // Write to file
            string[] usersNamesInUserList = { };
            if (userList != null) // Removes 'Could be null here' whinge notification from the code editor, Probably a good thing...
            {
                for (int i = 0; i < userList.Length; i++)
                {
                    string[] _tempList = new string[userList.Length + 1]; // create a new temp string for data to be temporarily stored in before being discarded
                    usersNamesInUserList.CopyTo(_tempList, 0); // copys the data from Perm to temp, otherwise prevents a data loss
                    _tempList[i] = userList[i].UserName; // copys the Usernames from the User[] list to the string[] list  
                    Array.Resize(ref usersNamesInUserList, _tempList.Length); // resize perm stream to be able to accomidate temp stream
                    _tempList.CopyTo(usersNamesInUserList, 0); // Finaly copy temp data to perm data
                }

                string users; // Empty string
                if (userList.Length > 1) {users = $"{string.Join("','", usersNamesInUserList)}"; }
                else {users = $"{usersNamesInUserList[0]}"; }
                if (users.EndsWith("','")) {users = users.Remove(users.Length-3, 3);} //Dumb code that removes otherwise extra (',') in the file that could fuck up the Read process
                
                string[] _UserListFileLines = // Defines what should be writen to the "UserListFile.userList" doccument...
                {
                    $"#UserCount={userList.Length};",
                    $"\r#Users='{users}';"
                };

                int _fileLine = 0;
                foreach (string line in _UserListFileLines) // Actualy writes to the "UserListFile.userList" doccument...
                {
                    Byte[] lineByte = new UTF8Encoding(true).GetBytes($"{line}");
                    _UserListFile.WriteAsync(lineByte, 0, lineByte.Length);
                    _fileLine += 1;
                }
                _UserListFile.Close(); // Closes the virtualised file to save both resources and the pain of figuring out why a file can't be opened. "wdym you cant write to this file!? oh shit, its because another thread is (still) writing to it..." 
            }
            else
            {
                string[] _UserListFileLines =
                {
                    $"#UserCount=0;",
                    $"\r#Users=null;"
                };
                int _fileLine = 0;
                foreach (string line in _UserListFileLines)
                {
                    Byte[] lineByte = new UTF8Encoding(true).GetBytes($"{line}");
                    _UserListFile.WriteAsync(lineByte, 0, lineByte.Length);
                    _fileLine += 1;
                }
                _UserListFile.Close(); // Closes the virtualised file to save resources like before...
            }
            
            /* No need for app data storage-- yet...
            string[] _ProgramDataFileLines =
            {

            };
            // Create and/or read to ProgramData file
            if (!File.Exists($"{ProgramDataFile}"))
            {
                //Create File
                FileStream _ProgramDataFile = File.Create($"{ProgramDataFile}");
                //Write to file
            }
            else
            {
                //Write to file
                FileStream _ProgramDataFile = File.Open($"{ProgramDataFile}", FileMode.Open);
            }
            */
        }
        public void LoadProgramData(User[] userList) // Just started work on the load script, hahaha this is gonna be fun (kill me, i know whats coming)
        {

        }
    }

    public static class App_StaticEventHandler // Seperate Handler Class to de-cluter other files
    {
        public static bool hasSpecialChar(string input)
        {
            string specialChar = @"\|!#$%&/()=?»«@£§€{}.-;'<>_,";
            foreach (var item in specialChar)
            {
                if (input.Contains(item)) return true;
            }
            return false;
        }
        public static bool hasChar(string[] key, string input)
        {
            foreach (var item in key)
            {
                if (input.Contains(item)) return true;
            }
            return false;
        }
    }
}
