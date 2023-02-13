using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Challenger_Launcher
{
    namespace Common
    {
        struct Version
        {
            internal static Version zero = new Version("NA", 0, 00, 0);
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
            public override string ToString()
            {
                return $"{build}V{major}.{minor}.{subminor}";
            }
        }
        
        struct CommonData
        {
            internal static string versionFileName = new string("Version.ver");
            internal static string rootFilePath = Directory.GetCurrentDirectory();
        }
    }
}
