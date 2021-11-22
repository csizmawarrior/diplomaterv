using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github
{
    public class Trap : Character
    {
        public Trap(TrapType type, Place place)
        {
            Type = type;
            Place = place;
        }
        public TrapType Type { get; private set; }
        public int Health { get; } = 1;

        public override void Damage(int amount)
        {
            return;
        }

        public override int GetHealth()
        {
            return Health;
        }

        public override CharacterType GetType()
        {
            return Type;
        }

        public override void Heal(int amount)
        {
            return;
        }
    }
}
