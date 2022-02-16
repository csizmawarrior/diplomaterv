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

        public string ErrorList { get; set; } = "";
        public bool BoolCompareFailed { get; set; } = false;
        public bool NumberCompareFailed { get; set; } = false;

        public ExpressionVisitor(ExpressionContext context)
        {
            ExpressionContext = context;
        }

        public void CheckTypes(ExpressionContext Excontext)
        {
            //try
            //{
            BoolCompareFailed = false;
                CheckBool(Excontext.expression().ElementAt(0));
            if(!BoolCompareFailed)
                CheckBool(Excontext.expression().ElementAt(1));
            if (BoolCompareFailed)
            {
                CheckNumber(Excontext.expression().ElementAt(0));
                if(!NumberCompareFailed)
                CheckNumber(Excontext.expression().ElementAt(1));
            }
            //}
            //catch(Exception e){
            //    if (!(e is InvalidOperationException || e is ArgumentException))
            //        throw e;
            //    try
            //    {
            //        CheckNumber(Excontext.expression().ElementAt(0));
            //        CheckNumber(Excontext.expression().ElementAt(1));
            //    }
            //    catch(Exception e2)
            //    {
            //        ErrorList += "error on Both CheckNumber and on CheckBool";
            //    }
            //}
        }
        
        public void CheckBool(ExpressionContext context)
        {
            if (context.ABSOLUTE().ToList().Count > 0)
            {
                ErrorList += "Absolute around bool expression:\n";
                ErrorList += context.GetText();
                BoolCompareFailed = true;
            }
            if (context.PARENTHESISSTART() != null || context.NEGATE() != null)
            {
                BoolCompareFailed = false;
                CheckBool(context.expression().ElementAt(0));
            }
            if (context.operation() == null)
                if (context.something() == null) {
                    ErrorList += "Input not recognized as an expression:\n";
                    ErrorList += context.GetText();
                    BoolCompareFailed = true;
                }
                else
                    throw new InvalidOperationException("unexpted input");
            if (context.operation().ALIVE() != null || context.operation().NEAR() != null)
            {
                if ((context.expression().ElementAt(1).something().NOTHING() != null))
                    if (context.expression().ElementAt(0).something().character() != null)
                        return;
                    else
                        throw new InvalidOperationException("unexpted input"); //TODO: operation about characters, without character
                else
                    throw new InvalidOperationException("unexpted input");
            }
            if (context.operation().NUMCONNECTER() != null)
                throw new InvalidOperationException("bool expected, number found!");
            if (context.operation().BOOLCONNECTER() != null)
            {
                CheckBool(context.expression().ElementAt(0));
                CheckBool(context.expression().ElementAt(1));
            }
            if (context.operation().NUMCOMPARE() != null)
            {
                CheckNumber(context.expression().ElementAt(0));
                CheckNumber(context.expression().ElementAt(1));
            }
            if (context.operation().ATTRIBUTE() != null)
            {
                throw new InvalidOperationException("bool expression expected, ATTRIBUTE found");
            }
            if (context.operation().COMPARE() != null)
                CheckTypes(context);
        }

        private void CheckAttribute(ExpressionContext expressionContext)
        {
            if (!(expressionContext.expression().ElementAt(0).something().character() != null && expressionContext.expression().ElementAt(1).something().possibleAttributes() != null))
                throw new ArgumentException("Attribute operation used incorrectly");
            string attribute = expressionContext.expression().ElementAt(1).something().possibleAttributes().GetText();
            switch (attribute)
            {
                case "x":
                    return;
                case "y":
                    return;
                case "health":
                    return;
                case "heal":
                    if(expressionContext.expression().ElementAt(0).something().character().TRAP() != null)
                        return;
                    else
                        throw new ArgumentException("Attribute operation used incorrectly");
                case "damage":
                    return;
                case "teleport.x":
                    if (expressionContext.expression().ElementAt(0).something().character().TRAP() != null)
                        return;
                    else
                        throw new ArgumentException("Attribute operation used incorrectly");
                case "teleport.y":
                    if (expressionContext.expression().ElementAt(0).something().character().TRAP() != null)
                        return;
                    else
                        throw new ArgumentException("Attribute operation used incorrectly");
                case "spawn.x":
                    if (expressionContext.expression().ElementAt(0).something().character().TRAP() != null)
                        return;
                    else
                        throw new ArgumentException("Attribute operation used incorrectly");
                case "spawn.y":
                    if (expressionContext.expression().ElementAt(0).something().character().TRAP() != null)
                        return;
                    else
                        throw new ArgumentException("Attribute operation used incorrectly");
                default:
                    throw new ArgumentException("Attribute operation used incorrectly");
            }
        }

        public void CheckNumber(ExpressionContext context)
        {
            if (context.ABSOLUTE().ToList().Count > 0)
                //TODO: check if it works this way or not
                CheckNumber(context.expression().ElementAt(0));
            if (context.PARENTHESISSTART() != null)
            {
                CheckNumber(context.expression().ElementAt(0));
            }
            if (context.NEGATE() != null)
                throw new InvalidOperationException("Number exprected, bool found");
            if(context.operation() == null && context.something() != null)
            {
                if (context.something().NUMBER() != null)
                    return;
                if (context.something().ROUND() != null)
                    return;
                throw new InvalidOperationException("unexpected expression");
            }
            if (context.operation() == null)
                return;
            if(context.operation().BOOLCONNECTER() != null || context.operation().NEAR() != null || context.operation().ALIVE() != null
                 || context.operation().NUMCOMPARE() != null || context.operation().COMPARE() != null)
            {
                throw new InvalidOperationException("Number exprected, bool found");
            }
            if (context.operation().ATTRIBUTE() != null)
                CheckAttribute(context);
            if (context.operation().NUMCONNECTER() != null)
                if (context.expression().Count() > 1)
                {
                    CheckNumber(context.expression().ElementAt(0));
                    CheckNumber(context.expression().ElementAt(1));
                }
                else
                    throw new InvalidOperationException("invalid number of arguments");
        }

    }
}