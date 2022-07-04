using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ModelsWorker.model;
using ModelsWorker;
using Newtonsoft.Json;

namespace __Model_Studio.Classes
{
    class JSONActions
    {

        public static string Faces = "\n\t\t\t\"faces\": {\n\t\t\t\t\"north\": {\"uv\": [0, 0, 0.25, 0.5], \"texture\": \"#missing\"},\n\t\t\t\t\"east\": {\"uv\":  [0, 0, 0.25, 0.5], \"texture\": \"#missing\"},\n\t\t\t\t\"south\": {\"uv\": [0, 0, 0.25, 0.5], \"texture\": \"#missing\"},\n\t\t\t\t\"west\": {\"uv\":  [0, 0, 0.25, 0.5], \"texture\": \"#missing\"},\n\t\t\t\t\"up\": {\"uv\":    [0, 0, 0.25, 0.5], \"texture\": \"#missing\"},\n\t\t\t\t\"down\": {\"uv\":  [0, 0, 0.25, 0.5], \"texture\": \"#missing\"}\n\t\t\t}";

        #region SupportFunctions

        static byte[] StringToByteArrayFastest(string hex)
        {
            if (hex.Length % 2 == 1)
                try
                {
                    hex = "0" + hex;
                }
                catch
                {
                    throw new Exception("The binary key cannot have an odd number of digits");
                }

            byte[] arr = new byte[hex.Length >> 1];

            for (int i = 0; i < hex.Length >> 1; ++i)
            {
                arr[i] = (byte)((GetHexVal(hex[i << 1]) << 4) + (GetHexVal(hex[(i << 1) + 1])));
            }

            return arr;
        }

        static int GetHexVal(char hex)
        {
            int val = (int)hex;
            //For uppercase A-F letters:
            //return val - (val < 58 ? 48 : 55);
            //For lowercase a-f letters:
            //return val - (val < 58 ? 48 : 87);
            //Or the two combined, but a bit slower:
            return val - (val < 58 ? 48 : (val < 97 ? 55 : 87));
        }

        #endregion

        public static void ModelToJSON(string OutputFilePath, ModelPiece MCon)
        {
            string JSONText = "{\n\t\"credit\": \"Generated with Spark Model Editor\",\n\t";

            int Width = MCon.TextureWidth;
            int Height = MCon.TextureHeight;

            JSONText += "\"texture_size\": ["+Width+", "+Height+"],\n\t";

            string Groups = "\"groups\": [";
            string Elements = "\"elements\": [";
            int i = 0;
            int y = 0;
            foreach(KeyValuePair<string, ModelsWorker.model.ModelPart> Part in MCon.Parts)
            {

                Groups += "\n\t\t{";
                Groups += "\n\t\t\t\"name\": \"" + Part.Key + "\",";
                Groups += "\n\t\t\t\"origin\": [" + Part.Value.TranslationX + ", " + Part.Value.TranslationY + ", " + Part.Value.RotationZ + "],";
                Groups += "\n\t\t\t\"rotation\": [" + Part.Value.RotationX + ", " + Part.Value.RotationY + ", " + Part.Value.RotationZ + "],";
                Groups += "\n\t\t\t\"color\": 0,";
                Groups += "\n\t\t\t\"shade\": false,";
                Groups += "\n\t\t\t\"children\": [";

                foreach (KeyValuePair<string, ModelsWorker.model.ModelBox> Box in Part.Value.Boxes)
                {
                    Elements += "\n\t\t{";
                    Elements += "\n\t\t\t\"name\": \"Box " + Box.Key + "\",";
                    Elements += "\n\t\t\t\"from\": [" + Box.Value.PositionX + ", " + Box.Value.PositionY + ", " + Box.Value.PositionZ + "],";
                    Elements += "\n\t\t\t\"to\": [" + (Box.Value.PositionX + Box.Value.Length) + ", " + (Box.Value.PositionY + Box.Value.Height) + ", " + (Box.Value.PositionZ + Box.Value.Width) + "],";
                    Elements += "\n\t\t\t\"color\": "+y+",";
                    Elements += "\n\t\t\t\"shade\": false,";
                    Elements += Faces;
                    Elements += "\n\t\t},";
                    Groups += i + ",";
                    i++;
                    y++;

                }
                Groups += "]";
                Groups += "\n\t\t},";
            }
            Groups += "]";
            Elements += "\t],";
            Elements.Replace(",]", "]");
            Elements.Replace("},\t]", "}]");
            Groups.Replace(",]", "]");
            Groups.Replace("},\t]", "}]");

            JSONText += Elements;
            JSONText += Groups;
            JSONText += "\n}";
            File.WriteAllText(OutputFilePath, JSONText);

        }

        public static void JSONToCSM(string InputFilePath, string OutputFilePath)
        {
            dynamic jsonDe = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(InputFilePath));
            string CSMData = "";
            foreach (JObjectGroup group in jsonDe.groups)
            {
                string PARENT = group.name;
                foreach(int i in group.children)
                {
                    string name = jsonDe.elements[i].name;
                    float PosX = jsonDe.elements[i].from[0] + group.origin[0];
                    float PosY = jsonDe.elements[i].from[1] + group.origin[1];
                    float PosZ = jsonDe.elements[i].from[2] + group.origin[2];
                    float SizeX = jsonDe.elements[i].to[0] - jsonDe.elements[i].from[0];
                    float SizeY = jsonDe.elements[i].to[1] - jsonDe.elements[i].from[1];
                    float SizeZ = jsonDe.elements[i].to[2] - jsonDe.elements[i].from[2];
                    float UvX = 0;
                    float UvY = 0;

                    CSMData += name + "\n" + PARENT + "\n" + name + "\n" + PosX + "\n" + PosY + "\n" + PosZ + "\n" + SizeX + "\n" + SizeY + "\n" + SizeZ + "\n" + UvX + "\n" + UvY + "\n";
                }
            }
            File.WriteAllText(OutputFilePath, CSMData);
        }
    }

    class JObject
    {
        public string credit;
        public int[] texture_size;
        public JObjectElement[] elements;
        public JObjectGroup[] groups;
    }
    class JObjectElement
    {
        public string name;
        public float[] from;
        public float[] to;

    }
    class JObjectGroup
    {
        public string name;
        public float[] origin;
        public int[] children;

    }
}
