using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslationDBWorker.model
{
    public class TranslationContainer
    {
        public int Version;
        public Dictionary<string, Model> Models = new Dictionary<string, Model>();
    }

    public class Model
    {
        public Dictionary<string, Part> Translations = new Dictionary<string, Part>();
    }

    public class Part
    {
        public float[] Translation = new float[3];
    }
}
