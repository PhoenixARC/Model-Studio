using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace __Model_Studio.Forms
{
    public partial class ValueEditor : Form
    {
        public ValueEditor(TreeNode tn, int mode)
        {
            InitializeComponent();
            Node = tn;
            NodeMode = mode;
            Init(mode);
        }

        public TreeNode Node;
        public int NodeMode;
        public bool LittleEndianInt = false;

        public void Init(int mode)
        {
            textBox1.Enabled = true;
            switch (mode)
            {
                case 1:
                    textBox1.Text = BitConverter.ToSingle(StringToByteArrayFastest(Node.Tag.ToString().Replace("-", "")), 0).ToString();
                    break;
                case 2:
                    if (BitConverter.ToInt32(StringToByteArrayFastest(Node.Tag.ToString().Replace("-", "")).ToArray(), 0) > 1000)
                    {
                        textBox1.Text = BitConverter.ToInt32(StringToByteArrayFastest(Node.Tag.ToString().Replace("-", "")).Reverse().ToArray(), 0).ToString();
                        LittleEndianInt = true;
                    }
                    else
                        textBox1.Text = BitConverter.ToInt32(StringToByteArrayFastest(Node.Tag.ToString().Replace("-", "")).ToArray(), 0).ToString();
                    break;
            }
        }

        public static byte[] ObjectToByteArray(Object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

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

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            var regex = new Regex(@"[^0-9.\u002d\s\b]");
            if (NodeMode == 2)
            {
                regex = new Regex(@"[^0-9\u002d\s\b]");
            }
            if (regex.IsMatch(e.KeyChar.ToString()))
            {
                e.Handled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (NodeMode == 1)
                Node.Tag = BitConverter.ToString(BitConverter.GetBytes(float.Parse(textBox1.Text)).ToArray());
            else if (NodeMode == 2)
            {
                if (!LittleEndianInt)
                    Node.Tag = BitConverter.ToString(BitConverter.GetBytes(Int32.Parse(textBox1.Text)).ToArray());
                else
                    Node.Tag = BitConverter.ToString(BitConverter.GetBytes(Int32.Parse(textBox1.Text)).Reverse().ToArray());
            }
            Node.Text = Node.Text.Split(':')[0] + ": " + textBox1.Text;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
