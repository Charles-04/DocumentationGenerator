using System;
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


                

                foreach (var member in members)
                {

                    var attributes = member.GetCustomAttributes(typeof(DocumentAttribute), true);
                    var clasAttributes = member.DeclaringType.GetCustomAttributes(typeof(DocumentAttribute), true);
                    if (attributes.Length > 0)
                    {
                        var doc = (DocumentAttribute)attributes[0];
                        Console.WriteLine($"\nClass : {member.DeclaringType.Name}\n");
                    if (clasAttributes.Length > 0)
                    {
                        var classDoc = (DocumentAttribute)clasAttributes[0];
                        Console.WriteLine($"Description : {classDoc.Description}\n");
                    }
                       
                        Console.WriteLine($"Member Type: {member.MemberType}");
                        Console.WriteLine($"Name: {member.Name}");

                        Console.WriteLine($"\nDescription: {doc.Description}\n");
                    if ( doc.Input != null)
                    {
                        Console.WriteLine($"Input: {doc.Input}");
                    }
                    if (doc.Output!=null)
                    {
                        Console.WriteLine($"\nOutput: {doc.Output}\n");
                    }
                    
                    }

                } }


        }
    }




