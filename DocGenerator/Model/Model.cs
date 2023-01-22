using CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DocGenerator.Model
{
    [Document(description:"This class cotains the student model")]
    public class Student
    {
        [Document(description:"The name of the student")]
        public string Name { get; set; }

        [Document(description:"This property stores the age of the student")]
        public int Age { get; set; }

        [Document(description:"This property stores the student phone number")]
        [Phone]

        public long Phone { get; set; }

        [Document(description:"This method returns student age","Takes an input of type datetime, date of birth","Output Age")]
        public int GetAge(DateTime dayOfBirth)
        {
           
            return Age;
        }
    }
}
