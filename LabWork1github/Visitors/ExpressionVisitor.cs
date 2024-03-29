﻿using System;
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
                    bool isFirstExpressionNumber = IsNumberExpressionNumber(context.numberExpression().ElementAt(0));
                    bool isSecondExpressionNumber = IsNumberExpressionNumber(context.numberExpression().ElementAt(1));
                    if (((context.numberExpression().ElementAt(0).numberExpression() != null && context.numberExpression().ElementAt(0).numberExpression().Length > 0) ||
                        (context.numberExpression().ElementAt(1).numberExpression() != null && context.numberExpression().ElementAt(1).numberExpression().Length > 0))
                        && (!isFirstExpressionNumber || !isSecondExpressionNumber ))
                        {
                            ErrorList += ErrorMessages.ExpressionError.NOT_NUMBER_EXPRESSIONS_HANDLED_AS_NUMBER;
                            ErrorList += context.GetText() + "\n";
                            CheckFailed = true;
                            return base.VisitBoolExpression(context);
                        }
                    if(isFirstExpressionNumber != isSecondExpressionNumber )
                    {
                        ErrorList += ErrorMessages.ExpressionError.NOT_NUMBER_EXPRESSION_COMPARED_WITH_NUMBER;
                        ErrorList += context.GetText() + "\n";
                        CheckFailed = true;
                        return base.VisitBoolExpression(context);
                    }
                    if (isFirstExpressionNumber && isSecondExpressionNumber)
                        return base.VisitBoolExpression(context);


                    //if a NumberExpression is not a number, then it can be only a place,
                    //a type or a name, or there is an error
                    if(context.numberExpression().ElementAt(0).something().attribute() != null &&
                        context.numberExpression().ElementAt(1).something().attribute() != null)
                    {
                        if ((context.numberExpression().ElementAt(0).something().attribute()
                            .possibleAttributes().GetText().Equals("type") ||
                           context.numberExpression().ElementAt(0).something().attribute()
                            .possibleAttributes().GetText().Equals("spawn_type")) 
                            &&
                            ( ! (context.numberExpression().ElementAt(1).something().attribute()
                                .possibleAttributes().GetText().Equals("type") ||
                                context.numberExpression().ElementAt(1).something().attribute()
                                .possibleAttributes().GetText().Equals("spawn_type"))))
                            {
                                ErrorList += ErrorMessages.ExpressionError.TYPE_COMPARED_WITH_OTHER_ATTRIBUTE;
                                ErrorList += context.GetText() + "\n";
                                CheckFailed = true;
                            }

                        if ((context.numberExpression().ElementAt(0).something().attribute()
                            .possibleAttributes().GetText().Equals("place") || context.numberExpression().ElementAt(0)
                            .something().attribute().possibleAttributes().GetText().Equals("spawn_place") ||
                            context.numberExpression().ElementAt(0).something().attribute()
                            .possibleAttributes().GetText().Equals("teleport_place"))
                            &&
                            ( ! (context.numberExpression().ElementAt(1).something().attribute()
                                .possibleAttributes().GetText().Equals("place") || context.numberExpression().ElementAt(1)
                                .something().attribute().possibleAttributes().GetText().Equals("spawn_place") ||
                                context.numberExpression().ElementAt(1).something().attribute()
                                .possibleAttributes().GetText().Equals("teleport_place"))))
                            {
                                ErrorList += ErrorMessages.ExpressionError.PLACE_COMPARED_WITH_OTHER_ATTRIBUTE;
                                ErrorList += context.GetText() + "\n";
                                CheckFailed = true;
                            }
                        if((context.numberExpression().ElementAt(0).something().attribute()
                            .possibleAttributes().GetText().Equals("name") ||
                           (context.numberExpression().ElementAt(0).something().attribute()
                            .possibleAttributes().possibleAttributes() != null &&
                            context.numberExpression().ElementAt(0).something().attribute()
                            .possibleAttributes().possibleAttributes().Length > 1 &&
                           context.numberExpression().ElementAt(0).something().attribute()
                            .possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("name") ))
                            &&
                            ( ! (context.numberExpression().ElementAt(1).something().attribute()
                            .possibleAttributes().GetText().Equals("name") ||
                            (context.numberExpression().ElementAt(1).something().attribute()
                            .possibleAttributes().possibleAttributes() != null &&
                            context.numberExpression().ElementAt(1).something().attribute()
                            .possibleAttributes().possibleAttributes().Length > 1 &&
                           context.numberExpression().ElementAt(1).something().attribute()
                            .possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("name")))))
                            {
                                ErrorList += ErrorMessages.ExpressionError.NAME_COMPARED_WITH_OTHER_ATTRIBUTE;
                                ErrorList += context.GetText() + "\n";
                                CheckFailed = true;
                            }
                    }
                }
            }
            return base.VisitBoolExpression(context);
        }
        public bool IsNumberExpressionNumber([NotNull] NumberExpressionContext context)
        {
            if((context.ABSOLUTESTART() != null) &&
                ( ! IsNumberExpressionNumber(context.numberExpression().ElementAt(0))))
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
                bool isFirstExpressionNumber = IsNumberExpressionNumber(context.numberExpression().ElementAt(0));
                bool isSecondExpressionNumber = IsNumberExpressionNumber(context.numberExpression().ElementAt(1));
                if ( ! isFirstExpressionNumber || !isSecondExpressionNumber)
                {
                    ErrorList += ErrorMessages.ExpressionError.NOT_NUMBER_EXPRESSIONS_HANDLED_AS_NUMBER;
                    ErrorList += context.GetText() + "\n";
                    CheckFailed = true;
                }
                return isFirstExpressionNumber && isSecondExpressionNumber;
            }
            if(context.something() != null)
                return IsSomethingNumber(context.something());
            return false;
        }
        public bool IsSomethingNumber([NotNull] SomethingContext context)
        {
            if (context.NUMBER() != null)
            {
                double outputDouble;
                if (double.TryParse(context.NUMBER().GetText(), out outputDouble))
                {
                    return true;
                }
                else
                {
                    int output;
                    if (int.TryParse(context.NUMBER().GetText(), out output))
                    {
                        return true;
                    }
                }
            }
            if (context.attribute() == null && context.ROUND() != null)
                return true;

            if (context.attribute().possibleAttributes().GetText().Equals("health") ||
                context.attribute().possibleAttributes().GetText().Equals("heal") ||
                context.attribute().possibleAttributes().GetText().Equals("damage") ||
                (context.attribute().possibleAttributes().possibleAttributes() != null &&
                context.attribute().possibleAttributes().possibleAttributes().Length > 1 &&
                (((context.attribute().possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("type") ||
                context.attribute().possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("spawn_type")) &&
                (context.attribute().possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("health") ||
                context.attribute().possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("heal") ||
                context.attribute().possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("damage"))) || 
                ((context.attribute().possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("place") ||
                context.attribute().possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("spawn_place") ||
                context.attribute().possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("teleport_place")) &&
                (context.attribute().possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("x") ||
                context.attribute().possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("y"))))))
            {
                    return true;
            }
            
            return false;
        }

        public override object VisitAttribute([NotNull] AttributeContext context)
        {
            if ((context.character().GetText().Equals(Types.MONSTER) || (context.character().GetText().Equals(Types.ME) && CharacterType.Equals(Types.MONSTER)) )
                &&
                (!(context.possibleAttributes().GetText().Equals("place") || context.possibleAttributes().GetText().Equals("health") ||
                    context.possibleAttributes().GetText().Equals("damage") || context.possibleAttributes().GetText().Equals("type") ||
                    context.possibleAttributes().GetText().Equals("name"))))
            {
                    if ((context.possibleAttributes().possibleAttributes() != null && context.possibleAttributes().possibleAttributes().Length > 1) && context.possibleAttributes().possibleAttributes().Length > 1)
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
            if ((context.character().GetText().Equals(Types.PLAYER)) &&
                (!(context.possibleAttributes().GetText().Equals("place") || context.possibleAttributes().GetText().Equals("health") ||
                    context.possibleAttributes().GetText().Equals("damage") || context.possibleAttributes().GetText().Equals("name"))))
                {
                    if ((context.possibleAttributes().possibleAttributes() != null && context.possibleAttributes().possibleAttributes().Length > 1) && context.possibleAttributes().possibleAttributes().Length > 1)
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
            if ((context.character().GetText().Equals(Types.PARTNER)) &&
                (!(context.possibleAttributes().GetText().Equals("place") || context.possibleAttributes().GetText().Equals("damage") ||
                      context.possibleAttributes().GetText().Equals("type") || context.possibleAttributes().GetText().Equals("teleport_place") ||
                      context.possibleAttributes().GetText().Equals("spawn_place") || context.possibleAttributes().GetText().Equals("spawn_type") ||
                      context.possibleAttributes().GetText().Equals("heal") || context.possibleAttributes().GetText().Equals("health") ||
                      context.possibleAttributes().GetText().Equals("name"))))
                {
                    if ((context.possibleAttributes().possibleAttributes() != null && context.possibleAttributes().possibleAttributes().Length > 1) && context.possibleAttributes().possibleAttributes().Length > 1)
                    {
                        if (!(context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("place") ||
                                context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("teleport_place") ||
                                context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("spawn_place") ||
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
            if ((context.character().GetText().Equals(Types.TRAP) || (context.character().GetText().Equals(Types.ME) && CharacterType.Equals(Types.TRAP)) )
                &&
                (!(context.possibleAttributes().GetText().Equals("place") || context.possibleAttributes().GetText().Equals("damage") || 
                    context.possibleAttributes().GetText().Equals("type") || context.possibleAttributes().GetText().Equals("teleport_place") || 
                    context.possibleAttributes().GetText().Equals("spawn_place") ||  context.possibleAttributes().GetText().Equals("spawn_type") ||
                    context.possibleAttributes().GetText().Equals("heal") || context.possibleAttributes().GetText().Equals("name"))))
                {
                    if ((context.possibleAttributes().possibleAttributes() != null && context.possibleAttributes().possibleAttributes().Length > 1) && context.possibleAttributes().possibleAttributes().Length > 1)
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
            if ((context.possibleAttributes() != null) &&
                ((context.possibleAttributes().possibleAttributes() != null && context.possibleAttributes().possibleAttributes().Length > 1) && context.possibleAttributes().possibleAttributes().Length > 1))
                {
                    if ((context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("place") || 
                        context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("teleport_place") ||
                        context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("spawn_place"))
                        &&
                        (!(context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("x") ||
                                    context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("y"))))
                        {
                                ErrorList += ErrorMessages.ExpressionError.PLACE_DOES_NOT_HAVE_THIS_ATTRIBUTE;
                                ErrorList += context.GetText() + "\n";
                                CheckFailed = true;
                        }
                    if ((context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("spawn_type"))
                        &&
                        (!(context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("health") ||
                            context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("damage") ||
                            context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("name"))))
                        {
                            ErrorList += ErrorMessages.ExpressionError.MONSTER_DOES_NOT_HAVE_THIS_ATTRIBUTE;
                            ErrorList += context.GetText() + "\n";
                            CheckFailed = true;
                        }
                    if ((context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("type"))
                        &&
                        (!(context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("health") ||
                            context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("heal") ||
                            context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("damage") ||
                            context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("name"))))
                        {
                            ErrorList += ErrorMessages.ExpressionError.ENEMY_DOES_NOT_HAVE_THIS_ATTRIBUTE;
                            ErrorList += context.GetText() + "\n";
                            CheckFailed = true;
                        }
                }
                return base.VisitAttribute(context);
        }
    }
}