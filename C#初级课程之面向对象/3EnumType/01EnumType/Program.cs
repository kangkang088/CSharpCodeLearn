using System;

namespace _01EnumType {
    public enum E_Week_Type {
        Monday = 1,Tuesday, Wednesday,Thursday,Friday, Saturday,Sunday,
    }
    internal class Program {
        static void Main(string[] args) {
            int e_Week_Type = (int)E_Week_Type.Tuesday;
            Console.WriteLine(e_Week_Type);
        }
    }
}
