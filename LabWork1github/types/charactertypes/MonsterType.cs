
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github
{

    public class MonsterType : CharacterType
    {
        public MonsterType()
        {
        }

        public MonsterType(string name)
        {
            this.Name = name;
        }

        public override void Step(GameParamProvider provider)
        {
            Commands.ElementAt(provider.GetRound()).Execute(provider);
        }
    }
}
