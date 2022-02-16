using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        public static void ModelToCSM(string OutputFilePath, TreeNode Modelnode)
        {


            string OutputCSM = "";
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
            File.WriteAllText(OutputFilePath, OutputCSM);

        }
    }
}
