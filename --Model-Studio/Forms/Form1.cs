using System;
using System.Net;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;

namespace __Model_Studio
{
    public partial class Form1 : Form
    {

        #region Variables

        ModelsBin.ModelFile mf = new ModelsBin.ModelFile();
        ModelsWorker.model.ModelContainer MCon = new ModelsWorker.model.ModelContainer();
        ModelsWorker.ModelParser ModelParser = new ModelsWorker.ModelParser();
        ModelsWorker.ModelBuilder ModelBuilder = new ModelsWorker.ModelBuilder();
        bool HasFileOpen = false;


        static string[] IconSheetIndex =
{"unknown",
"creeper",
"skeleton",
"spider",
"zombie",
"slime",
"ghast",
"pigzombie",
"enderman",
"cavespider",
"silverfish",
"blaze",
"lavaslime",
"enderdragon",
"witherBoss",
"bat",
"villager.witch",
"endermite",
"guardian",
"shulker",
"pig",
"sheep",
"cow",
"chicken",
"squid",
"wolf",
"redcow",
"snowgolem",
"ocelot",
"irongolem",
"horse",
"donkey",
"mule",
"skeletonhorse",
"zombiehorse",
"rabbit",
"polarbear",
"llama",
"parrot",
"villager",
"vindicator",
"evoker",
"vex",
"skeleton.stray",
"illusioner",
"endcrystal",
"zombie.villager",
"elderguardian",
"zombie.husk",
"skeleton.wither",
"package",
"folder",
"single",
"float",
"text",
"integer",
"dolphin",
"dragon",
"trident",
"boat",
"minecart",
"dragon_head",
"creeper_head",
"sheep.sheared"};

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
                //mf.OpenModel(opf.FileName, FileNodeTree, EntryNodeTree);
                MCon = ModelParser.Parse(opf.FileName);
                GetNodes(FileNodeTree, EntryNodeTree);
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

        private static int GetIndex(string nom)
        {
            //Console.WriteLine(nom + " -- " + Array.IndexOf(IconSheetIndex, nom));
            return Array.IndexOf(IconSheetIndex, nom);
        }

        public void GetNodes(TreeView treeView0, TreeView treeView1)
        {


            #region AddImageList

            // Get the inputs.
            Bitmap bm = (Bitmap)Properties.Resources.mobs;
            int wid = 32;
            int hgt = 32;


            ImageList imageList = new ImageList();
            imageList.ColorDepth = ColorDepth.Depth32Bit;
            imageList.ImageSize = new Size(20, 20);


            // Start splitting the Bitmap.
            Bitmap piece = new Bitmap(wid, hgt);
            Rectangle dest_rect = new Rectangle(0, 0, wid, hgt);
            using (Graphics gr = Graphics.FromImage(piece))
            {
                int num_rows = bm.Height / hgt;
                int num_cols = bm.Width / wid;
                Rectangle source_rect = new Rectangle(0, 0, wid, hgt);
                for (int row = 0; row < num_rows; row++)
                {
                    source_rect.X = 0;
                    for (int col = 0; col < num_cols; col++)
                    {
                        // Copy the piece of the image.
                        gr.Clear(Color.Transparent);
                        gr.DrawImage(bm, dest_rect, source_rect,
                            GraphicsUnit.Pixel);

                        // Save the piece.
                        MemoryStream ms = new MemoryStream();
                        piece.Save(ms, ImageFormat.Png);
                        Image imag = Image.FromStream(ms);
                        imageList.Images.Add(imag);

                        // Move to the next column.
                        source_rect.X += wid;
                    }
                    source_rect.Y += hgt;
                }
            }
            Console.WriteLine(imageList.Images.Count + "--");
            treeView0.ImageList = imageList;
            treeView1.ImageList = imageList;

            #endregion

            TreeNode treeNode = new TreeNode();
            treeNode.Text = "Models.bin [" + MCon.models.Count + " Models]";

            treeNode.ImageIndex = GetIndex("package");
            treeNode.SelectedImageIndex = GetIndex("package");

            foreach (KeyValuePair<string, ModelsWorker.model.ModelPiece> Piece in MCon.models)
            {
                TreeNode treeNodeModel = new TreeNode(Piece.Key);
                treeNodeModel.Text = Piece.Key.ToString();
                treeNodeModel.ImageIndex = GetIndex(Piece.Key.ToString());
                treeNodeModel.SelectedImageIndex = GetIndex(Piece.Key.ToString());

                foreach (KeyValuePair<string, ModelsWorker.model.ModelPart> Part in Piece.Value.Parts)
                {
                    TreeNode treeNodeModelPiece = new TreeNode(Part.Key);
                    treeNodeModelPiece.Text = Part.Key.ToString();
                    treeNodeModelPiece.ImageIndex = GetIndex(Part.Key.ToString());
                    treeNodeModelPiece.SelectedImageIndex = GetIndex(Part.Key.ToString());


                    foreach (KeyValuePair<string, ModelsWorker.model.ModelBox> Box in Part.Value.Boxes)
                    {
                        TreeNode treeNodeModelPieceBox = new TreeNode();
                        treeNodeModelPieceBox.Text = "Box " + Box.Key.ToString();
                        treeNodeModelPieceBox.ImageIndex = GetIndex(Part.Key.ToString());
                        treeNodeModelPieceBox.SelectedImageIndex = GetIndex(Part.Key.ToString());

                        treeNodeModelPiece.Nodes.Add(treeNodeModelPieceBox);
                    }

                    treeNodeModel.Nodes.Add(treeNodeModelPiece);
                }

                treeNode.Nodes.Add(treeNodeModel);
            }

            treeView0.Nodes.Add(treeNode);
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
            try
            {
                FileNodeTree.SelectedNode = FileNodeTree.Nodes[0];
            }
            catch
            {

            }

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
            try
            {
                FileNodeTree.SelectedNode = FileNodeTree.Nodes[0];
            }
            catch
            {

            }
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
        }

        private void sourceCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/PhoenixARC/Model-Studio");
        }

        private void sendABugReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string ver = Application.ProductVersion;
            string trace = Environment.StackTrace;
            string link = "mailto:phoenixarc.canarynotifs@gmail.com?subject=Spark%20Model%20Editor%20BUGREPORT&body=Version%3A%20$s%0A%0AStack%20Trace%3A%0A$b";
            System.Diagnostics.Process.Start(link.Replace("$s", ver).Replace("$b", trace));
        }

        #endregion

        private void FileNodeTree_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (HasFileOpen)
            {
                if (MessageBox.Show("You have an open model!\ndo you want to discard it?", "Warning!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    FileNodeTree.Nodes.Clear();
                    EntryNodeTree.Nodes.Clear();
                    mf.OpenModel(files[0], FileNodeTree, EntryNodeTree);
                    saveToolStripMenuItem.Enabled = true;
                    HasFileOpen = true;
                }
            }
            else
            {
                FileNodeTree.Nodes.Clear();
                EntryNodeTree.Nodes.Clear();
                mf.OpenModel(files[0], FileNodeTree, EntryNodeTree);
                saveToolStripMenuItem.Enabled = true;
                HasFileOpen = true;
            }
            try
            {
                FileNodeTree.SelectedNode = FileNodeTree.Nodes[0];
            }
            catch
            {

            }
        }

        private void FileNodeTree_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }
    }
}
