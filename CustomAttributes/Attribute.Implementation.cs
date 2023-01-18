using System;
using System.Linq;
using System.Reflection;

namespace CustomAttributes
{
    public delegate void DocumentDelegate(Type type);
    public static class Implementation
    {
        
        public static void GetDocs(Type type)
        {
            DocumentDelegate documentDelegate = new DocumentDelegate(DisplayClasses);
            documentDelegate += DisplayMethods;
            documentDelegate += DisplayProperties;

            documentDelegate.Invoke(type);

        }



        public static void DisplayClasses(Type type)
        {

            Console.WriteLine($"Assembly: {Assembly.GetExecutingAssembly().FullName}");


            Console.WriteLine("\nClass: \n\n{0}", type.Name);

            Attribute[] attributes = type.GetCustomAttributes().ToArray();

            foreach (Attribute attribute in attributes)
            {
                switch (attribute)
                {
                    case DocumentAttribute docAttribute:
                         Console.WriteLine($"\nDescription:\n\t{docAttribute.Description}");
                        break;
                }
            }





        }
        public static void DisplayMethods(Type type)
        {
            Console.WriteLine("\nMethods:\n");
            MethodInfo[] methods = type.GetMethods();


            for (int i = 0; i < methods.GetLength(0); i++)
            {
                object[] methAttr = methods[i].GetCustomAttributes(true);

                foreach (Attribute item in methAttr)
                {
                    switch (item)
                    {
                        case DocumentAttribute docAttribute:
                            Console.WriteLine($"{methods[i].Name}\nDescription:\n\t{docAttribute.Description}\nInput:\n\t{docAttribute.Input}\n{docAttribute.Output}\n");
                            break;
                    }
                }
            }
        }

        public static void DisplayProperties(Type type)
        {
            Console.WriteLine("\n\nProperties: ");
            Console.WriteLine();

            PropertyInfo[] properties = type.GetProperties();

            for (int i = 0; i < properties.GetLength(0); i++)
            {
                object[] propAttr = properties[i].GetCustomAttributes(true);

                foreach (Attribute item in propAttr)
                {

                    switch (item)
                    {
                        case DocumentAttribute docAttribute:
                            Console.WriteLine($"{properties[i].Name}\nDescription:\n\t{docAttribute.Description}\nInput:\n\t{docAttribute.Input}\n{docAttribute.Output}\n");
                            break;
                    }
                }
            }
        }
        public static void GetDocs()
        {

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var members = from assembly in assemblies
                          from type in assembly.GetTypes()
                          from member in type.GetMembers()
                          select member;

            foreach (var member in members)
            {
                
                    var attributes = member.GetCustomAttributes(typeof(DocumentAttribute), true);

                    if (attributes.Length > 0)
                    {
                        var doc = (DocumentAttribute)attributes[0];
                        Console.WriteLine($"Member Type: {member.MemberType}");
                        Console.WriteLine($"Name: {member.Name}");
                        Console.WriteLine($"\nDescription: {doc.Description}\nInput: {doc.Input}\n Output: {doc.Output}");

                    }
                
            }
        }
    }
}



