using System;

namespace _01Struct {
    public struct Person {
        public string name;
        public int age;
        public int gender;
        public int address;
        public Person(string name, int age, int gender,int address) {
            this.name = name;
            this.age = age;
            this.gender = gender;
            this.address = address;
        }
    }
    internal class Program {
        static void Main(string[] args) {
            Person person = new Person();
            Console.WriteLine(person.address);
            Person person1 = new Person("kangkang",18,0,155);
            Console.WriteLine(person1.address);
        }
    }
}
