using LabWork1github.static_constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LabWork1github.DynamicEnemyGrammarParser;

namespace LabWork1github.Visitors
{
    class ConditionVisitor : DynamicEnemyGrammarBaseVisitor<object>
    {   //TODO: Partner integration
        //TODO: While refactoring pay attention to possible type errors
        //TODO: Check double int mismatches or just remove double-ness, it has to be integrated to a lot of places
        public BoolExpressionContext BoolExpressionContext { get; set; }

        public GameParamProvider Provider { get; set; }

        public string ErrorList { get; set; }

        public bool ErrorFound { get; set; }

        public List<double> NumberList { get; set; } = new List<double>();
        public List<MathSymbol> SymbolList { get; set; } = new List<MathSymbol>();

        public ConditionVisitor(GameParamProvider provider, BoolExpressionContext context)
        { 
            this.BoolExpressionContext = context;
            this.Provider = provider;
        }
        public bool CheckConditions()
        {
            ErrorList = "";
            ErrorFound = false;
            return CheckBoolExpression(BoolExpressionContext);
        }
        public bool CheckBoolExpression(BoolExpressionContext context)
        {
            bool expressionValue = false;

            if (context.PARENTHESISSTART() != null)
            {
                expressionValue = CheckBoolExpression(context.boolExpression());
            }
            if (context.NEGATE() != null)
            {
                expressionValue = !CheckBoolExpression(context.boolExpression());
            }
            if (context.functionExpression() != null) 
            {
                expressionValue = CheckFunctionExpression(context.functionExpression());
            }
            if(context.numberExpression() != null && context.numberExpression().Length > 1)
            {
                if (context.numToBoolOperation().NUMCOMPARE() != null)
                    expressionValue = CheckNumCompareExpression(context);
                if (context.numToBoolOperation().COMPARE() != null)
                    expressionValue = CheckCompareExpression(context);
            }
            if (context.nextBoolExpression() != null)
            {
                expressionValue = CheckNextBoolExpression(expressionValue, context.nextBoolExpression());
            }
            return expressionValue;
        }

        private bool CheckNumCompareExpression(BoolExpressionContext context)
        {
            if (context.numToBoolOperation().NUMCOMPARE().Equals("<"))
                return CheckNumberExpression(context.numberExpression().ElementAt(0)) < CheckNumberExpression(context.numberExpression().ElementAt(1));
            else
                return CheckNumberExpression(context.numberExpression().ElementAt(0)) > CheckNumberExpression(context.numberExpression().ElementAt(1));
        }

        public bool CheckCompareExpression(BoolExpressionContext context)
        {
            if (context.numToBoolOperation().COMPARE().Equals("!="))
                return CheckNumberOrAttributeExpressionNotEquals(context.numberExpression().ElementAt(0), context.numberExpression().ElementAt(1));
            else
                return CheckNumberOrAttributeExpressionEquals(context.numberExpression().ElementAt(0), context.numberExpression().ElementAt(1));
        }

        private bool CheckFunctionExpression(FunctionExpressionContext functionExpressionContext)
        {
            if (functionExpressionContext.function().NEAR() != null)
            {
                if (functionExpressionContext.character().ME() != null)
                    return true;
                if (functionExpressionContext.character().MONSTER() != null)
                {
                    if (Math.Abs(Provider.GetMe().Place.X - Provider.GetMonster().Place.X) <= Provider.GetNear())
                    {
                        if (Math.Abs(Provider.GetMe().Place.Y - Provider.GetMonster().Place.Y) <= Provider.GetNear())
                            return true;
                    }
                }
                if (functionExpressionContext.character().PARTNER() != null)
                {
                    if (Provider.GetPartner() == null)
                        return false;
                    if (Math.Abs(Provider.GetMe().Place.X - Provider.GetPartner().Place.X) <= Provider.GetNear())
                    {
                        if (Math.Abs(Provider.GetMe().Place.Y - Provider.GetPartner().Place.Y) <= Provider.GetNear())
                            return true;
                    }
                }
                if (functionExpressionContext.character().TRAP() != null)
                {
                    if (Math.Abs(Provider.GetMe().Place.X - Provider.GetTrap().Place.X) <= Provider.GetNear())
                    {
                        if (Math.Abs(Provider.GetMe().Place.Y - Provider.GetTrap().Place.Y) <= Provider.GetNear())
                            return true;
                    }
                }
                if (functionExpressionContext.character().PLAYER() != null)
                {
                    if (Math.Abs(Provider.GetMe().Place.X - Provider.GetPlayer().Place.X) <= Provider.GetNear())
                    {
                        if (Math.Abs(Provider.GetMe().Place.Y - Provider.GetPlayer().Place.Y) <= Provider.GetNear())
                            return true;
                    }
                }

            }
            if(functionExpressionContext.function().ALIVE() != null)
            {
                if (functionExpressionContext.character().ME() != null)
                    return Provider.GetMe().GetHealth() > 0;
                if (functionExpressionContext.character().MONSTER() != null)
                    return Provider.GetMonster().GetHealth() > 0;
                if (functionExpressionContext.character().TRAP() != null)
                    return true;
                if (functionExpressionContext.character().PLAYER() != null)
                    return true;
                if (functionExpressionContext.character().PARTNER() != null)
                    return (Provider.GetPartner() != null && Provider.GetPartner().GetHealth() > 0);
            }
            return false;
        }

        public bool CheckNextBoolExpression(bool boolExp, NextBoolExpressionContext context)
        {
            if (context.BOOLCONNECTER().Equals("||"))
                return boolExp || CheckBoolExpression(context.boolExpression());
            else
                return boolExp && CheckBoolExpression(context.boolExpression());
        }

        public double CheckNumberExpression(NumberExpressionContext context)
        {
            if (context.PARENTHESISSTART() != null)
                return CheckNumberExpression(context.numberExpression().ElementAt(0));
            if(context.ABSOLUTESTART() != null)
            {
                return Math.Abs(CheckNumberExpression(context.numberExpression().ElementAt(0)));
            }
            if(context.NUMCONNECTERMULTIP() != null)
            {
                if (context.NUMCONNECTERMULTIP().GetText().Equals("*"))
                    return CheckNumberExpression(context.numberExpression().ElementAt(0)) *
                            CheckNumberExpression(context.numberExpression().ElementAt(1));
                if (context.NUMCONNECTERMULTIP().GetText().Equals("/"))
                    return CheckNumberExpression(context.numberExpression().ElementAt(0)) /
                            CheckNumberExpression(context.numberExpression().ElementAt(1));
                if (context.NUMCONNECTERMULTIP().GetText().Equals("%"))
                    return CheckNumberExpression(context.numberExpression().ElementAt(0)) %
                            CheckNumberExpression(context.numberExpression().ElementAt(1));
            }
            if(context.NUMCONNECTERADD() != null)
            {
                if(context.NUMCONNECTERADD().GetText().Equals("+"))
                    return CheckNumberExpression(context.numberExpression().ElementAt(0)) +
                            CheckNumberExpression(context.numberExpression().ElementAt(1));
                if (context.NUMCONNECTERADD().GetText().Equals("-"))
                    return CheckNumberExpression(context.numberExpression().ElementAt(0)) -
                            CheckNumberExpression(context.numberExpression().ElementAt(1));
            }
            if (context.something() != null)
                return CheckNumberSomething(context.something());
            ErrorList += ErrorMessages.ConditionError.UNRECOGNIZED_EXPRESSION;
            ErrorList += (context.GetText() + "\n");
            ErrorFound = true;
            return -1;
        }

        private double CheckNumberSomething(SomethingContext context)
        {
            if (context.ROUND() != null)
                return Provider.GetRound();

            if(context.NUMBER() != null)
            {
                double outputDouble;
                if (double.TryParse(context.NUMBER().GetText(), out outputDouble))
                {
                    return outputDouble;
                }
            }
                
            if (context.attribute() != null)
            {
                return CheckNumberAttributeExpression(context.attribute());
            }

            ErrorList += ErrorMessages.ConditionError.UNRECOGNIZED_EXPRESSION;
            ErrorList += (context.GetText() + "\n");
            ErrorFound = true;
            return -1;
        }

        public double CheckNumberAttributeExpression(AttributeContext context)
        {
            if (context.character().PLAYER() != null)
            {
                return PlayerNumberAttribute(context);
            }
            if (context.character().MONSTER() != null)
            {
                return MonsterNumberAttribute(context);
            }
            if(context.character().TRAP() != null)
            {
                return TrapNumberAttribute(context);
            }
            if(context.character().ME() != null)
            {
                return MeNumberAttribute(context);
            }
            if(context.character().PARTNER() != null)
            {
                if (Provider.GetPartner() == null)
                {
                    ErrorList += ErrorMessages.ConditionError.UNRECOGNIZED_ATTRIBUTE_ERROR + Provider.GetMe().Partner;
                    ErrorList += (context.GetText() + "\n");
                    ErrorFound = true;
                    return -1;
                }
                if (Provider.GetPartner() is Monster)
                {
                    return MonsterNumberAttribute(context);
                }
                if (Provider.GetPartner() is Trap)
                {
                    return TrapNumberAttribute(context);
                }
                if (Provider.GetPartner() is Player)
                {
                    return PlayerNumberAttribute(context);
                }
            }
            ErrorList += ErrorMessages.ConditionError.UNRECOGNIZED_ATTRIBUTE_ERROR;
            ErrorList += (context.GetText() + "\n");
            ErrorFound = true;
            return -1;
        }

