using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github
{
    public abstract class Character
    {
        public Place Place { get; set; }

        public abstract CharacterType GetCharacterType();

        public abstract Character GetPartner();

        public string Name { get; set; }

        public string PartnerName { get; set; } = "";

        public Character Partner { get; set; } = null;

        public abstract int GetHealth();

        public abstract void Damage(int amount);

        public abstract void Heal(int amount);
    }
}
