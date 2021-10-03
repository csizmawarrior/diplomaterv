using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github
{
    public class ShootCommand
    {
        public int Distance { get; set; } = 1;
        public string Direction { get; set; }
        public int Round { get; set; }
        public Place targetPlace { get; set; }
        public int Damage { get; set; }


        public void Shoot(Player p, List<Monster> monsters, List<Trap> traps, int round) { }
    }
}
