using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github
{

    public class MonsterType : EnemyType
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

        public override void Step()
        {
            throw new NotImplementedException();
        }
    }
}
