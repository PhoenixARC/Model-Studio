using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaterialWorker.model;

namespace MaterialWorker
{
    public class MaterialBuilder
    {
        private ArraySupport ArrSupport;
        public MaterialBuilder()
        {
            ArrSupport = new ArraySupport();
        }
        public void Build(MaterialContainer Mc, string FilePath)
        {
            int OSet = 0;
            FileStream fs = new FileStream(FilePath, FileMode.Create, FileAccess.Write);
            ArrSupport.WriteIntToStream(Mc.Version, fs);
            ArrSupport.WriteIntToStream(Mc.materials.Count, fs);
            foreach (Material mat in Mc.materials)
            {
                ArrSupport.WriteStringToStream(mat.MaterialName, fs);
                ArrSupport.WriteStringToStream(mat.MaterialType, fs);

            }
            fs.Close();
            fs.Dispose();
        }
    }
}
