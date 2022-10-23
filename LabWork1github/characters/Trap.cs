﻿using LabWork1github.static_constants;
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
        public double Health { get; } = StaticStartValues.TRAP_HEALTH;

        public override void Damage(double amount)
        {
            return;
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
            return;
        }

        public override Character GetPartner()
        {
            return this.Partner;
        }
    }
}
