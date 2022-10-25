using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LabWork1github.DynamicEnemyGrammarParser;
using static LabWork1github.DynamicEnemyGrammarLexer;
using Antlr4.Runtime.Misc;

namespace LabWork1github.Listeners
{
    public class DynamicEnemyGrammarListener : DynamicEnemyGrammarBaseListener
    {
        public override void EnterNumberExpression([NotNull] NumberExpressionContext context)
        {
            base.EnterNumberExpression(context);
        }
        public override void ExitNumberExpression([NotNull] NumberExpressionContext context)
        {
            base.EnterNumberExpression(context);
        }
    }
}
