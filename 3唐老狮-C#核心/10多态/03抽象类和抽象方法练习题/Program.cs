using System;

namespace _03抽象类和抽象方法练习题 {
    //      创建一个图形类，有求面积和周长两个方法
    //      创建矩形类，正方形类，圆形类继承图形类
    //      实例化矩形、正方形、圆形对象求面积和周长
    abstract class Graphics {
        public abstract double GetAreas();
        public abstract double GetPerimeters();
    }
    class Rectangle : Graphics {
        public double x;
        public double y;

        public Rectangle() {
            x = 5;
            y = 5;
        }

        public override double GetAreas() {
            return x * y;
        }

        public override double GetPerimeters() {
            return (x + y) * 2;
        }
    }
    class Square : Graphics {
        public double x;
        public Square() {
            x = 5;
        }
        public override double GetAreas() {
            return x * x;
        }

        public override double GetPerimeters() {
            return x * 4;
        }
    }
    class Circular : Graphics {
        public static double PI = Math.PI;
        public double r;
        public Circular() {
            r = 5;
        }
        public override double GetAreas() {
            return PI * r * r;
        }

        public override double GetPerimeters() {
            return 2 * PI * r;
        }
    }
    internal class Program {
        static void Main(string[] args) {
            Graphics rect = new Rectangle();
            Console.WriteLine(rect.GetPerimeters());
            Console.WriteLine(rect.GetAreas());
            Graphics square = new Square();
            Console.WriteLine(square.GetPerimeters());
            Console.WriteLine(square.GetAreas());
            Graphics circular = new Circular();
            Console.WriteLine(circular.GetPerimeters());
            Console.WriteLine(circular.GetAreas());

        }
        abstract class Animals {
            public abstract void Call();
        }
        class Person : Animals {
            public override void Call() {
                Console.WriteLine("人叫");
            }
        }
        class Dog : Animals {
            public override void Call() {
                Console.WriteLine("狗叫");
            }
        }
        class Cat : Animals {
            public override void Call() {
                Console.WriteLine("猫叫");
            }
        }
    }
}
