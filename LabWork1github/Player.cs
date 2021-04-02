using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1
{
    public class Player
    {
        public Player(Place p, int hp)
        {
            health = hp;
            place = p;
        }
        public int health { get; set; }
        public Place place { get; set; }
    }
}
