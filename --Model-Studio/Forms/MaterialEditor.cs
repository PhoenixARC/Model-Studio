using System;
using System.IO;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialWorker;
using MaterialWorker.model;

namespace __Model_Studio.Forms
{
    public partial class MaterialEditor : Form
    {
        public MaterialEditor()
        {
            InitializeComponent();
        }

        #region Variables


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

        List<string> materialNames = new List<string> 
        {
            "entity_multitexture_alpha_test",
            "entity_change_color",
            "entity_emissive_alpha",
            "entity_alphatest_change_color",
            "entity_emissive_alpha_only",
            "entity_alphatest",
        };

        List<string> materialDescs = new List<string>
        {
            "* Transparency on second layer",
            "* Completely Opaque",
            "* Emissive\n* Alpha Channel\n* Transparency",
            "* Transparency\n* Translucency as Opaque",
            "* it is unknown what this does",
            "* Transparency\n* Translucency as Transparency"
        };

        MaterialContainer MCon = new MaterialContainer();
        MaterialParser MPar = new MaterialParser();
        MaterialBuilder MBui = new MaterialBuilder();

        int LastIndex;

        #endregion

        #region Form Actions

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = materialDescs[comboBox1.SelectedIndex];
            if (treeView1.SelectedNode != treeView1.Nodes[0] && treeView1.SelectedNode.Index == LastIndex)
            {
                EditMaterial(treeView1.SelectedNode.Index, comboBox1.SelectedIndex);
                LastIndex = treeView1.SelectedNode.Index;
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Open();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void MaterialEditor_Load(object sender, EventArgs e)
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
            treeView1.ImageList = imageList;

            #endregion
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode != treeView1.Nodes[0])
            {
                int Combo = GetMNameIndex(MCon.materials[treeView1.SelectedNode.Index].MaterialType);
                comboBox1.Enabled = true;
                comboBox1.SelectedItem = comboBox1.Items[Combo];
                LastIndex = treeView1.SelectedNode.Index;
            }
            else
            {
                comboBox1.Enabled = false;
            }
        }

        #endregion

        #region Functions

        public void Open()
        {
            treeView1.Nodes.Clear();
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Entity Materials|*.bin";
            if(opf.ShowDialog() == DialogResult.OK)
            {
                MCon = MPar.Parse(opf.FileName);

                TreeNode TN1 = new TreeNode();
                TN1.Text = "EntityMaterials";
                TN1.ImageIndex = GetIndex("package");
                TN1.SelectedImageIndex = GetIndex("package");
                treeView1.Nodes.Add(TN1);
                foreach(Material mat in MCon.materials)
                {
                    AddTreenode(mat.MaterialName);
                }
            }
        }

        public void Save()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Entity Materials|*.bin";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                MBui.Build(MCon, sfd.FileName);

                foreach(TreeNode tn in treeView1.Nodes[0].Nodes)
                {
                    tn.Text = tn.Text.Replace("*","");
                }
            }
        }
        public int GetIndex(string nom)
        {
            return Array.IndexOf(IconSheetIndex, nom);
        }
        public int GetMNameIndex(string nom)
        {
            return Array.IndexOf(materialNames.ToArray(), nom);
        }

        public void AddTreenode(string NodeName)
        {
            TreeNode TN1 = new TreeNode();
            TN1.Text = NodeName;
            TN1.ImageIndex = GetIndex(NodeName);
            TN1.SelectedImageIndex = GetIndex(NodeName);
            treeView1.Nodes[0].Nodes.Add(TN1);
        }

        public void EditMaterial(int MaterialIndex, int Material)
        {
            MCon.materials[MaterialIndex].MaterialType = materialNames[Material];
            if(!treeView1.Nodes[0].Nodes[MaterialIndex].Text.Contains("*"))
                treeView1.Nodes[0].Nodes[MaterialIndex].Text += "*";
        }

        #endregion
    }
}
