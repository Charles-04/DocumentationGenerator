using CustomAttributes;
using DocGenerator.Model;

namespace DocGenerator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Implementation.GetDocs(typeof(Student));
            //Implementation.GetDocs();
        }
    }
}