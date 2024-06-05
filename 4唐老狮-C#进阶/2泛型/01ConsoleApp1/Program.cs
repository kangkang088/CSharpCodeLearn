namespace _01ConsoleApp1 {
    internal class Program {
        class TestClass<T> {
            public T value;
        }
        interface TestInterface<T> {
            public T Value {
                get; set;
            }
        }
        static void Main(string[] args) {
            #region
            TestClass<int> testClass = new TestClass<int>();
            testClass.value = 1;
            Console.WriteLine(testClass.value);
            TestClass<string> testClass1 = new TestClass<string>();
            testClass1.value = 1.ToString();
            Console.WriteLine(testClass1.value);
            #endregion
            
            Console.WriteLine(Func<int>());
            Console.WriteLine(sizeof(double));
        }
        static string Func<T>() {
            if (typeof(T) == typeof(int)) {
                return "整型 四字节";
            }
            if (typeof(T) == typeof(float)) {
                return "单精度浮点型 四字节";
            }
            if (typeof(T) == typeof(double)) {
                return "双精度浮点型 八字节";
            }
            if (typeof(T) == typeof(string)) {
                return "字符串类型";
            }
            return "";
        }
    }
}
