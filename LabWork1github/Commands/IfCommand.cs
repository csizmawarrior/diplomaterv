using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LabWork1github.DynamicEnemyGrammarParser;

namespace LabWork1github
{
    public delegate bool Condition(GameParamProvider provider, ExpressionContext context);

    public class IfCommand : Command
    {
        public int CommandCount { get; set; }
        public ExpressionContext MyContext { get; set; }
        public Condition Condition { get; set; }
        public List<Command> CommandList { get; set; }

        public override void Execute(GameParamProvider provider)
        {
            if(Condition(provider, MyContext));
        }
    }
}
