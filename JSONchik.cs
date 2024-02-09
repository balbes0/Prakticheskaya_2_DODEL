using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Prakticheskaya_2
{
    internal class JSONchik
    {
        public static void JsonSerialize<T>(T notebook)
        {
            string json = JsonConvert.SerializeObject(notebook);
            File.WriteAllText("C:\\Users\\erton\\Desktop\\Notebooks.json", json);
        }
        public static T JsonDeserialize<T>()
        {
            string json = File.ReadAllText("C:\\Users\\erton\\Desktop\\Notebooks.json");
            T notebookdeser = JsonConvert.DeserializeObject<T>(json);
            return notebookdeser;
        }
    }
}
