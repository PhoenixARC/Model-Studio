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
            int OSet = 0;
            FileStream fs = new FileStream(FilePath, FileMode.Create, FileAccess.Write);
            fs.Write(new byte[] { 0x00, 0x00, 0x00, 0x01 }, OSet, 1);
            OSet += 3;
            fs.Write(BitConverter.GetBytes(Mc.models.Count), OSet, 1);
            OSet += 3; /*
            foreach (Material mat in Mc.materials)
            {
                fs.Write(BitConverter.GetBytes((Int16)(mat.MaterialName.Length)), OSet, 1);
                OSet += 1;
                fs.Write(Encoding.Default.GetBytes(mat.MaterialName), OSet, 1);
                OSet += mat.MaterialName.Length - 1;
                fs.Write(BitConverter.GetBytes((Int16)(mat.MaterialType.Length)), OSet, 1);
                OSet += 1;
                fs.Write(Encoding.Default.GetBytes(mat.MaterialType), OSet, 1);
                OSet += mat.MaterialType.Length - 1;
            }*/
        }
    }
}
