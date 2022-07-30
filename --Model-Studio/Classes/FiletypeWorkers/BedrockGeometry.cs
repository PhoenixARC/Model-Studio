using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsWorker.model;
using Newtonsoft.Json;
using TranslationDBWorker.model;
using TranslationDBWorker;

namespace __Model_Studio.Classes.FiletypeWorkers
{
    public class BedrockJSONtoCSM
    {

        public string[,] ModelnameArray =
        {
            {"bat.geo", "bat"},
            {"bed.geo", "bed"},
            {"blaze.geo", "blaze"},
            {"boat.geo", "boat"},
            {"cat.geo", "cat"},
            {"chicken.geo", "chicken"},
            {"cod.geo", "cod"},
            {"creeper.geo", "creeper"},
            {"creeper_head.geo", "creeper_head"},
            {"dolphin.geo", "dolphin"},
            {"dragon.geo", "dragon"},
            {"dragon_head.geo", "dragon_head"},
            {"zombie.drowned.geo", "zombie.drowned"},
            {"enderman.geo", "enderman"},
            {"endermite.geo", "endermite"},
            {"evoker.geo", "evoker"},
            {"ghast.geo", "ghast"},
            {"guardian.geo", "guardian"},
            {"horse.v2.geo", "horse.v2"},
            {"irongolem.geo", "irongolem"},
            {"lavaslime.geo", "lavaslime"},
            {"llama.geo", "llama"},
            {"minecart.geo", "minecart"},
            {"ocelot.geo", "ocelot"},
            {"panda.geo", "panda"},
            {"parrot.geo", "parrot"},
            {"phantom.geo", "phantom"},
            {"pig.geo", "pig"},
            {"pigzombie.geo", "pigzombie"},
            {"polarbear.geo", "polarbear"},
            {"pufferfish.large.geo", "pufferfish.large"},
            {"pufferfish.mid.geo", "pufferfish.mid"},
            {"pufferfish.small.geo", "pufferfish.small"},
            {"rabbit.geo", "rabbit"},
            {"salmon.geo", "salmon"},
            {"turtle.geo", "turtle"},
            {"sheep.geo", "sheep"},
            {"sheep.sheared.geo", "sheep.sheared"},
            {"shulker.geo", "shulker"},
            {"silverfish.geo", "silverfish"},
            {"skeleton.geo", "skeleton"},
            {"skeleton_head.geo", "skeleton_head"},
            {"skeleton.stray.geo", "skeleton.stray"},
            {"skeleton.wither.geo", "skeleton.wither"},
            {"skeleton_wither_head.geo", "skeleton_wither_head"},
            {"slime.geo", "slime"},
            {"slime.armor.geo", "slime.armor"},
            {"snowgolem.geo", "snowgolem"},
            {"spider.geo", "spider"},
            {"squid.geo", "squid"},
            {"stray.armor.geo", "stray.armor"},
            {"trident.geo", "trident"},
            {"tropicalfish_a.geo", "tropicalfish_a"},
            {"tropicalfish_b.geo", "tropicalfish_b"},
            {"vex.geo", "vex"},
            {"villager.geo", "villager"},
            {"villager.witch.geo", "villager.witch"},
            {"vindicator.geo", "vindicator"},
            {"witherboss.geo", "witherBoss"},
            {"wolf.geo", "wolf"},
            {"zombie.geo", "zombie"},
            {"zombie_head.geo", "zombie_head"},
            {"zombie.husk.geo", "zombie.husk"},
            {"zombie.villager.geo", "zombie.villager"}
        };


        public string ModelToJSON(ModelPiece mp)
        {

            JObject jobj = new JObject();
            List<JBone> bones = new List<JBone>();
            foreach (KeyValuePair<string, ModelPart> mpart in mp.Parts)
            {
                JBone jb = new JBone();
                jb.name = mpart.Key;
                List<JCube> cubes = new List<JCube>();
                foreach (KeyValuePair<string, ModelBox> mbox in mpart.Value.Boxes)
                {
                    JCube NewCube = new JCube();

                    NewCube.origin[0] = mbox.Value.PositionX + mpart.Value.TranslationX;
                    NewCube.origin[1] = mbox.Value.PositionY + mpart.Value.TranslationY - 24;
                    NewCube.origin[2] = mbox.Value.PositionZ + mpart.Value.TranslationZ;
                    NewCube.size[0] = mbox.Value.Length;
                    NewCube.size[1] = mbox.Value.Height;
                    NewCube.size[2] = mbox.Value.Width;
                    NewCube.uv[0] = mbox.Value.UvX;
                    NewCube.uv[1] = mbox.Value.UvY;
                    NewCube.inflate = mbox.Value.Scale;
                    NewCube.mirror = mbox.Value.Mirror;
                    cubes.Add(NewCube);
                }
                jb.rotation[0] = mpart.Value.RotationX;
                jb.rotation[1] = mpart.Value.RotationY;
                jb.rotation[2] = mpart.Value.RotationZ;
                jb.cubes = cubes.ToArray();
                bones.Add(jb);
            }
            jobj.bones = bones.ToArray();
            jobj.description.Add("identifier", "geometry.steve");
            jobj.description.Add("texture_width", mp.TextureWidth);
            jobj.description.Add("texture_height", mp.TextureHeight);
            jobj.description.Add("visible_bounds_width", 2);
            jobj.description.Add("visible_bounds_height", 3.5f);
            jobj.description.Add("visible_bounds_offset", new float[] { 0, 1.25f, 0 });
            WholeJSON WJ = new WholeJSON();
            WJ.entries.Add("format_version", "1.12.0");
            WJ.entries.Add("minecraft:geometry", new JObject[] { jobj });
            string JSONDATA = JsonConvert.SerializeObject(WJ.entries, Formatting.Indented);
            return JSONDATA;
        }

