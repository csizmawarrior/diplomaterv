using LabWork1github.types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github
{
    public enum TrapEffect 
    {
        Damage,
        Heal,
        Teleport,
        Spawner
    }

    public class TrapType : CharacterType
    {
        private string name = "";
        public TrapType(string _name)
        {
            Name = _name;
        }
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (name.Equals(""))
                    name = value;
            }
        }

        public int MoveRound { get; set; }
        public TrapEffect EffectType { get; set; }
        private Place effectPlace;
        public Place EffectPlace
        {
            get { return effectPlace; }
            set
            {
                if (EffectType == TrapEffect.Spawner || EffectType == TrapEffect.Teleport)
                    effectPlace = value;
            }
        }
        private int effectNumber = 0;
        public int EffectNumber
        {
            get { return effectNumber; }
            set
            {
                if (EffectType == TrapEffect.Damage || EffectType == TrapEffect.Heal)
                    effectNumber = value;
            }
        }

        public override void Step(GameParamProvider provider)
        {
            Commands.ElementAt(provider.GetRound()).Execute(provider);
        }

        public override bool CompatibleCompare(object param2)
        {
            if (param2 is TrapType)
                return true;
            return false;
        }

        public override bool CompatibleNumCompare(object param2)
        {
            return false;
        }

        public override bool CompatibleAttribue(object param2)
        {
            if (param2 is StringType)
                return true;
            return false;
        }

        public override bool CompatibleNumConnecter(object param2)
        {
            return false;
        }

        public override bool CompatibleBoolConnecter(object param2)
        {
            return false;
        }

        public override bool CompatibleAlive(object param2)
        {
            if (param2 == null)
                return true;
            throw new ArgumentException("not null parameter for absolute typecheck");
        }

        public override bool CompatibleIsNear(object param2)
        {
            if (param2 == null)
                return true;
            throw new ArgumentException("not null parameter for absolute typecheck");
        }

        public override bool CompatibleAbsolue(object param2)
        {
            return false;
        }

        public override bool CompatibleNegate(object param2)
        {
            return false;
        }
    }
}
