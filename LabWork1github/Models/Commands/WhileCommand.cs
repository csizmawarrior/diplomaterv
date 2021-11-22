using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github
{

    public class WhileCommand : Command
    {
        public Condition Condition { get; set; }
        public int CommandCount { get; set; }

        public override void Execute(GameParamProvider provider)
        {
            if (Condition(provider))
                provider.Repeat(CommandCount);
        }
    }
}
