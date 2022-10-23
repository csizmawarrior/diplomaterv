using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using LabWork1github.static_constants;
using static LabWork1github.DynamicEnemyGrammarParser;

namespace LabWork1github
{
    public class ExpressionVisitor : DynamicEnemyGrammarBaseVisitor<object>
    {

        public BoolExpressionContext BoolExpressionContext { get; set; }

        public string ErrorList { get; set; } = "";
        public bool CheckFailed { get; set; } = false;
        public bool NumberCheckFailed { get; set; } = false;
        public string CharacterType { get; set; }

        public ExpressionVisitor(BoolExpressionContext context, string myType)
        {
            BoolExpressionContext = context;
            CharacterType = myType;
        }
        
        public void CheckBool(BoolExpressionContext context)
        {
            this.Visit(context);
        }
        //this is a visitor so it will be called for the nextBoolExpression as well if present
        public override object VisitBoolExpression([NotNull] BoolExpressionContext context)
        {
            if(context.numberExpression() != null && context.numberExpression().Length > 1)
            {
                if(context.numToBoolOperation().NUMCOMPARE() != null)
                {
                    if (!IsNumberExpressionNumber(context.numberExpression().ElementAt(0)) ||
                        !IsNumberExpressionNumber(context.numberExpression().ElementAt(1)))
                    {
                        ErrorList += ErrorMessages.ExpressionError.NOT_NUMBER_EXPRESSION_HANDLED_AS_NUMBER;
                        ErrorList += context.GetText() + "\n";
                        CheckFailed = true;
                    }
                    return base.VisitBoolExpression(context);
                }
                if (context.numToBoolOperation().COMPARE() != null)
                {
                    if(context.numberExpression().ElementAt(0).numberExpression() != null ||
                        context.numberExpression().ElementAt(1).numberExpression() != null)
                    {
                        if (!IsNumberExpressionNumber(context.numberExpression().ElementAt(0)) ||
                        !IsNumberExpressionNumber(context.numberExpression().ElementAt(1)))
                        {
                            ErrorList += ErrorMessages.ExpressionError.NOT_NUMBER_EXPRESSIONS_HANDLED_AS_NUMBER;
                            ErrorList += context.GetText() + "\n";
                            CheckFailed = true;
                        }
                    }
                    if(IsNumberExpressionNumber(context.numberExpression().ElementAt(0)) !=
                        IsNumberExpressionNumber(context.numberExpression().ElementAt(1)))
                    {
                        ErrorList += ErrorMessages.ExpressionError.NOT_NUMBER_EXPRESSION_COMPARED_WITH_NUMBER;
                        ErrorList += context.GetText() + "\n";
                        CheckFailed = true;
                    }
                    if (IsNumberExpressionNumber(context.numberExpression().ElementAt(0)) &&
                        IsNumberExpressionNumber(context.numberExpression().ElementAt(1)))
                        return base.VisitBoolExpression(context);


                    //if a NumberExpression is not a number, then there is no "nextNumberExpression" and such
                    //and there must be an attribute, if there isn't then a check failed and we mustn't check
                    //a failed expression again
                    if(context.numberExpression().ElementAt(0).something().attribute() != null &&
                        context.numberExpression().ElementAt(1).something().attribute() != null)
                    {
                        if (context.numberExpression().ElementAt(0).something().attribute()
                            .possibleAttributes().Equals("type") ||
                           context.numberExpression().ElementAt(0).something().attribute()
                            .possibleAttributes().Equals("spawn_type"))
                        {
                            if ( ! (context.numberExpression().ElementAt(1).something().attribute()
                                .possibleAttributes().Equals("type") ||
                                context.numberExpression().ElementAt(1).something().attribute()
                                .possibleAttributes().Equals("spawn_type")))
                            {
                                ErrorList += ErrorMessages.ExpressionError.TYPE_COMPARED_WITH_OTHER_ATTRIBUTE;
                                ErrorList += context.GetText() + "\n";
                                CheckFailed = true;
                            }
                        }

                        if (context.numberExpression().ElementAt(0).something().attribute()
                            .possibleAttributes().Equals("place") || context.numberExpression().ElementAt(0)
                            .something().attribute().possibleAttributes().Equals("spawn_place") ||
                            context.numberExpression().ElementAt(0).something().attribute()
                            .possibleAttributes().Equals("teleport_place"))
                        {
                            if ( ! (context.numberExpression().ElementAt(1).something().attribute()
                                .possibleAttributes().Equals("type") || context.numberExpression().ElementAt(1)
                                .something().attribute().possibleAttributes().Equals("spawn_type") ||
                                context.numberExpression().ElementAt(1).something().attribute()
                                .possibleAttributes().Equals("teleport_place")))
                            {
                                ErrorList += ErrorMessages.ExpressionError.PLACE_COMPARED_WITH_OTHER_ATTRIBUTE;
                                ErrorList += context.GetText() + "\n";
                                CheckFailed = true;
                            }
                        }
                        if(context.numberExpression().ElementAt(0).something().attribute()
                            .possibleAttributes().Equals("name") ||
                           context.numberExpression().ElementAt(0).something().attribute()
                            .possibleAttributes().Equals("name"))
                        {
                            if( ! (context.numberExpression().ElementAt(1).something().attribute()
                            .possibleAttributes().Equals("name") ||
                           context.numberExpression().ElementAt(1).something().attribute()
                            .possibleAttributes().Equals("name") ))
                            {
                                ErrorList += ErrorMessages.ExpressionError.NAME_COMPARED_WITH_OTHER_ATTRIBUTE;
                                ErrorList += context.GetText() + "\n";
                                CheckFailed = true;
                            }
                        }
                    }
                }
            }
            return base.VisitBoolExpression(context);
        }
        public bool IsNumberExpressionNumber([NotNull] NumberExpressionContext context)
        {
            if(context.ABSOLUTESTART() != null)
                if ( ! IsNumberExpressionNumber(context.numberExpression().ElementAt(0)))
                {
                    ErrorList += ErrorMessages.ExpressionError.NOT_NUMBER_EXPRESSION_HANDLED_AS_NUMBER;
                    ErrorList += context.GetText() + "\n";
                    CheckFailed = true;
                    return false;
                }
            if (context.PARENTHESISSTART() != null || context.ABSOLUTESTART() != null)
                return IsNumberExpressionNumber(context.numberExpression().ElementAt(0));
            if(context.NUMCONNECTERADD() != null || context.NUMCONNECTERMULTIP() != null)
            {
                if( ! IsNumberExpressionNumber(context.numberExpression().ElementAt(0)) ||
                    !IsNumberExpressionNumber(context.numberExpression().ElementAt(1)))
                {
                    ErrorList += ErrorMessages.ExpressionError.NOT_NUMBER_EXPRESSIONS_HANDLED_AS_NUMBER;
                    ErrorList += context.GetText() + "\n";
                    CheckFailed = true;
                }
                if(context.NUMCONNECTERMULTIP() != null && (context.NUMCONNECTERMULTIP().GetText().Equals("%") ||
                    context.NUMCONNECTERMULTIP().GetText().Equals("/")))
                    if (!IsNumberExpressionNumber(context.numberExpression().ElementAt(1)))
                    {
                        ErrorList += ErrorMessages.ExpressionError.DIVIDING_WITH_ZERO;
                        ErrorList += context.numberExpression().ElementAt(1).GetText() + "\n";
                        CheckFailed = true;
                    }
                return IsNumberExpressionNumber(context.numberExpression().ElementAt(0)) &&
                    IsNumberExpressionNumber(context.numberExpression().ElementAt(1));
            }
            if(context.something() != null)
                return IsSomethingNumber(context.something());
            return false;
        }
        public bool IsSomethingNumber([NotNull] SomethingContext context)
        {
            if (context.NUMBER() != null)
            {
                int output;
                if (int.TryParse(context.NUMBER().GetText(), out output))
                {
                    return true;
                }
                double outputDouble;
                if (double.TryParse(context.NUMBER().GetText(), out outputDouble))
                {
                    return true;
                }
            }
            //because then it is ROUND
            if (context.attribute() == null && context.ROUND() != null)
                return true;

            if (context.attribute().possibleAttributes().GetText().Equals("type") ||
                context.attribute().possibleAttributes().GetText().Equals("spwan_type") ||
                context.attribute().possibleAttributes().GetText().Equals("place") ||
                context.attribute().possibleAttributes().GetText().Equals("spawn_place") ||
                context.attribute().possibleAttributes().GetText().Equals("teleport_place") ||
                context.attribute().possibleAttributes().GetText().Equals("name"))
            {
                    return false;
            }
            //We consider that the VisitAttribute
            return true;
        }