        public ModelPiece JSONToModel(string json, string Filename, ModelContainer mc)
        {

            TranslationParser TP = new TranslationParser();
            TranslationContainer TCon = TP.Parse(Properties.Resources.MobTranslations);
            ModelPiece mp = new ModelPiece();
            WholeJSON WJ = JsonConvert.DeserializeObject<WholeJSON>(json);
            object jsonDe = JsonConvert.DeserializeObject<OrderedDictionary>(json)[1];
            WJ.entries.Add("minecraft:geometry", jsonDe);
            JObject jobj = JsonConvert.DeserializeObject<JObject>(WJ.entries["minecraft:geometry"].ToString());
            try
            {
                mp.TextureWidth = Convert.ToInt32(jobj.texturewidth);
                mp.TextureHeight = Convert.ToInt32(jobj.textureheight);
            }
            catch (Exception)
            {
                Console.WriteLine("TEXTURE DIMENSIONS NOT PRESENT IN JSON");
            }
            string modelname = ModelnameArray[FindRow(Filename), 1];
            foreach (JBone bone in jobj.bones)
            {
                ModelPart mpart = new ModelPart();
                int i = 0;
                mpart.UnknownFloat = 0;
                mpart.TranslationX = TCon.Models[modelname].Translations[bone.name].Translation[0];
                mpart.TranslationY = TCon.Models[modelname].Translations[bone.name].Translation[1];
                mpart.TranslationZ = TCon.Models[modelname].Translations[bone.name].Translation[2];
                mpart.TextureOffsetX = 0;
                mpart.TextureOffsetY = 0;
                mpart.RotationX = bone.rotation[0];
                mpart.RotationY = bone.rotation[1];
                mpart.RotationZ = bone.rotation[2];
                foreach (JCube cube in bone.cubes)
                {
                    ModelBox mbox = new ModelBox();
                    mbox.PositionX = cube.origin[0] - TCon.Models[modelname].Translations[bone.name].Translation[0];
                    mbox.PositionY = cube.origin[1] + 24 - TCon.Models[modelname].Translations[bone.name].Translation[1];
                    mbox.PositionZ = cube.origin[2] - TCon.Models[modelname].Translations[bone.name].Translation[2];
                    mbox.Length = Convert.ToInt32(cube.size[0]);
                    mbox.Height = Convert.ToInt32(cube.size[1]);
                    mbox.Width= Convert.ToInt32(cube.size[2]);
                    mbox.UvX = cube.uv[0];
                    mbox.UvY = cube.uv[1];
                    mbox.Scale = cube.inflate;
                    mbox.Mirror = cube.mirror;
                    mpart.Boxes.Add(i.ToString(), mbox);
                    i++;
                }
                mp.Parts.Add(bone.name, mpart);
            }
            mc.models.Add(modelname, mp);
            return mp;
        }


        private int FindRow(string elem)
        {
            int rowCount = ModelnameArray.GetLength(0),
                colCount = ModelnameArray.GetLength(1);
            for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                for (int colIndex = 0; colIndex < colCount; colIndex++)
                {
                    if (ModelnameArray[rowIndex, colIndex] == elem)
                    {
                        return rowIndex;
                    }
                }
            }

            string NewModelname = "";
            Forms.UnidentifiedModel UM = new Forms.UnidentifiedModel();
            UM.ShowDialog();
            NewModelname = UM.ReturnValue1;
            int rowCount1 = ModelnameArray.GetLength(0),
                colCount1 = ModelnameArray.GetLength(1);
            for (int rowIndex = 0; rowIndex < rowCount1; rowIndex++)
            {
                for (int colIndex = 0; colIndex < colCount1; colIndex++)
                {
                    if (ModelnameArray[rowIndex, colIndex] == NewModelname)
                    {
                        return rowIndex;
                    }
                }
            }
            return -1;
        }
    }

    internal class WholeJSON
    {
        public string format_version = "1.12.0";
        public Dictionary<string, object> entries = new Dictionary<string, object>();
    }

    internal class JObject
    {
        public int texturewidth = 0;
        public int textureheight = 0;
        public Dictionary<string, object> description = new Dictionary<string, object>();
        public JBone[] bones = { };
    }
    internal class JBone
    {
        public string name = "";
        public string parent = "";
        public float[] pivot = { 0, 0, 0 };
        public float[] rotation = { 0, 0, 0 };
        public JCube[] cubes = { };
    }
    internal class JCube
    {
        public float[] origin = new float[3];
        public float[] size = new float[3];
        public float[] uv = new float[2];
        public float inflate = 0.0f;
        public bool mirror = false;
    }
}
