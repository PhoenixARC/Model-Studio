using System;
using System.IO;
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
    public partial class ChangeLog : Form
    {
        public ChangeLog()
        {
            InitializeComponent();
        }

        private void ChangeLog_Load(object sender, EventArgs e)
        {
            string docuDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            richTextBox1.Text = File.ReadAllText(docuDir + "\\ModelStudio\\UserData\\Change.log");
        }
    }
}
