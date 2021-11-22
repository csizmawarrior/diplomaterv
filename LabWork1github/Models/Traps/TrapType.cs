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

    public class TrapType : EnemyType
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

        public override void Step()
        {
            throw new NotImplementedException();
        }
    }
}
