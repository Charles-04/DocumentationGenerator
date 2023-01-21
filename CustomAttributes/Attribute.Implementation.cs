using System;
using System.IO;
using System.Linq;


namespace CustomAttributes
{

    public static class Implementation
    {


        public static void GetDocs()
        {

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var members = from assembly in assemblies
                          from type in assembly.GetTypes()
                          from member in type.GetMembers()
                          select member;


           
            using (StreamWriter writer = File.CreateText(@"Documentation.txt"))
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

                Console.WriteLine("Documentation created at the project bin folder");
            }


        }
    }
    }




