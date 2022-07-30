using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranslationDBWorker.model;

namespace TranslationDBWorker
{
    public class TranslationBuilder
    {
        private ArraySupport ArrSupport;

        public TranslationBuilder()
        {
            ArrSupport = new ArraySupport();
        }

        public void Build(TranslationContainer tc, string FilePath)
        {
            FileStream s = new FileStream(FilePath, FileMode.CreateNew);

            ArrSupport.WriteIntToStream(tc.Version, s);
            ArrSupport.WriteInt16ToStream((Int16)tc.Models.Count, s);
            foreach(KeyValuePair<string, Model> pair in tc.Models)
            {
                ArrSupport.WriteStringToStream(pair.Key, s);
                ArrSupport.WriteInt16ToStream((Int16)pair.Value.Translations.Count, s);
                foreach (KeyValuePair<string, Part> part in pair.Value.Translations)
                {
                    ArrSupport.WriteStringToStream(part.Key, s);
                    ArrSupport.WriteFloatToStream(part.Value.Translation[0], s);
                    ArrSupport.WriteFloatToStream(part.Value.Translation[1], s);
                    ArrSupport.WriteFloatToStream(part.Value.Translation[2], s);

                }
            }
            s.Close();
            s.Dispose();
        }
    }
}
