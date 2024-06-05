using System;

namespace _01ConsoleApp1 {
    internal class Program {
        static void Main(string[] args) {
            Person person = new Person();
            person[0] = new Person();
        }
    }
    class Person {
        private string name;
        private int age;
        private Person[] friend;
        public Person() {
            friend = new Person[15];
        }
        public Person this[int index] {
            get {
                if (index >= friend.Length) {
                    return null;
                } else {
                    return friend[index];
                }
            }
            set {
                if (value is Person) {
                    friend[index] = value;
                } else {
                    friend[index] = null;
                }
                
            }
        }
    }
}
