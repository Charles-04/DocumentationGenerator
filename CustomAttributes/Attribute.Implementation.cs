using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections.Generic;
using System.Dynamic;

namespace CustomAttributes
{

    public static class Implementation
    {
       static List<dynamic> _jsonFormatDocs = new List<dynamic>();

        public  async static Task  GenerateTextDoc()
        {
            

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var members = from assembly in assemblies
                          from type in assembly.GetTypes()
                          from member in type.GetMembers()
                          select member;


           
           await using (StreamWriter writer = File.CreateText(@"Documentation.txt"))
            {
                
                foreach (var member in members)
                {

                    var attributes = member.GetCustomAttributes(typeof(DocumentAttribute), true);
                    var clasAttributes = member.DeclaringType.GetCustomAttributes(typeof(DocumentAttribute), true);
                    if (attributes.Length > 0)
                    {
                        var doc = (DocumentAttribute)attributes[0];
                        writer.WriteLine($"\nClass : {member.DeclaringType.Name}\n");
                        if (clasAttributes.Length > 0)
                        {
                            var classDoc = (DocumentAttribute)clasAttributes[0];
                            writer.WriteLine($"Description : {classDoc.Description}\n");
                        }

                        writer.WriteLine($"Member Type: {member.MemberType}");
                        writer.WriteLine($"Name: {member.Name}");

                        writer.WriteLine($"\nDescription: {doc.Description}\n");
                        if (doc.Input != null)
                        {
                            writer.WriteLine($"Input: {doc.Input}");
                        }
                        if (doc.Output != null)
                        {
                            writer.WriteLine($"\nOutput: {doc.Output}\n");
                        }

                    }

                }

                Console.WriteLine(@"Documentation created at the folder : C:\Users\Charles\source\repos\DocumentationGenerator\DocGenerator\bin\Debug\net6.0");

               
            }


        }

        public static void GenerateJson()
        {
           // await GenerateTextDoc();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var types = from _assembly in assemblies
                           from _types in _assembly.GetTypes()
                          select _types;

            foreach (var item in types)
            {
                var members = item.GetMembers();
                for (int i = 0; i < members.Length; i++)
                {
                    _jsonFormatDocs.Add(new ExpandoObject());

                    var attributes = members[i].GetCustomAttributes(typeof(DocumentAttribute), true);
                    var clasAttributes = members[i].DeclaringType.GetCustomAttributes(typeof(DocumentAttribute), true);
                    if (attributes.Length > 0)
                    {
                        var doc = (DocumentAttribute)attributes[0];
                        _jsonFormatDocs[i].ClassName = $"\nClass : {members[i].DeclaringType.Name}\n";

                        if (clasAttributes.Length > 0)
                        {
                            var classDoc = (DocumentAttribute)clasAttributes[0];
                            _jsonFormatDocs[i].Description = ($"Description : {classDoc.Description}\n");
                        }

                        _jsonFormatDocs[i].MemberType = ($"Member Type: {members[i].MemberType}");
                        _jsonFormatDocs[i].MemberName = ($"Name: {members[i].Name}");

                        _jsonFormatDocs[i].MemberDescription = ($"\nDescription: {doc.Description}\n");
                        if (doc.Input != null)
                        {
                            _jsonFormatDocs[i].Input = ($"Input: {doc.Input}");
                        }
                        if (doc.Output != null)
                        {
                            _jsonFormatDocs[i].Output = ($"\nOutput: {doc.Output}\n");
                        }

                    }

                }

               


            }

            JsonSerialization.SaveAsJsonFormat(_jsonFormatDocs, "Document.json");
            Console.WriteLine("Done");
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



