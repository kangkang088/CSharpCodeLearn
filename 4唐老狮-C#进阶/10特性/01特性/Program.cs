using System.Xml.Serialization;

namespace _01特性 {
    [AttributeUsage(AttributeTargets.Class,AllowMultiple = true,Inherited = true)]
    class MyCustorAttribute : Attribute {
        public string info;
        public MyCustorAttribute(string info) {
            this.info = info;
        }
    }
    [MyCustor("12312313")]
    internal class Program {
        static void Main(string[] args) {
            
        }
    }
}
