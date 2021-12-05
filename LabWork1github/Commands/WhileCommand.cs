using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LabWork1github.DynamicEnemyGrammarParser;

namespace LabWork1github
{

    public class WhileCommand : Command
    {
        public Condition Condition { get; set; }
        public ExpressionContext MyContext { get; set; }

        public override void Execute(GameParamProvider provider)
        {
            while (Condition(provider, MyContext))
            {
                foreach (Command command in CommandList)
                    command.Execute(provider);
            }
        }
    }
}