        public bool CheckPlaceAttributeExpression(BoolExpressionContext context)
        {
            //monster is the first character
            if(context.attribute().ElementAt(0).character().MONSTER() != null || 
                (context.attribute().ElementAt(0).character().ME() != null && Provider.GetMe().GetCharacterType() is MonsterType) ||
                (context.attribute().ElementAt(0).character().PARTNER() != null && Provider.GetPartner() != null && Provider.GetPartner() is Monster))
            {
                //monster is the first character, type is the first attribute
                if (context.attribute().ElementAt(0).possibleAttributes().GetText().Equals("type") && 
                    context.attribute().ElementAt(0).possibleAttributes().possibleAttributes().Length == 0)
                {
                    if (context.attribute().ElementAt(1).character().TRAP() != null)
                    {
                        if (context.attribute().ElementAt(1).possibleAttributes().GetText().Equals("type"))
                        {
                            ErrorList += (ErrorMessages.ConditionError.TYPE_MISMATCH);
                            ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                            ErrorList += (context.GetText() + "\n");
                            ErrorFound = true;
                            return false;
                        }
                        if (context.attribute().ElementAt(0).character().MONSTER() != null)
                        {
                            if (context.COMPARE().GetText().Equals("=="))
                                return Provider.GetMonster().GetCharacterType() == Provider.GetTrap().GetCharacterType().SpawnType;
                            if (context.COMPARE().GetText().Equals("!="))
                                return Provider.GetMonster().GetCharacterType() != Provider.GetTrap().GetCharacterType().SpawnType;
                        }
                        if (context.attribute().ElementAt(0).character().ME() != null && Provider.GetMe().GetCharacterType() is MonsterType)
                        {
                            if (context.COMPARE().GetText().Equals("=="))
                                return Provider.GetMe().GetCharacterType() == Provider.GetTrap().GetCharacterType().SpawnType;
                            if (context.COMPARE().GetText().Equals("!="))
                                return Provider.GetMe().GetCharacterType() != Provider.GetTrap().GetCharacterType().SpawnType;
                        }
                        else
                        {
                            if (context.COMPARE().GetText().Equals("=="))
                                return Provider.GetPartner().GetCharacterType() == Provider.GetTrap().GetCharacterType().SpawnType;
                            if (context.COMPARE().GetText().Equals("!="))
                                return Provider.GetPartner().GetCharacterType() != Provider.GetTrap().GetCharacterType().SpawnType;
                        }
                    }
                    if(context.attribute().ElementAt(1).character().ME() != null && Provider.GetMe().GetCharacterType() is TrapType)
                    {
                        if(context.attribute().ElementAt(1).possibleAttributes().GetText().Equals("type"))
                        {
                            ErrorList += (ErrorMessages.ConditionError.TYPE_MISMATCH);
                            ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                            ErrorList += (context.GetText() + "\n");
                            ErrorFound = true;
                            return false;
                        }
                        if (context.attribute().ElementAt(0).character().MONSTER() != null)
                        {
                            if (context.COMPARE().GetText().Equals("=="))
                                return Provider.GetMonster().GetCharacterType() == Provider.GetMe().GetCharacterType().SpawnType;
                            if (context.COMPARE().GetText().Equals("!="))
                                return Provider.GetMonster().GetCharacterType() != Provider.GetMe().GetCharacterType().SpawnType;
                        }
                        else
                        {
                            ErrorList += (ErrorMessages.ConditionError.TYPE_MISMATCH_UNEXPECTED);
                            ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                            ErrorList += (context.GetText() + "\n");
                            ErrorFound = true;
                            return false;
                        }
                    }
                    if(context.attribute().ElementAt(1).character().MONSTER() != null)
                    {
                        if (context.attribute().ElementAt(0).character().MONSTER() != null)
                        {
                            if (context.COMPARE().GetText().Equals("=="))
                                return Provider.GetMonster().GetCharacterType() == Provider.GetMonster().GetCharacterType();
                            if (context.COMPARE().GetText().Equals("!="))
                                return Provider.GetMonster().GetCharacterType() != Provider.GetMonster().GetCharacterType();
                        }
                        else
                        {
                            if (context.COMPARE().GetText().Equals("=="))
                                return Provider.GetMe().GetCharacterType() == Provider.GetMonster().GetCharacterType();
                            if (context.COMPARE().GetText().Equals("!="))
                                return Provider.GetMe().GetCharacterType() != Provider.GetMonster().GetCharacterType();
                        }
                    }
                    if (context.attribute().ElementAt(1).character().ME() != null && Provider.GetMe().GetCharacterType() is MonsterType)
                    {
                        if (context.attribute().ElementAt(0).character().MONSTER() != null)
                        {
                            if (context.COMPARE().GetText().Equals("=="))
                                return Provider.GetMonster().GetCharacterType() == Provider.GetMe().GetCharacterType();
                            if (context.COMPARE().GetText().Equals("!="))
                                return Provider.GetMonster().GetCharacterType() != Provider.GetMe().GetCharacterType();
                        }
                        else
                        {
                            if (context.COMPARE().GetText().Equals("=="))
                                return Provider.GetMe().GetCharacterType() == Provider.GetMe().GetCharacterType();
                            if (context.COMPARE().GetText().Equals("!="))
                                return Provider.GetMe().GetCharacterType() != Provider.GetMe().GetCharacterType();
                        }
                    }
                }
                //monster is the first character, place is the attribute
                if(context.attribute().ElementAt(0).possibleAttributes().GetText().Equals("place") &&
                    context.attribute().ElementAt(0).possibleAttributes().possibleAttributes().Length == 0)
                {
                    if(context.attribute().ElementAt(1).character().PLAYER() != null)
                    {
                        if (context.attribute().ElementAt(0).character().MONSTER() != null)
                        {
                            if (context.COMPARE().GetText().Equals("=="))
                                return Provider.GetMonster().Place == Provider.GetPlayer().Place;
                            if (context.COMPARE().GetText().Equals("!="))
                                return Provider.GetMonster().Place != Provider.GetPlayer().Place;
                        }
                        else
                        {
                            if (context.COMPARE().GetText().Equals("=="))
                                return Provider.GetMe().Place == Provider.GetPlayer().Place;
                            if (context.COMPARE().GetText().Equals("!="))
                                return Provider.GetMe().Place == Provider.GetPlayer().Place;
                        }
                    }
                    if(context.attribute().ElementAt(1).character().TRAP() != null)
                    {
                        if (context.attribute().ElementAt(1).possibleAttributes().GetText().Equals("place"))
                        {
                            if (context.attribute().ElementAt(0).character().MONSTER() != null)
                            {
                                if (context.COMPARE().GetText().Equals("=="))
                                    return Provider.GetMonster().Place == Provider.GetTrap().Place;
                                if (context.COMPARE().GetText().Equals("!="))
                                    return Provider.GetMonster().Place != Provider.GetTrap().Place;
                            }
                            else
                            {
                                if (context.COMPARE().GetText().Equals("=="))
                                    return Provider.GetMe().Place == Provider.GetTrap().Place;
                                if (context.COMPARE().GetText().Equals("!="))
                                    return Provider.GetMe().Place == Provider.GetTrap().Place;
                            }
                        }
                        if (context.attribute().ElementAt(1).possibleAttributes().GetText().Equals("teleport_place"))
                        {
                            if (context.attribute().ElementAt(0).character().MONSTER() != null)
                            {
                                if (context.COMPARE().GetText().Equals("=="))
                                    return Provider.GetMonster().Place == Provider.GetTrap().GetCharacterType().TeleportPlace;
                                if (context.COMPARE().GetText().Equals("!="))
                                    return Provider.GetMonster().Place != Provider.GetTrap().GetCharacterType().TeleportPlace;
                            }
                            else
                            {
                                if (context.COMPARE().GetText().Equals("=="))
                                    return Provider.GetMe().Place == Provider.GetTrap().GetCharacterType().TeleportPlace;
                                if (context.COMPARE().GetText().Equals("!="))
                                    return Provider.GetMe().Place == Provider.GetTrap().GetCharacterType().TeleportPlace;
                            }
                        }
                        if (context.attribute().ElementAt(1).possibleAttributes().GetText().Equals("spawn_place"))
                        {
                            if (context.attribute().ElementAt(0).character().MONSTER() != null)
                            {
                                if (context.COMPARE().GetText().Equals("=="))
                                    return Provider.GetMonster().Place == Provider.GetTrap().GetCharacterType().SpawnPlace;
                                if (context.COMPARE().GetText().Equals("!="))
                                    return Provider.GetMonster().Place != Provider.GetTrap().GetCharacterType().SpawnPlace;
                            }
                            else
                            {
                                if (context.COMPARE().GetText().Equals("=="))
                                    return Provider.GetMe().Place == Provider.GetTrap().GetCharacterType().SpawnPlace;
                                if (context.COMPARE().GetText().Equals("!="))
                                    return Provider.GetMe().Place == Provider.GetTrap().GetCharacterType().SpawnPlace;
                            }
                        }
                    }
                    if(context.attribute().ElementAt(1).character().ME() != null && Provider.GetMe().GetCharacterType() is TrapType)
                    {
                        if (context.attribute().ElementAt(1).possibleAttributes().GetText().Equals("place")) { 
                            if (context.attribute().ElementAt(0).character().MONSTER() != null)
                            {
                                if (context.COMPARE().GetText().Equals("=="))
                                    return Provider.GetMonster().Place == Provider.GetMe().Place;
                                if (context.COMPARE().GetText().Equals("!="))
                                    return Provider.GetMonster().Place != Provider.GetMe().Place;
                            }
                        }
                        if (context.attribute().ElementAt(1).possibleAttributes().GetText().Equals("teleport_place"))
                        {
                            if (context.attribute().ElementAt(0).character().MONSTER() != null)
                            {
                                if (context.COMPARE().GetText().Equals("=="))
                                    return Provider.GetMonster().Place == Provider.GetMe().GetCharacterType().TeleportPlace;
                                if (context.COMPARE().GetText().Equals("!="))
                                    return Provider.GetMonster().Place != Provider.GetMe().GetCharacterType().TeleportPlace;
                            }
                        }
                        if (context.attribute().ElementAt(1).possibleAttributes().GetText().Equals("spawn_place"))
                        {
                            if (context.attribute().ElementAt(0).character().MONSTER() != null)
                            {
                                if (context.COMPARE().GetText().Equals("=="))
                                    return Provider.GetMonster().Place == Provider.GetMe().GetCharacterType().SpawnPlace;
                                if (context.COMPARE().GetText().Equals("!="))
                                    return Provider.GetMonster().Place != Provider.GetMe().GetCharacterType().SpawnPlace;
                            }
                        }
                    }
                    if (context.attribute().ElementAt(1).character().MONSTER() != null)
                    {
                        if (context.attribute().ElementAt(0).character().MONSTER() != null)
                        {
                            if (context.COMPARE().GetText().Equals("=="))
                                return Provider.GetMonster().Place == Provider.GetMonster().Place;
                            if (context.COMPARE().GetText().Equals("!="))
                                return Provider.GetMonster().Place != Provider.GetMonster().Place;
                        }
                        else
                        {
                            if (context.COMPARE().GetText().Equals("=="))
                                return Provider.GetMe().Place == Provider.GetMonster().Place;
                            if (context.COMPARE().GetText().Equals("!="))
                                return Provider.GetMe().Place == Provider.GetMonster().Place;
                        }
                    }
                    if (context.attribute().ElementAt(1).character().ME() != null && Provider.GetMe().GetCharacterType() is MonsterType)
                    {
                        if (context.attribute().ElementAt(0).character().MONSTER() != null)
                        {
                            if (context.COMPARE().GetText().Equals("=="))
                                return Provider.GetMonster().Place == Provider.GetMe().Place;
                            if (context.COMPARE().GetText().Equals("!="))
                                return Provider.GetMonster().Place != Provider.GetMe().Place;
                        }
                        else
                        {
                            if (context.COMPARE().GetText().Equals("=="))
                                return Provider.GetMe().Place == Provider.GetMe().Place;
                            if (context.COMPARE().GetText().Equals("!="))
                                return Provider.GetMe().Place == Provider.GetMe().Place;
                        }
                    }
                }
                //first character is monster and the attribute isn't type or place
                else
                {
                    if (context.COMPARE().GetText().Equals("=="))
                        return CheckNumberAttributeExpression(context.attribute().ElementAt(0)) == CheckNumberAttributeExpression(context.attribute().ElementAt(1));
                    if (context.COMPARE().GetText().Equals("!="))
                        return CheckNumberAttributeExpression(context.attribute().ElementAt(0)) != CheckNumberAttributeExpression(context.attribute().ElementAt(1));
                }
            }
            //if the firt character is trap
            if (context.attribute().ElementAt(0).character().TRAP() != null ||
                (context.attribute().ElementAt(0).character().ME() != null && Provider.GetMe().GetCharacterType() is TrapType))
            {
                //first character is trap and the attribute is type
                if (context.attribute().ElementAt(0).possibleAttributes().GetText().Equals("type") &&
                    context.attribute().ElementAt(0).possibleAttributes().possibleAttributes().Length == 0)
                {
                    if (context.attribute().ElementAt(1).possibleAttributes().GetText().Equals("spawn_type"))
                    {
                        ErrorList += (ErrorMessages.ConditionError.TYPE_MISMATCH);
                        ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                        ErrorList += (context.GetText() + "\n");
                        ErrorFound = true;
                        return false;
                    }
                    if (context.attribute().ElementAt(1).character().TRAP() != null)
                    {
                        if (context.attribute().ElementAt(0).character().TRAP() != null)
                        {
                            if (context.COMPARE().GetText().Equals("=="))
                                return Provider.GetTrap().GetCharacterType() == Provider.GetTrap().GetCharacterType();
                            if (context.COMPARE().GetText().Equals("!="))
                                return Provider.GetTrap().GetCharacterType() != Provider.GetTrap().GetCharacterType();
                        }
                        else
                        {
                            if (context.COMPARE().GetText().Equals("=="))
                                return Provider.GetMe().GetCharacterType() == Provider.GetTrap().GetCharacterType();
                            if (context.COMPARE().GetText().Equals("!="))
                                return Provider.GetMe().GetCharacterType() != Provider.GetTrap().GetCharacterType();
                        }
                    }
                    else
                    if (context.attribute().ElementAt(1).character().ME() != null && Provider.GetMe().GetCharacterType() is TrapType)
                    {
                        if (context.attribute().ElementAt(0).character().TRAP() != null)
                        {
                            if (context.COMPARE().GetText().Equals("=="))
                                return Provider.GetTrap().GetCharacterType() == Provider.GetMe().GetCharacterType();
                            if (context.COMPARE().GetText().Equals("!="))
                                return Provider.GetTrap().GetCharacterType() != Provider.GetMe().GetCharacterType();
                        }
                        else
                        {
                            if (context.COMPARE().GetText().Equals("=="))
                                return Provider.GetMe().GetCharacterType() == Provider.GetMe().GetCharacterType();
                            if (context.COMPARE().GetText().Equals("!="))
                                return Provider.GetMe().GetCharacterType() != Provider.GetMe().GetCharacterType();
                        }
                    }
                    if ((context.attribute().ElementAt(1).character().ME() != null && Provider.GetMe().GetCharacterType() is MonsterType) ||
                        context.attribute().ElementAt(1).character().MONSTER() != null)
                    {
                            ErrorList += (ErrorMessages.ConditionError.TYPE_MISMATCH);
                            ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                            ErrorList += (context.GetText() + "\n");
                            ErrorFound = true;
                            return false;
                    }
                }
                //first character is trap and the attribute is spawn type
                if (context.attribute().ElementAt(0).possibleAttributes().GetText().Equals("spawn_type") &&
                    context.attribute().ElementAt(0).possibleAttributes().possibleAttributes().Length == 0)
                {
                    if (context.attribute().ElementAt(1).character().TRAP() != null)
                    {
                        if (context.attribute().ElementAt(1).possibleAttributes().GetText().Equals("type"))
                        {
                            ErrorList += (ErrorMessages.ConditionError.TYPE_MISMATCH);
                            ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                            ErrorList += (context.GetText() + "\n");
                            ErrorFound = true;
                            return false;
                        }
                        if (context.attribute().ElementAt(0).character().TRAP() != null)
                        {
                            if (context.COMPARE().GetText().Equals("=="))
                                return Provider.GetTrap().GetCharacterType().SpawnType == Provider.GetTrap().GetCharacterType().SpawnType;
                            if (context.COMPARE().GetText().Equals("!="))
                                return Provider.GetTrap().GetCharacterType().SpawnType != Provider.GetTrap().GetCharacterType().SpawnType;
                        }
                        else
                        {
                            if (context.COMPARE().GetText().Equals("=="))
                                return Provider.GetMe().GetCharacterType().SpawnType == Provider.GetTrap().GetCharacterType().SpawnType;
                            if (context.COMPARE().GetText().Equals("!="))
                                return Provider.GetMe().GetCharacterType().SpawnType != Provider.GetTrap().GetCharacterType().SpawnType;
                        }
                    }
                    else
                    if (context.attribute().ElementAt(1).character().ME() != null && Provider.GetMe().GetCharacterType() is TrapType)
                    {
                        if (context.attribute().ElementAt(1).possibleAttributes().GetText().Equals("type"))
                        {
                            ErrorList += (ErrorMessages.ConditionError.TYPE_MISMATCH);
                            ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                            ErrorList += (context.GetText() + "\n");
                            ErrorFound = true;
                            return false;
                        }
                        if (context.attribute().ElementAt(0).character().TRAP() != null)
                        {
                            if (context.COMPARE().GetText().Equals("=="))
                                return Provider.GetTrap().GetCharacterType().SpawnType == Provider.GetMe().GetCharacterType().SpawnType;
                            if (context.COMPARE().GetText().Equals("!="))
                                return Provider.GetTrap().GetCharacterType().SpawnType != Provider.GetMe().GetCharacterType().SpawnType;
                        }
                        else
                        {
                            if (context.COMPARE().GetText().Equals("=="))
                                return Provider.GetMe().GetCharacterType().SpawnType == Provider.GetMe().GetCharacterType().SpawnType;
                            if (context.COMPARE().GetText().Equals("!="))
                                return Provider.GetMe().GetCharacterType().SpawnType != Provider.GetMe().GetCharacterType().SpawnType;
                        }
                    }
                    if (context.attribute().ElementAt(1).character().MONSTER() != null)
                    {
                        if (context.attribute().ElementAt(0).character().TRAP() != null)
                        {
                            if (context.COMPARE().GetText().Equals("=="))
                                return Provider.GetTrap().GetCharacterType().SpawnType == Provider.GetMonster().GetCharacterType();
                            if (context.COMPARE().GetText().Equals("!="))
                                return Provider.GetTrap().GetCharacterType().SpawnType != Provider.GetMonster().GetCharacterType();
                        }
                        else
                        {
                            if (context.COMPARE().GetText().Equals("=="))
                                return Provider.GetTrap().GetCharacterType().SpawnType == Provider.GetMonster().GetCharacterType();
                            if (context.COMPARE().GetText().Equals("!="))
                                return Provider.GetTrap().GetCharacterType().SpawnType != Provider.GetMonster().GetCharacterType();
                        }
                    }
                    if (context.attribute().ElementAt(1).character().ME() != null && Provider.GetMe().GetCharacterType() is MonsterType)
                    {
                        if (context.attribute().ElementAt(0).character().TRAP() != null)
                        {
                            if (context.COMPARE().GetText().Equals("=="))
                                return Provider.GetTrap().GetCharacterType().SpawnType == Provider.GetMe().GetCharacterType();
                            if (context.COMPARE().GetText().Equals("!="))
                                return Provider.GetTrap().GetCharacterType().SpawnType != Provider.GetMe().GetCharacterType();
                        }
                        else
                        {
                            if (context.COMPARE().GetText().Equals("=="))
                                return Provider.GetMe().GetCharacterType().SpawnType == Provider.GetMe().GetCharacterType();
                            if (context.COMPARE().GetText().Equals("!="))
                                return Provider.GetMe().GetCharacterType().SpawnType != Provider.GetMe().GetCharacterType();
                        }
                    }
                }
                //first character is trap and attribute is place
                if (context.attribute().ElementAt(0).possibleAttributes().GetText().Equals("place") &&
                    context.attribute().ElementAt(0).possibleAttributes().possibleAttributes().Length == 0)
                {
                    if (context.attribute().ElementAt(1).character().PLAYER() != null)
                    {
                        if (context.attribute().ElementAt(0).character().TRAP() != null)
                        {
                            if (context.COMPARE().GetText().Equals("=="))
                                return Provider.GetTrap().Place == Provider.GetPlayer().Place;
                            if (context.COMPARE().GetText().Equals("!="))
                                return Provider.GetTrap().Place != Provider.GetPlayer().Place;
                        }
                        else
                        {
                            if (context.COMPARE().GetText().Equals("=="))
                                return Provider.GetMe().Place == Provider.GetPlayer().Place;
                            if (context.COMPARE().GetText().Equals("!="))
                                return Provider.GetMe().Place == Provider.GetPlayer().Place;
                        }
                    }
                    if (context.attribute().ElementAt(1).character().TRAP() != null)
                    {
                        if (context.attribute().ElementAt(1).possibleAttributes().GetText().Equals("place"))
                        {
                            if (context.attribute().ElementAt(0).character().TRAP() != null)
                            {
                                if (context.COMPARE().GetText().Equals("=="))
                                    return Provider.GetTrap().Place == Provider.GetTrap().Place;
                                if (context.COMPARE().GetText().Equals("!="))
                                    return Provider.GetTrap().Place != Provider.GetTrap().Place;
                            }
                            else
                            {
                                if (context.COMPARE().GetText().Equals("=="))
                                    return Provider.GetMe().Place == Provider.GetTrap().Place;
                                if (context.COMPARE().GetText().Equals("!="))
                                    return Provider.GetMe().Place == Provider.GetTrap().Place;
                            }
                        }
                        if (context.attribute().ElementAt(1).possibleAttributes().GetText().Equals("spawn_place"))
                        {
                            if (context.attribute().ElementAt(0).character().TRAP() != null)
                            {
                                if (context.COMPARE().GetText().Equals("=="))
                                    return Provider.GetTrap().Place == Provider.GetTrap().GetCharacterType().TeleportPlace;
                                if (context.COMPARE().GetText().Equals("!="))
                                    return Provider.GetTrap().Place != Provider.GetTrap().GetCharacterType().TeleportPlace;
                            }
                            else
                            {
                                if (context.COMPARE().GetText().Equals("=="))
                                    return Provider.GetMe().Place == Provider.GetTrap().GetCharacterType().TeleportPlace;
                                if (context.COMPARE().GetText().Equals("!="))
                                    return Provider.GetMe().Place == Provider.GetTrap().GetCharacterType().TeleportPlace;
                            }
                        }
                        if (context.attribute().ElementAt(1).possibleAttributes().GetText().Equals("spawn_place"))
                        {
                            if (context.attribute().ElementAt(0).character().TRAP() != null)
                            {
                                if (context.COMPARE().GetText().Equals("=="))
                                    return Provider.GetTrap().Place == Provider.GetTrap().GetCharacterType().SpawnPlace;
                                if (context.COMPARE().GetText().Equals("!="))
                                    return Provider.GetTrap().Place != Provider.GetTrap().GetCharacterType().SpawnPlace;
                            }
                            else
                            {
                                if (context.COMPARE().GetText().Equals("=="))
                                    return Provider.GetMe().Place == Provider.GetTrap().GetCharacterType().SpawnPlace;
                                if (context.COMPARE().GetText().Equals("!="))
                                    return Provider.GetMe().Place == Provider.GetTrap().GetCharacterType().SpawnPlace;
                            }
                        }
                    }
                    if (context.attribute().ElementAt(1).character().ME() != null && Provider.GetMe().GetCharacterType() is TrapType)
                    {
                        if (context.attribute().ElementAt(1).possibleAttributes().GetText().Equals("place"))
                        {
                            if (context.attribute().ElementAt(0).character().TRAP() != null)
                            {
                                if (context.COMPARE().GetText().Equals("=="))
                                    return Provider.GetTrap().Place == Provider.GetMe().Place;
                                if (context.COMPARE().GetText().Equals("!="))
                                    return Provider.GetTrap().Place != Provider.GetMe().Place;
                            }
                            else
                            {
                                if (context.COMPARE().GetText().Equals("=="))
                                    return Provider.GetMe().Place == Provider.GetMe().Place;
                                if (context.COMPARE().GetText().Equals("!="))
                                    return Provider.GetMe().Place == Provider.GetMe().Place;
                            }
                        }
                        if (context.attribute().ElementAt(1).possibleAttributes().GetText().Equals("spawn_place"))
                        {
                            if (context.attribute().ElementAt(0).character().TRAP() != null)
                            {
                                if (context.COMPARE().GetText().Equals("=="))
                                    return Provider.GetTrap().Place == Provider.GetMe().GetCharacterType().TeleportPlace;
                                if (context.COMPARE().GetText().Equals("!="))
                                    return Provider.GetTrap().Place != Provider.GetMe().GetCharacterType().TeleportPlace;
                            }
                            else
                            {
                                if (context.COMPARE().GetText().Equals("=="))
                                    return Provider.GetMe().Place == Provider.GetMe().GetCharacterType().TeleportPlace;
                                if (context.COMPARE().GetText().Equals("!="))
                                    return Provider.GetMe().Place == Provider.GetMe().GetCharacterType().TeleportPlace;
                            }
                        }
                        if (context.attribute().ElementAt(1).possibleAttributes().GetText().Equals("spawn_place"))
                        {
                            if (context.attribute().ElementAt(0).character().TRAP() != null)
                            {
                                if (context.COMPARE().GetText().Equals("=="))
                                    return Provider.GetTrap().Place == Provider.GetMe().GetCharacterType().SpawnPlace;
                                if (context.COMPARE().GetText().Equals("!="))
                                    return Provider.GetTrap().Place != Provider.GetMe().GetCharacterType().SpawnPlace;
                            }
                            else
                            {
                                if (context.COMPARE().GetText().Equals("=="))
                                    return Provider.GetMe().Place == Provider.GetMe().GetCharacterType().SpawnPlace;
                                if (context.COMPARE().GetText().Equals("!="))
                                    return Provider.GetMe().Place == Provider.GetMe().GetCharacterType().SpawnPlace;
                            }
                        }
                    }
                    if (context.attribute().ElementAt(1).character().MONSTER() != null)
                    {
                        if (context.attribute().ElementAt(0).character().TRAP() != null)
                        {
                            if (context.COMPARE().GetText().Equals("=="))
                                return Provider.GetTrap().Place == Provider.GetMonster().Place;
                            if (context.COMPARE().GetText().Equals("!="))
                                return Provider.GetTrap().Place != Provider.GetMonster().Place;
                        }
                        else
                        {
                            if (context.COMPARE().GetText().Equals("=="))
                                return Provider.GetMe().Place == Provider.GetMonster().Place;
                            if (context.COMPARE().GetText().Equals("!="))
                                return Provider.GetMe().Place == Provider.GetMonster().Place;
                        }
                    }
                    if (context.attribute().ElementAt(1).character().ME() != null && Provider.GetMe().GetCharacterType() is MonsterType)
                    {
                        if (context.attribute().ElementAt(0).character().TRAP() != null)
                        {
                            if (context.COMPARE().GetText().Equals("=="))
                                return Provider.GetTrap().Place == Provider.GetMe().Place;
                            if (context.COMPARE().GetText().Equals("!="))
                                return Provider.GetTrap().Place != Provider.GetMe().Place;
                        }
                    }
                }
                //first character is trap and the attribute is teleport place
                if (context.attribute().ElementAt(0).possibleAttributes().GetText().Equals("teleport_place") &&
                    context.attribute().ElementAt(0).possibleAttributes().possibleAttributes().Length == 0)
                {
                    if (context.attribute().ElementAt(1).character().PLAYER() != null)
                    {
                        if (context.attribute().ElementAt(0).character().TRAP() != null)
                        {
                            if (context.COMPARE().GetText().Equals("=="))
                                return Provider.GetTrap().GetCharacterType().TeleportPlace == Provider.GetPlayer().Place;
                            if (context.COMPARE().GetText().Equals("!="))
                                return Provider.GetTrap().GetCharacterType().TeleportPlace != Provider.GetPlayer().Place;
                        }
                        else
                        {
                            if (context.COMPARE().GetText().Equals("=="))
                                return Provider.GetMe().GetCharacterType().TeleportPlace == Provider.GetPlayer().Place;
                            if (context.COMPARE().GetText().Equals("!="))
                                return Provider.GetMe().GetCharacterType().TeleportPlace == Provider.GetPlayer().Place;
                        }
                    }
                    if (context.attribute().ElementAt(1).character().TRAP() != null)
                    {
                        if (context.attribute().ElementAt(1).possibleAttributes().GetText().Equals("place"))
                        {
                            if (context.attribute().ElementAt(0).character().TRAP() != null)
                            {
                                if (context.COMPARE().GetText().Equals("=="))
                                    return Provider.GetTrap().GetCharacterType().TeleportPlace == Provider.GetTrap().Place;
                                if (context.COMPARE().GetText().Equals("!="))
                                    return Provider.GetTrap().GetCharacterType().TeleportPlace != Provider.GetTrap().Place;
                            }
                            else
                            {
                                if (context.COMPARE().GetText().Equals("=="))
                                    return Provider.GetMe().GetCharacterType().TeleportPlace == Provider.GetTrap().Place;
                                if (context.COMPARE().GetText().Equals("!="))
                                    return Provider.GetMe().GetCharacterType().TeleportPlace == Provider.GetTrap().Place;
                            }
                        }
                        if (context.attribute().ElementAt(1).possibleAttributes().GetText().Equals("spawn_place"))
                        {
                            if (context.attribute().ElementAt(0).character().TRAP() != null)
                            {
                                if (context.COMPARE().GetText().Equals("=="))
                                    return Provider.GetTrap().GetCharacterType().TeleportPlace == Provider.GetTrap().GetCharacterType().TeleportPlace;
                                if (context.COMPARE().GetText().Equals("!="))
                                    return Provider.GetTrap().GetCharacterType().TeleportPlace != Provider.GetTrap().GetCharacterType().TeleportPlace;
                            }
                            else
                            {
                                if (context.COMPARE().GetText().Equals("=="))
                                    return Provider.GetMe().GetCharacterType().TeleportPlace == Provider.GetTrap().GetCharacterType().TeleportPlace;
                                if (context.COMPARE().GetText().Equals("!="))
                                    return Provider.GetMe().GetCharacterType().TeleportPlace == Provider.GetTrap().GetCharacterType().TeleportPlace;
                            }
                        }
                        if (context.attribute().ElementAt(1).possibleAttributes().GetText().Equals("spawn_place"))
                        {
                            if (context.attribute().ElementAt(0).character().TRAP() != null)
                            {
                                if (context.COMPARE().GetText().Equals("=="))
                                    return Provider.GetTrap().GetCharacterType().TeleportPlace == Provider.GetTrap().GetCharacterType().SpawnPlace;
                                if (context.COMPARE().GetText().Equals("!="))
                                    return Provider.GetTrap().GetCharacterType().TeleportPlace != Provider.GetTrap().GetCharacterType().SpawnPlace;
                            }
                            else
                            {
                                if (context.COMPARE().GetText().Equals("=="))
                                    return Provider.GetMe().GetCharacterType().TeleportPlace == Provider.GetTrap().GetCharacterType().SpawnPlace;
                                if (context.COMPARE().GetText().Equals("!="))
                                    return Provider.GetMe().GetCharacterType().TeleportPlace == Provider.GetTrap().GetCharacterType().SpawnPlace;
                            }
                        }
                    }
                    if (context.attribute().ElementAt(1).character().ME() != null && Provider.GetMe().GetCharacterType() is TrapType)
                    {
                        if (context.attribute().ElementAt(1).possibleAttributes().GetText().Equals("place"))
                        {
                            if (context.attribute().ElementAt(0).character().TRAP() != null)
                            {
                                if (context.COMPARE().GetText().Equals("=="))
                                    return Provider.GetTrap().GetCharacterType().TeleportPlace == Provider.GetMe().Place;
                                if (context.COMPARE().GetText().Equals("!="))
                                    return Provider.GetTrap().GetCharacterType().TeleportPlace != Provider.GetMe().Place;
                            }
                            else
                            {
                                if (context.COMPARE().GetText().Equals("=="))
                                    return Provider.GetMe().GetCharacterType().TeleportPlace == Provider.GetMe().Place;
                                if (context.COMPARE().GetText().Equals("!="))
                                    return Provider.GetMe().GetCharacterType().TeleportPlace == Provider.GetMe().Place;
                            }
                        }
                        if (context.attribute().ElementAt(1).possibleAttributes().GetText().Equals("spawn_place"))
                        {
                            if (context.attribute().ElementAt(0).character().TRAP() != null)
                            {
                                if (context.COMPARE().GetText().Equals("=="))
                                    return Provider.GetTrap().GetCharacterType().TeleportPlace == Provider.GetMe().GetCharacterType().TeleportPlace;
                                if (context.COMPARE().GetText().Equals("!="))
                                    return Provider.GetTrap().GetCharacterType().TeleportPlace != Provider.GetMe().GetCharacterType().TeleportPlace;
                            }
                            else
                            {
                                if (context.COMPARE().GetText().Equals("=="))
                                    return Provider.GetMe().GetCharacterType().TeleportPlace == Provider.GetMe().GetCharacterType().TeleportPlace;
                                if (context.COMPARE().GetText().Equals("!="))
                                    return Provider.GetMe().GetCharacterType().TeleportPlace == Provider.GetMe().GetCharacterType().TeleportPlace;
                            }
                        }
                        if (context.attribute().ElementAt(1).possibleAttributes().GetText().Equals("spawn_place"))
                        {
                            if (context.attribute().ElementAt(0).character().TRAP() != null)
                            {
                                if (context.COMPARE().GetText().Equals("=="))
                                    return Provider.GetTrap().GetCharacterType().TeleportPlace == Provider.GetMe().GetCharacterType().SpawnPlace;
                                if (context.COMPARE().GetText().Equals("!="))
                                    return Provider.GetTrap().GetCharacterType().TeleportPlace != Provider.GetMe().GetCharacterType().SpawnPlace;
                            }
                            else
                            {
                                if (context.COMPARE().GetText().Equals("=="))
                                    return Provider.GetMe().GetCharacterType().TeleportPlace == Provider.GetMe().GetCharacterType().SpawnPlace;
                                if (context.COMPARE().GetText().Equals("!="))
                                    return Provider.GetMe().GetCharacterType().TeleportPlace == Provider.GetMe().GetCharacterType().SpawnPlace;
                            }
                        }
                    }
                    if (context.attribute().ElementAt(1).character().MONSTER() != null)
                    {
                        if (context.attribute().ElementAt(0).character().TRAP() != null)
                        {
                            if (context.COMPARE().GetText().Equals("=="))
                                return Provider.GetTrap().GetCharacterType().TeleportPlace == Provider.GetMonster().Place;
                            if (context.COMPARE().GetText().Equals("!="))
                                return Provider.GetTrap().GetCharacterType().TeleportPlace != Provider.GetMonster().Place;
                        }
                        else
                        {
                            if (context.COMPARE().GetText().Equals("=="))
                                return Provider.GetMe().GetCharacterType().TeleportPlace == Provider.GetMonster().Place;
                            if (context.COMPARE().GetText().Equals("!="))
                                return Provider.GetMe().GetCharacterType().TeleportPlace == Provider.GetMonster().Place;
                        }
                    }
                    if (context.attribute().ElementAt(1).character().ME() != null && Provider.GetMe().GetCharacterType() is MonsterType)
                    {
                        if (context.attribute().ElementAt(0).character().TRAP() != null)
                        {
                            if (context.COMPARE().GetText().Equals("=="))
                                return Provider.GetTrap().GetCharacterType().TeleportPlace == Provider.GetMe().Place;
                            if (context.COMPARE().GetText().Equals("!="))
                                return Provider.GetTrap().GetCharacterType().TeleportPlace != Provider.GetMe().Place;
                        }
                    }
                }
                //first character is trap and the attribute is spawn place
                if (context.attribute().ElementAt(0).possibleAttributes().GetText().Equals("spawn_place") &&
                    context.attribute().ElementAt(0).possibleAttributes().possibleAttributes().Length == 0)
                {
                    if (context.attribute().ElementAt(1).character().PLAYER() != null)
                    {
                        if (context.attribute().ElementAt(0).character().TRAP() != null)
                        {
                            if (context.COMPARE().GetText().Equals("=="))
                                return Provider.GetTrap().GetCharacterType().SpawnPlace == Provider.GetPlayer().Place;
                            if (context.COMPARE().GetText().Equals("!="))
                                return Provider.GetTrap().GetCharacterType().SpawnPlace != Provider.GetPlayer().Place;
                        }
                        else
                        {
                            if (context.COMPARE().GetText().Equals("=="))
                                return Provider.GetMe().GetCharacterType().SpawnPlace == Provider.GetPlayer().Place;
                            if (context.COMPARE().GetText().Equals("!="))
                                return Provider.GetMe().GetCharacterType().SpawnPlace == Provider.GetPlayer().Place;
                        }
                    }
                    if (context.attribute().ElementAt(1).character().TRAP() != null)
                    {
                        if (context.attribute().ElementAt(1).possibleAttributes().GetText().Equals("place"))
                        {
                            if (context.attribute().ElementAt(0).character().TRAP() != null)
                            {
                                if (context.COMPARE().GetText().Equals("=="))
                                    return Provider.GetTrap().GetCharacterType().SpawnPlace == Provider.GetTrap().Place;
                                if (context.COMPARE().GetText().Equals("!="))
                                    return Provider.GetTrap().GetCharacterType().SpawnPlace != Provider.GetTrap().Place;
                            }
                            else
                            {
                                if (context.COMPARE().GetText().Equals("=="))
                                    return Provider.GetMe().GetCharacterType().SpawnPlace == Provider.GetTrap().Place;
                                if (context.COMPARE().GetText().Equals("!="))
                                    return Provider.GetMe().GetCharacterType().SpawnPlace == Provider.GetTrap().Place;
                            }
                        }
                        if (context.attribute().ElementAt(1).possibleAttributes().GetText().Equals("spawn_place"))
                        {
                            if (context.attribute().ElementAt(0).character().TRAP() != null)
                            {
                                if (context.COMPARE().GetText().Equals("=="))
                                    return Provider.GetTrap().GetCharacterType().SpawnPlace == Provider.GetTrap().GetCharacterType().TeleportPlace;
                                if (context.COMPARE().GetText().Equals("!="))
                                    return Provider.GetTrap().GetCharacterType().SpawnPlace != Provider.GetTrap().GetCharacterType().TeleportPlace;
                            }
                            else
                            {
                                if (context.COMPARE().GetText().Equals("=="))
                                    return Provider.GetMe().GetCharacterType().SpawnPlace == Provider.GetTrap().GetCharacterType().TeleportPlace;
                                if (context.COMPARE().GetText().Equals("!="))
                                    return Provider.GetMe().GetCharacterType().SpawnPlace == Provider.GetTrap().GetCharacterType().TeleportPlace;
                            }
                        }
                        if (context.attribute().ElementAt(1).possibleAttributes().GetText().Equals("spawn_place"))
                        {
                            if (context.attribute().ElementAt(0).character().TRAP() != null)
                            {
                                if (context.COMPARE().GetText().Equals("=="))
                                    return Provider.GetTrap().GetCharacterType().SpawnPlace == Provider.GetTrap().GetCharacterType().SpawnPlace;
                                if (context.COMPARE().GetText().Equals("!="))
                                    return Provider.GetTrap().GetCharacterType().SpawnPlace != Provider.GetTrap().GetCharacterType().SpawnPlace;
                            }
                            else
                            {
                                if (context.COMPARE().GetText().Equals("=="))
                                    return Provider.GetMe().GetCharacterType().SpawnPlace == Provider.GetTrap().GetCharacterType().SpawnPlace;
                                if (context.COMPARE().GetText().Equals("!="))
                                    return Provider.GetMe().GetCharacterType().SpawnPlace == Provider.GetTrap().GetCharacterType().SpawnPlace;
                            }
                        }
                    }
                    if (context.attribute().ElementAt(1).character().ME() != null && Provider.GetMe().GetCharacterType() is TrapType)
                    {
                        if (context.attribute().ElementAt(1).possibleAttributes().GetText().Equals("place"))
                        {
                            if (context.attribute().ElementAt(0).character().TRAP() != null)
                            {
                                if (context.COMPARE().GetText().Equals("=="))
                                    return Provider.GetTrap().GetCharacterType().SpawnPlace == Provider.GetMe().Place;
                                if (context.COMPARE().GetText().Equals("!="))
                                    return Provider.GetTrap().GetCharacterType().SpawnPlace != Provider.GetMe().Place;
                            }
                            else
                            {
                                if (context.COMPARE().GetText().Equals("=="))
                                    return Provider.GetMe().GetCharacterType().SpawnPlace == Provider.GetMe().Place;
                                if (context.COMPARE().GetText().Equals("!="))
                                    return Provider.GetMe().GetCharacterType().SpawnPlace == Provider.GetMe().Place;
                            }
                        }
                        if (context.attribute().ElementAt(1).possibleAttributes().GetText().Equals("spawn_place"))
                        {
                            if (context.attribute().ElementAt(0).character().TRAP() != null)
                            {
                                if (context.COMPARE().GetText().Equals("=="))
                                    return Provider.GetTrap().GetCharacterType().SpawnPlace == Provider.GetMe().GetCharacterType().TeleportPlace;
                                if (context.COMPARE().GetText().Equals("!="))
                                    return Provider.GetTrap().GetCharacterType().SpawnPlace != Provider.GetMe().GetCharacterType().TeleportPlace;
                            }
                            else
                            {
                                if (context.COMPARE().GetText().Equals("=="))
                                    return Provider.GetMe().GetCharacterType().SpawnPlace == Provider.GetMe().GetCharacterType().TeleportPlace;
                                if (context.COMPARE().GetText().Equals("!="))
                                    return Provider.GetMe().GetCharacterType().SpawnPlace == Provider.GetMe().GetCharacterType().TeleportPlace;
                            }
                        }
                        if (context.attribute().ElementAt(1).possibleAttributes().GetText().Equals("spawn_place"))
                        {
                            if (context.attribute().ElementAt(0).character().TRAP() != null)
                            {
                                if (context.COMPARE().GetText().Equals("=="))
                                    return Provider.GetTrap().GetCharacterType().SpawnPlace == Provider.GetMe().GetCharacterType().SpawnPlace;
                                if (context.COMPARE().GetText().Equals("!="))
                                    return Provider.GetTrap().GetCharacterType().SpawnPlace != Provider.GetMe().GetCharacterType().SpawnPlace;
                            }
                            else
                            {
                                if (context.COMPARE().GetText().Equals("=="))
                                    return Provider.GetMe().GetCharacterType().SpawnPlace == Provider.GetMe().GetCharacterType().SpawnPlace;
                                if (context.COMPARE().GetText().Equals("!="))
                                    return Provider.GetMe().GetCharacterType().SpawnPlace == Provider.GetMe().GetCharacterType().SpawnPlace;
                            }
                        }
                    }
                    if (context.attribute().ElementAt(1).character().MONSTER() != null)
                    {
                        if (context.attribute().ElementAt(0).character().TRAP() != null)
                        {
                            if (context.COMPARE().GetText().Equals("=="))
                                return Provider.GetTrap().GetCharacterType().SpawnPlace == Provider.GetMonster().Place;
                            if (context.COMPARE().GetText().Equals("!="))
                                return Provider.GetTrap().GetCharacterType().SpawnPlace != Provider.GetMonster().Place;
                        }
                        else
                        {
                            if (context.COMPARE().GetText().Equals("=="))
                                return Provider.GetMe().GetCharacterType().SpawnPlace == Provider.GetMonster().Place;
                            if (context.COMPARE().GetText().Equals("!="))
                                return Provider.GetMe().GetCharacterType().SpawnPlace == Provider.GetMonster().Place;
                        }
                    }
                    if (context.attribute().ElementAt(1).character().ME() != null && Provider.GetMe().GetCharacterType() is MonsterType)
                    {
                        if (context.attribute().ElementAt(0).character().TRAP() != null)
                        {
                            if (context.COMPARE().GetText().Equals("=="))
                                return Provider.GetTrap().GetCharacterType().SpawnPlace == Provider.GetMe().Place;
                            if (context.COMPARE().GetText().Equals("!="))
                                return Provider.GetTrap().GetCharacterType().SpawnPlace != Provider.GetMe().Place;
                        }
                    }
                }
                //first character is trap, and attribute isn't a place or type
                else
                {
                    if (context.COMPARE().GetText().Equals("=="))
                        return CheckNumberAttributeExpression(context.attribute().ElementAt(0)) == CheckNumberAttributeExpression(context.attribute().ElementAt(1));
                    if (context.COMPARE().GetText().Equals("!="))
                        return CheckNumberAttributeExpression(context.attribute().ElementAt(0)) != CheckNumberAttributeExpression(context.attribute().ElementAt(1));
                }
            }
            //if the first character is Player, it can't be "ME"
            if(context.attribute().ElementAt(0).character().PLAYER() != null)
            {
                //first character is placer, attribute is place
                if(context.attribute().ElementAt(0).possibleAttributes().GetText().Equals("place") &&
                    context.attribute().ElementAt(0).possibleAttributes().possibleAttributes().Length == 0)
                {
                    if (context.attribute().ElementAt(1).character().PLAYER() != null)
                    {
                            if (context.COMPARE().GetText().Equals("=="))
                                return Provider.GetPlayer().Place == Provider.GetPlayer().Place;
                            if (context.COMPARE().GetText().Equals("!="))
                                return Provider.GetPlayer().Place != Provider.GetPlayer().Place;
                    }
                    if (context.attribute().ElementAt(1).character().TRAP() != null)
                    {
                        if (context.attribute().ElementAt(1).possibleAttributes().GetText().Equals("place"))
                        {
                                if (context.COMPARE().GetText().Equals("=="))
                                    return Provider.GetPlayer().Place == Provider.GetTrap().Place;
                                if (context.COMPARE().GetText().Equals("!="))
                                    return Provider.GetPlayer().Place != Provider.GetTrap().Place;
                        }
                        if (context.attribute().ElementAt(1).possibleAttributes().GetText().Equals("teleport_place"))
                        {
                                if (context.COMPARE().GetText().Equals("=="))
                                    return Provider.GetPlayer().Place == Provider.GetTrap().GetCharacterType().TeleportPlace;
                                if (context.COMPARE().GetText().Equals("!="))
                                    return Provider.GetPlayer().Place != Provider.GetTrap().GetCharacterType().TeleportPlace;
                        }
                        if (context.attribute().ElementAt(1).possibleAttributes().GetText().Equals("spawn_place"))
                        {
                                if (context.COMPARE().GetText().Equals("=="))
                                    return Provider.GetPlayer().Place == Provider.GetTrap().GetCharacterType().SpawnPlace;
                                if (context.COMPARE().GetText().Equals("!="))
                                    return Provider.GetPlayer().Place != Provider.GetTrap().GetCharacterType().SpawnPlace;
                        }
                    }
                    if (context.attribute().ElementAt(1).character().ME() != null && Provider.GetMe().GetCharacterType() is TrapType)
                    {
                        if (context.attribute().ElementAt(1).possibleAttributes().GetText().Equals("place"))
                        {
                                if (context.COMPARE().GetText().Equals("=="))
                                    return Provider.GetPlayer().Place == Provider.GetMe().Place;
                                if (context.COMPARE().GetText().Equals("!="))
                                    return Provider.GetPlayer().Place != Provider.GetMe().Place;
                        }
                        if (context.attribute().ElementAt(1).possibleAttributes().GetText().Equals("teleport_place"))
                        {
                                if (context.COMPARE().GetText().Equals("=="))
                                    return Provider.GetPlayer().Place == Provider.GetMe().GetCharacterType().TeleportPlace;
                                if (context.COMPARE().GetText().Equals("!="))
                                    return Provider.GetPlayer().Place != Provider.GetMe().GetCharacterType().TeleportPlace;
                        }
                        if (context.attribute().ElementAt(1).possibleAttributes().GetText().Equals("spawn_place"))
                        {
                                if (context.COMPARE().GetText().Equals("=="))
                                    return Provider.GetPlayer().Place == Provider.GetMe().GetCharacterType().SpawnPlace;
                                if (context.COMPARE().GetText().Equals("!="))
                                    return Provider.GetPlayer().Place != Provider.GetMe().GetCharacterType().SpawnPlace;
                        }
                    }
                    if (context.attribute().ElementAt(1).character().MONSTER() != null)
                    {
                            if (context.COMPARE().GetText().Equals("=="))
                                return Provider.GetPlayer().Place == Provider.GetMonster().Place;
                            if (context.COMPARE().GetText().Equals("!="))
                                return Provider.GetPlayer().Place != Provider.GetMonster().Place;
                    }
                    if (context.attribute().ElementAt(1).character().ME() != null && Provider.GetMe().GetCharacterType() is MonsterType)
                    {
                            if (context.COMPARE().GetText().Equals("=="))
                                return Provider.GetPlayer().Place == Provider.GetMe().Place;
                            if (context.COMPARE().GetText().Equals("!="))
                                return Provider.GetPlayer().Place != Provider.GetMe().Place;
                    }
                }
                //first character is player, and the first attribute isn'T a place or type
                else
                {
                    if (context.COMPARE().GetText().Equals("=="))
                        return CheckNumberAttributeExpression(context.attribute().ElementAt(0)) == CheckNumberAttributeExpression(context.attribute().ElementAt(1));
                    if (context.COMPARE().GetText().Equals("!="))
                        return CheckNumberAttributeExpression(context.attribute().ElementAt(0)) != CheckNumberAttributeExpression(context.attribute().ElementAt(1));
                }
            }
            return false;
        }

