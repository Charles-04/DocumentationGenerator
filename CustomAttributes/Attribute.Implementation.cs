using System;
using System.Reflection;
using System.Linq;
using CustomAttributes;
using System.ComponentModel;

namespace CustomAttributes
{
    public static class Implementation
    {
       

           public static void GetDocs(Type t)
            {
                
                Console.WriteLine("Assembly: {0}", Assembly.GetExecutingAssembly().FullName);


                Console.WriteLine("\nClass: \n\n{0}", t.Name);
               
                Attribute[] attributes = t.GetCustomAttributes().ToArray();

                foreach (Attribute attribute in attributes)
                {
                    switch (attribute)
                    {
                        case DocumentAttribute docAttribute:
                            DocumentAttribute doc = (DocumentAttribute)docAttribute;
                            Console.WriteLine($"\nDescription:\n\t{doc.Description}");
                            break;
                    }
                }

               
            }
            public static void GetDocs()
            {
                
                var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                foreach (var assembly in assemblies)
                {
                    var types = assembly.GetTypes();
                    foreach (var type in types)
                    {
                        var members = type.GetMembers();
                        foreach (var member in members)
                        {
                            var attributes = member.GetCustomAttributes(typeof(DocumentAttribute), false);
                            
                            if (attributes.Length > 0)
                            {
                            var doc = (DocumentAttribute)attributes[0];
                            Console.WriteLine($"Member Type: {member.MemberType}" );
                                Console.WriteLine($"Name: {member.Name}" );
                                Console.WriteLine($"\nDescription: {(doc).Description}\nInput: {(doc).Input}\n Output: {(doc).Output}");
                               
                            }
                        }
                    }
                }
            }
        }
    }
    

