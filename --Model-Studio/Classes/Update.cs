using System;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace __Model_Studio.Classes
{
    class Update
    {
        static string UpdateURL = "studio/Download/executable/SparkEditor-Setup.msi";
        static string BetaUpdateURL = "studio/Download/executable/beta/SparkEditorBeta-Setup.msi";

        public static void UpdateProgram(bool Beta)
        {
            Forms.FakeProgressBar fb = new Forms.FakeProgressBar();
            Thread thr = new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                fb.ShowDialog();
            });
            try
            {
                string DLPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/Temp/";
                switch (Beta)
                {
                    case true:
                        thr.Start();
                        try
                        {
                            DownloadFile(DLPath + "SparkEditorSetupBETA.msi", Classes.Network.MainURL + BetaUpdateURL);
                            Process.Start(DLPath + "SparkEditorSetupBETA.msi");
                            Application.Exit();
                        }
                        catch
                        {
                            DownloadFile(DLPath + "SparkEditorSetupBETA.msi", Classes.Network.BackURL + BetaUpdateURL);
                            Process.Start(DLPath + "SparkEditorSetupBETA.msi");
                            Application.Exit();
                        }
                        break;
                    case false:
                        thr.Start();
                        try
                        {
                            DownloadFile(DLPath + "SparkEditorSetup.msi", Classes.Network.MainURL + UpdateURL);
                            Process.Start(DLPath + "SparkEditorSetup.msi");
                            Application.Exit();
                        }
                        catch
                        {
                            DownloadFile(DLPath + "SparkEditorSetup.msi", Classes.Network.BackURL + UpdateURL);
                            Process.Start(DLPath + "SparkEditorSetup.msi");
                            Application.Exit();
                        }
                        break;
                }
            }
            catch
            {
                thr.Abort();
                MessageBox.Show("Could not update!");
            }
        }

        static void DownloadFile(string Path, string URL)
        {
            WebClient wc = new WebClient();
            wc.DownloadFile(Path, URL);
            wc.Dispose();
        }
    }
}
