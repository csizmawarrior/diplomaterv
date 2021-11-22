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
                CheckOperation(ExpressionContext);
                    
            VisitExpression(ExpressionContext);
        }


        public void CheckOperation(ExpressionContext context)
        {
            if (ExpressionContext.operation().ALIVE() != null || ExpressionContext.operation().NEAR() != null)
                if (!(ExpressionContext.expression().ElementAt(1).something().NOTHING() != null))
                    throw new ArgumentException("An operation has more arguments than exprected");
            if(ExpressionContext.operation().NUMCOMPARE() != null || ExpressionContext.operation().NUMCONNECTER() != null)
            {
                CheckNumber(ExpressionContext.expression().ElementAt(0));
                CheckNumber(ExpressionContext.expression().ElementAt(1));
            }
            if(ExpressionContext.operation().ATTRIBUTE() != null)
            {
                CheckAttribute(ExpressionContext);
            }
        }

        private void CheckAttribute(ExpressionContext expressionContext)
        {
            if (!(ExpressionContext.expression().ElementAt(0).something().character() != null && ExpressionContext.expression().ElementAt(1).something().possibleAttributes() != null))
                throw new ArgumentException("Attribute operation used incorrectly");
            string attribute = ExpressionContext.expression().ElementAt(1).something().possibleAttributes().GetText();
            switch (attribute)
            {
                //TODO: decide if how to deal with me.place.x case grammar changing?
                }
        }

        public void CheckNumber(ExpressionContext context)
        {

        }

    }
}
