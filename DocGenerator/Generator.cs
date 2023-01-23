using CustomAttributes;
using System.Dynamic;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DocGenerator
{
    internal class Generator
    {
        public static void GetDocs()
        {
            //Implementation.GenerateDocuments();
            var docs = JsonDeserialization.ReadAsJsonFormat<dynamic>("Document.json");
           Console.WriteLine(docs);
           // var txt = File.ReadAllText("Documentation.txt");
           // Console.WriteLine(txt.ToString());
    }       
    }
    public static class JsonDeserialization
    {
        public static T ReadAsJsonFormat<T>(string fileName) =>
            JsonSerializer.Deserialize<T>(File.ReadAllText(fileName));
    }
}
