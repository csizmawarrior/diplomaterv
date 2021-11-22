using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github
{

    public class MonsterType : CharacterType
    {
        public string Name { get; set; }

        public MonsterType(string name)
        {
            base.Name = name;
        }

        public int Damage { get; set; }
        public int Health { get; set; }

        public List<Command> Commands { get; set; }

        //TODO: get out, now only for old code
        public int MoveRound { get; set; }
        public int ShootRound { get; set; }

        public override void Step(GameParamProvider provider)
        {
            Commands.ElementAt(provider.GetRound()).Execute(provider);
        }

        public override bool CompatibleCompare(object param2)
        {
            throw new NotImplementedException();
        }

        public override bool CompatibleEquals(object param2)
        {
            throw new NotImplementedException();
        }
    }
}
