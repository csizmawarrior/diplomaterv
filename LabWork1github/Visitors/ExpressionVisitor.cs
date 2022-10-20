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
                    if(context.numberExpression().ElementAt(0).nextNumberExpression() != null ||
                        context.numberExpression().ElementAt(1).nextNumberExpression() != null)
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
                    if(context.numberExpression().ElementAt(0).numberMultipExpression()
                        .numberFirstExpression().something().attribute() != null &&
                        context.numberExpression().ElementAt(1).numberMultipExpression()
                        .numberFirstExpression().something().attribute() != null)
                    {
                        if (context.numberExpression().ElementAt(0).numberMultipExpression().numberFirstExpression()
                            .something().attribute().possibleAttributes().Equals("type") ||
                           context.numberExpression().ElementAt(0).numberMultipExpression().numberFirstExpression()
                            .something().attribute().possibleAttributes().Equals("spawn_type"))
                        {
                            if ( ! (context.numberExpression().ElementAt(1).numberMultipExpression().numberFirstExpression()
                                    .something().attribute().possibleAttributes().Equals("type") ||
                                context.numberExpression().ElementAt(1).numberMultipExpression().numberFirstExpression()
                                    .something().attribute().possibleAttributes().Equals("spawn_type")))
                            {
                                ErrorList += ErrorMessages.ExpressionError.TYPE_COMPARED_WITH_OTHER_ATTRIBUTE;
                                ErrorList += context.GetText() + "\n";
                                CheckFailed = true;
                            }
                        }

                        if (context.numberExpression().ElementAt(0).numberMultipExpression().numberFirstExpression()
                            .something().attribute().possibleAttributes().Equals("place") ||
                           context.numberExpression().ElementAt(0).numberMultipExpression().numberFirstExpression()
                            .something().attribute().possibleAttributes().Equals("spawn_place") ||
                            context.numberExpression().ElementAt(0).numberMultipExpression().numberFirstExpression()
                            .something().attribute().possibleAttributes().Equals("teleport_place"))
                        {
                            if ( ! (context.numberExpression().ElementAt(1).numberMultipExpression().numberFirstExpression()
                                    .something().attribute().possibleAttributes().Equals("type") ||
                                context.numberExpression().ElementAt(1).numberMultipExpression().numberFirstExpression()
                                    .something().attribute().possibleAttributes().Equals("spawn_type") ||
                                context.numberExpression().ElementAt(1).numberMultipExpression().numberFirstExpression()
                                    .something().attribute().possibleAttributes().Equals("teleport_place")))
                            {
                                ErrorList += ErrorMessages.ExpressionError.PLACE_COMPARED_WITH_OTHER_ATTRIBUTE;
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
            bool isfirstExpressionNumber = IsNumberMultipExpressionNumber(context.numberMultipExpression(), true);
            if (context.nextNumberExpression() != null)
            {
                if (!isfirstExpressionNumber ||
                        !IsNumberExpressionNumber(context.nextNumberExpression().numberExpression()))
                {
                    ErrorList += ErrorMessages.ExpressionError.NOT_NUMBER_EXPRESSIONS_HANDLED_AS_NUMBER;
                    ErrorList += context.GetText() + "\n";
                    CheckFailed = true;
                    return false;
                }
            }
            return isfirstExpressionNumber;
        }
        public bool IsNumberMultipExpressionNumber([NotNull] NumberMultipExpressionContext context, bool isZeroAllowed)
        {
            bool isfirstExpressionNumber = IsNumberFirstExpressionValidNumber(context.numberFirstExpression(), isZeroAllowed);
            if(context.nextNumberMultipExpression() != null)
            {
                if (context.nextNumberMultipExpression().NUMCONNECTERMULTIP().GetText().Equals("*"))
                {
                    if (!isfirstExpressionNumber ||
                        !IsNumberMultipExpressionNumber(context.nextNumberMultipExpression().numberMultipExpression(), true))
                    {
                        ErrorList += ErrorMessages.ExpressionError.NOT_NUMBER_EXPRESSIONS_HANDLED_AS_NUMBER;
                        ErrorList += context.GetText() + "\n";
                        CheckFailed = true;
                        return false;
                    }
                }
                else
                {
                    if (!isfirstExpressionNumber ||
                        !IsNumberMultipExpressionNumber(context.nextNumberMultipExpression().numberMultipExpression(), false))
                    {
                        ErrorList += ErrorMessages.ExpressionError.NOT_NUMBER_EXPRESSIONS_HANDLED_AS_NUMBER;
                        ErrorList += context.GetText() + "\n";
                        CheckFailed = true;
                        return false;
                    }
                }
            }
            return isfirstExpressionNumber;
        }
        public bool IsNumberFirstExpressionValidNumber([NotNull] NumberFirstExpressionContext context, bool isZeroAllowed)
        {
            if(context.PARENTHESISSTART() != null || (context.ABSOLUTESTART() != null && context.ABSOLUTEEND() != null))
            {
                bool isInsideExpressionNumber = IsNumberExpressionNumber(context.numberExpression());
                if (!isInsideExpressionNumber && (context.ABSOLUTESTART() != null && context.ABSOLUTEEND() != null))
                {
                    ErrorList += ErrorMessages.ExpressionError.NOT_NUMBER_EXPRESSION_HANDLED_AS_NUMBER;
                    ErrorList += context.GetText() + "\n";
                    CheckFailed = true;
                    return false;
                }
                return isInsideExpressionNumber;
            }
            if (context.something().NUMBER() != null)
            {
                int output;
                if (int.TryParse(context.something().NUMBER().GetText(), out output))
                {
                    if (output == 0)
                    {
                        if (!isZeroAllowed)
                        {
                            ErrorList += ErrorMessages.ExpressionError.DIVIDING_WITH_ZERO;
                            CheckFailed = true;
                        }
                        return isZeroAllowed;
                    }
                }
                double outputDouble;
                if (double.TryParse(context.something().NUMBER().GetText(), out outputDouble))
                {
                    if (output == 0.0)
                    {
                        if (isZeroAllowed)
                        {
                            ErrorList += ErrorMessages.ExpressionError.DIVIDING_WITH_ZERO;
                            CheckFailed = true;
                        }
                        return isZeroAllowed;
                    }
                }
            }
            //because then it is ROUND
            if (context.something().attribute() == null)
                return true;

            if (context.something().attribute().possibleAttributes().GetText().Equals("type") ||
                context.something().attribute().possibleAttributes().GetText().Equals("spwan_type") ||
                context.something().attribute().possibleAttributes().GetText().Equals("place") ||
                context.something().attribute().possibleAttributes().GetText().Equals("spawn_place") ||
                context.something().attribute().possibleAttributes().GetText().Equals("teleport_place"))
            {
                    return false;
            }
            return true;
        }

        public override object VisitAttribute([NotNull] AttributeContext context)
        {
            if (context.character().GetText().Equals(Types.MONSTER) || (context.character().GetText().Equals(Types.ME) && CharacterType.Equals(Types.MONSTER)) )
            {
                if (!(context.possibleAttributes().GetText().Equals("place") || context.possibleAttributes().GetText().Equals("health") ||
                    context.possibleAttributes().GetText().Equals("damage") || context.possibleAttributes().GetText().Equals("type")))
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
                    context.possibleAttributes().GetText().Equals("damage")))
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
                      context.possibleAttributes().GetText().Equals("heal") || context.possibleAttributes().GetText().Equals("health")))
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
                    context.possibleAttributes().GetText().Equals("heal")))
                {
                    if (context.possibleAttributes().possibleAttributes() != null && context.possibleAttributes().possibleAttributes().Length > 1)
                    {
                        if(! (context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("place") ||
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
                            context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("damage")))
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
                            context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("damage")))
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