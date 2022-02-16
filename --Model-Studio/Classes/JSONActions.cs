using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        public static void ModelToJSON(string OutputFilePath, TreeNode Modelnode)
        {
            byte[] InitModelData = StringToByteArrayFastest(Modelnode.Tag.ToString().Replace("-", ""));

            string JSONText = "{\n\t\"credit\": \"Generated with Spark Model Editor\",\n\t";

            int Width = BitConverter.ToInt32(InitModelData.Skip(InitModelData.Length - 8).Take(4).Reverse().ToArray(), 0);
            int Height = BitConverter.ToInt32(InitModelData.Skip(InitModelData.Length - 4).Take(4).Reverse().ToArray(), 0);

            JSONText += "\"texture_size\": ["+Width+", "+Height+"],\n\t";

            string Groups = "\"groups\": [";
            string Elements = "\"elements\": [";
            int i = 0;
            int y = 0;
            foreach(TreeNode tn1 in Modelnode.Nodes)
            {
                byte[] GroupData = StringToByteArrayFastest(tn1.Tag.ToString().Replace("-", ""));


                byte[] partNameLength = GroupData.Skip(0).Take(2).Reverse().ToArray();
                byte[] partName = GroupData.Skip(2).Take(BitConverter.ToUInt16(partNameLength, 0)).ToArray();
                byte[] blankBuffer = GroupData.Skip(0 + (2 + BitConverter.ToUInt16(partNameLength, 0))).Take(4).Reverse().ToArray();
                byte[] TranslationX = GroupData.Skip(4 + (2 + BitConverter.ToUInt16(partNameLength, 0))).Take(4).Reverse().ToArray();
                byte[] TranslationY = GroupData.Skip(8 + (2 + BitConverter.ToUInt16(partNameLength, 0))).Take(4).Reverse().ToArray();
                byte[] TranslationZ = GroupData.Skip(12 + (2 + BitConverter.ToUInt16(partNameLength, 0))).Take(4).Reverse().ToArray();
                byte[] TextureOffsetX = GroupData.Skip(16 + (2 + BitConverter.ToUInt16(partNameLength, 0))).Take(4).Reverse().ToArray();
                byte[] TextureOffsetY = GroupData.Skip(20 + (2 + BitConverter.ToUInt16(partNameLength, 0))).Take(4).Reverse().ToArray();
                byte[] RotationX = GroupData.Skip(24 + (2 + BitConverter.ToUInt16(partNameLength, 0))).Take(4).Reverse().ToArray();
                byte[] RotationY = GroupData.Skip(28 + (2 + BitConverter.ToUInt16(partNameLength, 0))).Take(4).Reverse().ToArray();
                byte[] RotationZ = GroupData.Skip(32 + (2 + BitConverter.ToUInt16(partNameLength, 0))).Take(4).Reverse().ToArray();

                Groups += "\n\t\t{";
                Groups += "\n\t\t\t\"name\": \"" + Encoding.Default.GetString(partName) + "\",";
                Groups += "\n\t\t\t\"origin\": [" + BitConverter.ToSingle(TranslationX, 0) + ", " + BitConverter.ToSingle(TranslationY, 0) + ", " + BitConverter.ToSingle(TranslationZ, 0) + "],";
                Groups += "\n\t\t\t\"rotation\": [" + BitConverter.ToSingle(RotationX, 0) + ", " + BitConverter.ToSingle(RotationY, 0) + ", " + BitConverter.ToSingle(RotationZ, 0) + "],";
                Groups += "\n\t\t\t\"color\": 0,";
                Groups += "\n\t\t\t\"shade\": false,";
                Groups += "\n\t\t\t\"children\": [";

                foreach (TreeNode tn2 in tn1.Nodes)
                {
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

                    Elements += "\n\t\t{";
                    Elements += "\n\t\t\t\"name\": \"" + Encoding.Default.GetString(partName) + "\",";
                    Elements += "\n\t\t\t\"from\": [" + BitConverter.ToSingle(PositionX, 0) + ", " + BitConverter.ToSingle(PositionY, 0) + ", " + BitConverter.ToSingle(PositionZ, 0) + "],";
                    Elements += "\n\t\t\t\"to\": [" + (BitConverter.ToSingle(PositionX, 0) + BitConverter.ToInt32(BoxLength, 0)) + ", " + (BitConverter.ToSingle(PositionY, 0) + BitConverter.ToInt32(BoxHeight, 0)) + ", " + (BitConverter.ToSingle(PositionZ, 0) + BitConverter.ToInt32(BoxWidth, 0)) + "],";
                    Elements += "\n\t\t\t\"color\": "+y+",";
                    Elements += "\n\t\t\t\"shade\": false,";
                    Elements += Faces;
                    Elements += "\n\t\t},";
                    Groups += i + ", ";
                    i++;
                    y++;

                }
                Groups += "]";
                Groups += "\n\t\t},";
            }
            Groups += "\t]";
            Elements += "\t]";

            JSONText += Elements;
            JSONText += Groups;
            JSONText += "\n}";
            File.WriteAllText(OutputFilePath, JSONText);

        }
    }
}
