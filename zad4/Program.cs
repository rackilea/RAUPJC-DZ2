using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zad4
{
    class Program
    {
        static void Main(string[] args)
        {
            //Treci zadatak:

            int[] integers = new[] { 1, 2, 2, 2, 3, 3, 4, 5 };
            String[] strings = integers.GroupBy(i => i).Select(i => "Broj " + i.Key + " ponavlja se " + i.Count() + " puta.").ToArray();

            for (int i = 0; i < strings.Length; ++i)
            {
                Console.WriteLine("strings [" + i + "] = " + strings[i]);
            }
            // strings [0] = Broj 1 ponavlja se 1 puta
            // strings [1] = Broj 2 ponavlja se 3 puta 
            // strings [2] = Broj 3 ponavlja se 2 puta
            // strings [3] = Broj 4 ponavlja se 1 puta
            // strings [4] = Broj 5 ponavlja se 1 puta

            //Cetvrti zadatak:

            var list = new List<Student>()
            {
                new Student (" Ivan ", jmbag :" 001234567 ")
            };
            var ivan = new Student(" Ivan ", jmbag: " 001234567 ");
            Console.WriteLine("Does Ivan exists? {0}", list.Any(x => x == ivan));
            list.Add(new Student(" Ivan ", jmbag: " 001234567 "));
            var distinctStudents = list.Distinct().Count();
            var t = ivan.GetHashCode();
            Console.WriteLine("The list contains {0} distinct student{1}", distinctStudents, distinctStudents == 1 ? "" : "s");


            //Peti zadatak:
            University[] universities = GetAllCroatianUniversities();
            Student[] allCroatianStudents = universities.SelectMany(i => i.Students)
                                                                    .Distinct()
                                                                    .ToArray();
            Console.WriteLine("All Croatian students:");
            foreach (var p in allCroatianStudents)
            {
                Console.WriteLine(p);
            }

            Student[] croatianStudentsOnMultipleUniversities =
                universities.SelectMany(i => i.Students)
                                        .GroupBy(x => x)
                                        .Where(group => group.Count() > 1)
                                        .Select(group => group.Key)
                                        .ToArray();
            Console.WriteLine("Croatian students on multiple universities:");
            foreach (var i in croatianStudentsOnMultipleUniversities)
            {
                Console.WriteLine(i);
            }
            Student[] studentsOnMaleOnlyUniversities =
                universities.Where(i => i.Students.All(g => g.Gender.Equals(Gender.Male)))
                                  .SelectMany(x => x.Students)
                                  .ToArray();

            Console.WriteLine("Croatian students on male only univerities.");
            foreach (var i in studentsOnMaleOnlyUniversities)
            {
                Console.WriteLine(i);
            }
        }

            public static University[] GetAllCroatianUniversities()
        {
            University Zg = new University("University of Zagreb",
                new[]
                {
                    new Student("Lea", "00361", Gender.Female),
                    new Student("Ivana", "00362", Gender.Female),
                    new Student("Ana", "00363", Gender.Female),
                }
            );
            University St = new University("University of Split",
                new[]
                {
                    new Student("Ivan", "00381", Gender.Male),
                    new Student("Ana", "00362"),
                    new Student("Petra", "00372"),
                }
            );
            University Ri = new University("University of Rijeka",
                new[]
                {
                    new Student("Ivan", "00381",Gender.Male),
                    new Student("Marko", "00382",Gender.Male),
                    new Student("Pero", "00383",Gender.Male),
                }
            );
            return new[] { Zg, St, Ri };
        }
    }
     
}
   


    

