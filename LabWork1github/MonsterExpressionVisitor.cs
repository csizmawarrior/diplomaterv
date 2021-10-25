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
            {
                if (context.operation().BOOLCONNECTER() != null)
                    VisitBoolExpression(context.expression().ElementAt(0));
                if (context.operation().ALIVE() != null || context.operation().NEAR() != null)
                    VisitFunctionExpression(context);
                if (context.operation().COMPARE() != null)
                    VisitNumberOperation(context.expression());
            }
            if (context.PARENTHESISCLOSE() != null || context.NEGATE() != null)
                VisitBoolExpression(context.expression().ElementAt(0));
            else throw new InvalidOperationException("Not bool Expression used as condition!");
            return base.VisitExpression(context);
        }
        public void VisitBoolExpression([NotNull] ExpressionContext context)
        {
            if (context.operation() != null)
            {
                if (context.operation().ATTRIBUTE() != null || context.operation().NUMCONNECTER() != null)
                    throw new InvalidOperationException("Number operation sent instead of bool operation.");
                if(context.operation().BOOLCONNECTER() != null)
                    VisitBoolExpression(context.expression().ElementAt(0));
                if (context.operation().ALIVE() != null || context.operation().NEAR() != null)
                        VisitFunctionExpression(context);
                if (context.operation().COMPARE() != null)
                    VisitNumberOperation(context.expression());
                throw new InvalidOperationException("Not recognizeed operation option!");
            }
            if (context.PARENTHESISCLOSE() != null || context.NEGATE() != null)
                VisitBoolExpression(context.expression().ElementAt(0));
            else
                throw new InvalidOperationException("Not recognizeed operation option!");
        }

        private void VisitFunctionExpression(ExpressionContext context)
        {
            if (context.expression().ElementAt(1).something() == null || context.expression().ElementAt(1).something().NOTHING() == null)
                throw new InvalidOperationException("Function used incorrectly!");
            if (context.expression().ElementAt(0).something() == null || context.expression().ElementAt(0).something().character() == null)
                throw new ArgumentException("Function used incorrectly!");
        }

        private void VisitNumberOperation(ExpressionContext[] expressionContext)
        {
            foreach(ExpressionContext exp in expressionContext)
            {
                if (exp.ABSOLUTE() != null || exp.PARENTHESISCLOSE() != null) ;
                    VisitNumberOperation(exp.expression());
                if(exp.operation() != null)
                {
                    if (exp.operation().NUMCONNECTER() != null)
                        VisitNumberOperation(exp.expression());
                    else
                    if (exp.operation().ATTRIBUTE() != null)
                    {
                        VisitAttributeExpression(exp.expression());
                    }
                    else
                        throw new InvalidOperationException("Wrong usage of Number Operation!");
                }
                if (exp.something() == null)
                    throw new ArgumentException("Wrong argument for NumberOperation!");
                if (exp.something().ROUND() != null || exp.something().NUMBER() != null)
                    continue;
                else
                    throw new ArgumentException("Not recognized option for NumberOperation!");
            }
        }

        private void VisitAttributeExpression(ExpressionContext[] expressionContext)
        {
            if (expressionContext.ElementAt(0).something() == null)
                throw new ArgumentException("Attribute method used badly!");
            if (expressionContext.ElementAt(1).something() == null)
                throw new ArgumentException("Attribute method used badly!");
            if (expressionContext.ElementAt(0).something().character() == null)
                throw new ArgumentException("Attribute method used badly!");
            if (expressionContext.ElementAt(1).something().possibleAttributes() == null)
                throw new ArgumentException("Attribute method used badly!");
        }
    }
}
