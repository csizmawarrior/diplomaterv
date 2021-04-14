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

        public void Start()
        {
            while (Player.Health > 0 && Monsters.Count > 0)
            {
                round++;
                Step();
            }
        }

        public void Step()
        {
            //TODO: asking a player for a move then check if it's possible
            //TODO: make monsters and traps move that can
            //TODO: check if a  trap spawned another monster or not, if yes then spawn a basic monster (0th item in monstertype list)
            
        }

        public void Init()
        {
            monsterAI = new MonsterAI(Monsters);
            trapAI = new TrapAI(Traps);
            foreach(Monster monster in Monsters)
            {
                if (monster.Place.X > Board.Width || monster.Place.Y > Board.Height)
                    Monsters.Remove(monster);
            }
            foreach (Trap Trap in Traps)
            {
                if (Trap.Place.X > Board.Width || Trap.Place.Y > Board.Height)
                    Traps.Remove(Trap);
                if (Trap.Type.EffectPlace != null)
                {
                    if (Trap.Type.EffectPlace.X > Board.Width || Trap.Type.EffectPlace.Y > Board.Height)
                        Traps.Remove(Trap);
                }
            }
            if (Player.Place.X > Board.Width || Player.Place.Y > Board.Height)
                throw new NullReferenceException("Player is not on the board");
        }
    }
}
