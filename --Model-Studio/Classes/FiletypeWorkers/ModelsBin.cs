using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace __Model_Studio
{
    static class ModelsBin
    {


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

        public class ModelFile
        {
            public byte[] data;
            TreeView tv;
            public string SaveLocation;

            public Header header;
            public List<ModelName> models = new List<ModelName>();


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

            public void OpenModel(string ModelFilePath, TreeView tv1, TreeView tv2)
            {
                header = new Header();
                tv = tv1;
                data = File.ReadAllBytes(ModelFilePath);

                if (!header.OpenFile(this, tv, tv2))
                {
                    MessageBox.Show("Error opening!");
                }
            }
            public void SaveModel(string ModelFilePath, TreeNode tv1)
            {

                List<byte> saveArray = new List<byte>();


                saveArray.AddRange(new byte[] {0x00, 0x00, 0x00, 0x01});
                saveArray.AddRange(BitConverter.GetBytes(tv1.Nodes.Count).Reverse().ToArray());

                foreach(TreeNode tn in tv1.Nodes)
                {
                    //Int16 num = BitConverter.ToInt16(BitConverter.GetBytes(tn.Text.Length), 0);
                    //saveArray.AddRange(BitConverter.GetBytes(num).Reverse().ToArray());
                    //saveArray.AddRange(Encoding.Default.GetBytes(tn.Text));
                    
                    saveArray.AddRange(StringToByteArrayFastest(tn.Tag.ToString().Replace("-", "")));
                    int num = BitConverter.ToInt32(BitConverter.GetBytes(tn.Nodes.Count), 0);
                    saveArray.AddRange(BitConverter.GetBytes(num).Reverse().ToArray());
                    foreach (TreeNode tn1 in tn.Nodes)
                    {
                        saveArray.AddRange(StringToByteArrayFastest(tn1.Tag.ToString().Replace("-", "")));
                        int num1 = BitConverter.ToInt32(BitConverter.GetBytes(tn1.Nodes.Count), 0);

                        foreach (TreeNode tn2 in tn1.Nodes)
                        {
                            saveArray.AddRange(StringToByteArrayFastest(tn2.Tag.ToString().Replace("-", "")));
                            saveArray.AddRange(new byte[] { 0x00 });

                        }
                    }

                }

                File.WriteAllBytes(ModelFilePath, saveArray.ToArray());
            }
        }

        public static int GetNodeparents(TreeNode tn)
        {
            Console.WriteLine("NodeLevel - " + tn.FullPath.Split(new[] { "\\" }, StringSplitOptions.None).Length);
            return tn.FullPath.Split(new[] { "\\" }, StringSplitOptions.None).Length;
        }

        private static int GetIndex(string nom)
        {
            //Console.WriteLine(nom + " -- " + Array.IndexOf(IconSheetIndex, nom));
            return Array.IndexOf(IconSheetIndex, nom);
        }

        #region Conversions

        static uint ConvertByteToUint32(byte[] array)
        {
            byte[] dat = array;
            Array.Reverse(dat);
            uint floatArr = BitConverter.ToUInt32(dat, 0);
            return floatArr;
        }

        static short ConvertByteToInt16(byte[] array)
        {
            byte[] dat = array;
            Array.Reverse(dat);
            short floatArr = BitConverter.ToInt16(dat, 0);
            return floatArr;
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

        static byte[] ConvertInt16ToByte(int i)
        {

            byte[] arr = { 00, StringToByteArrayFastest(i.ToString())[0] };
            return arr;
        }

        static float ConvertByteToFloat(byte[] array, bool ToBigEndian)
        {
            byte[] dat = array;
            if (ToBigEndian)
                Array.Reverse(dat);
            if (BitConverter.ToSingle(dat, 0).ToString().Contains("E"))
                Array.Reverse(dat);
            float floatArr = BitConverter.ToSingle(dat, 0);
            //Console.WriteLine(BitConverter.ToString(dat) + " - " + floatArr.ToString());
            return floatArr;
            //return ReadSingle(dat, 0, true);
        }

        static int FloatBytestoInt(byte[] array, bool ToBigEndian)
        {
            //byte[] dat = new byte[4];
            //dat[0] = array[1];
            byte[] dat = array;
            if (!ToBigEndian)
            {
                Array.Reverse(dat);
            }
            float bytes = ConvertByteToFloat(array, ToBigEndian);
            int OutInt = (int)Math.Round(bytes);
            return OutInt;
        }

        #endregion

        #region ModelDataTypes

        public class Header // 0x08 size
        {
            public byte[] FileVer; // 0x04 bytes
            public byte[] AmtOfModels; // 0x04 bytes

            public bool OpenFile(ModelFile mf, TreeView tv, TreeView tvX)
            {
                try
                {
                    byte[] data = mf.data;

                    mf.header.FileVer = data.Skip(0).Take(4).Reverse().ToArray();
                    mf.header.AmtOfModels = data.Skip(4).Take(4).Reverse().ToArray();

                    TreeNode tn = new TreeNode("Models.bin (" + BitConverter.ToInt32(mf.header.AmtOfModels, 0) + " Models)[V" + BitConverter.ToInt32(mf.header.FileVer, 0) + "]");

                    int offset = 8;

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
                    tv.ImageList = imageList;
                    tvX.ImageList = imageList;

                    #endregion

                    int ImageID = GetIndex("package");

                    tn.ImageIndex = ImageID;
                    tn.SelectedImageIndex = ImageID;



                    tn.Tag = "";
                    int LeftAT = 8;
                    int i = 0;
                    while (i < BitConverter.ToInt32(mf.header.AmtOfModels, 0))
                    {
                        TreeNode tn1 = new TreeNode();

                        int NameLength = BitConverter.ToInt16(data.Skip(offset).Take(0x02).Reverse().ToArray(), 0);
                        tn1.Text = Encoding.Default.GetString(data.Skip(offset + 0x02).Take(NameLength).ToArray());

                        tn1.ImageIndex = GetIndex(tn1.Text);
                        tn1.SelectedImageIndex = GetIndex(tn1.Text);
                        tn1.Tag = BitConverter.ToString(data.Skip(offset).Take(0x0A + NameLength).ToArray(), 0);
                        offset += tn1.Text.Length + 0x0E;

                        int y = 0;
                        int NumofModelParts = BitConverter.ToInt32(data.Skip(offset - 4).Take(0x04).Reverse().ToArray(), 0);
                        while (y < NumofModelParts)
                        {
                            Console.WriteLine(NumofModelParts);
                            TreeNode tn2 = new TreeNode();

                            int PartNameLength = BitConverter.ToInt16(data.Skip(offset).Take(0x02).Reverse().ToArray(), 0);
                            tn2.Text = Encoding.Default.GetString(data.Skip(offset + 0x02).Take(PartNameLength).ToArray());
                            tn2.Tag = BitConverter.ToString(data.Skip(offset).Take(0x2A + PartNameLength).ToArray(), 0);
                            offset += 0x2A + PartNameLength;
                            int x = 1;

                            int NumOfBoxes = BitConverter.ToInt32(data.Skip(offset - 4).Take(0x04).Reverse().ToArray(), 0);
                            if (NumOfBoxes > 300)
                                break;
                            while (x <= NumOfBoxes)
                            {
                                TreeNode tn3 = new TreeNode();
                                tn3.Text = "Box" + x;
                                string str = BitConverter.ToString(data.Skip(offset).Take(0x24).ToArray(), 0);
                                tn3.Tag = str;
                                offset += 0x25;

                                tn2.Nodes.Add(tn3);
                                x++;
                            }

                            tn1.Nodes.Add(tn2);

                            y++;
                        }


                        tn.Nodes.Add(tn1);
                        i++;
                    }

                    tv.Nodes.Add(tn);
                    tn.Expand();
                    return true;
                }
                catch (Exception err)
                {
                    Console.WriteLine(err.Message);
                    Console.WriteLine(err.StackTrace);
                    return false;
                }
                return true;
            }
        }

        public class ModelName // 0x0A + name size
        {
            public byte[] NameLength; // 0x02 bytes
            public byte[] Name;
            public byte[] TextureWidth; // 0x04 bytes
            public byte[] TextureHeight; // 0x04 bytes
            public byte[] AmtOfParts; // 0x04 bytes
            public List<ModelName> parts = new List<ModelName>();

            public void SetTreeData(byte[] data, TreeView EntryNodeView)
            {
                NameLength = data.Skip(0).Take(2).Reverse().ToArray();
                Name = data.Skip(2).Take(BitConverter.ToUInt16(NameLength, 0)).ToArray();
                TextureWidth = data.Skip(2 + BitConverter.ToInt16(NameLength, 0)).Take(4).ToArray();
                TextureHeight = data.Skip(6 + BitConverter.ToInt16(NameLength, 0)).Take(4).ToArray();

                EntryNodeView.Nodes.Clear();
                TreeNode tn = new TreeNode();
                tn.Text = "MODEL: [" + Encoding.Default.GetString(Name) + "]";

                tn.ImageIndex = GetIndex("package");
                tn.SelectedImageIndex = GetIndex("package");

                {
                    TreeNode tn1 = new TreeNode();
                    tn1.ImageIndex = GetIndex("text");
                    tn1.SelectedImageIndex = GetIndex("text");

                    string str = "Model Name: \"" + Encoding.Default.GetString(Name) + "\"";
                    tn1.Text = str;
                    tn1.Tag = BitConverter.ToString(Name);

                    tn.Nodes.Add(tn1);
                }
                {
                    TreeNode tn1 = new TreeNode();
                    tn1.ImageIndex = GetIndex("integer");
                    tn1.SelectedImageIndex = GetIndex("integer");

                    string str = "Texture Width: " + BitConverter.ToInt32(TextureWidth.Reverse().ToArray(), 0);
                    tn1.Text = str;
                    tn1.Tag = BitConverter.ToString(TextureWidth);

                    tn.Nodes.Add(tn1);
                }
                {
                    TreeNode tn1 = new TreeNode();
                    tn1.ImageIndex = GetIndex("integer");
                    tn1.SelectedImageIndex = GetIndex("integer");

                    string str = "Texture Height: " + BitConverter.ToInt32(TextureHeight.Reverse().ToArray(), 0);
                    tn1.Text = str;
                    tn1.Tag = BitConverter.ToString(TextureHeight);

                    tn.Nodes.Add(tn1);
                }

                EntryNodeView.Nodes.Add(tn);
                EntryNodeView.ExpandAll();
            }
        }

        public class ModelPart // 0x2A + name size
        {
            public byte[] partNameLength; // 0x02 bytes
            public byte[] partName;
            public byte[] blankBuffer; // 0x04 bytes
            public byte[] TranslationX; // 0x04 bytes
            public byte[] TranslationY; // 0x04 bytes
            public byte[] TranslationZ; // 0x04 bytes
            public byte[] TextureOffsetX; // 0x04 bytes
            public byte[] TextureOffsetY; // 0x04 bytes
            public byte[] RotationX; // 0x04 bytes
            public byte[] RotationY; // 0x04 bytes
            public byte[] RotationZ; // 0x04 bytes
            public byte[] AmtOfBoxes; // 0x04 bytes
            public List<ModelBox> Boxes = new List<ModelBox>();

            public void SetTreeData(byte[] data, TreeView EntryNodeView)
            {
                partNameLength = data.Skip(0).Take(2).Reverse().ToArray();
                partName = data.Skip(2).Take(BitConverter.ToUInt16(partNameLength, 0)).ToArray();
                blankBuffer = data.Skip(0 + (2 + BitConverter.ToUInt16(partNameLength, 0))).Take(4).Reverse().ToArray();
                TranslationX = data.Skip(4 + (2 + BitConverter.ToUInt16(partNameLength, 0))).Take(4).Reverse().ToArray();
                TranslationY = data.Skip(8 + (2 + BitConverter.ToUInt16(partNameLength, 0))).Take(4).Reverse().ToArray();
                TranslationZ = data.Skip(12 + (2 + BitConverter.ToUInt16(partNameLength, 0))).Take(4).Reverse().ToArray();
                TextureOffsetX = data.Skip(16 + (2 + BitConverter.ToUInt16(partNameLength, 0))).Take(4).Reverse().ToArray();
                TextureOffsetY = data.Skip(20 + (2 + BitConverter.ToUInt16(partNameLength, 0))).Take(4).Reverse().ToArray();
                RotationX = data.Skip(24 + (2 + BitConverter.ToUInt16(partNameLength, 0))).Take(4).Reverse().ToArray();
                RotationY = data.Skip(28 + (2 + BitConverter.ToUInt16(partNameLength, 0))).Take(4).Reverse().ToArray();
                RotationZ = data.Skip(32 + (2 + BitConverter.ToUInt16(partNameLength, 0))).Take(4).Reverse().ToArray();

                EntryNodeView.Nodes.Clear();
                TreeNode tn = new TreeNode();
                tn.Text = "PART: [" + Encoding.Default.GetString(partName) + "]";

                tn.ImageIndex = GetIndex("package");
                tn.SelectedImageIndex = GetIndex("package");

                {
                    TreeNode tn1 = new TreeNode();
                    tn1.ImageIndex = GetIndex("text");
                    tn1.SelectedImageIndex = GetIndex("text");

                    string str = "Part Name: \"" + Encoding.Default.GetString(partName) + "\"";
                    tn1.Text = str;
                    tn1.Tag = partName;

                    tn.Nodes.Add(tn1);
                }
                {
                    TreeNode tn1 = new TreeNode();
                    tn1.ImageIndex = GetIndex("float");
                    tn1.SelectedImageIndex = GetIndex("float");

                    string str = "Translation X: " + BitConverter.ToSingle(TranslationX, 0);
                    tn1.Text = str;
                    tn1.Tag = BitConverter.ToString(TranslationX);

                    tn.Nodes.Add(tn1);
                }
                {
                    TreeNode tn1 = new TreeNode();
                    tn1.ImageIndex = GetIndex("float");
                    tn1.SelectedImageIndex = GetIndex("float");

                    string str = "Translation Y: " + BitConverter.ToSingle(TranslationY, 0);
                    tn1.Text = str;
                    tn1.Tag = BitConverter.ToString(TranslationY);

                    tn.Nodes.Add(tn1);
                }
                {
                    TreeNode tn1 = new TreeNode();
                    tn1.ImageIndex = GetIndex("float");
                    tn1.SelectedImageIndex = GetIndex("float");

                    string str = "Translation Z: " + BitConverter.ToSingle(TranslationZ, 0);
                    tn1.Text = str;
                    tn1.Tag = BitConverter.ToString(TranslationZ);

                    tn.Nodes.Add(tn1);
                }
                {
                    TreeNode tn1 = new TreeNode();
                    tn1.ImageIndex = GetIndex("float");
                    tn1.SelectedImageIndex = GetIndex("float");

                    string str = "Texture Offset X: " + BitConverter.ToSingle(TextureOffsetX, 0);
                    tn1.Text = str;
                    tn1.Tag = BitConverter.ToString(TextureOffsetX);

                    tn.Nodes.Add(tn1);
                }
                {
                    TreeNode tn1 = new TreeNode();
                    tn1.ImageIndex = GetIndex("float");
                    tn1.SelectedImageIndex = GetIndex("float");

                    string str = "Texture Offset Y: " + BitConverter.ToSingle(TextureOffsetY, 0);
                    tn1.Text = str;
                    tn1.Tag = BitConverter.ToString(TextureOffsetY);

                    tn.Nodes.Add(tn1);
                }
                {
                    TreeNode tn1 = new TreeNode();
                    tn1.ImageIndex = GetIndex("float");
                    tn1.SelectedImageIndex = GetIndex("float");

                    string str = "Rotation X: " + BitConverter.ToSingle(RotationX, 0);
                    tn1.Text = str;
                    tn1.Tag = BitConverter.ToString(RotationX);

                    tn.Nodes.Add(tn1);
                }
                {
                    TreeNode tn1 = new TreeNode();
                    tn1.ImageIndex = GetIndex("float");
                    tn1.SelectedImageIndex = GetIndex("float");

                    string str = "Rotation Y: " + BitConverter.ToSingle(RotationY, 0);
                    tn1.Text = str;
                    tn1.Tag = BitConverter.ToString(RotationY);

                    tn.Nodes.Add(tn1);
                }
                {
                    TreeNode tn1 = new TreeNode();
                    tn1.ImageIndex = GetIndex("float");
                    tn1.SelectedImageIndex = GetIndex("float");

                    string str = "Rotation Z: " + BitConverter.ToSingle(RotationZ, 0);
                    tn1.Text = str;
                    tn1.Tag = BitConverter.ToString(RotationZ);

                    tn.Nodes.Add(tn1);
                }


                EntryNodeView.Nodes.Add(tn);
                EntryNodeView.ExpandAll();
            }
        }

        public class ModelBox // 0x24 (+0x01) size
        {
            public byte[] PositionX; // 0x04 bytes
            public byte[] PositionY; // 0x04 bytes
            public byte[] PositionZ; // 0x04 bytes
            public byte[] Length; // 0x04 bytes
            public byte[] Height; // 0x04 bytes
            public byte[] Width; // 0x04 bytes
            public byte[] UvX; // 0x04 bytes
            public byte[] UvY; // 0x04 bytes
            public byte[] Scale; // 0x04 bytes
            public byte[] SpacerByte; // may or may not be empty, max size 0x01

            public void SetTreeData(byte[] data, TreeView EntryNodeView)
            {
                PositionX = data.Skip(0).Take(4).Reverse().ToArray();
                PositionY = data.Skip(4).Take(4).Reverse().ToArray();
                PositionZ = data.Skip(8).Take(4).Reverse().ToArray();
                Length = data.Skip(12).Take(4).Reverse().ToArray();
                Height = data.Skip(16).Take(4).Reverse().ToArray();
                Width = data.Skip(20).Take(4).Reverse().ToArray();
                UvX = data.Skip(24).Take(4).Reverse().ToArray();
                UvY = data.Skip(28).Take(4).Reverse().ToArray();
                Scale = data.Skip(32).Take(4).Reverse().ToArray();

                EntryNodeView.Nodes.Clear();
                TreeNode tn = new TreeNode();
                tn.Text = "BOX";

                tn.ImageIndex = GetIndex("package");
                tn.SelectedImageIndex= GetIndex("package");

                {
                    TreeNode tn1 = new TreeNode();
                    tn1.ImageIndex = GetIndex("float");
                    tn1.SelectedImageIndex= GetIndex("float");
                    string str = "Position X: " + BitConverter.ToSingle(PositionX, 0);
                    tn1.Text = str;
                    tn1.Tag = BitConverter.ToString(PositionX);
                    Console.WriteLine("PosX -- " + BitConverter.ToString(PositionX));
                    tn.Nodes.Add(tn1);
                }
                {
                    TreeNode tn1 = new TreeNode();
                    tn1.ImageIndex = GetIndex("float");
                    tn1.SelectedImageIndex= GetIndex("float");
                    string str = "Position Y: " + BitConverter.ToSingle(PositionY, 0);
                    tn1.Text = str;
                    tn1.Tag = BitConverter.ToString(PositionY);
                    tn.Nodes.Add(tn1);
                }
                {
                    TreeNode tn1 = new TreeNode();
                    tn1.ImageIndex = GetIndex("float");
                    tn1.SelectedImageIndex= GetIndex("float");
                    string str = "Position Z: " + BitConverter.ToSingle(PositionZ, 0);
                    tn1.Text = str;
                    tn1.Tag = BitConverter.ToString(PositionZ);
                    tn.Nodes.Add(tn1);
                }
                {
                    TreeNode tn1 = new TreeNode();
                    tn1.ImageIndex = GetIndex("integer");
                    tn1.SelectedImageIndex= GetIndex("integer");
                    string str = "Length: " + BitConverter.ToInt32(Length, 0);
                    tn1.Text = str;
                    tn1.Tag = BitConverter.ToString(Length);
                    tn.Nodes.Add(tn1);
                }
                {
                    TreeNode tn1 = new TreeNode();
                    tn1.ImageIndex = GetIndex("integer");
                    tn1.SelectedImageIndex= GetIndex("integer");
                    string str = "Height: " + BitConverter.ToInt32(Height, 0);
                    tn1.Text = str;
                    tn1.Tag = BitConverter.ToString(Height);
                    tn.Nodes.Add(tn1);
                }
                {
                    TreeNode tn1 = new TreeNode();
                    tn1.ImageIndex = GetIndex("integer");
                    tn1.SelectedImageIndex= GetIndex("integer");
                    string str = "Width: " + BitConverter.ToInt32(Width, 0);
                    tn1.Text = str;
                    tn1.Tag = BitConverter.ToString(Width);
                    tn.Nodes.Add(tn1);
                }
                {
                    TreeNode tn1 = new TreeNode();
                    tn1.ImageIndex = GetIndex("float");
                    tn1.SelectedImageIndex= GetIndex("float");
                    string str = "Uv X: " + BitConverter.ToSingle(UvX, 0);
                    tn1.Text = str;
                    tn1.Tag = BitConverter.ToString(UvX);
                    tn.Nodes.Add(tn1);
                }
                {
                    TreeNode tn1 = new TreeNode();
                    tn1.ImageIndex = GetIndex("float");
                    tn1.SelectedImageIndex= GetIndex("float");
                    string str = "Uv Y: " + BitConverter.ToSingle(UvX, 0);
                    tn1.Text = str;
                    tn1.Tag = BitConverter.ToString(UvY);
                    tn.Nodes.Add(tn1);
                }
                {
                    TreeNode tn1 = new TreeNode();
                    tn1.ImageIndex = GetIndex("float");
                    tn1.SelectedImageIndex= GetIndex("float");
                    string str = "Scale: " + BitConverter.ToSingle(Scale, 0);
                    tn1.Text = str;
                    tn1.Tag = BitConverter.ToString(Scale);
                    tn.Nodes.Add(tn1);
                }

                    EntryNodeView.Nodes.Add(tn);
                    EntryNodeView.ExpandAll();
            }
        }

        #endregion


    }


}
