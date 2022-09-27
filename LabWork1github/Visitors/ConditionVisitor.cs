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
    {
        public BoolExpressionContext BoolExpressionContext { get; set; }

        public GameParamProvider Provider { get; set; }

        public string ErrorList { get; set; }

        public bool ErrorFound { get; set; }

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
            if(context.numToBoolOperation() != null)
            {
                if (context.numToBoolOperation().NUMCOMPARE() != null)
                    expressionValue = CheckNumCompareExpression(context);
                if (context.numToBoolOperation().COMPARE() != null)
                    expressionValue = CheckCompareExpression(context);

            }
            if (context.attribute().Length > 1)
                expressionValue = CheckBoolAttributeExpression(context);
            if (context.nextBoolExpression() != null)
            {
                expressionValue = CheckNextBoolExpression(expressionValue, context.nextBoolExpression());
            }
            return expressionValue;
        }

        private bool CheckNumCompareExpression(BoolExpressionContext context)
        {
            if (context.numToBoolOperation().NUMCOMPARE().Equals("<"))
                return CheckNumberAddExpression(context.numberExpression().ElementAt(0)) < CheckNumberAddExpression(context.numberExpression().ElementAt(1));
            else
                return CheckNumberAddExpression(context.numberExpression().ElementAt(0)) > CheckNumberAddExpression(context.numberExpression().ElementAt(1));
        }

        public bool CheckCompareExpression(BoolExpressionContext context)
        {
            if (context.numToBoolOperation().COMPARE().Equals("!="))
                return CheckNumberAddExpression(context.numberExpression().ElementAt(0)) != CheckNumberAddExpression(context.numberExpression().ElementAt(1));
            else
                return CheckNumberAddExpression(context.numberExpression().ElementAt(0)) == CheckNumberAddExpression(context.numberExpression().ElementAt(1));
        }

        private bool CheckFunctionExpression(FunctionExpressionContext functionExpressionContext)
        {
            if (functionExpressionContext.function().NEAR() != null)
            {
                if (functionExpressionContext.character().ME() != null)
                    return true;
                if (functionExpressionContext.character().MONSTER() != null)
                {
                    if (Math.Abs(Provider.GetMe().Place.X - Provider.GetMonster().Place.X) <= Provider.GetNear() || Math.Abs(Provider.GetMonster().Place.X - Provider.GetMe().Place.X) <= Provider.GetNear())
                    {
                        if (Math.Abs(Provider.GetMe().Place.Y - Provider.GetMonster().Place.Y) <= Provider.GetNear() || Math.Abs(Provider.GetMonster().Place.Y - Provider.GetMe().Place.Y) <= Provider.GetNear())
                            return true;
                    }
                }
                if (functionExpressionContext.character().TRAP() != null)
                {
                    if (Math.Abs(Provider.GetMe().Place.X - Provider.GetTrap().Place.X) <= Provider.GetNear() || Math.Abs(Provider.GetTrap().Place.X - Provider.GetMe().Place.X) <= Provider.GetNear())
                    {
                        if (Math.Abs(Provider.GetMe().Place.Y - Provider.GetTrap().Place.Y) <= Provider.GetNear() || Math.Abs(Provider.GetTrap().Place.Y - Provider.GetMe().Place.Y) <= Provider.GetNear())
                            return true;
                    }
                }
                if (functionExpressionContext.character().PLAYER() != null)
                {
                    if (Math.Abs(Provider.GetMe().Place.X - Provider.GetPlayer().Place.X) <= Provider.GetNear() || Math.Abs(Provider.GetPlayer().Place.X - Provider.GetMe().Place.X) <= Provider.GetNear())
                    {
                        if (Math.Abs(Provider.GetMe().Place.Y - Provider.GetPlayer().Place.Y) <= Provider.GetNear() || Math.Abs(Provider.GetPlayer().Place.Y - Provider.GetMe().Place.Y) <= Provider.GetNear())
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

        public double CheckNumberAddExpression(NumberExpressionContext context)
        {
            double expressionValue = CheckNumberMultipExpression(context.numberMultipExpression().ElementAt(0));

            if(context.NUMCONNECTERADD().Length > 0)
            {
                for(int i=0; i<context.NUMCONNECTERADD().Length; i++)
                {
                    if (context.NUMCONNECTERADD().Equals("+"))
                        expressionValue += CheckNumberMultipExpression(context.numberMultipExpression().ElementAt(1));
                    if (context.NUMCONNECTERADD().Equals("-"))
                        expressionValue -= CheckNumberMultipExpression(context.numberMultipExpression().ElementAt(1));
                }
            }

            return expressionValue;
        }

        private double CheckNumberMultipExpression(NumberMultipExpressionContext context)
        {
            double expressionValue = CheckNumberFirstExpression(context.numberFirstExpression().ElementAt(0));

            if (context.NUMCONNECTERMULTIP().Length > 0)
            {
                for (int i = 0; i < context.NUMCONNECTERMULTIP().Length; i++)
                {
                    if (context.NUMCONNECTERMULTIP().Equals("/"))
                        expressionValue /= CheckNumberFirstExpression(context.numberFirstExpression().ElementAt(1));
                    if (context.NUMCONNECTERMULTIP().Equals("*"))
                        expressionValue *= CheckNumberFirstExpression(context.numberFirstExpression().ElementAt(1));
                    if (context.NUMCONNECTERMULTIP().Equals("%"))
                        expressionValue %= CheckNumberFirstExpression(context.numberFirstExpression().ElementAt(1));
                }
            }

            return expressionValue;
        }

        private double CheckNumberFirstExpression(NumberFirstExpressionContext context)
        {
            if (context.PARENTHESISCLOSE() != null)
                return CheckNumberAddExpression(context.numberExpression());
            if (context.ABSOLUTE().Length > 0)
                return Math.Abs(CheckNumberAddExpression(context.numberExpression()));
            if (context.something().ROUND() != null)
                return Provider.GetRound();
            if (context.something().NUMBER() != null)
                return Double.Parse(context.something().NUMBER().GetText());
            if (context.something().attribute() != null)
                return CheckNumberAttributeExpression(context.something().attribute());

            throw new ArgumentException(ErrorMessages.ConditionError.UNRECOGNIZED_NUMBER+context.GetText());
        }

        public double CheckNumberAttributeExpression(AttributeContext context)
        {
            if (context.character().PLAYER() != null)
            {
                return PlayerAttribute(context);
            }
            if (context.character().MONSTER() != null)
            {
                return MonsterAttribute(context);
            }
            if(context.character().TRAP() != null)
            {
                return TrapAttribute(context);
            }
            if(context.character().ME() != null)
            {
                //although it is unlikely for a player to become ME in this context
                if(Provider.GetMe().GetCharacterType() is PlayerType)
                {
                    return PlayerAttribute(context);
                }

                if(Provider.GetMe().GetCharacterType() is MonsterType)
                {
                    return MonsterAttribute(context);
                }
                if (Provider.GetMe().GetCharacterType() is TrapType)
                {
                    return TrapAttribute(context);
                }
            }
            ErrorList += ErrorMessages.ConditionError.UNRECOGNIZED_ATTRIBUTE_ERROR;
            ErrorList += (context.GetText() + "\n");
            ErrorFound = true;
            return -1;
        }

        public bool CheckBoolAttributeExpression(BoolExpressionContext context)
        {
            //monster is the first character
            if(context.attribute().ElementAt(0).character().MONSTER() != null || 
                (context.attribute().ElementAt(0).character().ME() != null && Provider.GetMe().GetCharacterType() is MonsterType))
            {
                //monster is the first character type is the attribute
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
                        else
                        {
                            if (context.COMPARE().GetText().Equals("=="))
                                return Provider.GetMe().GetCharacterType() == Provider.GetTrap().GetCharacterType().SpawnType;
                            if (context.COMPARE().GetText().Equals("!="))
                                return Provider.GetMe().GetCharacterType() != Provider.GetTrap().GetCharacterType().SpawnType;
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
                //monster is the first character, place is the attribue
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
        public double PlayerAttribute(AttributeContext context)
        {
            if (context.possibleAttributes().GetText().Equals("damage"))
                return Provider.GetPlayer().GetCharacterType().Damage;
            if (context.possibleAttributes().GetText().Equals("health"))
                return Provider.GetPlayer().GetHealth();
            if (context.possibleAttributes().GetText().Equals("place"))
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

        public double TrapAttribute(AttributeContext context)
        {
            if (context.possibleAttributes().GetText().Equals("damage"))
            {
                if(context.character().ME() != null)
                {
                    if (Provider.GetMe().GetCharacterType().Damage == -1)
                        return 0;
                    return Provider.GetMe().GetCharacterType().Damage;
                }
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

        public double MonsterAttribute(AttributeContext context)
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
