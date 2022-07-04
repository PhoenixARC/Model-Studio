using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsWorker.model;
using Newtonsoft.Json;

namespace __Model_Studio.Classes.FiletypeWorkers
{
    public class BedrockJSONtoCSM
    {
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
    }

    internal class WholeJSON
    {
        public string format_version = "1.12.0";
        public Dictionary<string, object> entries = new Dictionary<string, object>();
    }

    internal class JObject
    {
        public Dictionary<string, object> description = new Dictionary<string, object>();
        public JBone[] bones = { };
    }
    internal class JBone
    {
        public string name = "";
        public int[] pivot = { 0, 0, 0 };
        public float[] rotation = { 0, 0, 0 };
        public JCube[] cubes = { };
    }
    internal class JCube
    {
        public float[] origin = new float[3];
        public float[] size = new float[3];
        public float[] uv = new float[2];
    }
}
