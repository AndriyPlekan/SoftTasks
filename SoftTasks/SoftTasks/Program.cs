using System;
using System.Collections.Generic;
using System.Linq;

namespace SoftTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Student> students = new List<Student> { };
            var random = new Random();
            var listGroups = new List<string> { "pmi-41", "pmi-21", "pmi-31", "pmi-51" };
            var listDates = new List<DateTime> { DateTime.Parse("Jan 1, 2009"), DateTime.Parse("Jan 1, 2000"), DateTime.Parse("Jan 1, 2002"), DateTime.Parse("Jan 1, 1998") };

            for (int i = 0; i < 7; i++)
            {
                Student s1 = new Student("Name" + i, "Sur" + i, "Father" + i, listDates[random.Next(listDates.Count)], new List<int> { i + 1, i + 8, i + 3 }, listGroups[random.Next(listGroups.Count)]);
                students.Add(s1);
                Console.WriteLine($"Group: {s1.group}");
                Console.WriteLine($"Average mark: {s1.averageMark()}");
                Console.WriteLine($"Age: {s1.getAge()}");
            }

            // 4
            var groups = students.GroupBy(s=>s.group).Select(g=>new { name = g.Key, count = g.Count()});

            foreach (var group in groups)
            {
                Console.WriteLine($"{group.name} - {group.count}");
            }


            Console.WriteLine();
            var groups2 = students.GroupBy(s => s.group).Select(g => new { name = g.Key, count = g.Where(e => e.marks.All(m => m == 10)).Count() });

            foreach (var group in groups2)
            {
                Console.WriteLine($"{group.name} - {group.count}");
            }


            Console.WriteLine();
            var groups3 = students.GroupBy(s => s.group).Select(g => new { name = g.Key, count = g.Where(e => e.marks.All(m => m >= 3)).Count() });

            foreach (var group in groups3)
            {
                Console.WriteLine($"{group.name} - {group.count}");
            }


            Console.WriteLine();
            Console.WriteLine("Type a mark:");
            int number = int.Parse(Console.ReadLine());
            var groups4 = students.GroupBy(s => s.group).Select(g => new { name = g.Key, count = g.Where(e => e.marks.All(m => m > number)).Count() });

            foreach (var group in groups4)
            {
                Console.WriteLine($"{group.name} - {group.count}");
            }
            //5

            Console.ReadKey();
        }
        static public int NumberOfStudents(List<Student> group, double averMark = 4)
        {
            if (averMark < 0 || averMark > 5)
            {
                throw new ArgumentException("Value cannot be lass than 0 or greater than 5");
            }
            return group.Count;
        }
    }



    internal class Person
    {
        public string name;
        public string surName;
        public string fatherName;
        public DateTime birthday;

        public Person(string name, string surName, string fatherName, DateTime birthday)
        {
            this.name = name;
            this.surName = surName;
            this.fatherName = fatherName;
            this.birthday = birthday;
        }
    }

    internal class Student : Person
    {
        public List<int> marks;
        public string group;

        public Student(string name, string surName, string fatherName, DateTime birthday, List<int> marks, string group) : base(name, surName, fatherName, birthday)
        {
            this.marks = marks;
            this.group = group;
        }

        public double averageMark()
        {
            return marks.Sum() / marks.Count;
        }

        public int getAge()
        {
            var currentDay = DateTime.Today;
            return currentDay.Year - this.birthday.Year;
        }

    }
}
