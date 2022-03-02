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
        public bool BoolCheckFailed { get; set; } = false;
        public bool NumberCheckFailed { get; set; } = false;

        public ExpressionVisitor(ExpressionContext context)
        {
            ExpressionContext = context;
        }

        public void CheckTypes(ExpressionContext Excontext)
        {
            //try
            //{
            BoolCheckFailed = false;
                CheckBool(Excontext.expression().ElementAt(0));
            if(!BoolCheckFailed)
                CheckBool(Excontext.expression().ElementAt(1));
            if (BoolCheckFailed)
            {
                NumberCheckFailed = false;
                CheckNumber(Excontext.expression().ElementAt(0));
                if (!NumberCheckFailed)
                    CheckNumber(Excontext.expression().ElementAt(1));
                if(NumberCheckFailed)
                {
                    ErrorList += "Compare type check failed:\n";
                    ErrorList += Excontext.GetText() + "\n";
                }
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
                ErrorList += context.GetText() + "\n";
                BoolCheckFailed = true;
            }
            if (context.PARENTHESISSTART() != null || context.NEGATE() != null)
            {
                var helpCheck = false;
                if (BoolCheckFailed)
                    helpCheck = true;
                BoolCheckFailed = false;
                CheckBool(context.expression().ElementAt(0));
                if (BoolCheckFailed)
                {
                    ErrorList += "Bool expected, something else found:\n";
                    ErrorList += context.GetText() + "\n";
                }
                if (helpCheck)
                    BoolCheckFailed = true;
            }
            if ((context.PARENTHESISSTART() == null && context.NEGATE() == null && context.ABSOLUTE().ToList().Count == 0) && (context.operation()==null && context.something() ==null))
            {
                ErrorList += "Input not recognized as an expression:\n";
                ErrorList += context.GetText() + "\n";
                BoolCheckFailed = true;
            }
            if (context.operation() != null)
            {
                if (context.operation().ALIVE() != null || context.operation().NEAR() != null)
                {
                    if ((context.expression().ElementAt(1).something().NOTHING() != null))
                        if (context.expression().ElementAt(0).something().character() != null)
                        { }
                        else
                        {
                            ErrorList += "Character operation without character:\n";
                            ErrorList += context.GetText() + "\n";
                            BoolCheckFailed = true;
                        }
                    else
                    {
                        ErrorList += "Character operation with to many parameters:\n";
                        ErrorList += context.GetText() + "\n";
                        BoolCheckFailed = true;
                    }
                }
                if (context.operation().NUMCONNECTER() != null)
                {
                    ErrorList += "Number operation on bool value:\n";
                    ErrorList += context.GetText() + "\n";
                    BoolCheckFailed = true;
                }
                if (context.operation().BOOLCONNECTER() != null)
                {
                    var helpCheck = false;
                    if (BoolCheckFailed)
                        helpCheck = true;
                    BoolCheckFailed = false;
                    CheckBool(context.expression().ElementAt(0));
                    if (!BoolCheckFailed)
                        CheckBool(context.expression().ElementAt(1));
                    if (BoolCheckFailed)
                    {
                        ErrorList += "Bool connecter type check failed:\n";
                        ErrorList += context.GetText() + "\n";
                    }
                    if (helpCheck)
                        BoolCheckFailed = true;
                }
                if (context.operation().NUMCOMPARE() != null)
                {
                    var helpCheck = false;
                    if (NumberCheckFailed)
                        helpCheck = true;
                    NumberCheckFailed = false;
                    CheckNumber(context.expression().ElementAt(0));
                    if (!NumberCheckFailed)
                        CheckNumber(context.expression().ElementAt(1));
                    if (NumberCheckFailed)
                    {
                        ErrorList += "Number Compare type check failed:\n";
                        ErrorList += context.GetText() + "\n";
                    }
                    if (helpCheck)
                        NumberCheckFailed = true;
                }
                if (context.operation().DOT() != null)
                {
                    ErrorList += "Bool expected, attribute found:\n";
                    ErrorList += context.GetText() + "\n";
                    BoolCheckFailed = true;
                }
                if (context.operation().COMPARE() != null)
                    CheckTypes(context);
            }
        }
        //TODO: rethink and redo attribute handling because of changes, 3. gyak
        private void CheckAttribute(ExpressionContext expressionContext)
        {
            if (!(expressionContext.expression().ElementAt(0).something().character() != null && expressionContext.expression().ElementAt(1).something().possibleAttributes() != null))
            {
                ErrorList += "not a valid attribute:\n";
                ErrorList += expressionContext.GetText() + "\n";
                NumberCheckFailed = true;
                return;
            }
            string attribute = expressionContext.expression().ElementAt(1).something().possibleAttributes().GetText() + "\n";
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
                    {
                        ErrorList += "A non trap wants to heal:\n";
                        ErrorList += expressionContext.GetText() + "\n";
                        NumberCheckFailed = true;
                        return;
                    }
                case "damage":
                    return;
                case "teleport.x":
                    if (expressionContext.expression().ElementAt(0).something().character().TRAP() != null)
                        return;
                    else
                    {
                        ErrorList += "A non trap wants to teleport:\n";
                        ErrorList += expressionContext.GetText() + "\n";
                        NumberCheckFailed = true;
                        return;
                    }
                case "teleport.y":
                    if (expressionContext.expression().ElementAt(0).something().character().TRAP() != null)
                        return;
                    else
                    {
                        ErrorList += "A non trap wants to teleport:\n";
                        ErrorList += expressionContext.GetText() + "\n";
                        NumberCheckFailed = true;
                        return;
                    }
                case "spawn.x":
                    if (expressionContext.expression().ElementAt(0).something().character().TRAP() != null)
                        return;
                    else
                    {
                        ErrorList += "A non trap wants to spawn:\n";
                        ErrorList += expressionContext.GetText() + "\n";
                        NumberCheckFailed = true;
                        return;
                    }
                case "spawn.y":
                    if (expressionContext.expression().ElementAt(0).something().character().TRAP() != null)
                        return;
                    else
                    {
                        ErrorList += "A non trap wants to spawn:\n";
                        ErrorList += expressionContext.GetText() + "\n";
                        NumberCheckFailed = true;
                        return;
                    }
                default:
                    {
                        ErrorList += "Unrecognized attribute:\n";
                        ErrorList += expressionContext.GetText() + "\n";
                        NumberCheckFailed = true;
                        return;
                    }
            }
        }

        public void CheckNumber(ExpressionContext context)
        {
            if (context.ABSOLUTE().ToList().Count > 0)
            {
                var helpCheck = false;
                if (NumberCheckFailed)
                    helpCheck = true;
                NumberCheckFailed = false;
                CheckNumber(context.expression().ElementAt(0));
                if(NumberCheckFailed)
                {
                    ErrorList += "Attribute used on a non Number:\n";
                    ErrorList += context.GetText() + "\n";
                }
                if (helpCheck)
                    NumberCheckFailed = true;
            }
            if (context.PARENTHESISSTART() != null)
            {
                var helpCheck = false;
                if (NumberCheckFailed)
                    helpCheck = true;
                NumberCheckFailed = false;
                CheckNumber(context.expression().ElementAt(0));
                if (NumberCheckFailed)
                {
                    ErrorList += "Attribute used on a non Number:\n";
                    ErrorList += context.GetText() + "\n";
                }
                if (helpCheck)
                    NumberCheckFailed = true;
            }
            if (context.NEGATE() != null)
            {
                ErrorList += "Negating on a number:\n";
                ErrorList += context.GetText() + "\n";
                NumberCheckFailed = true;
            }
            if (context.operation() == null && context.something() != null)
            {
                if (!(context.something().NUMBER() != null || context.something().ROUND() != null))
                {
                    ErrorList += "A non number is being used as a number:\n";
                    ErrorList += context.GetText() + "\n";
                    NumberCheckFailed = true;
                }
            }
            if ((context.PARENTHESISSTART() == null && context.NEGATE() == null && context.ABSOLUTE().ToList().Count == 0) && (context.operation() == null && context.something() == null))
            {
                ErrorList += "Not recognized number expression:\n";
                ErrorList += context.GetText() + "\n";
                NumberCheckFailed = true;
            }
            if (context.operation() != null)
            {
                if (context.operation().BOOLCONNECTER() != null || context.operation().NEAR() != null || context.operation().ALIVE() != null
                     || context.operation().NUMCOMPARE() != null || context.operation().COMPARE() != null)
                {
                    ErrorList += "Bool value found, number expected, realized by operation:\n";
                    ErrorList += context.GetText() + "\n";
                    NumberCheckFailed = true;
                }
                if (context.operation().DOT() != null)
                {
                    var helpCheck = false;
                    if (NumberCheckFailed)
                        helpCheck = true;
                    NumberCheckFailed = false;
                    CheckAttribute(context);
                    if (NumberCheckFailed)
                    {
                        ErrorList += "Attribute is not valid:\n";
                        ErrorList += context.GetText() + "\n";
                        NumberCheckFailed = true;
                    }
                    if (helpCheck)
                        NumberCheckFailed = true;
                }
                if (context.operation().NUMCONNECTER() != null)
                    if (context.expression().Count() > 1)
                    {
                        var helpCheck = false;
                        if (NumberCheckFailed)
                            helpCheck = true;
                        NumberCheckFailed = false;
                        CheckNumber(context.expression().ElementAt(0));
                        if (!NumberCheckFailed)
                            CheckNumber(context.expression().ElementAt(1));
                        if (NumberCheckFailed)
                        {
                            ErrorList += "Number Connecter having non number parameter:\n";
                            ErrorList += context.GetText() + "\n";
                        }
                        if (helpCheck)
                            NumberCheckFailed = true;
                    }
                    else
                    {
                        ErrorList += "Not enough parameter for numconnecter:\n";
                        ErrorList += context.GetText() + "\n";
                        NumberCheckFailed = true;
                    }
            }
        }

    }
}