namespace _02泛型约束 {
    class Test1<T> where T : struct {
        public T value;
        public void TestFunc<K>(K v) where K : struct {

        }
    }
    class Student {
    }
    internal class Program {
        
        static void Main(string[] args) {
            int a = 10;
            int b = 20;
            Student student = new Student();
            Student student1 = student;
            Console.WriteLine(object.Equals(student,student1));
        }
    }
}
