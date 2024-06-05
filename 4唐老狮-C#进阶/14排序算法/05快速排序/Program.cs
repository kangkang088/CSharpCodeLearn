using System.Collections.Generic;
using System.Numerics;

namespace _05快速排序 {
    internal class Program {
        static void Main(string[] args) {
            CalcCount("XXYYYXZ");
        }
        void Rest() {
        }
        static void CalcCount(string s) {
            string s1 = s.ToUpper();//65-90
            Dictionary<char,int> keyValuePairs = new Dictionary<char,int>();
            Stack<int> stack1 = new Stack<int>();
            Stack<char> stack2 = new Stack<char>();
            foreach (char c in s1)
            {
                if((int)c >= 65 && (int)c <= 90) {//A-Z
                    stack2.Push(c);
                    if(!keyValuePairs.ContainsKey(c))
                        keyValuePairs.Add(c,1);
                    else
                        keyValuePairs[c]++;
                }
                if((int)c >= 49 && (int)c <= 57) {//1-9
                    stack1.Push((int)c - 48);
                }
            }
        }
    }
}
