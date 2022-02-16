using System;
using System.Threading;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;

namespace __Model_Studio.Classes
{
    class Network
    {
        static string Version = "0.2";
        public static bool Beta = true;
        public static bool NeedsUpdate = false;
        public static string MainURL = "https://pckstudio.xyz/";
        public static string BackURL = "http://phoenixarc.ddns.net/";
        static string UpdateURL = "studio/Model/api/update.txt";
        static string BetaUpdateURL = "studio/Model/api/updateB.txt";

        public static void CheckUpdate()
        {
            WebClient wc = new WebClient();
            string docuDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            try
            {
                switch (Beta)
                {
                    case false:
                        if (float.Parse(Version) < float.Parse(wc.DownloadString(MainURL + UpdateURL)))
                        {
                            if (MessageBox.Show("An update is available! do you want to update?\nYour Version:" + Version + "\nAvailable version:" + wc.DownloadString(MainURL + UpdateURL), "Update Available", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                Classes.Update.UpdateProgram(Beta);
                            }
                            else
                            {
                                NeedsUpdate = true;
                            }
                            File.WriteAllText(docuDir + "\\ModelStudio\\UserData\\Change.log", MainURL + "studio/Model/api/changelog.txt");
                        }
                        break;
                    case true:
                        if (float.Parse(Version) < float.Parse(wc.DownloadString(MainURL + BetaUpdateURL)))
                        {
                            if (MessageBox.Show("An update is available! do you want to update?\nYour Version:" + Version + "\nAvailable version:" + wc.DownloadString(MainURL + UpdateURL), "Update Available", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                Classes.Update.UpdateProgram(Beta);
                            }
                            else
                            {
                                NeedsUpdate = true;
                            }
                            File.WriteAllText(docuDir + "\\ModelStudio\\UserData\\Change.log", MainURL + "studio/Model/api/changelogBeta.txt");
                        }
                        break;
                }
            }
            catch
            {
                try
                {
                    switch (Beta)
                    {
                        case false:
                            if (float.Parse(Version) < float.Parse(wc.DownloadString(BackURL + UpdateURL)))
                            {
                                if (MessageBox.Show("An update is available! do you want to update?\nYour Version:" + Version + "\nAvailable version:" + wc.DownloadString(BackURL + UpdateURL), "Update Available", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    Classes.Update.UpdateProgram(Beta);
                                }
                                else
                                {
                                    NeedsUpdate = true;
                                }
                                File.WriteAllText(docuDir + "\\ModelStudio\\UserData\\Change.log", BackURL + "studio/Model/api/changelog.txt");
                            }
                            break;
                        case true:
                            if (float.Parse(Version) < float.Parse(wc.DownloadString(BackURL + BetaUpdateURL)))
                            {
                                if (MessageBox.Show("An update is available! do you want to update?\nYour Version:" + Version + "\nAvailable version:" + wc.DownloadString(BackURL + UpdateURL), "Update Available", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    Classes.Update.UpdateProgram(Beta);
                                }
                                else
                                {
                                    NeedsUpdate = true;
                                }
                                File.WriteAllText(docuDir + "\\ModelStudio\\UserData\\Change.log", BackURL + "studio/Model/api/changelogBeta.txt");
                            }
                            break;
                    }
                }
                catch
                {
                    MessageBox.Show("Server unavailabe", "Cannot connect to the server!");
                }
            }
        }


    }
}
