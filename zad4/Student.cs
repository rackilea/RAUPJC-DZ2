using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zad4
{
    public class Student
    {
        public string Name { get; set; }
        public string Jmbag { get; set; }
        public Gender Gender { get; set; }
        public Student(string name, string jmbag)
        {
            Name = name;
            Jmbag = jmbag;
            Gender = Gender.Female;
        }

        public Student(string name, string jmbag,Gender gender)
        {
            Name = name;
            Jmbag = jmbag;
            Gender = gender;
        }
        public static bool operator ==(Student s1, Student s2)
        {
            return s1.Jmbag == s2.Jmbag;
        }

        public static bool operator !=(Student s1, Student s2)
        {
            return !(s1 == s2);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || Jmbag == null)
            {
                return false;
            }
            return Jmbag.Equals(((Student)obj).Jmbag);
        }

        public override int GetHashCode()
        {
            return Jmbag != null ? StringComparer.CurrentCulture.GetHashCode(Jmbag) : 0;
        }
    }
    public enum Gender
    {
        Male, Female
    }

}
