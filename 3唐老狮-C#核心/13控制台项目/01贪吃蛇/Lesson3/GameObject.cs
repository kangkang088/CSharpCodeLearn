using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01贪吃蛇.Lesson3 {
    internal abstract class GameObject : IDraw {
        public Position pos;
        public abstract void Draw();
    }
}
