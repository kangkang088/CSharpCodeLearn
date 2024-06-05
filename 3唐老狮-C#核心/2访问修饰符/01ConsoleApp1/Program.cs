using System;

namespace _01ConsoleApp1 {
    internal class Program {
        class Student {
            public int age;
            public Student deskmate;
        }
        static void Main(string[] args) {
            Student s = new Student();
            s.deskmate = new Student();
            s.deskmate.age = 10;
            Student s2 = s.deskmate;
            s2.age = 20;
            Console.WriteLine(s.deskmate.age);

            
        }
    }
    
}
