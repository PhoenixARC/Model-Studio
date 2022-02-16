using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace __Model_Studio
{
    public partial class Form1 : Form
    {

        #region Variables

        ModelsBin.ModelFile mf = new ModelsBin.ModelFile();
        bool HasFileOpen = false;

        #endregion

        #region functions

        public void OpenModels()
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Models File|*.bin";
            opf.InitialDirectory = Environment.CurrentDirectory;
            if (opf.ShowDialog() == DialogResult.OK)
            {
                FileNodeTree.Nodes.Clear();
                EntryNodeTree.Nodes.Clear();
                mf.OpenModel(opf.FileName, FileNodeTree, EntryNodeTree);
                saveToolStripMenuItem.Enabled = true;
                HasFileOpen = true;
            }
        }

        public void SaveModels()
        {
            SaveFileDialog opf = new SaveFileDialog();
            opf.Filter = "Models File|*.bin";
            opf.InitialDirectory = Environment.CurrentDirectory;
            if (opf.ShowDialog() == DialogResult.OK)
            {
                mf.SaveModel(opf.FileName, FileNodeTree.Nodes[0]);
            }
        }

        public static byte[] StringToByteArrayFastest(string hex)
        {
            if (hex.Length % 2 == 1)
                throw new Exception("The binary key cannot have an odd number of digits");

            byte[] arr = new byte[hex.Length >> 1];

            for (int i = 0; i < hex.Length >> 1; ++i)
            {
                arr[i] = (byte)((GetHexVal(hex[i << 1]) << 4) + (GetHexVal(hex[(i << 1) + 1])));
            }

            return arr;
        }

        public static int GetHexVal(char hex)
        {
            int val = (int)hex;
            //For uppercase A-F letters:
            //return val - (val < 58 ? 48 : 55);
            //For lowercase a-f letters:
            //return val - (val < 58 ? 48 : 87);
            //Or the two combined, but a bit slower:
            return val - (val < 58 ? 48 : (val < 97 ? 55 : 87));
        }

        public void ResetNodeCount(TreeNode tn)
        {
            byte[] data = StringToByteArrayFastest(tn.Tag.ToString().Replace("-", ""));
            List<byte> newdat = new List<byte>();
            newdat.AddRange(data.Take(data.Length - 4));
            newdat.AddRange(BitConverter.GetBytes(tn.Nodes.Count).Reverse().ToArray());
            tn.Tag = BitConverter.ToString(newdat.ToArray());
        }

        public void RebuildFileNode()
        {

            int g = ModelsBin.GetNodeparents(FileNodeTree.SelectedNode);
            List<byte> OutputBytes = new List<byte>();

            switch (g)
            {
                case (4):
                    {
                        foreach(TreeNode tn in EntryNodeTree.Nodes[0].Nodes)
                        {
                            byte[] data = StringToByteArrayFastest(tn.Tag.ToString().Replace("-", "")).Reverse().ToArray();
                            OutputBytes.AddRange(data);
                        }
                        FileNodeTree.SelectedNode.Tag = BitConverter.ToString(OutputBytes.ToArray());
                    }
                    break;
                case (3):
                    {

                        byte[] nomLen = BitConverter.GetBytes(Int16.Parse(FileNodeTree.SelectedNode.Text.Length.ToString())).Reverse().ToArray();
                        byte[] nom = Encoding.Default.GetBytes(FileNodeTree.SelectedNode.Text);
                        byte[] TranslationX = StringToByteArrayFastest(EntryNodeTree.Nodes[0].Nodes[1].Tag.ToString().Replace("-", "")).ToArray();
                        byte[] TranslationY = StringToByteArrayFastest(EntryNodeTree.Nodes[0].Nodes[2].Tag.ToString().Replace("-", "")).ToArray();
                        byte[] TranslationZ = StringToByteArrayFastest(EntryNodeTree.Nodes[0].Nodes[3].Tag.ToString().Replace("-", "")).ToArray();
                        byte[] OffsetX = StringToByteArrayFastest(EntryNodeTree.Nodes[0].Nodes[4].Tag.ToString().Replace("-", "")).ToArray();
                        byte[] OffsetY = StringToByteArrayFastest(EntryNodeTree.Nodes[0].Nodes[5].Tag.ToString().Replace("-", "")).ToArray();
                        byte[] RotationX = StringToByteArrayFastest(EntryNodeTree.Nodes[0].Nodes[6].Tag.ToString().Replace("-", "")).ToArray();
                        byte[] RotationY = StringToByteArrayFastest(EntryNodeTree.Nodes[0].Nodes[7].Tag.ToString().Replace("-", "")).ToArray();
                        byte[] RotationZ = StringToByteArrayFastest(EntryNodeTree.Nodes[0].Nodes[8].Tag.ToString().Replace("-", "")).ToArray();

                        OutputBytes.AddRange(nomLen);
                        OutputBytes.AddRange(nom);
                        OutputBytes.AddRange(new byte[] { 0x00, 0x00, 0x00, 0x00 });
                        OutputBytes.AddRange(TranslationX);
                        OutputBytes.AddRange(TranslationY);
                        OutputBytes.AddRange(TranslationZ);
                        OutputBytes.AddRange(OffsetX);
                        OutputBytes.AddRange(OffsetY);
                        OutputBytes.AddRange(RotationX);
                        OutputBytes.AddRange(RotationY);
                        OutputBytes.AddRange(RotationZ);
                        OutputBytes.AddRange(new byte[] { 0x00, 0x00, 0x00, 0x00 });
                        FileNodeTree.SelectedNode.Tag = BitConverter.ToString(OutputBytes.ToArray());
                        ResetNodeCount(FileNodeTree.SelectedNode);
                    }
                    break;
                case (2):
                    {
                        byte[] nomLen = BitConverter.GetBytes(Int16.Parse(FileNodeTree.SelectedNode.Text.Length.ToString())).Reverse().ToArray();
                        byte[] nom = Encoding.Default.GetBytes(FileNodeTree.SelectedNode.Text);
                        byte[] wid = StringToByteArrayFastest(EntryNodeTree.Nodes[0].Nodes[1].Tag.ToString().Replace("-", "")).ToArray();
                        byte[] hei = StringToByteArrayFastest(EntryNodeTree.Nodes[0].Nodes[2].Tag.ToString().Replace("-", "")).ToArray();
                        OutputBytes.AddRange(nomLen);
                        OutputBytes.AddRange(nom);
                        OutputBytes.AddRange(wid);
                        OutputBytes.AddRange(hei);
                        OutputBytes.AddRange(new byte[] { 0x00, 0x00, 0x00, 0x00 });
                        FileNodeTree.SelectedNode.Tag = BitConverter.ToString(OutputBytes.ToArray());
                        ResetNodeCount(FileNodeTree.SelectedNode);
                    }
                    break;
            }

        }

        #endregion

        #region Form Functions

        public Form1()
        {
            Classes.UserInteraction.GenerateDocumentData();
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (HasFileOpen)
            {
                if (MessageBox.Show("You have an open model!\ndo you want to discard it?", "Warning!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    OpenModels();
            }
            else
                OpenModels();
            FileNodeTree.SelectedNode = FileNodeTree.Nodes[0];

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            int g = ModelsBin.GetNodeparents(FileNodeTree.SelectedNode);
            if(g == 4)
            {
                ModelsBin.ModelBox mb = new ModelsBin.ModelBox();
                mb.SetTreeData(StringToByteArrayFastest(FileNodeTree.SelectedNode.Tag.ToString().Replace("-","")), EntryNodeTree) ;
                ModelStrip.Items[0].Enabled = false;
                ModelStrip.Items[1].Enabled = true;
                ModelStrip.Items[2].Enabled = false;
            }
            if(g == 3)
            {
                ModelsBin.ModelPart mb = new ModelsBin.ModelPart();
                mb.SetTreeData(StringToByteArrayFastest(FileNodeTree.SelectedNode.Tag.ToString().Replace("-","")), EntryNodeTree);
                ModelStrip.Items[0].Enabled = true;
                ModelStrip.Items[1].Enabled = true;
                ModelStrip.Items[2].Enabled = false;
            }
            if(g == 2)
            {
                ModelsBin.ModelName mb = new ModelsBin.ModelName();
                mb.SetTreeData(StringToByteArrayFastest(FileNodeTree.SelectedNode.Tag.ToString().Replace("-","")), EntryNodeTree);
                ModelStrip.Items[0].Enabled = true;
                ModelStrip.Items[1].Enabled = true;
                ModelStrip.Items[2].Enabled = true;
            }
            if(g == 1)
            {
                ModelStrip.Items[0].Enabled = true;
                ModelStrip.Items[1].Enabled = false;
                ModelStrip.Items[2].Enabled = false;
            }
        }

        private void EntryNodeTree_AfterSelect(object sender, EventArgs e)
        {

            if (EntryNodeTree.SelectedNode.ImageIndex == 53)
            {
                Forms.ValueEditor ve = new Forms.ValueEditor(EntryNodeTree.SelectedNode, 1);
                ve.ShowDialog();
                RebuildFileNode();
            }
            else if (EntryNodeTree.SelectedNode.ImageIndex == 55)
            {
                Forms.ValueEditor ve = new Forms.ValueEditor(EntryNodeTree.SelectedNode, 2);
                ve.ShowDialog();
                RebuildFileNode();
            }
        }

        private void viewItemHexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.HexBox ve = new Forms.HexBox(EntryNodeTree.SelectedNode);
            ve.ShowDialog();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.About abt = new Forms.About();
            abt.ShowDialog();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveModels();
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FileNodeTree.SelectedNode == FileNodeTree.Nodes[0])
                MessageBox.Show("Cannot remove head node!");
            else
                FileNodeTree.Nodes.Remove(FileNodeTree.SelectedNode);
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int g = ModelsBin.GetNodeparents(FileNodeTree.SelectedNode);

            switch (g)
            {
                case (3):
                    List<byte> OutputList = new List<byte>();
                    byte[] Arr1 = new byte[0x24];
                    for (int i = 0; i < Arr1.Length; i++)
                    {
                        Arr1[i] = 0x00;
                    }
                    TreeNode tn0 = new TreeNode("Box" + (FileNodeTree.SelectedNode.Nodes.Count + 1));
                    tn0.Tag = BitConverter.ToString(Arr1);
                    FileNodeTree.SelectedNode.Nodes.Add(tn0);
                    ResetNodeCount(FileNodeTree.SelectedNode);
                    break;
                case (2):
                    Forms.NewEntry ne = new Forms.NewEntry(FileNodeTree.SelectedNode, g);
                    ne.ShowDialog();
                    ResetNodeCount(FileNodeTree.SelectedNode);
                    break;
                case (1):
                    Forms.NewEntry ne1 = new Forms.NewEntry(FileNodeTree.SelectedNode, g);
                    ne1.ShowDialog();
                    ResetNodeCount(FileNodeTree.SelectedNode);
                    break;
                default:
                    break;
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Classes.Network.CheckUpdate();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (HasFileOpen)
            {
                if (MessageBox.Show("You have an open model!\ndo you want to discard it?", "Warning!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    FileNodeTree.Nodes.Clear();
                    EntryNodeTree.Nodes.Clear();
                    mf.OpenModel(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\ModelStudio\\TemplateModels\\models.bin", FileNodeTree, EntryNodeTree);
                    saveToolStripMenuItem.Enabled = true;
                    HasFileOpen = true;
                }
            }
            else
            {
                FileNodeTree.Nodes.Clear();
                EntryNodeTree.Nodes.Clear();
                mf.OpenModel(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\ModelStudio\\TemplateModels\\models.bin", FileNodeTree, EntryNodeTree);
                saveToolStripMenuItem.Enabled = true;
                HasFileOpen = true;
            }
            FileNodeTree.SelectedNode = FileNodeTree.Nodes[0];
        }

        private void convertToCSMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CustomSkinModel| *.CSM";
            if(sfd.ShowDialog() == DialogResult.OK)
            {
                Classes.CSM_Actions.ModelToCSM(sfd.FileName, FileNodeTree.SelectedNode);
                //Classes.JSONActions.ModelToJSON(sfd.FileName, FileNodeTree.SelectedNode);
            }
        }

        private void changeLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.ChangeLog cl = new Forms.ChangeLog();
            cl.ShowDialog();
        }

        private void tESTINGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.FakeProgressBar fb = new Forms.FakeProgressBar();
            fb.ShowDialog();
        }

        private void sourceCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/PhoenixARC/--Model-Studio");
        }

        #endregion
    }
}
