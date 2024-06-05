using System;

namespace ConsoleApp1 {
    enum E_MonsterType {
        Normal,
        Boss,
    }
    enum E_PlayerType {
        Main,
        Other
    }
    class Program {
        static void Main(string[] args) {
            E_MonsterType monsterType = E_MonsterType.Normal;
            E_PlayerType playerType = 0;
            int i = (int)E_MonsterType.Normal;
            Console.WriteLine(monsterType);
            Console.WriteLine(playerType);
            switch (monsterType) {
                case E_MonsterType.Normal:
                    break;
                case E_MonsterType.Boss:
                    break;
            }
        }
    }
}
