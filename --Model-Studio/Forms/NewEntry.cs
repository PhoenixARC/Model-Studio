using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace __Model_Studio.Forms
{
    public partial class NewEntry : Form
    {
        public NewEntry(TreeNode tn, int Level)
        {
            InitializeComponent();
            tn1 = tn;
            Levl = Level;
        }
        public TreeNode tn1;
        public int Levl;

        private void button1_Click(object sender, EventArgs e)
        {
            Int16 nomlen = Int16.Parse(textBox1.Text.Length.ToString());
            string nom = textBox1.Text;
            byte[] NomLenByte = BitConverter.GetBytes(nomlen).Reverse().ToArray();
            byte[] NomByte = Encoding.Default.GetBytes(nom);
            List<byte> OutputList = new List<byte>();
            OutputList.AddRange(NomLenByte);
            OutputList.AddRange(NomByte);
            byte[] Arr1 = new byte[0x0A];
            byte[] Arr2 = new byte[0x2A];

            for (int i = 0; i < Arr1.Length; i++)
            {
                Arr1[i] = 0x00;
            }
            for (int i = 0; i < Arr2.Length; i++)
            {
                Arr2[i] = 0x00;
            }

            if (Levl == 1)
                OutputList.AddRange(Arr1);
            if (Levl == 2)
                OutputList.AddRange(Arr2);

            TreeNode tn0 = new TreeNode(textBox1.Text);

                tn0.Tag = BitConverter.ToString(OutputList.ToArray());

            tn1.Nodes.Add(tn0);
            this.Dispose();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }
    }
}
