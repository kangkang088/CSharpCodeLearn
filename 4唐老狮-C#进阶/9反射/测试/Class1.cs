using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrWei {
    class MyCustomAttribute : Attribute {
        
    }
    public struct Postion {
        public int x;
        public int y;
        public Postion(int x, int y) {
            this.x = x;
            this.y = y;
        }
    }
    public class Player {
        [MyCustom]
        public string name;
        public int hp;
        public int atk;
        public int def;
        public Postion pos;
    }
}
