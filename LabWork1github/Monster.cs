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
            Health = hp;
            Type = type;
            Place = place;
        }
        public int Health { get; set; }
        public MonsterType Type { get; private set; }
        public Place Place { get; set; }

        public void Damage(int amount)
        {
            Health -= amount;
        }
    }
}
