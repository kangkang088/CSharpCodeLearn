using System.Collections;

namespace _04Hashtable {
    internal class Program {
        static void Main(string[] args) {
            MonsterMgr.Instance.AddMonster();
            MonsterMgr.Instance.AddMonster();
            MonsterMgr.Instance.AddMonster();
            MonsterMgr.Instance.AddMonster();
            MonsterMgr.Instance.AddMonster();

            MonsterMgr.Instance.RemoveMonster(0);
            MonsterMgr.Instance.RemoveMonster(3);
        }
    }
    class MonsterMgr {
        //单例模式
        private static MonsterMgr instance = new MonsterMgr();
        private MonsterMgr() {

        }
        public static MonsterMgr Instance {
            get {
                return instance;
            }
        }


        private Hashtable monsterHashtable = new Hashtable();
        
        private int monsterId = 0;
        public void AddMonster() {
            Monster monster = new Monster(monsterId);
            monsterId++;
            monsterHashtable.Add(monster.id, monster);
        }
        public void RemoveMonster(int monsterId) {
            if (monsterHashtable.ContainsKey(monsterId)) {
                (monsterHashtable[monsterId] as Monster)?.Dead();
                monsterHashtable.Remove(monsterId);
            }
        }
    }
    class Monster {
        public int id;

        public Monster(int id) {
            this.id = id;
        }
        public void Dead() {
            Console.WriteLine("怪物{0}死亡",id);
        }
    }
}
