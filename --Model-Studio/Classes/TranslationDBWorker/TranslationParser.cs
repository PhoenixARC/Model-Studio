using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranslationDBWorker.model;

namespace TranslationDBWorker
{
    public class TranslationParser
    {
        private ArraySupport ArrSupport;

        public TranslationParser()
        {
            ArrSupport = new ArraySupport();
        }
        public TranslationContainer Parse(byte[] data)
        {
            TranslationContainer tc = new TranslationContainer();
            MemoryStream s = new MemoryStream(data);

            tc.Version = ArrSupport.GetInt32(s);
            int NumOfModels = ArrSupport.GetInt16(s);
            for(int i = 0; i < NumOfModels; i++)
            {
                Model m = new Model();
                string name = ArrSupport.GetString(s);
                int NumOfParts = ArrSupport.GetInt16(s);
                for (int y = 0; y < NumOfParts; y++)
                {
                    Part p = new Part();
                    string partname = ArrSupport.GetString(s);
                    p.Translation[0] = ArrSupport.Getfloat(s);
                    p.Translation[1] = ArrSupport.Getfloat(s);
                    p.Translation[2] = ArrSupport.Getfloat(s);
                    m.Translations.Add(partname, p);
                }
                tc.Models.Add(name, m);
            }
            s.Close();
            s.Dispose();
            return tc;
        }
    }
}
