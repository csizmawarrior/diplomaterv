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
        public Player player { get; set; }

        public List<Monster> Monsters { get; set; }

        public List<Trap> Traps { get; set; }
    }
}
