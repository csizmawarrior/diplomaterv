using LabWork1github.static_constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github
{
    public class Board
    {

        public Player Player { get; set; }

        public string Name { get; set; }

        public List<Monster> Monsters { get; set; } = new List<Monster>();
        
        public List<Trap> Traps { get; set; } = new List<Trap>();

        public int Width { get; set; }
        public int Height { get; set; }
    }
}
