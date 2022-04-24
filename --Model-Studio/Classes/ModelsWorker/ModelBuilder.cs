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

        public void Build(ModelContainer Mc, string FilePath)
        {
            FileStream fs = new FileStream(FilePath, FileMode.Create, FileAccess.Write);
            
            fs.Write(new byte[] { 0x00, 0x00, 0x00, 0x01 }, 0, 4);
            
            fs.Write(BitConverter.GetBytes(Mc.models.Count).Reverse().ToArray(), 0, 4);
            
            foreach (KeyValuePair<string, ModelPiece> Model in Mc.models)
            {
                fs.Write(BitConverter.GetBytes(Int16.Parse(Model.Key.Length.ToString())).Reverse().ToArray(), 0, 2);
                fs.Write(Encoding.Default.GetBytes(Model.Key), 0, Model.Key.Length);
                fs.Write(BitConverter.GetBytes((Model.Value.TextureHeight)).Reverse().ToArray(), 0, 4);
                fs.Write(BitConverter.GetBytes((Model.Value.TextureWidth)).Reverse().ToArray(), 0, 4);
                fs.Write(BitConverter.GetBytes((Model.Value.Parts.Count)).Reverse().ToArray(), 0, 4);
                foreach (KeyValuePair<string, ModelPart> Part in Model.Value.Parts)
                {
                    fs.Write(BitConverter.GetBytes(Int16.Parse(Part.Key.Length.ToString())).Reverse().ToArray(), 0, 2);
                    fs.Write(Encoding.Default.GetBytes(Part.Key), 0, Part.Key.Length);
                    fs.Write(new byte[] { 0x00, 0x00, 0x00, 0x00 }, 0, 4);
                    fs.Write(BitConverter.GetBytes((Part.Value.TranslationX)).Reverse().ToArray(), 0, 4);
                    fs.Write(BitConverter.GetBytes((Part.Value.TranslationY)).Reverse().ToArray(), 0, 4);
                    fs.Write(BitConverter.GetBytes((Part.Value.TranslationZ)).Reverse().ToArray(), 0, 4);
                    fs.Write(BitConverter.GetBytes((Part.Value.TextureOffsetX)).Reverse().ToArray(), 0, 4);
                    fs.Write(BitConverter.GetBytes((Part.Value.TextureOffsetY)).Reverse().ToArray(), 0, 4);
                    fs.Write(BitConverter.GetBytes((Part.Value.RotationX)).Reverse().ToArray(), 0, 4);
                    fs.Write(BitConverter.GetBytes((Part.Value.RotationY)).Reverse().ToArray(), 0, 4);
                    fs.Write(BitConverter.GetBytes((Part.Value.RotationZ)).Reverse().ToArray(), 0, 4);
                    fs.Write(BitConverter.GetBytes((Part.Value.Boxes.Count)).Reverse().ToArray(), 0, 4);
                    foreach (KeyValuePair<string, ModelBox> box in Part.Value.Boxes)
                    {
                        fs.Write(BitConverter.GetBytes((box.Value.PositionX)).Reverse().ToArray(), 0, 4);
                        fs.Write(BitConverter.GetBytes((box.Value.PositionY)).Reverse().ToArray(), 0, 4);
                        fs.Write(BitConverter.GetBytes((box.Value.PositionZ)).Reverse().ToArray(), 0, 4);
                        fs.Write(BitConverter.GetBytes((box.Value.Length)).Reverse().ToArray(), 0, 4);
                        fs.Write(BitConverter.GetBytes((box.Value.Height)).Reverse().ToArray(), 0, 4);
                        fs.Write(BitConverter.GetBytes((box.Value.Width)).Reverse().ToArray(), 0, 4);
                        fs.Write(BitConverter.GetBytes((box.Value.UvX)).Reverse().ToArray(), 0, 4);
                        fs.Write(BitConverter.GetBytes((box.Value.UvY)).Reverse().ToArray(), 0, 4);
                        fs.Write(BitConverter.GetBytes((box.Value.Scale)).Reverse().ToArray(), 0, 4);
                        fs.Write(new byte[] {0x00}, 0, 1);
                        
                    }
                }
            }
            fs.Flush();
            fs.Close();
            fs.Dispose();
        }
    }
}
