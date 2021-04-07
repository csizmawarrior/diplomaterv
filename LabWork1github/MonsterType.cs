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
        public int Range { get; set; }
        public int MoveRound { get; set; }
        public int ShootRound { get; set; }
    }
}
