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
        public TrapType()
        {
        }

        public TrapType(string name)
        {
            this.Name = name;
        }

        public int MoveRound { get; set; }


        public override void Step(GameParamProvider provider)
        {
            if (provider.GetRound() < Commands.Count)
                Commands.ElementAt(provider.GetRound()).Execute(provider);
        }

       
    }
}
