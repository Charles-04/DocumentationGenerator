using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CustomAttributes
{

    public static class Implementation
    {
        static List<dynamic> _jsonFormatDocs = new List<dynamic>();

       

        public static void GenerateDocuments()
        {
            // await GenerateTextDoc();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var types = from _assembly in assemblies
                        from _types in _assembly.GetTypes()
                        select _types;
            using (StreamWriter writer = File.CreateText(@"Documentation.txt"))
            {
                foreach (var item in types)
                {
                    var members = item.GetMembers();
                    for (int i = 0; i < members.Length; i++)
                    {



                        var attributes = members[i].GetCustomAttributes(typeof(DocumentAttribute), true);
                        var clasAttributes = members[i].DeclaringType.GetCustomAttributes(typeof(DocumentAttribute), true);

                       



                        if (attributes.Length > 0 && members[i] != null)
                        {
                            _jsonFormatDocs.Add(new ExpandoObject());
                            var lastIndex = _jsonFormatDocs.Count() - 1;
                            
                            var doc = (DocumentAttribute)attributes[0];
                            _jsonFormatDocs[lastIndex].ClassName = $"\nClass : {members[i].DeclaringType.Name}\n";
                            writer.WriteLine($"\nClass : {members[i].DeclaringType.Name}\n");

                            if (clasAttributes.Length > 0)
                            {
                                var classDoc = (DocumentAttribute)clasAttributes[0];
                                _jsonFormatDocs[lastIndex].Description = ($"Description : {classDoc.Description}\n");
                                writer.WriteLine($"Description : {classDoc.Description}\n");
                            }

                            _jsonFormatDocs[lastIndex].MemberType = ($"Member Type: {members[i].MemberType}");
                            writer.WriteLine($"Member Type: {members[i].MemberType}");

                            _jsonFormatDocs[lastIndex].MemberName = ($"Name: {members[i].Name}");
                            writer.WriteLine($"Name: {members[i].Name}");

                            _jsonFormatDocs[lastIndex].MemberDescription = ($" {doc.Description}");
                            writer.WriteLine($" {doc.Description}");
                            if (doc.Input != null)
                            {
                                _jsonFormatDocs[lastIndex].Input = ($"Input: {doc.Input}");
                                writer.WriteLine($"Input: {doc.Input}");
                            }
                            if (doc.Output != null)
                            {
                                _jsonFormatDocs[lastIndex].Output = ($"{doc.Output}");
                                writer.WriteLine($"\nOutput: {doc.Output}\n");
                            }

                        }

                    }
                }

               
            }

            /*var jsonDoc = new List<dynamic>();
            for (int i = 0; i < _jsonFormatDocs.Count(); i++)
            {
                if (!((IDictionary<String, Object>)_jsonFormatDocs[i]).ContainsKey("Class"))
                {
                    _jsonFormatDocs.Remove(_jsonFormatDocs[i]);
                }
                else
                {
                    jsonDoc.Add(_jsonFormatDocs[i]);
                }
            }*/
            JsonSerialization.SaveAsJsonFormat(_jsonFormatDocs, "Document.json");
            Console.WriteLine("Documentation created in Json and txt format");


        }
    }
}
public static class JsonSerialization
{
    public static void SaveAsJsonFormat<T>(T objGraph, string fileName)
    {
        var options = new JsonSerializerOptions
        {
            IncludeFields = true,
            WriteIndented = true
        };
        File.WriteAllText(fileName, JsonSerializer.Serialize(objGraph, options));
    }
}



