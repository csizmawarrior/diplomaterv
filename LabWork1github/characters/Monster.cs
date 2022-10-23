using LabWork1github.static_constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github
{
    public class Monster : Character
    {
        public Monster(double hp, MonsterType type, Place place)
        {
            Type = (MonsterType)Program.GetCharacterType(type.Name);
            if (Type.Health != StaticStartValues.PLACEHOLDER_HEALTH)
                Health = Type.Health;
            else
                Health = hp;
            Place = place;
        }
        public double Health { get; set; } = StaticStartValues.STARTER_MONSTER_HP;
        public MonsterType Type { get; private set; }

        public override void Damage(double amount)
        {
            Health -= amount;
        }

        public override double GetHealth()
        {
            return Health;
        }

        public override CharacterType GetCharacterType()
        {
            return Type;
        }

        public override void Heal(double amount)
        {
            Health += amount;
        }

        public override Character GetPartner()
        {
                return this.Partner;
        }
    }
}
