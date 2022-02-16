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
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void About_Load(object sender, EventArgs e)
        {
            if (Classes.Network.NeedsUpdate)
            {
                if (Classes.Network.Beta)
                    label1.Text = label1.Text.Replace("%s", Application.ProductVersion + "[BETA] [OUTDATED]");
                else
                    label1.Text = label1.Text.Replace("%s", Application.ProductVersion + "[OUTDATED]");
            }
            else
            {
                if (Classes.Network.Beta)
                    label1.Text = label1.Text.Replace("%s", Application.ProductVersion + "[BETA]");
                else
                    label1.Text = label1.Text.Replace("%s", Application.ProductVersion);
            }
        }
    }
}
