using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace __Model_Studio.Classes
{
    class UserInteraction
    {

        public static void GenerateDocumentData()
        {
            try
            {
                string docuDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                if(!File.Exists(docuDir + "\\ModelStudio\\UserData\\PreviouslyLoaded.files"))
                {
                    Directory.CreateDirectory(docuDir + "\\ModelStudio\\UserData\\");
                    Directory.CreateDirectory(docuDir + "\\ModelStudio\\TemplateModels\\");
                    File.Create(docuDir + "\\ModelStudio\\UserData\\PreviouslyLoaded.files");
                    File.Create(docuDir + "\\ModelStudio\\UserData\\Change.log");
                    File.WriteAllBytes(docuDir + "\\ModelStudio\\TemplateModels\\models.bin", Properties.Resources.TemplateModels);
                }
                if (!File.Exists(docuDir + "\\ModelStudio\\TemplateModels\\BedrockGeometry.json"))
                {
                    File.WriteAllBytes(docuDir + "\\ModelStudio\\TemplateModels\\BedrockGeometry.json", Properties.Resources.mobsJSON);
                }
            }
            catch
            {
                try
                {

                    string docuDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    Directory.CreateDirectory(docuDir + "\\ModelStudio\\UserData\\");
                    Directory.CreateDirectory(docuDir + "\\ModelStudio\\TemplateModels\\");
                    File.Create(docuDir + "\\ModelStudio\\UserData\\PreviouslyLoaded.files");
                    File.Create(docuDir + "\\ModelStudio\\UserData\\Change.log");
                    File.WriteAllBytes(docuDir + "\\ModelStudio\\TemplateModels\\models.bin", Properties.Resources.TemplateModels);
                }
                catch { }
            }
        }

    }
}
