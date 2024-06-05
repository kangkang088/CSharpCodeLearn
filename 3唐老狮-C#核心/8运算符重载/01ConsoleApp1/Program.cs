using System;
using System.Security.Cryptography;

namespace _01ConsoleApp1 {
    internal class Program {
        static void Main(string[] args) {
            Point p = new Point();
            p.x = 1;
            p.y = 1;
            Point point = new Point();
            point.x = 2;
            point.y = 2;
            Point pp = p + point;
            Console.WriteLine(pp.x);
            Console.WriteLine(pp.y);
        }
    }
    class Point {
        public int x;
        public int y;
        public static Point operator+(Point p1,Point p2) {
            Point point = new Point();
            point.x = p1.x + p2.x;
            point.y = p1.y + p2.y;
            return point;
        }
    }
}
