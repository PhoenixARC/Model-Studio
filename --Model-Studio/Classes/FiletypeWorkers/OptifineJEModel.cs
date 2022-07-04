using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsWorker.model;
using Newtonsoft.Json;

namespace __Model_Studio.Classes.FiletypeWorkers
{
    public class OptifineJEModel
    {

        public string ModelToJEM(ModelPiece mp)
        {
            List<JEModelPart> Parts = new List<JEModelPart>();
            foreach (KeyValuePair<string, ModelPart> mpart in mp.Parts)
            {
                JEModelPart ModelPart = new JEModelPart();
                ModelPart.part = mpart.Key;
                ModelPart.id = mpart.Key;
                List<JEBox> boxes = new List<JEBox>();
                foreach (KeyValuePair<string, ModelBox> mbox in mpart.Value.Boxes)
                {
                    JEBox Box = new JEBox();

                    Box.coordinates[0] = mbox.Value.PositionX + mpart.Value.TranslationX;
                    Box.coordinates[1] = mbox.Value.PositionY + mpart.Value.TranslationY - 24;
                    Box.coordinates[2] = mbox.Value.PositionZ + mpart.Value.TranslationZ;
                    Box.coordinates[3] = mbox.Value.Length;
                    Box.coordinates[4] = mbox.Value.Height;
                    Box.coordinates[5] = mbox.Value.Width;
                    Box.textureOffset[0] = Convert.ToInt32(mbox.Value.UvX);
                    Box.textureOffset[1] = Convert.ToInt32(mbox.Value.UvY);
                    boxes.Add(Box);
                }
                ModelPart.translate[0] = 0;
                ModelPart.translate[1] = 0;
                ModelPart.translate[2] = 0;
                ModelPart.boxes = boxes.ToArray();
                Parts.Add(ModelPart);
            }
            JEM WholeJEM = new JEM();
            WholeJEM.textureSize[0] = mp.TextureWidth;
            WholeJEM.textureSize[1] = mp.TextureHeight;
            WholeJEM.models = Parts.ToArray();
            string JSONDATA = JsonConvert.SerializeObject(WholeJEM, Formatting.Indented);
            return JSONDATA;
        }

    }

    internal class JEM
    {
        public int[] textureSize = new int[2];
        public JEModelPart[] models;
    }
    internal class JEModelPart
    {
        public string part = "";
        public string id = "";
        public string invertAxis = "xy";
        public float[] translate = new float[3];
        public JEBox[] boxes;
    }

    internal class JEBox
    {
        public float[] coordinates = new float[6];
        public int[] textureOffset = new int[2];
    }

}


