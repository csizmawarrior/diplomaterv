using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github
{
    public delegate bool WhileDelegate(GameParamProvider provider, WhileCommand command);

    public class WhileCommand : Command
    {
        public WhileDelegate WhileDelegate { get; set; }
        public int CommandCount { get; set; }

        public override void Execute(GameParamProvider provider)
        {
            if (WhileDelegate(provider, this))
                provider.Repeat(CommandCount);
        }
    }
}
