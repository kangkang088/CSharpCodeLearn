using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01俄罗斯方块 {
    internal class InputThread {
        //线程成员变量
        Thread inputThread;
        private static InputThread instance = new InputThread();

        public event Action inputEvent;

        private InputThread() {
            inputThread = new Thread(InputCheck);
            inputThread.IsBackground = true;
            inputThread.Start();
        }


        public static InputThread Instance {
            get {
                return instance;
            }
        }

        private void InputCheck(object? obj) {
            while (true) {
                inputEvent?.Invoke();
            }
        }


    }
}
