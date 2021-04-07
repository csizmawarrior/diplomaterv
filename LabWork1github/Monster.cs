using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github
{
    public class Monster
    {
        public Monster(int hp, MonsterType type, Place place)
        {
            health = hp;
            Type = type;
            Place = place;
        }
        public int health { get; set; }
        public MonsterType Type { get; private set; }
        public Place Place { get; set; }
    }
}
