namespace _01ToUpper {
    internal class Program {
        static void Main(string[] args) {
            string name = "KangKang";
            name = name.ToUpper();
            Console.WriteLine(name);
            name = name.ToLower();
            Console.WriteLine(name);

            string email = "1808131844@qq.com";
            string userName = email.Split(new char[] { '@' })[0];
            for (int i = 0; i < email.Split(new char[] { '@', '.' }).Length; i++) {
                Console.WriteLine(email.Split(new char[] { '@', '.' })[i]);
            }
            Console.WriteLine(userName);
            string str1 = "kangkang";
            str1 = str1.Substring(1,2);
            Console.WriteLine(str1);
            string str2 = "kangkang541";
            Console.WriteLine(str2.LastIndexOf("41"));

            string str3 = "MrKang";
            str3 = str3.Replace("Mr", "Mrs");
            Console.WriteLine(str3);
        }
    }
}
