using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ModelsWorker.model;
using ModelsWorker;

namespace __Model_Studio.Classes
{
    class CSM_Actions
    {
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

        public static string Faces = "\n\t\t\t\"faces\": {\n\t\t\t\t\"north\": {\"uv\": [0, 0, 0.25, 0.5], \"texture\": \"#missing\"},\n\t\t\t\t\"east\": {\"uv\":  [0, 0, 0.25, 0.5], \"texture\": \"#missing\"},\n\t\t\t\t\"south\": {\"uv\": [0, 0, 0.25, 0.5], \"texture\": \"#missing\"},\n\t\t\t\t\"west\": {\"uv\":  [0, 0, 0.25, 0.5], \"texture\": \"#missing\"},\n\t\t\t\t\"up\": {\"uv\":    [0, 0, 0.25, 0.5], \"texture\": \"#missing\"},\n\t\t\t\t\"down\": {\"uv\":  [0, 0, 0.25, 0.5], \"texture\": \"#missing\"}\n\t\t\t}";

        public static void ModelToCSM(string OutputFilePath, ModelPiece MCon)
        {


            string OutputCSM = "";
            int i = 0;
            foreach (KeyValuePair<string, ModelsWorker.model.ModelPart> Part in MCon.Parts)
            {
                string name = Part.Key;
                foreach (KeyValuePair<string, ModelsWorker.model.ModelBox> Box in Part.Value.Boxes)
                {
                    OutputCSM += name + i + " BODY " + name + i + " ";


                    OutputCSM += Box.Value.PositionX + " ";
                    OutputCSM += Box.Value.PositionY + " ";
                    OutputCSM += Box.Value.PositionZ + " ";
                    OutputCSM += Box.Value.Length + " ";
                    OutputCSM += Box.Value.Height + " ";
                    OutputCSM += Box.Value.Width + " ";
                    OutputCSM += Box.Value.UvX + " ";
                    OutputCSM += Box.Value.UvY + "\n";

                    i++;

                }
            }
            File.WriteAllText(OutputFilePath, OutputCSM);

        }

        public static void CSMToModel(string InputFilePath, TreeNode Modelnode)
        {
            string OutputCSM = "";


            //Create list of CSM Boxes
            string[] CSMData = File.ReadAllText(InputFilePath).Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            int NumOfLines = CSMData.Length / 11;

            int x = 0;
            List<string> CSMLines = new List<string>();

            while (x < NumOfLines)
            {
                CSMLines.Add(String.Join("\n", CSMData.Skip(11 * x).Take(11).ToArray()));
                x++;
            }



            int i = 0;
            foreach (TreeNode tn1 in Modelnode.Nodes)
            {

                byte[] GroupData = StringToByteArrayFastest(tn1.Tag.ToString().Replace("-", ""));


                byte[] partNameLength = GroupData.Skip(0).Take(2).Reverse().ToArray();
                byte[] partName = GroupData.Skip(2).Take(BitConverter.ToUInt16(partNameLength, 0)).ToArray();

                string name = Encoding.Default.GetString(partName);
                foreach (TreeNode tn2 in tn1.Nodes)
                {
                    OutputCSM += name + i + " BODY " + name + i + " ";


                    byte[] ElementData = StringToByteArrayFastest(tn2.Tag.ToString().Replace("-", ""));


                    byte[] PositionX = ElementData.Skip(0).Take(4).Reverse().ToArray();
                    byte[] PositionY = ElementData.Skip(4).Take(4).Reverse().ToArray();
                    byte[] PositionZ = ElementData.Skip(8).Take(4).Reverse().ToArray();
                    byte[] BoxLength = ElementData.Skip(12).Take(4).Reverse().ToArray();
                    byte[] BoxHeight = ElementData.Skip(16).Take(4).Reverse().ToArray();
                    byte[] BoxWidth = ElementData.Skip(20).Take(4).Reverse().ToArray();
                    byte[] UvX = ElementData.Skip(24).Take(4).Reverse().ToArray();
                    byte[] UvY = ElementData.Skip(28).Take(4).Reverse().ToArray();
                    byte[] Scale = ElementData.Skip(32).Take(4).Reverse().ToArray();

                    OutputCSM += BitConverter.ToSingle(PositionX, 0) + " ";
                    OutputCSM += BitConverter.ToSingle(PositionY, 0) + " ";
                    OutputCSM += BitConverter.ToSingle(PositionZ, 0) + " ";
                    OutputCSM += BitConverter.ToInt32(BoxLength, 0) + " ";
                    OutputCSM += BitConverter.ToInt32(BoxHeight, 0) + " ";
                    OutputCSM += BitConverter.ToInt32(BoxWidth, 0) + " ";
                    OutputCSM += BitConverter.ToSingle(UvX, 0) + " ";
                    OutputCSM += BitConverter.ToSingle(UvY, 0) + "\n";

                    i++;

                }
            }
            //File.WriteAllText(OutputFilePath, OutputCSM);
        }

        public static void CSMToJSON(string InputFilePath, string OutputFilePath)
        {
            //Create list of CSM Boxes
            string[] CSMData = File.ReadAllText(InputFilePath).Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);


            int NumOfLines = CSMData.Length / 11;

            int x = 0;
            List<string> CSMLines = new List<string>();

            while (x < NumOfLines)
            {
                CSMLines.Add(String.Join("\n", CSMData.Skip(11 * x).Take(11).ToArray()));
                x++;
            }
            string JSONText = "{\n\t\"credit\": \"Generated with Spark Model Editor\",\n\t";


            JSONText += "\"texture_size\": [64,32],\n\t";

            string Groups = "\"groups\": [";
            string Elements = "\"elements\": [";
            int i = 0;
            int y = 0;
            Groups += "\n\t\t{";
            Groups += "\n\t\t\t\"name\": \"" + CSMData[1] + "\",";
            Groups += "\n\t\t\t\"origin\": [0, 24, 0],";
            Groups += "\n\t\t\t\"color\": 0,";
            Groups += "\n\t\t\t\"shade\": false,";
            Groups += "\n\t\t\t\"children\": [";


            foreach (string line in CSMLines)
            {
                string[] data = line.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

                string nom = data[0];
                string parent = data[1];

                float PosX = float.Parse(data[3]);
                float PosY = float.Parse(data[4]);
                float PosZ = float.Parse(data[5]);
                int Length = int.Parse(data[6]);
                int Height = int.Parse(data[7]);
                int Width = int.Parse(data[8]);
                float UvX = float.Parse(data[9]);
                float UvY = float.Parse(data[10]);

                // Write JSON


                Elements += "\n\t\t{";
                Elements += "\n\t\t\t\"name\": \"" + nom + "\",";
                Elements += "\n\t\t\t\"from\": [" + PosX + ", " + PosY + ", " + PosZ + "],";
                Elements += "\n\t\t\t\"to\": [" + (PosX + Length) + ", " + (PosY + Height) + ", " + (PosZ + Width) + "],";
                Elements += "\n\t\t\t\"color\": " + y + ",";
                Elements += "\n\t\t\t\"shade\": false,";
                Elements += Faces;
                Elements += "\n\t\t},";
                Groups += i + ",";
                i++;
                y++;

            }
            Groups += "]";
            Groups += "\n\t\t},";

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
    }
}
