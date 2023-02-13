using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Windows;
using Challenger_Launcher.Common;

namespace Challenger_Launcher
{
    internal class UpdateLauncher
    {
        private string zippedFile;

        public enum LauncherUpdateStatus
        {
            ready,
            updatingFiles,
            failedToUpdateFiles
        }

        public LauncherUpdateStatus _updateStatus;
        internal LauncherUpdateStatus UpdateStatus
        {
            get => _updateStatus;
            set
            {
                _updateStatus = value;
                switch (_updateStatus)
                {
                    case LauncherUpdateStatus.ready:
                        break;
                    case LauncherUpdateStatus.updatingFiles:
                        break;
                    case LauncherUpdateStatus.failedToUpdateFiles:
                        break;
                    default:
                        break;
                }
            }
        }

        public void CheckLauncherForUpdates()
        {
            if (File.Exists(CommonData.rootFilePath + "/" + CommonData.versionFileName))
            {
                Common.Version localLauncherVersion = new Common.Version(File.ReadAllText(CommonData.versionFileName));
                AppSettings.Default.LauncherVersion.Replace(AppSettings.Default.LauncherVersion, localLauncherVersion.ToString());

                try
                {
                    WebClient webClient = new WebClient();
                    Common.Version onlineLauncherVersion = new Common.Version(webClient.DownloadString(CommonData.versionFileName));

                    if (onlineLauncherVersion.IsDifferentThan(localLauncherVersion))
                    {
                        UpdateGameLauncher(onlineLauncherVersion);
                    }
                    else
                    {
                        UpdateStatus = LauncherUpdateStatus.ready;
                    }
                }
                catch (Exception ex)
                {
                    UpdateStatus = LauncherUpdateStatus.failedToUpdateFiles;
                    MessageBox.Show($"Error attempting to update launcher: {ex}");
                }
            }
            else
            {
                MessageBox.Show("Failed to get launcher Version, Nothing found at" + CommonData.rootFilePath + "/" + CommonData.versionFileName);
                UpdateGameLauncher(Common.Version.zero);
            }
        }

        public void UpdateGameLauncher(Common.Version version)
        {
            try
            {
                WebClient webClient = new WebClient();
                UpdateStatus = LauncherUpdateStatus.updatingFiles;
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadCompleted);
                webClient.DownloadFileAsync(new Uri(""), zippedFile, version);
            }
            catch (Exception ex)
            {
                UpdateStatus = LauncherUpdateStatus.failedToUpdateFiles;
                MessageBox.Show($"Error attempting to update launcher: {ex}");
            }
        }
        private void DownloadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                string version = ((Common.Version)e.UserState).ToString();
                ZipFile.ExtractToDirectory(zippedFile, CommonData.rootFilePath, true);
                File.Delete(zippedFile);
                File.WriteAllText(CommonData.versionFileName, version);
                UpdateStatus = LauncherUpdateStatus.ready;
            }
            catch (Exception ex)
            {
                UpdateStatus = LauncherUpdateStatus.failedToUpdateFiles;
                MessageBox.Show($"Error attempting to update launcher: {ex}");
            }
        }
    }
}
