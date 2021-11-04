using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using static LabWork1github.DynamicMonsterParser;

namespace LabWork1github
{ 
    class MonsterExpressionParserVisitor : DynamicMonsterBaseVisitor<object>
    {
        public ExpressionContext Context { get; set; }
        public Condition Condition { get; set; }

        public MonsterExpressionParserVisitor(ExpressionContext context)
        {
            Context = context;
        }

        public Condition GetCondition()
        {
            VisitExpression(Context);
            return Condition;
        }

        public override object VisitExpression([NotNull] ExpressionContext context)
        {

            return base.VisitExpression(context);
        }
    }
}
