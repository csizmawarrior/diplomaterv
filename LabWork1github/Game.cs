using LabWork1github;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github
{
    class Game
    {
        public Board Board { get; set; }

        public Player Player { get; set; }

        public List<Monster> Monsters { get; set; }

        public List<Trap> Traps { get; set; }

        private MonsterAI monsterAI;

        private TrapAI trapAI;

        public static PlayerMove move = new PlayerMove();

        private int round = 0;

        public int Round { get {
                return round; }
        }

        public void Init()
        {
            monsterAI = new MonsterAI(Monsters);
            trapAI = new TrapAI(Traps);
        }
    }
}
