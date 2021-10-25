using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LabWork1github.DynamicMonsterParser;
using LabWork1github;

namespace LabWork1github
{
    class MonsterExpressionVisitor : DynamicMonsterBaseVisitor<object>
    {
        public Dictionary<int, Symbol> SymbolTable { get; set; }

        public override object Visit([NotNull] IParseTree tree)
        {
            return base.Visit(tree);
        }
        public override object VisitExpression([NotNull] ExpressionContext context)
        {
            if (context.operation() != null)
                if (context.operation().BOOLCONNECTER() != null)
                    VisitBoolExpression(context.expression().ElementAt(0));
            return base.VisitExpression(context);
        }
        public void VisitBoolExpression([NotNull] ExpressionContext context)
        {

        }
    }
}
