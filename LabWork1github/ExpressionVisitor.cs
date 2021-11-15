using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LabWork1github.DynamicEnemyGrammarParser;

namespace LabWork1github
{
    public class ExpressionVisitor : DynamicEnemyGrammarBaseVisitor<object>
    {
        public ExpressionContext ExpressionContext { get; set; }


        public ExpressionVisitor(ExpressionContext context)
        {
            ExpressionContext = context;
        }

        public void CheckTypes()
        {
            if (ExpressionContext.ABSOLUTE() != null)
                //TODO: check if it works this way or not
                CheckNumber(ExpressionContext.expression().ElementAt(0));
            if(ExpressionContext.PARENTHESISSTART() != null || ExpressionContext.NEGATE() != null)
            {
                ExpressionVisitor helperVisitor = new ExpressionVisitor(ExpressionContext.expression().ElementAt(0));
                helperVisitor.CheckTypes();
            }
            if (ExpressionContext.operation() != null) 
                if (ExpressionContext.operation().ALIVE() != null || ExpressionContext.operation().NEAR() != null)
                    if (!(ExpressionContext.expression().ElementAt(1).something().NOTHING() != null))
                        throw new ArgumentException("An operation has more arguments than exprected");
                    
            VisitExpression(ExpressionContext);
        }
        public void CheckNumber(ExpressionContext context)
        {

        }

    }
}