        //if the ME is a player, that can't theoretically happen, then, since only 1 player exists, this still does the same
        public double PlayerNumberAttribute(AttributeContext context)
        {
            if (context.possibleAttributes().GetText().Equals("damage"))
                return Provider.GetPlayer().GetCharacterType().Damage;
            if (context.possibleAttributes().GetText().Equals("health"))
                return Provider.GetPlayer().GetHealth();
            if (context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("place"))
            {
                if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("x"))
                    return Provider.GetPlayer().Place.X;
                else
                    return Provider.GetPlayer().Place.Y;
            }
            ErrorList += (ErrorMessages.ConditionError.PLAYER_ATTRIBUTE_ERROR);
            ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
            ErrorList += (context.GetText() + "\n");
            ErrorFound = true;
            return -1;
        }

        private double MeNumberAttribute(AttributeContext context)
        {
            if (context.possibleAttributes().GetText().Equals("damage"))
            {
                if (Provider.GetMe().GetCharacterType().Damage == StaticStartValues.PLACEHOLDER_DAMAGE)
                {
                    ErrorList += (ErrorMessages.ConditionError.CHARACTER_TYPE_HAS_NO_DAMAGE_DEFINED);
                    ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                    ErrorList += (context.GetText() + "\n");
                    ErrorFound = true;
                    return -1;
                }
                return Provider.GetMe().GetCharacterType().Damage;
            }
            if (context.possibleAttributes().GetText().Equals("heal"))
            {
                if (Provider.GetMe().GetCharacterType().Heal == StaticStartValues.PLACEHOLDER_DAMAGE)
                {
                    ErrorList += (ErrorMessages.ConditionError.CHARACTER_TYPE_HAS_NO_HEAL_DEFINED);
                    ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                    ErrorList += (context.GetText() + "\n");
                    ErrorFound = true;
                    return -1;
                }
                if(Provider.GetMe() is Monster)
                {
                    ErrorList += (ErrorMessages.HealError.ONLY_TRAP_CAN_HEAL);
                    ErrorList += (context.GetText() + "\n");
                    ErrorFound = true;
                    return -1;
                }
                return Provider.GetMe().GetCharacterType().Heal;
            }
            if (context.possibleAttributes().GetText().Equals("health"))
            {
                if (Provider.GetMe() is Trap)
                {
                    ErrorList += (ErrorMessages.ParameterDeclarationError.TRAP_HAS_NO_HEALTH);
                    ErrorList += (context.GetText() + "\n");
                    ErrorFound = true;
                    return -1;
                }
                return Provider.GetMe().GetHealth();
            }
            if (context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("place"))
            {
                if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("x"))
                    return Provider.GetMe().Place.X;
                return Provider.GetMe().Place.Y;
            }
            if (context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("teleport_place"))
            {
                if (Provider.GetMe() is Monster)
                {
                    ErrorList += (ErrorMessages.TeleportError.ONLY_TRAP_CAN_TELEPORT);
                    ErrorList += (context.GetText() + "\n");
                    ErrorFound = true;
                    return -1;
                }
                if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("x"))
                    return Provider.GetMe().GetCharacterType().TeleportPlace.X;
                return Provider.GetMe().GetCharacterType().TeleportPlace.Y;
            }
            if (context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("spawn_place"))
            {
                if (Provider.GetMe() is Monster)
                {
                    ErrorList += (ErrorMessages.SpawnError.ONLY_TRAP_CAN_SPAWN);
                    ErrorList += (context.GetText() + "\n");
                    ErrorFound = true;
                    return -1;
                }
                if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("x"))
                    return Provider.GetMe().GetCharacterType().SpawnPlace.X;
                return Provider.GetMe().GetCharacterType().SpawnPlace.Y;
            }
            if (context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("type"))
            {
                if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("damage"))
                {
                    if (Provider.GetMe().GetCharacterType().Damage == StaticStartValues.PLACEHOLDER_DAMAGE)
                    {
                        ErrorList += (ErrorMessages.ConditionError.CHARACTER_TYPE_HAS_NO_DAMAGE_DEFINED);
                        ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                        ErrorList += (context.GetText() + "\n");
                        ErrorFound = true;
                        return -1;
                    }
                    return Provider.GetMe().GetCharacterType().Damage;
                }
                if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("heal"))
                {
                    if (Provider.GetMe().GetCharacterType().Heal == StaticStartValues.PLACEHOLDER_DAMAGE)
                    {
                        ErrorList += (ErrorMessages.ConditionError.CHARACTER_TYPE_HAS_NO_HEAL_DEFINED);
                        ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                        ErrorList += (context.GetText() + "\n");
                        ErrorFound = true;
                        return -1;
                    }
                    if (Provider.GetMe() is Monster)
                    {
                        ErrorList += (ErrorMessages.HealError.ONLY_TRAP_CAN_HEAL);
                        ErrorList += (context.GetText() + "\n");
                        ErrorFound = true;
                        return -1;
                    }
                    return Provider.GetMe().GetCharacterType().Heal;
                }
                if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("health"))
                {
                    if (Provider.GetMe().GetCharacterType().Health == StaticStartValues.PLACEHOLDER_HEALTH)
                    {
                        ErrorList += (ErrorMessages.ConditionError.CHARACTER_TYPE_HAS_NO_HEALTH_DEFINED);
                        ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                        ErrorList += (context.GetText() + "\n");
                        ErrorFound = true;
                        return -1;
                    }
                    return Provider.GetMe().GetCharacterType().Health;
                }
            }
            if (context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("spawn_type"))
            {
                if (Provider.GetMe() is Monster)
                {
                    ErrorList += (ErrorMessages.SpawnError.ONLY_TRAP_CAN_SPAWN);
                    ErrorList += (context.GetText() + "\n");
                    ErrorFound = true;
                    return -1;
                }
                if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("damage"))
                    return Provider.GetMe().GetCharacterType().SpawnType.Damage;
                if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("health"))
                {
                    return Provider.GetMe().GetCharacterType().SpawnType.Health;
                }
            }
            if (Provider.GetMe() is Trap)
                ErrorList += (ErrorMessages.ConditionError.TRAP_ATTRIBUTE_ERROR);
            if (Provider.GetMe() is Monster)
                ErrorList += (ErrorMessages.ConditionError.MONSTER_ATTRIBUTE_ERROR);
            ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
            ErrorList += (context.GetText() + "\n");
            ErrorFound = true;
            return -1;
        }


        public double TrapNumberAttribute(AttributeContext context)
        {
            if (context.character().PARTNER() != null)
            {
                if (context.possibleAttributes().GetText().Equals("damage"))
                    return Provider.GetPartner().GetCharacterType().Damage;
                if (context.possibleAttributes().GetText().Equals("heal"))
                    return Provider.GetPartner().GetCharacterType().Heal;
                if (context.possibleAttributes().GetText().Equals("health"))
                {
                    ErrorList += (ErrorMessages.ConditionError.TRAP_HAS_NO_HEALTH);
                    ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                    ErrorList += (context.GetText() + "\n");
                    ErrorFound = true;
                    return -1;
                }
                if (context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("place"))
                {
                    if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("x"))
                        return Provider.GetPartner().Place.X;
                    else
                        return Provider.GetPartner().Place.Y;
                }
                if (context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("teleport_place"))
                {
                    if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("x"))
                        return Provider.GetPartner().GetCharacterType().TeleportPlace.X;
                    else
                        return Provider.GetPartner().GetCharacterType().TeleportPlace.Y;
                }
                if (context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("spawn_place"))
                {
                    if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("x"))
                        return Provider.GetPartner().GetCharacterType().SpawnPlace.X;
                    else
                        return Provider.GetPartner().GetCharacterType().SpawnPlace.Y;
                }
                if (context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("type"))
                {
                    if (context.possibleAttributes().GetText().Equals("damage"))
                        return Provider.GetMe().GetCharacterType().Damage;
                    if (context.possibleAttributes().GetText().Equals("heal"))
                        return Provider.GetMe().GetCharacterType().Heal;
                    if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("health"))
                    {
                        ErrorList += (ErrorMessages.ConditionError.TRAP_HAS_NO_HEALTH);
                        ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                        ErrorList += (context.GetText() + "\n");
                        ErrorFound = true;
                        return -1;
                    }
                }
            }
            if(context.character().PARTNER() != null)
            {

            }
            if(context.character().TRAP() != null)
            {

            }
                if (context.possibleAttributes().GetText().Equals("damage"))
            {

                    if (Provider.GetMe().GetCharacterType().Damage == -1)
                        return 0;
                    return Provider.GetMe().GetCharacterType().Damage;

                if (Provider.GetTrap().GetCharacterType().Damage == -1)
                    return 0;
                return Provider.GetTrap().GetCharacterType().Damage;
            }
            //assuming that traps return a default 0 for heal or damage when e.g. they only teleport
            if (context.possibleAttributes().GetText().Equals("heal"))
            {
                if(context.character().ME() != null)
                {
                    if (Provider.GetMe().GetCharacterType().Heal == -1)
                        return 0;
                    return Provider.GetMe().GetCharacterType().Heal;
                }
                if (Provider.GetTrap().GetCharacterType().Heal == -1)
                    return 0;
                return Provider.GetTrap().GetCharacterType().Heal;
            }
            if (context.possibleAttributes().GetText().Equals("place"))
            {
                if(context.character().ME() != null)
                {
                    if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("x"))
                        return Provider.GetMe().Place.X;
                    else
                        return Provider.GetMe().Place.Y;
                }
                if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("x"))
                    return Provider.GetTrap().Place.X;
                else
                    return Provider.GetTrap().Place.Y;
            }
            if (context.possibleAttributes().GetText().Equals("teleport_place"))
            {
                if(context.character().ME() != null)
                {
                    if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("x"))
                        return Provider.GetMe().GetCharacterType().TeleportPlace.X;
                    else
                        return Provider.GetMe().GetCharacterType().TeleportPlace.Y;
                }

                if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("x"))
                    return Provider.GetTrap().GetCharacterType().TeleportPlace.X;
                else
                    return Provider.GetTrap().GetCharacterType().TeleportPlace.Y;
            }
            if (context.possibleAttributes().GetText().Equals("spawn_place"))
            {
                if(context.character().ME() != null)
                {
                    if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("x"))
                        return Provider.GetMe().GetCharacterType().SpawnPlace.X;
                    else
                        return Provider.GetMe().GetCharacterType().SpawnPlace.Y;
                }

                if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("x"))
                    return Provider.GetTrap().GetCharacterType().SpawnPlace.X;
                else
                    return Provider.GetTrap().GetCharacterType().SpawnPlace.Y;
            }
            if (context.possibleAttributes().GetText().Equals("type"))
            {
                if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("damage"))
                {
                    if(context.character().ME() != null)
                    {
                        if (Provider.GetMe().GetCharacterType().Damage == -1)
                        {
                            return 0;
                        }
                        return Provider.GetMe().GetCharacterType().Damage;
                    }

                    if (Provider.GetTrap().GetCharacterType().Damage == -1)
                    {
                        return 0;
                    }
                    return Provider.GetTrap().GetCharacterType().Damage;
                }
                if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("health"))
                {
                    ErrorList += (ErrorMessages.ConditionError.TRAP_HAS_NO_HEALTH);
                    ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                    ErrorList += (context.GetText() + "\n");
                    ErrorFound = true;
                    return -1;
                }
                if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("heal"))
                {
                    if(context.character().ME() != null)
                    {
                        if (Provider.GetMe().GetCharacterType().Heal == -1)
                        {
                            return 0;
                        }
                        return Provider.GetMe().GetCharacterType().Heal;
                    }

                    if (Provider.GetTrap().GetCharacterType().Heal == -1)
                    {
                        return 0;
                    }
                    return Provider.GetTrap().GetCharacterType().Heal;
                }
            }
            if (context.possibleAttributes().GetText().Equals("spawn_type"))
            {
                if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("damage"))
                {
                    if(context.character().ME() != null)
                    {
                        if (Provider.GetMe().GetCharacterType().SpawnType.Damage == -1)
                        {
                            ErrorList += (ErrorMessages.ConditionError.MONSTER_TYPE_HAS_NO_DAMAGE_DEFINED);
                            ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                            ErrorList += (context.GetText() + "\n");
                            ErrorFound = true;
                            return -1;
                        }
                        return Provider.GetMe().GetCharacterType().SpawnType.Damage;
                    }

                    if (Provider.GetTrap().GetCharacterType().SpawnType.Damage == -1)
                    {
                        ErrorList += (ErrorMessages.ConditionError.MONSTER_TYPE_HAS_NO_DAMAGE_DEFINED);
                        ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                        ErrorList += (context.GetText() + "\n");
                        ErrorFound = true;
                        return -1;
                    }
                    return Provider.GetTrap().GetCharacterType().SpawnType.Damage;
                }
                if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("health"))
                {
                    if (context.character().ME() != null)
                        return Provider.GetMe().GetCharacterType().SpawnType.Health;

                    return Provider.GetTrap().GetCharacterType().SpawnType.Health;
                }
                if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("heal"))
                {
                    ErrorList += (ErrorMessages.ConditionError.ONLY_TRAP_CAN_HEAL);
                    ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                    ErrorList += (context.GetText() + "\n");
                    ErrorFound = true;
                    return -1;
                }
            }

            ErrorList += (ErrorMessages.ConditionError.TRAP_ATTRIBUTE_ERROR);
            ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
            ErrorList += (context.GetText() + "\n");
            ErrorFound = true;
            return -1;
        }

        public double MonsterNumberAttribute(AttributeContext context)
        {
            if (context.possibleAttributes().GetText().Equals("damage")) {

                if(context.character().ME() != null)
                {
                    if (Provider.GetMe().GetCharacterType().Damage == -1)
                    {
                        ErrorList += (ErrorMessages.ConditionError.MONSTER_TYPE_HAS_NO_DAMAGE_DEFINED);
                        ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                        ErrorList += (context.GetText() + "\n");
                        ErrorFound = true;
                        return -1;
                    }
                    else
                        return Provider.GetMe().GetCharacterType().Damage;
                }

                if (Provider.GetMonster().GetCharacterType().Damage == -1)
                {
                    ErrorList += (ErrorMessages.ConditionError.MONSTER_TYPE_HAS_NO_DAMAGE_DEFINED);
                    ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                    ErrorList += (context.GetText() + "\n");
                    ErrorFound = true;
                    return -1;
                }
                else
                    return Provider.GetMonster().GetCharacterType().Damage;
            }
            if (context.possibleAttributes().GetText().Equals("health"))
            {
                if (context.character().ME() != null)
                    return Provider.GetMe().GetHealth();

                return Provider.GetMonster().GetHealth();
            }
            if (context.possibleAttributes().GetText().Equals("place"))
            {
                if(context.character().ME() != null)
                {
                    if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("x"))
                        return Provider.GetMe().Place.X;
                    else
                        return Provider.GetMe().Place.Y;
                }

                if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("x"))
                    return Provider.GetMonster().Place.X;
                else
                    return Provider.GetMonster().Place.Y;
            }
            if (context.possibleAttributes().GetText().Equals("type"))
            {
                if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("damage"))
                {
                    if(context.character().ME() != null)
                    {
                        if (Provider.GetMe().GetCharacterType().Damage == -1)
                        {
                            ErrorList += (ErrorMessages.ConditionError.MONSTER_TYPE_HAS_NO_DAMAGE_DEFINED);
                            ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                            ErrorList += (context.GetText() + "\n");
                            ErrorFound = true;
                            return -1;
                        }
                        return Provider.GetMe().GetCharacterType().Damage;
                    }


                    if (Provider.GetMonster().GetCharacterType().Damage == -1)
                    {
                        ErrorList += (ErrorMessages.ConditionError.MONSTER_TYPE_HAS_NO_DAMAGE_DEFINED);
                        ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                        ErrorList += (context.GetText() + "\n");
                        ErrorFound = true;
                        return -1;
                    }
                    return Provider.GetMonster().GetCharacterType().Damage;
                }
                if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("health"))
                {
                    if(context.character().ME() != null)
                        return Provider.GetMe().GetCharacterType().Health;

                    return Provider.GetMonster().GetCharacterType().Health;
                }
                if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("heal"))
                {
                    ErrorList += (ErrorMessages.ConditionError.ONLY_TRAP_CAN_HEAL);
                    ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                    ErrorList += (context.GetText() + "\n");
                    ErrorFound = true;
                    return -1;
                }
            }

            ErrorList += (ErrorMessages.ConditionError.MONSTER_ATTRIBUTE_ERROR);
            ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
            ErrorList += (context.GetText() + "\n");
            ErrorFound = true;
            return -1;
        }
        }
}
