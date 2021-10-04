using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github
{
    public delegate void MoveDelegate(Player player, List<Monster> monsters, Monster monster, List<Trap> traps, int round, MoveCommand command);

    public class MoveCommand
    {
        public int Distance { get; set; } = 1;
        public int Round { get; set; }
        public int Damage { get; set; } = 10;
        public Place targetPlace { get; set; }
        public string Direction { get; set; }

        public MoveDelegate MoveDelegate { get; set; }


        public void Move(Player player, List<Monster> monsters, Monster monster, List<Trap> traps, int round) {
            MoveDelegate(player, monsters, monster, traps, round, this);
        }

    }
}