        public override object VisitAttribute([NotNull] AttributeContext context)
        {
            if (context.character().GetText().Equals(Types.MONSTER) || (context.character().GetText().Equals(Types.ME) && CharacterType.Equals(Types.MONSTER)) )
            {
                if (!(context.possibleAttributes().GetText().Equals("place") || context.possibleAttributes().GetText().Equals("health") ||
                    context.possibleAttributes().GetText().Equals("damage") || context.possibleAttributes().GetText().Equals("type") ||
                    context.possibleAttributes().GetText().Equals("name")))
                {
                    if (context.possibleAttributes().possibleAttributes() != null && context.possibleAttributes().possibleAttributes().Length > 1)
                    {
                        if (!(context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("place") ||
                            context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("type")))
                        {
                            ErrorList += ErrorMessages.ExpressionError.MONSTER_DOES_NOT_HAVE_THIS_ATTRIBUTE;
                            ErrorList += context.GetText() + "\n";
                            CheckFailed = true;
                        }
                    }
                    else
                    {
                        ErrorList += ErrorMessages.ExpressionError.MONSTER_DOES_NOT_HAVE_THIS_ATTRIBUTE;
                        ErrorList += context.GetText() + "\n";
                        CheckFailed = true;
                    }
                }
            }
            if (context.character().GetText().Equals(Types.PLAYER))
            {
                if (!(context.possibleAttributes().GetText().Equals("place") || context.possibleAttributes().GetText().Equals("health") ||
                    context.possibleAttributes().GetText().Equals("damage") || context.possibleAttributes().GetText().Equals("name")))
                {
                    if (context.possibleAttributes().possibleAttributes() != null && context.possibleAttributes().possibleAttributes().Length > 1)
                    {
                        if (!context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("place"))
                        {
                            ErrorList += ErrorMessages.ExpressionError.PLAYER_DOES_NOT_HAVE_THIS_ATTRIBUTE;
                            ErrorList += context.GetText() + "\n";
                            CheckFailed = true;
                        }
                    }
                    else
                    {
                        ErrorList += ErrorMessages.ExpressionError.PLAYER_DOES_NOT_HAVE_THIS_ATTRIBUTE;
                        ErrorList += context.GetText() + "\n";
                        CheckFailed = true;
                    }
                }
            }
            if (context.character().GetText().Equals(Types.PARTNER))
            {
                if (!(context.possibleAttributes().GetText().Equals("place") || context.possibleAttributes().GetText().Equals("damage") ||
                      context.possibleAttributes().GetText().Equals("type") || context.possibleAttributes().GetText().Equals("teleport_place") ||
                      context.possibleAttributes().GetText().Equals("spawn_place") || context.possibleAttributes().GetText().Equals("spawn_type") ||
                      context.possibleAttributes().GetText().Equals("heal") || context.possibleAttributes().GetText().Equals("health") ||
                      context.possibleAttributes().GetText().Equals("name")))
                {
                    if (context.possibleAttributes().possibleAttributes() != null && context.possibleAttributes().possibleAttributes().Length > 1)
                    {
                        if (!(context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("place") ||
                                context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("type") ||
                                context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("spawn_type")))
                        {
                            ErrorList += ErrorMessages.ExpressionError.NOBODY_HAS_THIS_ATTRIBUTE;
                            ErrorList += context.GetText() + "\n";
                            CheckFailed = true;
                        }
                    }
                    else
                    {
                        ErrorList += ErrorMessages.ExpressionError.NOBODY_HAS_THIS_ATTRIBUTE;
                        ErrorList += context.GetText() + "\n";
                        CheckFailed = true;
                    }
                }
            }
            if (context.character().GetText().Equals(Types.TRAP) || (context.character().GetText().Equals(Types.ME) && CharacterType.Equals(Types.TRAP)) )
            {
                if (!(context.possibleAttributes().GetText().Equals("place") || context.possibleAttributes().GetText().Equals("damage") || 
                    context.possibleAttributes().GetText().Equals("type") || context.possibleAttributes().GetText().Equals("teleport_place") || 
                    context.possibleAttributes().GetText().Equals("spawn_place") ||  context.possibleAttributes().GetText().Equals("spawn_type") ||
                    context.possibleAttributes().GetText().Equals("heal") || context.possibleAttributes().GetText().Equals("name")))
                {
                    if (context.possibleAttributes().possibleAttributes() != null && context.possibleAttributes().possibleAttributes().Length > 1)
                    {
                        if(! (context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("place") ||
                            context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("teleport_place") ||
                            context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("spawn_place") ||
                            context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("type") ||
                            context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("spawn_type")))
                        {
                            ErrorList += ErrorMessages.ExpressionError.TRAP_DOES_NOT_HAVE_THIS_ATTRIBUTE;
                            ErrorList += context.GetText() + "\n";
                            CheckFailed = true;
                        }
                    }
                    else
                    {
                        ErrorList += ErrorMessages.ExpressionError.TRAP_DOES_NOT_HAVE_THIS_ATTRIBUTE;
                        ErrorList += context.GetText() + "\n";
                        CheckFailed = true;
                    }
                }
            }
            if (context.possibleAttributes() != null)
                if (context.possibleAttributes().possibleAttributes() != null && context.possibleAttributes().possibleAttributes().Length > 1)
                {
                    if (context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("place") || 
                        context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("teleport_place") ||
                        context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("spawn_place"))
                    {
                        if (!(context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("x") ||
                                    context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("y")))
                        {
                                ErrorList += ErrorMessages.ExpressionError.PLACE_DOES_NOT_HAVE_THIS_ATTRIBUTE;
                                ErrorList += context.GetText() + "\n";
                                CheckFailed = true;
                        }
                    }
                    else
        //by this we rerstrict the deepness of the reference for types, no need for further levels, the same things can be represented like this as well
        //and it would not be logical to have e.g. type.type.type, or spawn_type.type they should return the same type as the first one anyway
        //we can't ensure now that it will be a trap or a monster referred to under type, or a player, so the error for this can only be provided in runtime
                    if (context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("spawn_type"))
                    {
                        if (!(context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("health") ||
                            context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("damage") ||
                            context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("name")))
                        {
                            ErrorList += ErrorMessages.ExpressionError.MONSTER_DOES_NOT_HAVE_THIS_ATTRIBUTE;
                            ErrorList += context.GetText() + "\n";
                            CheckFailed = true;
                        }
                    }
                    if (context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("type"))
                    {
                        if (!(context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("health") ||
                            context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("heal") ||
                            context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("damage") ||
                            context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("name")))
                        {
                            ErrorList += ErrorMessages.ExpressionError.ENEMY_DOES_NOT_HAVE_THIS_ATTRIBUTE;
                            ErrorList += context.GetText() + "\n";
                            CheckFailed = true;
                        }
                    }
                }
                return base.VisitAttribute(context);
        }
    }
}