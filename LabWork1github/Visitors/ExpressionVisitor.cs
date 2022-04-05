using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using static LabWork1github.DynamicEnemyGrammarParser;

namespace LabWork1github
{
    public class ExpressionVisitor : DynamicEnemyGrammarBaseVisitor<object>
    {

        public BoolExpressionContext BoolExpressionContext { get; set; }

        public string ErrorList { get; set; } = "";
        public bool CheckFailed { get; set; } = false;
        public bool NumberCheckFailed { get; set; } = false;
        public string characterType { get; set; }

        public ExpressionVisitor(BoolExpressionContext context, string myType)
        {
            BoolExpressionContext = context;
            characterType = myType;
        }

        public void CheckTypes(BoolExpressionContext Excontext)
        {
            
        }
        
        public void CheckBool(BoolExpressionContext context)
        {
            this.Visit(context);
        }

        public override object VisitBoolExpression([NotNull] BoolExpressionContext context)
        {
            if(context.attribute().Length > 1)
            {
                //this way they can only be compared if both of them are a charactertype
                if((context.attribute().ElementAt(0).possibleAttributes().GetText().Equals("type") ||
                    context.attribute().ElementAt(0).possibleAttributes().GetText().Equals("spawn_type")) 
                    && context.attribute().ElementAt(0).possibleAttributes().possibleAttributes().Length == 0)

                    if (!((context.attribute().ElementAt(1).possibleAttributes().GetText().Equals("type") ||
                    context.attribute().ElementAt(1).possibleAttributes().GetText().Equals("spawn_type"))
                    && context.attribute().ElementAt(1).possibleAttributes().possibleAttributes().Length == 0))
                    {
                        ErrorList += "Type compared with a different type of attribue:\n";
                        ErrorList += context.GetText() + "\n";
                        CheckFailed = true;
                    }
                //this way they can only be compared if both of them are a place
                if ((context.attribute().ElementAt(0).possibleAttributes().GetText().Equals("place") ||
                    context.attribute().ElementAt(0).possibleAttributes().GetText().Equals("teleport_place") ||
                    context.attribute().ElementAt(0).possibleAttributes().GetText().Equals("spawn_place"))
                    && context.attribute().ElementAt(0).possibleAttributes().possibleAttributes().Length == 0)
                    if (!((context.attribute().ElementAt(1).possibleAttributes().GetText().Equals("place") ||
                        context.attribute().ElementAt(1).possibleAttributes().GetText().Equals("teleport_type") ||
                    context.attribute().ElementAt(1).possibleAttributes().GetText().Equals("spawn_place"))
                    && context.attribute().ElementAt(1).possibleAttributes().possibleAttributes().Length == 0))
                    {
                        ErrorList += "Place compared with a different type of attribue:\n";
                        ErrorList += context.GetText() + "\n";
                        CheckFailed = true;
                    }
            }
            return base.VisitBoolExpression(context);
        }

        public override object VisitNumberFirstExpression([NotNull] NumberFirstExpressionContext context)
        {
            if(context.something() == null || context.something().attribute() == null)
                return base.VisitNumberFirstExpression(context);
            if (context.something().attribute().possibleAttributes().GetText().Equals("type") ||
                context.something().attribute().possibleAttributes().GetText().Equals("spwan_type") ||
                context.something().attribute().possibleAttributes().GetText().Equals("place") ||
                context.something().attribute().possibleAttributes().GetText().Equals("spawn_place") ||
                context.something().attribute().possibleAttributes().GetText().Equals("teleport_place"))
                    if(context.something().attribute().possibleAttributes().possibleAttributes().Length < 2)
                {
                    ErrorList += "Place or type in iself is not a number:\n";
                    ErrorList += context.GetText() + "\n";
                    CheckFailed = true;
                }
            return base.VisitNumberFirstExpression(context);
        }

