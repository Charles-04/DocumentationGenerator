using System;
using System.Collections.Generic;
using System.Text;

namespace CustomAttributes
{
    [AttributeUsage( AttributeTargets.All)]
    public class DocumentAttribute : Attribute
    {
        public string Description { get; set; }
        public string Input { get; set; }
        public string Output { get; set; }
        public DocumentAttribute(string description, string input = null , string output = null )
        {
            Description = description;
            Input = input;
            Output = output;
        }
    }
}
