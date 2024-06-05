using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01俄罗斯方块 {
    internal struct Postion {
        public int x;
        public int y;
        public Postion(int x, int y) {
            this.x = x;
            this.y = y;
        }
        public static bool operator ==(Postion left, Postion right) {
            if (left.x == right.x && left.y == right.y)
                return true;
            return false;
        }
        public static bool operator !=(Postion left, Postion right) {
            if (left.x == right.x && left.y == right.y)
                return false;
            return true;
        }
        public static Postion operator +(Postion left, Postion right) {
            Postion postion = new Postion(left.x + right.x, left.y + right.y);
            return postion;
        }
    }
}
