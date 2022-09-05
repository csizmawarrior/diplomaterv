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
        public TrapType(string _name)
        {
            this.Name = _name;
        }

        public int MoveRound { get; set; }


        public override void Step(GameParamProvider provider)
        {
            Commands.ElementAt(provider.GetRound()).Execute(provider);
        }

       
    }
}
