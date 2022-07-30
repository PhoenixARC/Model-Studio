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
    public partial class UnidentifiedModel : Form
    {
        public UnidentifiedModel()
        {
            InitializeComponent();
        }
        public string ReturnValue1 { get; set; }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(comboBox1.SelectedItem.ToString()))
            {

                ReturnValue1 = comboBox1.SelectedItem.ToString();
                this.Close();
            }
        }
    }
}
