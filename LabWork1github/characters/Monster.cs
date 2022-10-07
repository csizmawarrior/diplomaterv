using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github
{
    public class Monster : Character
    {
        public Monster(int hp, MonsterType type, Place place)
        {
            Health = hp;
            Type = (MonsterType)Program.GetCharacterType(type.Name);
            Place = place;
        }
        public int Health { get; set; }
        public MonsterType Type { get; private set; }

        public override void Damage(int amount)
        {
            Health -= amount;
        }

        public override int GetHealth()
        {
            return Health;
        }

        public override CharacterType GetCharacterType()
        {
            return Type;
        }

        public override void Heal(int amount)
        {
            Health += amount;
        }

        public override Character GetPartner()
        {
                return this.Partner;
        }
    }
}
