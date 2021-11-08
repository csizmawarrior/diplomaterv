using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LabWork1github.DynamicEnemyGrammarParser;

namespace LabWork1github
{
    class ExpressionVisitor : DynamicEnemyGrammarBaseVisitor<object>
    {
        public ExpressionContext ExpressionContext { get; set; }


        public ExpressionVisitor(ExpressionContext context)
        {
            ExpressionContext = context;
        }

        public void CheckTypes()
        {
            VisitExpression(ExpressionContext);
        }
    }
}
