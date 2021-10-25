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


        //TODO: need to find what is required so every condition can be decided
        //can be done with config file? or a new type?

        public Condition Condition { get; set; }
        public List<Command> commandList { get; set; }

        public override void Execute(GameParamProvider provider)
        {
            if(Condition(provider));
        }
    }
}
