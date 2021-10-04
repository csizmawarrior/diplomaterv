using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github
{
    public class MonsterType
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
        public List<MoveCommand> Moves { get; set; }
        public List<IfCommand> Ifs { get; set; }
        public List<ShootCommand> Shoots { get; set; }



        //TODO: get out, now only for old code
        public int MoveRound { get; set; }
        public int ShootRound { get; set; }
    }
}
