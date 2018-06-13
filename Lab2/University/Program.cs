/// <summary>
/// Лабораторная №2
/// № в списке-23
/// Иван Трохименко IP-64
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Entities.Objective;

namespace University
{
    class Program
    {
        static void Main(string[] args)
        {
            University.Collections.University university = new Collections.University();

            Student student = new Student("Neznaiko", "1", new DateTime(1, 2, 3, 4, 5, 6), 1, "KPI", 8);
            Enrollee enrollee = new Enrollee("Samiy Umniy", "1", new DateTime(5, 2, 2, 4, 3, 1), 200, 100);
            Lecturer lecturer = new Lecturer("Javist", "1", new DateTime(4, 2, 3, 7, 8, 6), "Java", "KPI");


            university.Add(student);
            university.Add(enrollee);
            university.Add(lecturer);

            university.Show();

            Console.ReadKey();

        }
    }
}