        public override object VisitAttribute([NotNull] AttributeContext context)
        {
            if (context.character().GetText().Equals(Types.MONSTER) || characterType.Equals(Types.MONSTER))
            {
                if (!(context.possibleAttributes().GetText().Equals("place") || context.possibleAttributes().GetText().Equals("health") ||
                    context.possibleAttributes().GetText().Equals("damage") || context.possibleAttributes().GetText().Equals("type")))
                {
                    ErrorList += "Monster not having this attribute:\n";
                    ErrorList += context.GetText() + "\n";
                    CheckFailed = true;
                }
            }
            if (context.character().GetText().Equals(Types.PLAYER))
            {
                if (!(context.possibleAttributes().GetText().Equals("place") || context.possibleAttributes().GetText().Equals("health") ||
                    context.possibleAttributes().GetText().Equals("damage")))
                {
                    ErrorList += "Player not having this attribute:\n";
                    ErrorList += context.GetText() + "\n";
                    CheckFailed = true;
                }
            }
            if (context.character().GetText().Equals(Types.TRAP) || characterType.Equals(Types.TRAP))
            {
                if (!(context.possibleAttributes().GetText().Equals("place") || context.possibleAttributes().GetText().Equals("damage") || 
                    context.possibleAttributes().GetText().Equals("type") || context.possibleAttributes().GetText().Equals("teleport_place") || 
                    context.possibleAttributes().GetText().Equals("spawn_place") ||  context.possibleAttributes().GetText().Equals("spawn_type") ||
                    context.possibleAttributes().GetText().Equals("heal")))
                {
                    ErrorList += "Trap not having this attribute:\n";
                    ErrorList += context.GetText() + "\n";
                    CheckFailed = true;
                }
            }
            if (context.possibleAttributes() != null)
                if (context.possibleAttributes().possibleAttributes().Length > 0)
                {
                    if (context.possibleAttributes().GetText().Equals("place") || context.possibleAttributes().GetText().Equals("teleport_place") ||
                        context.possibleAttributes().GetText().Equals("spawn_place"))
                    {
                        if (!(context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("x") ||
                                    context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("y")))
                        {
                                ErrorList += "Place doesn't have other attributes:\n";
                                ErrorList += context.GetText() + "\n";
                                CheckFailed = true;
                        }
                    }
                    else
        //we can't ensure now that it will be a trap or a monster referred to under type, or a player, so the error for this can only be provided in runtime
                    if (context.possibleAttributes().GetText().Equals("spawn_type") || context.possibleAttributes().GetText().Equals("type"))
                    {
                        if (!(context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("health") ||
                            context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("heal") ||
                            context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("damage")))
                        {
                            ErrorList += "Enemy types do not have this attribute:\n";
                            ErrorList += context.GetText() + "\n";
                            CheckFailed = true;
                        }
                    }
        
                }
                return base.VisitAttribute(context);
        }























        //private void CheckAttribute(BoolExpressionContext expressionContext)
        //{
        //    //if (!(expressionContext.boolExpression().ElementAt(0).something().character() != null && expressionContext.boolExpression().ElementAt(1).something().possibleAttributes() != null))
        //    //{
        //    //    ErrorList += "not a valid attribute:\n";
        //    //    ErrorList += expressionContext.GetText() + "\n";
        //    //    NumberCheckFailed = true;
        //    //    return;
        //    //}
        //    //string attribute = expressionContext.expression().ElementAt(1).something().possibleAttributes().GetText() + "\n";
        //    //switch (attribute)
        //    //{
        //    //    case "x":
        //    //        return;
        //    //    case "y":
        //    //        return;
        //    //    case "health":
        //    //        return;
        //    //    case "heal":
        //    //        if(expressionContext.expression().ElementAt(0).something().character().TRAP() != null)
        //    //            return;
        //    //        else
        //    //        {
        //    //            ErrorList += "A non trap wants to heal:\n";
        //    //            ErrorList += expressionContext.GetText() + "\n";
        //    //            NumberCheckFailed = true;
        //    //            return;
        //    //        }
        //    //    case "damage":
        //    //        return;
        //    //    case "teleport.x":
        //    //        if (expressionContext.expression().ElementAt(0).something().character().TRAP() != null)
        //    //            return;
        //    //        else
        //    //        {
        //    //            ErrorList += "A non trap wants to teleport:\n";
        //    //            ErrorList += expressionContext.GetText() + "\n";
        //    //            NumberCheckFailed = true;
        //    //            return;
        //    //        }
        //    //    case "teleport.y":
        //    //        if (expressionContext.expression().ElementAt(0).something().character().TRAP() != null)
        //    //            return;
        //    //        else
        //    //        {
        //    //            ErrorList += "A non trap wants to teleport:\n";
        //    //            ErrorList += expressionContext.GetText() + "\n";
        //    //            NumberCheckFailed = true;
        //    //            return;
        //    //        }
        //    //    case "spawn.x":
        //    //        if (expressionContext.expression().ElementAt(0).something().character().TRAP() != null)
        //    //            return;
        //    //        else
        //    //        {
        //    //            ErrorList += "A non trap wants to spawn:\n";
        //    //            ErrorList += expressionContext.GetText() + "\n";
        //    //            NumberCheckFailed = true;
        //    //            return;
        //    //        }
        //    //    case "spawn.y":
        //    //        if (expressionContext.expression().ElementAt(0).something().character().TRAP() != null)
        //    //            return;
        //    //        else
        //    //        {
        //    //            ErrorList += "A non trap wants to spawn:\n";
        //    //            ErrorList += expressionContext.GetText() + "\n";
        //    //            NumberCheckFailed = true;
        //    //            return;
        //    //        }
        //    //    default:
        //    //        {
        //    //            ErrorList += "Unrecognized attribute:\n";
        //    //            ErrorList += expressionContext.GetText() + "\n";
        //    //            NumberCheckFailed = true;
        //    //            return;
        //    //        }
        //    //}
        //}

