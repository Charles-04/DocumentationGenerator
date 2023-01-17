using CustomAttributes;
using DocGenerator.Model;

namespace DocGenerator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Student student = new();
            Implementation.GetDocs(typeof(Student));
        }
    }
}