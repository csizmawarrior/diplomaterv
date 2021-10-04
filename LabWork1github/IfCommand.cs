using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github
{
    public delegate bool IfDelegate(Player player, List<Monster> monsters, Monster monster, List<Trap> traps, int round, IfCommand command);

    public class IfCommand
    {
        public int Round { get; set; }

        //TODO: need to find what is required so every condition can be decided
        //can be done with config file? or a new type?

        public IfDelegate IfDelegate { get; set; }

        public bool Execute(Player player, List<Monster> monsters, Monster monster, List<Trap> traps, int round, IfCommand command)
        {
            return IfDelegate(player, monsters, monster, traps, round, command);
        }

    }
}
