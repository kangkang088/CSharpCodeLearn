using System.Reflection;

namespace _02练习 {
    internal class Program {
        static void Main(string[] args) {
            Assembly assembly = Assembly.LoadFrom(@"D:\code\ownlearn\Unity\C#\4唐老狮-C#进阶\9反射\测试\bin\Debug\测试");
            Type[] types = assembly.GetTypes();
            foreach (Type t in types) {
                Console.WriteLine(t);
            }
            Console.WriteLine("******************");
            Type typePlayer = assembly.GetType("MrWei.Player");
            object o = Activator.CreateInstance(typePlayer);
            Console.WriteLine(o);
            //Console.WriteLine("******************");
            //ConstructorInfo constructorInfo = typePlayer.GetConstructor(new Type[0]);
            //object oo = constructorInfo.Invoke(null);
            //Console.WriteLine(oo);

            FieldInfo[] files = typePlayer.GetFields();
            foreach (FieldInfo f in files) {
                Console.WriteLine(f);
            }
            Type attribute = assembly.GetType("MrWei.MyCustomAttribute");
            FieldInfo fieldString = typePlayer.GetField("name");
            if (fieldString.GetCustomAttribute(attribute) != null) {
                Console.WriteLine("非法操作！随意修改name成员");
            } else {
                fieldString.SetValue(o, "123123");
            }
        }
    }
}
