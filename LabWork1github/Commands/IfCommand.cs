using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github
{
    public delegate bool Condition(GameParamProvider provider);

    public class IfCommand : Command
    {
        public int CommandCount { get; set; }

        public Condition Condition { get; set; }
        public List<Command> commandList { get; set; }

        public override void Execute(GameParamProvider provider)
        {
            if(Condition(provider));
        }
    }
}
