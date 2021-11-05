using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github
{

    public class MonsterType : EnemyType
    {
        private string name = "";
        public MonsterType(string _name)
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
