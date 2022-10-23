using LabWork1github.static_constants;
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

        public string Name { get; set; } = StaticStartValues.PLACEHOLDER_NAME;

        public string PartnerName { get; set; } = StaticStartValues.PLACEHOLDER_PARTNER_NAME;

        public Character Partner { get; set; } = null;

        public abstract double GetHealth();

        public abstract void Damage(double amount);

        public abstract void Heal(double amount);
    }
}
