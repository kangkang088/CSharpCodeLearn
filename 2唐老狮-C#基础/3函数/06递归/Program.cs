using System;
using System.Runtime.InteropServices;

namespace _06递归 {
    struct Student {
        int age;
        char gender;
        string name;
        public Student(int age,char gender,string name) {
            this.age = 0;
            this.gender = 'A';
            this.name = null;
        }
    }
    internal class Program {
        static void Main(string[] args) {
            Func2(1024,0);
        }
        static void Func2(double val,int day) {
            val /= 2;
            ++day;
            if (day == 10) {
                Console.WriteLine(val);
                return;
            }
            Func2(val, day);
        }
    }
}