        public void CheckNumber(BoolExpressionContext context)
        {
            //if (context.ABSOLUTE().ToList().Count > 0)
            //{
            //    var helpCheck = false;
            //    if (NumberCheckFailed)
            //        helpCheck = true;
            //    NumberCheckFailed = false;
            //    CheckNumber(context.expression().ElementAt(0));
            //    if(NumberCheckFailed)
            //    {
            //        ErrorList += "Attribute used on a non Number:\n";
            //        ErrorList += context.GetText() + "\n";
            //    }
            //    if (helpCheck)
            //        NumberCheckFailed = true;
            //}
            //if (context.PARENTHESISSTART() != null)
            //{
            //    var helpCheck = false;
            //    if (NumberCheckFailed)
            //        helpCheck = true;
            //    NumberCheckFailed = false;
            //    CheckNumber(context.expression().ElementAt(0));
            //    if (NumberCheckFailed)
            //    {
            //        ErrorList += "Attribute used on a non Number:\n";
            //        ErrorList += context.GetText() + "\n";
            //    }
            //    if (helpCheck)
            //        NumberCheckFailed = true;
            //}
            //if (context.NEGATE() != null)
            //{
            //    ErrorList += "Negating on a number:\n";
            //    ErrorList += context.GetText() + "\n";
            //    NumberCheckFailed = true;
            //}
            //if (context.operation() == null && context.something() != null)
            //{
            //    if (!(context.something().NUMBER() != null || context.something().ROUND() != null))
            //    {
            //        ErrorList += "A non number is being used as a number:\n";
            //        ErrorList += context.GetText() + "\n";
            //        NumberCheckFailed = true;
            //    }
            //}
            //if ((context.PARENTHESISSTART() == null && context.NEGATE() == null && context.ABSOLUTE().ToList().Count == 0) && (context.operation() == null && context.something() == null))
            //{
            //    ErrorList += "Not recognized number expression:\n";
            //    ErrorList += context.GetText() + "\n";
            //    NumberCheckFailed = true;
            //}
            //if (context.operation() != null)
            //{
            //    if (context.operation().BOOLCONNECTER() != null || context.operation().NEAR() != null || context.operation().ALIVE() != null
            //         || context.operation().NUMCOMPARE() != null || context.operation().COMPARE() != null)
            //    {
            //        ErrorList += "Bool value found, number expected, realized by operation:\n";
            //        ErrorList += context.GetText() + "\n";
            //        NumberCheckFailed = true;
            //    }
            //    if (context.operation().DOT() != null)
            //    {
            //        var helpCheck = false;
            //        if (NumberCheckFailed)
            //            helpCheck = true;
            //        NumberCheckFailed = false;
            //        CheckAttribute(context);
            //        if (NumberCheckFailed)
            //        {
            //            ErrorList += "Attribute is not valid:\n";
            //            ErrorList += context.GetText() + "\n";
            //            NumberCheckFailed = true;
            //        }
            //        if (helpCheck)
            //            NumberCheckFailed = true;
            //    }
            //    if (context.operation().NUMCONNECTER() != null)
            //        if (context.expression().Count() > 1)
            //        {
            //            var helpCheck = false;
            //            if (NumberCheckFailed)
            //                helpCheck = true;
            //            NumberCheckFailed = false;
            //            CheckNumber(context.expression().ElementAt(0));
            //            if (!NumberCheckFailed)
            //                CheckNumber(context.expression().ElementAt(1));
            //            if (NumberCheckFailed)
            //            {
            //                ErrorList += "Number Connecter having non number parameter:\n";
            //                ErrorList += context.GetText() + "\n";
            //            }
            //            if (helpCheck)
            //                NumberCheckFailed = true;
            //        }
            //        else
            //        {
            //            ErrorList += "Not enough parameter for numconnecter:\n";
            //            ErrorList += context.GetText() + "\n";
            //            NumberCheckFailed = true;
            //        }
            //}
        }
    }
}