using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsWorker.model;

namespace ModelsWorker
{
    public class ModelBuilder
    {

        private ArraySupport ArrSupport;

        public ModelBuilder()
        {
            ArrSupport = new ArraySupport();
        }


        public void Build(ModelContainer Mc, string FilePath)
        {
            FileStream fs = new FileStream(FilePath, FileMode.Create, FileAccess.Write);
            
            ArrSupport.WriteIntToStream(1, fs);
            ArrSupport.WriteIntToStream(Mc.models.Count, fs);
            
            foreach (KeyValuePair<string, ModelPiece> Model in Mc.models)
            {
                ArrSupport.WriteStringToStream(Model.Key, fs);
                ArrSupport.WriteIntToStream(Model.Value.TextureHeight, fs);
                ArrSupport.WriteIntToStream(Model.Value.TextureWidth, fs);
                ArrSupport.WriteIntToStream(Model.Value.Parts.Count, fs);
                foreach (KeyValuePair<string, ModelPart> Part in Model.Value.Parts)
                {
                    ArrSupport.WriteStringToStream(Part.Key, fs);
                    ArrSupport.WriteFloatToStream(Part.Value.TranslationX, fs);
                    ArrSupport.WriteFloatToStream(Part.Value.TranslationY, fs);
                    ArrSupport.WriteFloatToStream(Part.Value.TranslationZ, fs);
                    ArrSupport.WriteFloatToStream(Part.Value.UnknownFloat, fs);
                    ArrSupport.WriteFloatToStream(Part.Value.TextureOffsetX, fs);
                    ArrSupport.WriteFloatToStream(Part.Value.TextureOffsetY, fs);
                    ArrSupport.WriteFloatToStream(Part.Value.RotationX, fs);
                    ArrSupport.WriteFloatToStream(Part.Value.RotationY, fs);
                    ArrSupport.WriteFloatToStream(Part.Value.RotationZ, fs);
                    ArrSupport.WriteIntToStream(Part.Value.Boxes.Count, fs);
                    foreach (KeyValuePair<string, ModelBox> box in Part.Value.Boxes)
                    {
                        ArrSupport.WriteFloatToStream(box.Value.PositionX, fs);
                        ArrSupport.WriteFloatToStream(box.Value.PositionY, fs);
                        ArrSupport.WriteFloatToStream(box.Value.PositionZ, fs);
                        ArrSupport.WriteIntToStream(box.Value.Length, fs);
                        ArrSupport.WriteIntToStream(box.Value.Height, fs);
                        ArrSupport.WriteIntToStream(box.Value.Width, fs);
                        ArrSupport.WriteFloatToStream(box.Value.UvX, fs);
                        ArrSupport.WriteFloatToStream(box.Value.UvY, fs);
                        ArrSupport.WriteFloatToStream(box.Value.Scale, fs);
                        ArrSupport.WriteBoolToStream(box.Value.Mirror, fs);

                    }
                }
            }
            fs.Flush();
            fs.Close();
            fs.Dispose();
        }
    }
}
