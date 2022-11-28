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
        //TODO: Check double int mismatches with tests
        public BoolExpressionContext BoolExpressionContext { get; set; }

        public GameParamProvider Provider { get; set; }

        public string ErrorList { get; set; }

        public bool ErrorFound { get; set; }

        public List<double> NumberList { get; set; } = new List<double>();

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
            if (context.numberExpression() != null && context.numberExpression().Length > 1)
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
            if (context.numToBoolOperation().NUMCOMPARE().GetText().Equals("<"))
                return CheckNumberExpression(context.numberExpression().ElementAt(0)) < CheckNumberExpression(context.numberExpression().ElementAt(1));
            else
                return CheckNumberExpression(context.numberExpression().ElementAt(0)) > CheckNumberExpression(context.numberExpression().ElementAt(1));
        }

        public bool CheckCompareExpression(BoolExpressionContext context)
        {
            if (context.numToBoolOperation().COMPARE().GetText().Equals("!="))
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
            if (functionExpressionContext.function().ALIVE() != null)
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
            if (context.BOOLCONNECTER().GetText().Equals("||"))
                return boolExp || CheckBoolExpression(context.boolExpression());
            else
                return boolExp && CheckBoolExpression(context.boolExpression());
        }

        public double CheckNumberExpression(NumberExpressionContext context)
        {
            if (context.PARENTHESISSTART() != null)
                return CheckNumberExpression(context.numberExpression().ElementAt(0));
            if (context.ABSOLUTESTART() != null)
            {
                return Math.Abs(CheckNumberExpression(context.numberExpression().ElementAt(0)));
            }
            if (context.NUMCONNECTERMULTIP() != null)
            {
                if (context.NUMCONNECTERMULTIP().GetText().Equals("*"))
                    return CheckNumberExpression(context.numberExpression().ElementAt(0)) *
                            CheckNumberExpression(context.numberExpression().ElementAt(1));
                if (context.NUMCONNECTERMULTIP().GetText().Equals("/"))
                {
                    if (CheckNumberExpression(context.numberExpression().ElementAt(1)) == 0)
                    {
                        ErrorList += ErrorMessages.ExpressionError.DIVIDING_WITH_ZERO;
                        ErrorList += (context.GetText() + "\n");
                        ErrorFound = true;
                        return -1;
                    }
                    return CheckNumberExpression(context.numberExpression().ElementAt(0)) /
                            CheckNumberExpression(context.numberExpression().ElementAt(1));
                }
                if (context.NUMCONNECTERMULTIP().GetText().Equals("%"))
                {
                    if (CheckNumberExpression(context.numberExpression().ElementAt(1)) == 0)
                    {
                        ErrorList += ErrorMessages.ExpressionError.DIVIDING_WITH_ZERO;
                        ErrorList += (context.GetText() + "\n");
                        ErrorFound = true;
                        return -1;
                    }
                    return CheckNumberExpression(context.numberExpression().ElementAt(0)) %
                            CheckNumberExpression(context.numberExpression().ElementAt(1));
                }
            }
            if (context.NUMCONNECTERADD() != null)
            {
                if (context.NUMCONNECTERADD().GetText().Equals("+"))
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

            if (context.NUMBER() != null)
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

        private bool CheckNumberOrAttributeExpressionNotEquals(NumberExpressionContext context1, NumberExpressionContext context2)
        {
            if (IsNumberExpressionNumber(context1))
            {
                return CheckNumberExpression(context1) != CheckNumberExpression(context2);
            }
            return !CheckAttributeExpression(context1).Equals(CheckAttributeExpression(context2));
        }

        private bool CheckNumberOrAttributeExpressionEquals(NumberExpressionContext context1, NumberExpressionContext context2)
        {
            if (IsNumberExpressionNumber(context1))
            {
                return CheckNumberExpression(context1) == CheckNumberExpression(context2);
            }
            return CheckAttributeExpression(context1).Equals(CheckAttributeExpression(context2));
        }

        private object CheckAttributeExpression(NumberExpressionContext context)
        {
            if (context.PARENTHESISSTART() != null)
            {
                return CheckAttributeExpression(context.numberExpression().ElementAt(0));
            }
            if (context.something() != null)
            {
                if (context.something().attribute().possibleAttributes().GetText().Equals("place") ||
                        context.something().attribute().possibleAttributes().GetText().Equals("teleport_place") ||
                        context.something().attribute().possibleAttributes().GetText().Equals("spawn_place"))
                {
                    return CheckPlaceAttributeExpression(context.something().attribute());
                }
                if (context.something().attribute().possibleAttributes().GetText().Equals("name") ||
                    (context.something().attribute().possibleAttributes().possibleAttributes() != null && 
                    context.something().attribute().possibleAttributes().possibleAttributes().ElementAt(1)
                        .GetText().Equals("name")))
                {
                    return CheckNameAttributeExpression(context.something().attribute());
                }
                if (context.something().attribute().possibleAttributes().GetText().Equals("type") ||
                    context.something().attribute().possibleAttributes().GetText().Equals("spawn_type"))
                {
                    return CheckTypeAttributeExpression(context.something().attribute());
                }
            }
            ErrorList += ErrorMessages.ConditionError.UNRECOGNIZED_ATTRIBUTE_ERROR + Provider.GetMe().Partner;
            ErrorList += (context.GetText() + "\n");
            ErrorFound = true;
            return null;
        }

        public bool IsNumberExpressionNumber(NumberExpressionContext context)
        {
            if (context.PARENTHESISSTART() != null || context.ABSOLUTESTART() != null)
                return IsNumberExpressionNumber(context.numberExpression().ElementAt(0));
            if (context.NUMCONNECTERADD() != null || context.NUMCONNECTERMULTIP() != null)
            {
                return IsNumberExpressionNumber(context.numberExpression().ElementAt(0)) &&
                    IsNumberExpressionNumber(context.numberExpression().ElementAt(1));
            }
            if (context.something() != null)
                return IsSomethingNumber(context.something());
            return false;
        }
        public bool IsSomethingNumber(SomethingContext context)
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
            //because then it is ROUND
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
            if (context.character().TRAP() != null)
            {
                return TrapNumberAttribute(context);
            }
            if (context.character().ME() != null)
            {
                if (Provider.GetMe() is Monster)
                    return MonsterNumberAttribute(context);
                if (Provider.GetMe() is Trap)
                    return TrapNumberAttribute(context);
            }
            if (context.character().PARTNER() != null)
            {
                if (Provider.GetPartner() == null)
                {
                    ErrorList += ErrorMessages.PartnerError.NON_EXISTANT_PARTNER + Provider.GetMe().Name;
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
            }
            ErrorList += ErrorMessages.ConditionError.UNRECOGNIZED_ATTRIBUTE_ERROR;
            ErrorList += (context.GetText() + "\n");
            ErrorFound = true;
            return -1;
        }

        public double PlayerNumberAttribute(AttributeContext context)
        {
            if (context.possibleAttributes().GetText().Equals("damage"))
                return Provider.GetPlayer().GetCharacterType().Damage;
            if (context.possibleAttributes().GetText().Equals("health"))
                return Provider.GetPlayer().GetHealth();
            if ((context.possibleAttributes().possibleAttributes() != null && context.possibleAttributes().possibleAttributes().Length > 1) && 
                context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("place"))
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
                if (Provider.GetMe().GetCharacterType().Heal == StaticStartValues.PLACEHOLDER_HEAL)
                {
                    ErrorList += (ErrorMessages.ConditionError.CHARACTER_TYPE_HAS_NO_HEAL_DEFINED);
                    ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                    ErrorList += (context.GetText() + "\n");
                    ErrorFound = true;
                    return -1;
                }
                return Provider.GetMe().GetCharacterType().Heal;
            }
            if (context.possibleAttributes().GetText().Equals("health"))
            {
                return Provider.GetMe().GetHealth();
            }
            if ((context.possibleAttributes().possibleAttributes() != null && context.possibleAttributes().possibleAttributes().Length > 1) && 
                context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("place"))
            {
                if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("x"))
                    return Provider.GetMe().Place.X;
                return Provider.GetMe().Place.Y;
            }
            if ((context.possibleAttributes().possibleAttributes() != null && context.possibleAttributes().possibleAttributes().Length > 1) && 
                context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("teleport_place"))
            {
                if (Provider.GetMe().GetCharacterType().TeleportPlace.DirectionTo(StaticStartValues.PLACEHOLDER_PLACE)
                        == Directions.COLLISION)
                {
                    ErrorList += (ErrorMessages.ConditionError.CHARACTER_TYPE_HAS_NO_TELEPORT_PLACE_DEFINED);
                    ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                    ErrorList += (context.GetText() + "\n");
                    ErrorFound = true;
                    return -1;
                }
                if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("x"))
                    return Provider.GetMe().GetCharacterType().TeleportPlace.X;
                return Provider.GetMe().GetCharacterType().TeleportPlace.Y;
            }
            if ((context.possibleAttributes().possibleAttributes() != null && context.possibleAttributes().possibleAttributes().Length > 1) && 
                context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("spawn_place"))
            {
                if (Provider.GetMe().GetCharacterType().SpawnPlace.DirectionTo(StaticStartValues.PLACEHOLDER_PLACE)
                        == Directions.COLLISION)
                {
                    ErrorList += (ErrorMessages.ConditionError.CHARACTER_TYPE_HAS_NO_SPAWN_PLACE_DEFINED);
                    ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                    ErrorList += (context.GetText() + "\n");
                    ErrorFound = true;
                    return -1;
                }
                if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("x"))
                    return Provider.GetMe().GetCharacterType().SpawnPlace.X;
                return Provider.GetMe().GetCharacterType().SpawnPlace.Y;
            }
            if ((context.possibleAttributes().possibleAttributes() != null && context.possibleAttributes().possibleAttributes().Length > 1) && 
                context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("type"))
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
                    if (Provider.GetMe().GetCharacterType().Heal == StaticStartValues.PLACEHOLDER_HEAL)
                    {
                        ErrorList += (ErrorMessages.ConditionError.CHARACTER_TYPE_HAS_NO_HEAL_DEFINED);
                        ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
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
            if ((context.possibleAttributes().possibleAttributes() != null && context.possibleAttributes().possibleAttributes().Length > 1) && 
                context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("spawn_type"))
            {
                if (Provider.GetMe().GetCharacterType().SpawnType == null)
                {
                    ErrorList += (ErrorMessages.ConditionError.CHARACTER_TYPE_HAS_NO_SPAWN_TYPE_DEFINED);
                    ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                    ErrorList += (context.GetText() + "\n");
                    ErrorFound = true;
                    return -1;
                }
                if (Program.GetCharacterType(Provider.GetMe().GetCharacterType().SpawnType.Name) == null)
                {
                    ErrorList += (ErrorMessages.ConditionError.SPAWN_TYPE_NOT_FOUND);
                    ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                    ErrorList += (context.GetText() + "\n");
                    ErrorFound = true;
                    return -1;
                }
                if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("damage"))
                {
                    if (Program.GetCharacterType(Provider.GetMe().GetCharacterType().SpawnType.Name).Damage
                            == StaticStartValues.PLACEHOLDER_DAMAGE)
                    {
                        ErrorList += (ErrorMessages.ConditionError.CHARACTER_TYPE_HAS_NO_DAMAGE_DEFINED);
                        ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                        ErrorList += (context.GetText() + "\n");
                        ErrorFound = true;
                        return -1;
                    }
                    return Program.GetCharacterType(Provider.GetMe().GetCharacterType().SpawnType.Name).Damage;
                }
                if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("health"))
                {
                    if (Program.GetCharacterType(Provider.GetMe().GetCharacterType().SpawnType.Name).Health
                            == StaticStartValues.PLACEHOLDER_HEALTH)
                    {
                        ErrorList += (ErrorMessages.ConditionError.CHARACTER_TYPE_HAS_NO_HEALTH_DEFINED);
                        ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                        ErrorList += (context.GetText() + "\n");
                        ErrorFound = true;
                        return -1;
                    }
                    return Program.GetCharacterType(Provider.GetMe().GetCharacterType().SpawnType.Name).Health;
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

        public double MonsterNumberAttribute(AttributeContext context)
        {
            if (context.possibleAttributes().GetText().Equals("heal") ||
                    ((context.possibleAttributes().possibleAttributes() != null && context.possibleAttributes().possibleAttributes().Length > 1) &&
                    context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("type") &&
                        context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("heal")))
            {
                ErrorList += (ErrorMessages.ConditionError.ONLY_TRAP_CAN_HEAL);
                ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                ErrorList += (context.GetText() + "\n");
                ErrorFound = true;
                return -1;
            }
            if ((context.possibleAttributes().possibleAttributes() != null && context.possibleAttributes().possibleAttributes().Length > 1) && 
                context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("teleport_place"))
            {
                ErrorList += (ErrorMessages.TeleportError.ONLY_TRAP_CAN_TELEPORT);
                ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                ErrorList += (context.GetText() + "\n");
                ErrorFound = true;
                return -1;
            }
            if ((context.possibleAttributes().possibleAttributes() != null && context.possibleAttributes().possibleAttributes().Length > 1) && 
                (context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("spawn_place") ||
                context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("spawn_type")))
            {
                ErrorList += (ErrorMessages.SpawnError.ONLY_TRAP_CAN_SPAWN);
                ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                ErrorList += (context.GetText() + "\n");
                ErrorFound = true;
                return -1;
            }
            if (context.character().PARTNER() != null)
            {
                if (context.possibleAttributes().GetText().Equals("damage"))
                {
                    if (Provider.GetPartner().GetCharacterType().Damage == StaticStartValues.PLACEHOLDER_DAMAGE)
                    {
                        ErrorList += (ErrorMessages.ConditionError.CHARACTER_TYPE_HAS_NO_DAMAGE_DEFINED);
                        ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                        ErrorList += (context.GetText() + "\n");
                        ErrorFound = true;
                        return -1;
                    }
                    return Provider.GetPartner().GetCharacterType().Damage;
                }
                if (context.possibleAttributes().GetText().Equals("health"))
                {
                    return Provider.GetPartner().GetHealth();
                }
                if ((context.possibleAttributes().possibleAttributes() != null && context.possibleAttributes().possibleAttributes().Length > 1) && 
                    context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("place"))
                {
                    if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("x"))
                        return Provider.GetPartner().Place.X;
                    else
                        return Provider.GetPartner().Place.Y;
                }
                if ((context.possibleAttributes().possibleAttributes() != null && context.possibleAttributes().possibleAttributes().Length > 1) && 
                    context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("type"))
                {
                    if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("damage"))
                    {
                        if (Program.GetCharacterType(Provider.GetPartner().GetCharacterType().SpawnType.Name).Damage
                                == StaticStartValues.PLACEHOLDER_DAMAGE)
                        {
                            ErrorList += (ErrorMessages.ConditionError.CHARACTER_TYPE_HAS_NO_DAMAGE_DEFINED);
                            ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                            ErrorList += (context.GetText() + "\n");
                            ErrorFound = true;
                            return -1;
                        }
                        return Provider.GetPartner().GetCharacterType().Damage;
                    }
                    if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("health"))
                    {
                        if (Provider.GetPartner().GetCharacterType().Health == StaticStartValues.PLACEHOLDER_HEALTH)
                        {
                            ErrorList += (ErrorMessages.ConditionError.CHARACTER_TYPE_HAS_NO_HEALTH_DEFINED);
                            ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                            ErrorList += (context.GetText() + "\n");
                            ErrorFound = true;
                            return -1;
                        }
                        return Provider.GetPartner().GetCharacterType().Health;
                    }
                }
            }
            if (context.character().ME() != null)
            {
                return MeNumberAttribute(context);
            }
            if (context.character().MONSTER() != null)
            {
                if (context.possibleAttributes().GetText().Equals("damage"))
                {
                    if (Provider.GetMonster().GetCharacterType().Damage == StaticStartValues.PLACEHOLDER_DAMAGE)
                    {
                        ErrorList += (ErrorMessages.ConditionError.CHARACTER_TYPE_HAS_NO_DAMAGE_DEFINED);
                        ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                        ErrorList += (context.GetText() + "\n");
                        ErrorFound = true;
                        return -1;
                    }
                    return Provider.GetMonster().GetCharacterType().Damage;
                }
                if (context.possibleAttributes().GetText().Equals("health"))
                {
                    return Provider.GetMonster().GetHealth();
                }
                if ((context.possibleAttributes().possibleAttributes() != null && context.possibleAttributes().possibleAttributes().Length > 1) && 
                    context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("place"))
                {
                    if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("x"))
                        return Provider.GetMonster().Place.X;
                    else
                        return Provider.GetMonster().Place.Y;
                }
                if ((context.possibleAttributes().possibleAttributes() != null && context.possibleAttributes().possibleAttributes().Length > 1) && 
                    context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("type"))
                {
                    if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("damage"))
                    {
                        if (Program.GetCharacterType(Provider.GetMonster().GetCharacterType().SpawnType.Name).Damage
                                == StaticStartValues.PLACEHOLDER_DAMAGE)
                        {
                            ErrorList += (ErrorMessages.ConditionError.CHARACTER_TYPE_HAS_NO_DAMAGE_DEFINED);
                            ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                            ErrorList += (context.GetText() + "\n");
                            ErrorFound = true;
                            return -1;
                        }
                        return Provider.GetMonster().GetCharacterType().Damage;
                    }
                    if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("health"))
                    {
                        if (Provider.GetMonster().GetCharacterType().Health == StaticStartValues.PLACEHOLDER_HEALTH)
                        {
                            ErrorList += (ErrorMessages.ConditionError.CHARACTER_TYPE_HAS_NO_HEALTH_DEFINED);
                            ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                            ErrorList += (context.GetText() + "\n");
                            ErrorFound = true;
                            return -1;
                        }
                        return Provider.GetMonster().GetCharacterType().Health;
                    }
                }
            }

            ErrorList += (ErrorMessages.ConditionError.MONSTER_ATTRIBUTE_ERROR);
            ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
            ErrorList += (context.GetText() + "\n");
            ErrorFound = true;
            return -1;
        }

        public double TrapNumberAttribute(AttributeContext context)
        {
            if (context.possibleAttributes().GetText().Equals("health") ||
                    ((context.possibleAttributes().possibleAttributes() != null && context.possibleAttributes().possibleAttributes().Length > 1) && 
                    context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("type") &&
                        context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("health")))
            {
                ErrorList += (ErrorMessages.ConditionError.TRAP_HAS_NO_HEALTH);
                ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                ErrorList += (context.GetText() + "\n");
                ErrorFound = true;
                return -1;
            }
            if (context.character().PARTNER() != null)
            {
                if (context.possibleAttributes().GetText().Equals("damage"))
                {
                    if (Provider.GetPartner().GetCharacterType().Damage == StaticStartValues.PLACEHOLDER_DAMAGE)
                    {
                        ErrorList += (ErrorMessages.ConditionError.CHARACTER_TYPE_HAS_NO_DAMAGE_DEFINED);
                        ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                        ErrorList += (context.GetText() + "\n");
                        ErrorFound = true;
                        return -1;
                    }
                    return Provider.GetPartner().GetCharacterType().Damage;
                }
                if (context.possibleAttributes().GetText().Equals("heal"))
                {
                    if (Provider.GetPartner().GetCharacterType().Heal == StaticStartValues.PLACEHOLDER_HEAL)
                    {
                        ErrorList += (ErrorMessages.ConditionError.CHARACTER_TYPE_HAS_NO_HEAL_DEFINED);
                        ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                        ErrorList += (context.GetText() + "\n");
                        ErrorFound = true;
                        return -1;
                    }
                    return Provider.GetPartner().GetCharacterType().Heal;
                }
                if (context.possibleAttributes().GetText().Equals("health"))
                {
                    return Provider.GetPartner().GetHealth();
                }
                if ((context.possibleAttributes().possibleAttributes() != null && context.possibleAttributes().possibleAttributes().Length > 1) && 
                    context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("place"))
                {
                    if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("x"))
                        return Provider.GetPartner().Place.X;
                    else
                        return Provider.GetPartner().Place.Y;
                }
                if ((context.possibleAttributes().possibleAttributes() != null && context.possibleAttributes().possibleAttributes().Length > 1) && 
                    context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("teleport_place"))
                {
                    if (Provider.GetPartner().GetCharacterType().TeleportPlace.DirectionTo(StaticStartValues.PLACEHOLDER_PLACE)
                            == Directions.COLLISION)
                    {
                        ErrorList += (ErrorMessages.ConditionError.CHARACTER_TYPE_HAS_NO_TELEPORT_PLACE_DEFINED);
                        ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                        ErrorList += (context.GetText() + "\n");
                        ErrorFound = true;
                        return -1;
                    }
                    if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("x"))
                        return Provider.GetPartner().GetCharacterType().TeleportPlace.X;
                    else
                        return Provider.GetPartner().GetCharacterType().TeleportPlace.Y;
                }
                if ((context.possibleAttributes().possibleAttributes() != null && context.possibleAttributes().possibleAttributes().Length > 1) && 
                    context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("spawn_place"))
                {
                    if (Provider.GetPartner().GetCharacterType().SpawnPlace.DirectionTo(StaticStartValues.PLACEHOLDER_PLACE)
                            == Directions.COLLISION)
                    {
                        ErrorList += (ErrorMessages.ConditionError.CHARACTER_TYPE_HAS_NO_SPAWN_PLACE_DEFINED);
                        ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                        ErrorList += (context.GetText() + "\n");
                        ErrorFound = true;
                        return -1;
                    }
                    if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("x"))
                        return Provider.GetPartner().GetCharacterType().SpawnPlace.X;
                    else
                        return Provider.GetPartner().GetCharacterType().SpawnPlace.Y;
                }
                if ((context.possibleAttributes().possibleAttributes() != null && context.possibleAttributes().possibleAttributes().Length > 1) && 
                    context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("type"))
                {
                    if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("damage"))
                    {
                        if (Program.GetCharacterType(Provider.GetPartner().GetCharacterType().SpawnType.Name).Damage
                                == StaticStartValues.PLACEHOLDER_DAMAGE)
                        {
                            ErrorList += (ErrorMessages.ConditionError.CHARACTER_TYPE_HAS_NO_DAMAGE_DEFINED);
                            ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                            ErrorList += (context.GetText() + "\n");
                            ErrorFound = true;
                            return -1;
                        }
                        return Provider.GetPartner().GetCharacterType().Damage;
                    }
                    if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("heal"))
                    {
                        if (Provider.GetPartner().GetCharacterType().Heal == StaticStartValues.PLACEHOLDER_HEAL)
                        {
                            ErrorList += (ErrorMessages.ConditionError.CHARACTER_TYPE_HAS_NO_HEAL_DEFINED);
                            ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                            ErrorList += (context.GetText() + "\n");
                            ErrorFound = true;
                            return -1;
                        }
                        return Provider.GetPartner().GetCharacterType().Heal;
                    }
                }
                if ((context.possibleAttributes().possibleAttributes() != null && context.possibleAttributes().possibleAttributes().Length > 1) && 
                    context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("spawn_type"))
                {
                    if (Provider.GetPartner().GetCharacterType().SpawnType == null)
                    {
                        ErrorList += (ErrorMessages.ConditionError.CHARACTER_TYPE_HAS_NO_SPAWN_TYPE_DEFINED);
                        ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                        ErrorList += (context.GetText() + "\n");
                        ErrorFound = true;
                        return -1;
                    }
                    if (Program.GetCharacterType(Provider.GetPartner().GetCharacterType().SpawnType.Name) == null)
                    {
                        ErrorList += (ErrorMessages.ConditionError.SPAWN_TYPE_NOT_FOUND);
                        ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                        ErrorList += (context.GetText() + "\n");
                        ErrorFound = true;
                        return -1;
                    }
                    if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("damage"))
                    {
                        if (Program.GetCharacterType(Provider.GetPartner().GetCharacterType().SpawnType.Name).Damage
                                == StaticStartValues.PLACEHOLDER_DAMAGE)
                        {
                            ErrorList += (ErrorMessages.ConditionError.CHARACTER_TYPE_HAS_NO_DAMAGE_DEFINED);
                            ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                            ErrorList += (context.GetText() + "\n");
                            ErrorFound = true;
                            return -1;
                        }
                        return Program.GetCharacterType(Provider.GetPartner().GetCharacterType().SpawnType.Name).Damage;
                    }
                    if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("health"))
                    {
                        if (Program.GetCharacterType(Provider.GetPartner().GetCharacterType().SpawnType.Name).Health
                                == StaticStartValues.PLACEHOLDER_HEALTH)
                        {
                            ErrorList += (ErrorMessages.ConditionError.CHARACTER_TYPE_HAS_NO_HEALTH_DEFINED);
                            ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                            ErrorList += (context.GetText() + "\n");
                            ErrorFound = true;
                            return -1;
                        }
                        return Program.GetCharacterType(Provider.GetPartner().GetCharacterType().SpawnType.Name).Health;
                    }
                }
            }
            if (context.character().ME() != null)
            {
                return MeNumberAttribute(context);
            }
            if (context.character().TRAP() != null)
            {
                if (context.possibleAttributes().GetText().Equals("damage"))
                {
                    if (Provider.GetTrap().GetCharacterType().Damage == StaticStartValues.PLACEHOLDER_DAMAGE)
                    {
                        ErrorList += (ErrorMessages.ConditionError.CHARACTER_TYPE_HAS_NO_DAMAGE_DEFINED);
                        ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                        ErrorList += (context.GetText() + "\n");
                        ErrorFound = true;
                        return -1;
                    }
                    return Provider.GetTrap().GetCharacterType().Damage;
                }
                if (context.possibleAttributes().GetText().Equals("heal"))
                {
                    if (Provider.GetTrap().GetCharacterType().Heal == StaticStartValues.PLACEHOLDER_HEAL)
                    {
                        ErrorList += (ErrorMessages.ConditionError.CHARACTER_TYPE_HAS_NO_HEAL_DEFINED);
                        ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                        ErrorList += (context.GetText() + "\n");
                        ErrorFound = true;
                        return -1;
                    }
                    return Provider.GetTrap().GetCharacterType().Heal;
                }
                if (context.possibleAttributes().GetText().Equals("health"))
                {
                    return Provider.GetTrap().GetHealth();
                }
                if ((context.possibleAttributes().possibleAttributes() != null && context.possibleAttributes().possibleAttributes().Length > 1) && 
                    context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("place"))
                {
                    if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("x"))
                        return Provider.GetTrap().Place.X;
                    else
                        return Provider.GetTrap().Place.Y;
                }
                if ((context.possibleAttributes().possibleAttributes() != null && context.possibleAttributes().possibleAttributes().Length > 1) && 
                    context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("teleport_place"))
                {
                    if (Provider.GetTrap().GetCharacterType().TeleportPlace.DirectionTo(StaticStartValues.PLACEHOLDER_PLACE)
                            == Directions.COLLISION)
                    {
                        ErrorList += (ErrorMessages.ConditionError.CHARACTER_TYPE_HAS_NO_TELEPORT_PLACE_DEFINED);
                        ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                        ErrorList += (context.GetText() + "\n");
                        ErrorFound = true;
                        return -1;
                    }
                    if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("x"))
                        return Provider.GetTrap().GetCharacterType().TeleportPlace.X;
                    else
                        return Provider.GetTrap().GetCharacterType().TeleportPlace.Y;
                }
                if ((context.possibleAttributes().possibleAttributes() != null && context.possibleAttributes().possibleAttributes().Length > 1) && 
                    context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("spawn_place"))
                {
                    if (Provider.GetTrap().GetCharacterType().SpawnPlace.DirectionTo(StaticStartValues.PLACEHOLDER_PLACE)
                            == Directions.COLLISION)
                    {
                        ErrorList += (ErrorMessages.ConditionError.CHARACTER_TYPE_HAS_NO_SPAWN_PLACE_DEFINED);
                        ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                        ErrorList += (context.GetText() + "\n");
                        ErrorFound = true;
                        return -1;
                    }
                    if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("x"))
                        return Provider.GetTrap().GetCharacterType().SpawnPlace.X;
                    else
                        return Provider.GetTrap().GetCharacterType().SpawnPlace.Y;
                }
                if ((context.possibleAttributes().possibleAttributes() != null && context.possibleAttributes().possibleAttributes().Length > 1) && 
                    context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("type"))
                {
                    if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("damage"))
                    {
                        if (Program.GetCharacterType(Provider.GetTrap().GetCharacterType().SpawnType.Name).Damage
                                == StaticStartValues.PLACEHOLDER_DAMAGE)
                        {
                            ErrorList += (ErrorMessages.ConditionError.CHARACTER_TYPE_HAS_NO_DAMAGE_DEFINED);
                            ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                            ErrorList += (context.GetText() + "\n");
                            ErrorFound = true;
                            return -1;
                        }
                        return Provider.GetTrap().GetCharacterType().Damage;
                    }
                    if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("heal"))
                    {
                        if (Provider.GetTrap().GetCharacterType().Heal == StaticStartValues.PLACEHOLDER_HEAL)
                        {
                            ErrorList += (ErrorMessages.ConditionError.CHARACTER_TYPE_HAS_NO_HEAL_DEFINED);
                            ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                            ErrorList += (context.GetText() + "\n");
                            ErrorFound = true;
                            return -1;
                        }
                        return Provider.GetTrap().GetCharacterType().Heal;
                    }
                }
                if ((context.possibleAttributes().possibleAttributes() != null && context.possibleAttributes().possibleAttributes().Length > 1) && 
                    context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("spawn_type"))
                {
                    if (Provider.GetTrap().GetCharacterType().SpawnType == null)
                    {
                        ErrorList += (ErrorMessages.ConditionError.CHARACTER_TYPE_HAS_NO_SPAWN_TYPE_DEFINED);
                        ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                        ErrorList += (context.GetText() + "\n");
                        ErrorFound = true;
                        return -1;
                    }
                    if (Program.GetCharacterType(Provider.GetTrap().GetCharacterType().SpawnType.Name) == null)
                    {
                        ErrorList += (ErrorMessages.ConditionError.SPAWN_TYPE_NOT_FOUND);
                        ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                        ErrorList += (context.GetText() + "\n");
                        ErrorFound = true;
                        return -1;
                    }
                    if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("damage"))
                    {
                        if (Program.GetCharacterType(Provider.GetTrap().GetCharacterType().SpawnType.Name).Damage
                                == StaticStartValues.PLACEHOLDER_DAMAGE)
                        {
                            ErrorList += (ErrorMessages.ConditionError.CHARACTER_TYPE_HAS_NO_DAMAGE_DEFINED);
                            ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                            ErrorList += (context.GetText() + "\n");
                            ErrorFound = true;
                            return -1;
                        }
                        return Program.GetCharacterType(Provider.GetTrap().GetCharacterType().SpawnType.Name).Damage;
                    }
                    if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("health"))
                    {
                        if (Program.GetCharacterType(Provider.GetTrap().GetCharacterType().SpawnType.Name).Health
                                == StaticStartValues.PLACEHOLDER_HEALTH)
                        {
                            ErrorList += (ErrorMessages.ConditionError.CHARACTER_TYPE_HAS_NO_HEALTH_DEFINED);
                            ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                            ErrorList += (context.GetText() + "\n");
                            ErrorFound = true;
                            return -1;
                        }
                        return Program.GetCharacterType(Provider.GetTrap().GetCharacterType().SpawnType.Name).Health;
                    }
                }
            }

            ErrorList += (ErrorMessages.ConditionError.TRAP_ATTRIBUTE_ERROR);
            ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
            ErrorList += (context.GetText() + "\n");
            ErrorFound = true;
            return -1;
        }

        public Place CheckPlaceAttributeExpression(AttributeContext context)
        {
            if (context.character().PLAYER() != null)
            {
                return PlayerPlaceAttribute(context);
            }
            if (context.character().MONSTER() != null)
            {
                return MonsterPlaceAttribute(context);
            }
            if (context.character().TRAP() != null)
            {
                return TrapPlaceAttribute(context);
            }
            if (context.character().ME() != null)
            {
                if (Provider.GetMe() is Monster)
                    return MonsterPlaceAttribute(context);
                if (Provider.GetMe() is Trap)
                    return TrapPlaceAttribute(context);
            }
            if (context.character().PARTNER() != null)
            {
                if (Provider.GetPartner() == null)
                {
                    ErrorList += ErrorMessages.PartnerError.NON_EXISTANT_PARTNER + Provider.GetMe().Name;
                    ErrorList += (context.GetText() + "\n");
                    ErrorFound = true;
                    return StaticStartValues.PLACEHOLDER_PLACE;
                }
                if (Provider.GetPartner() is Monster)
                {
                    return MonsterPlaceAttribute(context);
                }
                if (Provider.GetPartner() is Trap)
                {
                    return TrapPlaceAttribute(context);
                }
            }
            ErrorList += ErrorMessages.ConditionError.UNRECOGNIZED_ATTRIBUTE_ERROR;
            ErrorList += (context.GetText() + "\n");
            ErrorFound = true;
            return StaticStartValues.PLACEHOLDER_PLACE;
        }

        private Place PlayerPlaceAttribute(AttributeContext context)
        {
            if (context.possibleAttributes().GetText().Equals("place"))
                return Provider.GetPlayer().Place;
            ErrorList += (ErrorMessages.ConditionError.PLAYER_ATTRIBUTE_ERROR);
            ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
            ErrorList += (context.GetText() + "\n");
            ErrorFound = true;
            return StaticStartValues.PLACEHOLDER_PLACE;
        }

        private Place MePlaceAttribute(AttributeContext context)
        {
            if (context.possibleAttributes().GetText().Equals("place"))
            {
                return Provider.GetMe().Place;
            }
            if (context.possibleAttributes().GetText().Equals("teleport_place"))
            {
                if (Provider.GetMe().GetCharacterType().TeleportPlace.Equals(StaticStartValues.PLACEHOLDER_PLACE))
                {
                    ErrorList += (ErrorMessages.ConditionError.CHARACTER_TYPE_HAS_NO_TELEPORT_PLACE_DEFINED);
                    ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                    ErrorList += (context.GetText() + "\n");
                    ErrorFound = true;
                    return StaticStartValues.PLACEHOLDER_PLACE;
                }
                return Provider.GetMe().GetCharacterType().TeleportPlace;
            }
            if (context.possibleAttributes().GetText().Equals("spawn_place"))
            {
                if (Provider.GetMe().GetCharacterType().SpawnPlace.Equals(StaticStartValues.PLACEHOLDER_PLACE))
                {
                    ErrorList += (ErrorMessages.ConditionError.CHARACTER_TYPE_HAS_NO_SPAWN_PLACE_DEFINED);
                    ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                    ErrorList += (context.GetText() + "\n");
                    ErrorFound = true;
                    return StaticStartValues.PLACEHOLDER_PLACE;
                }
                return Provider.GetMe().GetCharacterType().SpawnPlace;
            }
            ErrorList += ErrorMessages.ConditionError.UNRECOGNIZED_ATTRIBUTE_ERROR;
            ErrorList += (context.GetText() + "\n");
            ErrorFound = true;
            return StaticStartValues.PLACEHOLDER_PLACE;
        }

        private Place MonsterPlaceAttribute(AttributeContext context)
        {
            if (context.possibleAttributes().GetText().Equals("teleport_place"))
            {
                ErrorList += ErrorMessages.TeleportError.ONLY_TRAP_CAN_TELEPORT;
                ErrorList += (context.GetText() + "\n");
                ErrorFound = true;
                return StaticStartValues.PLACEHOLDER_PLACE;
            }
            if (context.possibleAttributes().GetText().Equals("spawn_place"))
            {
                ErrorList += ErrorMessages.SpawnError.ONLY_TRAP_CAN_SPAWN;
                ErrorList += (context.GetText() + "\n");
                ErrorFound = true;
                return StaticStartValues.PLACEHOLDER_PLACE;
            }
            if (context.character().PARTNER() != null)
            {
                if (context.possibleAttributes().GetText().Equals("place"))
                    return Provider.GetPartner().Place;
            }
            if (context.character().ME() != null)
            {
                return MePlaceAttribute(context);
            }
            if (context.character().MONSTER() != null)
            {
                if (context.possibleAttributes().GetText().Equals("place"))
                    return Provider.GetMonster().Place;
            }
            ErrorList += (ErrorMessages.ConditionError.MONSTER_ATTRIBUTE_ERROR);
            ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
            ErrorList += (context.GetText() + "\n");
            ErrorFound = true;
            return StaticStartValues.PLACEHOLDER_PLACE;
        }

        private Place TrapPlaceAttribute(AttributeContext context)
        {
            if (context.character().PARTNER() != null)
            {
                if (context.possibleAttributes().GetText().Equals("place"))
                {
                    return Provider.GetPartner().Place;
                }
                if (context.possibleAttributes().GetText().Equals("teleport_place"))
                {
                    if (Provider.GetPartner().GetCharacterType().TeleportPlace.Equals(StaticStartValues.PLACEHOLDER_PLACE))
                    {
                        ErrorList += (ErrorMessages.ConditionError.CHARACTER_TYPE_HAS_NO_TELEPORT_PLACE_DEFINED);
                        ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                        ErrorList += (context.GetText() + "\n");
                        ErrorFound = true;
                        return StaticStartValues.PLACEHOLDER_PLACE;
                    }
                    return Provider.GetPartner().GetCharacterType().TeleportPlace;
                }
                if (context.possibleAttributes().GetText().Equals("spawn_place"))
                {
                    if (Provider.GetPartner().GetCharacterType().SpawnPlace.Equals(StaticStartValues.PLACEHOLDER_PLACE))
                    {
                        ErrorList += (ErrorMessages.ConditionError.CHARACTER_TYPE_HAS_NO_SPAWN_PLACE_DEFINED);
                        ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                        ErrorList += (context.GetText() + "\n");
                        ErrorFound = true;
                        return StaticStartValues.PLACEHOLDER_PLACE;
                    }
                    return Provider.GetPartner().GetCharacterType().SpawnPlace;
                }
            }
            if(context.character().ME() != null)
            {
                return MePlaceAttribute(context);
            }
            if(context.character().TRAP() != null)
            {
                if (context.possibleAttributes().GetText().Equals("place"))
                {
                    return Provider.GetTrap().Place;
                }
                if (context.possibleAttributes().GetText().Equals("teleport_place"))
                {
                    if (Provider.GetTrap().GetCharacterType().TeleportPlace.Equals(StaticStartValues.PLACEHOLDER_PLACE))
                    {
                        ErrorList += (ErrorMessages.ConditionError.CHARACTER_TYPE_HAS_NO_TELEPORT_PLACE_DEFINED);
                        ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                        ErrorList += (context.GetText() + "\n");
                        ErrorFound = true;
                        return StaticStartValues.PLACEHOLDER_PLACE;
                    }
                    return Provider.GetTrap().GetCharacterType().TeleportPlace;
                }
                if (context.possibleAttributes().GetText().Equals("spawn_place"))
                {
                    if (Provider.GetTrap().GetCharacterType().SpawnPlace.Equals(StaticStartValues.PLACEHOLDER_PLACE))
                    {
                        ErrorList += (ErrorMessages.ConditionError.CHARACTER_TYPE_HAS_NO_SPAWN_PLACE_DEFINED);
                        ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                        ErrorList += (context.GetText() + "\n");
                        ErrorFound = true;
                        return StaticStartValues.PLACEHOLDER_PLACE;
                    }
                    return Provider.GetTrap().GetCharacterType().SpawnPlace;
                }
            }
            ErrorList += ErrorMessages.ConditionError.UNRECOGNIZED_ATTRIBUTE_ERROR;
            ErrorList += (context.GetText() + "\n");
            ErrorFound = true;
            return StaticStartValues.PLACEHOLDER_PLACE;
        }

        private string CheckNameAttributeExpression(AttributeContext context)
        {
            if (context.character().PLAYER() != null)
            {
                return PlayerNameAttribute(context);
            }
            if (context.character().MONSTER() != null)
            {
                return MonsterNameAttribute(context);
            }
            if (context.character().TRAP() != null)
            {
                return TrapNameAttribute(context);
            }
            if (context.character().ME() != null)
            {
                if (Provider.GetMe() is Monster)
                    return MonsterNameAttribute(context);
                if (Provider.GetMe() is Trap)
                    return TrapNameAttribute(context);
            }
            if (context.character().PARTNER() != null)
            {
                if (Provider.GetPartner() == null)
                {
                    ErrorList += ErrorMessages.PartnerError.NON_EXISTANT_PARTNER + Provider.GetMe().Name;
                    ErrorList += (context.GetText() + "\n");
                    ErrorFound = true;
                    return StaticStartValues.PLACEHOLDER_NAME;
                }
                if (Provider.GetPartner() is Monster)
                {
                    return MonsterNameAttribute(context);
                }
                if (Provider.GetPartner() is Trap)
                {
                    return TrapNameAttribute(context);
                }
            }
            ErrorList += ErrorMessages.ConditionError.UNRECOGNIZED_ATTRIBUTE_ERROR;
            ErrorList += (context.GetText() + "\n");
            ErrorFound = true;
            return StaticStartValues.PLACEHOLDER_NAME;
        }

        private string PlayerNameAttribute(AttributeContext context)
        {
            if ((context.possibleAttributes().possibleAttributes() != null && context.possibleAttributes().possibleAttributes().Length > 1) && 
                context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("type"))
            {
                return Provider.GetPlayer().GetCharacterType().Name;
            }
            if (context.possibleAttributes().GetText().Equals("name"))
            {
                if (Provider.GetPlayer().Name.Equals(StaticStartValues.PLACEHOLDER_NAME))
                {
                    ErrorList += ErrorMessages.ConditionError.PLAYER_HAS_NO_NAME;
                    ErrorList += (context.GetText() + "\n");
                    ErrorFound = true;
                    return StaticStartValues.PLACEHOLDER_NAME;
                }
                return Provider.GetPlayer().Name;
            }
            ErrorList += ErrorMessages.ConditionError.PLAYER_ATTRIBUTE_ERROR;
            ErrorList += (context.GetText() + "\n");
            ErrorFound = true;
            return StaticStartValues.PLACEHOLDER_NAME;
        }

        public string MeNameAttribute(AttributeContext context)
        {
            if (context.possibleAttributes().GetText().Equals("name"))
            {
                if (Provider.GetMe().Name.Equals(StaticStartValues.PLACEHOLDER_NAME))
                {
                    ErrorList += ErrorMessages.ConditionError.CHARACTER_HAS_NO_NAME;
                    ErrorList += (context.GetText() + "\n");
                    ErrorFound = true;
                    return StaticStartValues.PLACEHOLDER_NAME;
                }
                return Provider.GetMe().Name;
            }
            if ((context.possibleAttributes().possibleAttributes() != null && context.possibleAttributes().possibleAttributes().Length > 1) && 
                context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("type"))
            {
                return Provider.GetMe().GetCharacterType().Name;
            }
            if ((context.possibleAttributes().possibleAttributes() != null && context.possibleAttributes().possibleAttributes().Length > 1) && 
                context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("spawn_type"))
            {
                if (Provider.GetMe().GetCharacterType().SpawnType == null)
                {
                    ErrorList += (ErrorMessages.ConditionError.CHARACTER_TYPE_HAS_NO_SPAWN_TYPE_DEFINED);
                    ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                    ErrorList += (context.GetText() + "\n");
                    ErrorFound = true;
                    return StaticStartValues.PLACEHOLDER_NAME;
                }
                if (Program.GetCharacterType(Provider.GetMe().GetCharacterType().SpawnType.Name) == null)
                {
                    ErrorList += (ErrorMessages.ConditionError.SPAWN_TYPE_NOT_FOUND);
                    ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                    ErrorList += (context.GetText() + "\n");
                    ErrorFound = true;
                    return StaticStartValues.PLACEHOLDER_NAME;
                }
                return Provider.GetMe().GetCharacterType().SpawnType.Name;
            }
            ErrorList += ErrorMessages.ConditionError.UNRECOGNIZED_ATTRIBUTE_ERROR;
            ErrorList += (context.GetText() + "\n");
            ErrorFound = true;
            return StaticStartValues.PLACEHOLDER_NAME;
        }

        public string MonsterNameAttribute(AttributeContext context)
        {
            if ((context.possibleAttributes().possibleAttributes() != null && context.possibleAttributes().possibleAttributes().Length > 1) && 
                context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("spawn_type"))
            {
                ErrorList += ErrorMessages.SpawnError.ONLY_TRAP_CAN_SPAWN;
                ErrorList += (context.GetText() + "\n");
                ErrorFound = true;
                return StaticStartValues.PLACEHOLDER_NAME;
            }
            if(context.character().PARTNER() != null)
            {
                if (context.possibleAttributes().GetText().Equals("name"))
                {
                    if (Provider.GetPartner().Name.Equals(StaticStartValues.PLACEHOLDER_NAME))
                    {
                        ErrorList += ErrorMessages.ConditionError.CHARACTER_HAS_NO_NAME;
                        ErrorList += (context.GetText() + "\n");
                        ErrorFound = true;
                        return StaticStartValues.PLACEHOLDER_NAME;
                    }
                    return Provider.GetPartner().Name;
                }
                if ((context.possibleAttributes().possibleAttributes() != null && context.possibleAttributes().possibleAttributes().Length > 1) && 
                    context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("type"))
                {
                    return Provider.GetPartner().GetCharacterType().Name;
                }
            }
            if(context.character().ME() != null)
            {
                return MeNameAttribute(context);
            }
            if (context.character().MONSTER() != null)
            {
                if (context.possibleAttributes().GetText().Equals("name"))
                {
                    if (Provider.GetMonster().Name.Equals(StaticStartValues.PLACEHOLDER_NAME))
                    {
                        ErrorList += ErrorMessages.ConditionError.CHARACTER_HAS_NO_NAME;
                        ErrorList += (context.GetText() + "\n");
                        ErrorFound = true;
                        return StaticStartValues.PLACEHOLDER_NAME;
                    }
                    return Provider.GetMonster().Name;
                }
                if ((context.possibleAttributes().possibleAttributes() != null && context.possibleAttributes().possibleAttributes().Length > 1) && 
                    context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("type"))
                {
                    return Provider.GetMonster().GetCharacterType().Name;
                }
            }
            ErrorList += ErrorMessages.ConditionError.MONSTER_ATTRIBUTE_ERROR;
            ErrorList += (context.GetText() + "\n");
            ErrorFound = true;
            return StaticStartValues.PLACEHOLDER_NAME;
        }

        public string TrapNameAttribute(AttributeContext context)
        {
            if (context.character().PARTNER() != null)
            {
                if (context.possibleAttributes().GetText().Equals("name"))
                {
                    if (Provider.GetPartner().Name.Equals(StaticStartValues.PLACEHOLDER_NAME))
                    {
                        ErrorList += ErrorMessages.ConditionError.CHARACTER_HAS_NO_NAME;
                        ErrorList += (context.GetText() + "\n");
                        ErrorFound = true;
                        return StaticStartValues.PLACEHOLDER_NAME;
                    }
                    return Provider.GetPartner().Name;
                }
                if ((context.possibleAttributes().possibleAttributes() != null && context.possibleAttributes().possibleAttributes().Length > 1) && 
                    context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("type"))
                {
                    return Provider.GetPartner().GetCharacterType().Name;
                }
                if ((context.possibleAttributes().possibleAttributes() != null && context.possibleAttributes().possibleAttributes().Length > 1) && 
                    context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("spawn_type"))
                {
                    if (Provider.GetPartner().GetCharacterType().SpawnType == null)
                    {
                        ErrorList += (ErrorMessages.ConditionError.CHARACTER_TYPE_HAS_NO_SPAWN_TYPE_DEFINED);
                        ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                        ErrorList += (context.GetText() + "\n");
                        ErrorFound = true;
                        return StaticStartValues.PLACEHOLDER_NAME;
                    }
                    if (Program.GetCharacterType(Provider.GetPartner().GetCharacterType().SpawnType.Name) == null)
                    {
                        ErrorList += (ErrorMessages.ConditionError.SPAWN_TYPE_NOT_FOUND);
                        ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                        ErrorList += (context.GetText() + "\n");
                        ErrorFound = true;
                        return StaticStartValues.PLACEHOLDER_NAME;
                    }
                    return Provider.GetPartner().GetCharacterType().SpawnType.Name;
                }
            }
            if (context.character().ME() != null)
            {
                return MeNameAttribute(context);
            }
            if (context.character().TRAP() != null)
            {
                if (context.possibleAttributes().GetText().Equals("name"))
                {
                    if (Provider.GetTrap().Name.Equals(StaticStartValues.PLACEHOLDER_NAME))
                    {
                        ErrorList += ErrorMessages.ConditionError.CHARACTER_HAS_NO_NAME;
                        ErrorList += (context.GetText() + "\n");
                        ErrorFound = true;
                        return StaticStartValues.PLACEHOLDER_NAME;
                    }
                    return Provider.GetTrap().Name;
                }
                if ((context.possibleAttributes().possibleAttributes() != null && context.possibleAttributes().possibleAttributes().Length > 1) && 
                    context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("type"))
                {
                    return Provider.GetTrap().GetCharacterType().Name;
                }
                if ((context.possibleAttributes().possibleAttributes() != null && context.possibleAttributes().possibleAttributes().Length > 1) && 
                    context.possibleAttributes().possibleAttributes().ElementAt(0).GetText().Equals("spawn_type"))
                {
                    if (Provider.GetTrap().GetCharacterType().SpawnType == null)
                    {
                        ErrorList += (ErrorMessages.ConditionError.CHARACTER_TYPE_HAS_NO_SPAWN_TYPE_DEFINED);
                        ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                        ErrorList += (context.GetText() + "\n");
                        ErrorFound = true;
                        return StaticStartValues.PLACEHOLDER_NAME;
                    }
                    if (Program.GetCharacterType(Provider.GetTrap().GetCharacterType().SpawnType.Name) == null)
                    {
                        ErrorList += (ErrorMessages.ConditionError.SPAWN_TYPE_NOT_FOUND);
                        ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                        ErrorList += (context.GetText() + "\n");
                        ErrorFound = true;
                        return StaticStartValues.PLACEHOLDER_NAME;
                    }
                    return Provider.GetTrap().GetCharacterType().SpawnType.Name;
                }
            }
            ErrorList += ErrorMessages.ConditionError.TRAP_ATTRIBUTE_ERROR;
            ErrorList += (context.GetText() + "\n");
            ErrorFound = true;
            return StaticStartValues.PLACEHOLDER_NAME;
        }

        public CharacterType CheckTypeAttributeExpression(AttributeContext context)
        {
            if (context.character().PLAYER() != null)
            {
                return PlayerTypeAttribute(context);
            }
            if (context.character().MONSTER() != null)
            {
                return MonsterTypeAttribute(context);
            }
            if (context.character().TRAP() != null)
            {
                return TrapTypeAttribute(context);
            }
            if (context.character().ME() != null)
            {
                if (Provider.GetMe() is Monster)
                    return MonsterTypeAttribute(context);
                if (Provider.GetMe() is Trap)
                    return TrapTypeAttribute(context);
            }
            if (context.character().PARTNER() != null)
            {
                if (Provider.GetPartner() == null)
                {
                    ErrorList += ErrorMessages.PartnerError.NON_EXISTANT_PARTNER + Provider.GetMe().Name;
                    ErrorList += (context.GetText() + "\n");
                    ErrorFound = true;
                    return null;
                }
                if (Provider.GetPartner() is Monster)
                {
                    return MonsterTypeAttribute(context);
                }
                if (Provider.GetPartner() is Trap)
                {
                    return TrapTypeAttribute(context);
                }
            }
            ErrorList += ErrorMessages.ConditionError.UNRECOGNIZED_ATTRIBUTE_ERROR;
            ErrorList += (context.GetText() + "\n");
            ErrorFound = true;
            return null;
        }

        public CharacterType PlayerTypeAttribute(AttributeContext context)
        {
            if (context.possibleAttributes().GetText().Equals("type"))
                return Provider.GetPlayer().GetCharacterType();
            ErrorList += ErrorMessages.ConditionError.PLAYER_ATTRIBUTE_ERROR;
            ErrorList += (context.GetText() + "\n");
            ErrorFound = true;
            return null;
        }

        public CharacterType MeTypeAttribute(AttributeContext context)
        {
            if (context.possibleAttributes().GetText().Equals("type"))
            {
                return Provider.GetMe().GetCharacterType();
            }
            if (context.possibleAttributes().GetText().Equals("spawn_type"))
            {
                if (Provider.GetMe().GetCharacterType().SpawnType == null)
                {
                    ErrorList += (ErrorMessages.ConditionError.CHARACTER_TYPE_HAS_NO_SPAWN_TYPE_DEFINED);
                    ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                    ErrorList += (context.GetText() + "\n");
                    ErrorFound = true;
                    return null;
                }
                if (Program.GetCharacterType(Provider.GetMe().GetCharacterType().SpawnType.Name) == null)
                {
                    ErrorList += (ErrorMessages.ConditionError.SPAWN_TYPE_NOT_FOUND);
                    ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                    ErrorList += (context.GetText() + "\n");
                    ErrorFound = true;
                    return null;
                }
                return Program.GetCharacterType(Provider.GetMe().GetCharacterType().SpawnType.Name);
            }
            ErrorList += ErrorMessages.ConditionError.UNRECOGNIZED_ATTRIBUTE_ERROR;
            ErrorList += (context.GetText() + "\n");
            ErrorFound = true;
            return null;
        }

        public CharacterType MonsterTypeAttribute(AttributeContext context)
        {
            if (context.possibleAttributes().GetText().Equals("spawn_type"))
            {
                ErrorList += ErrorMessages.SpawnError.ONLY_TRAP_CAN_SPAWN;
                ErrorList += (context.GetText() + "\n");
                ErrorFound = true;
                return null;
            }
            if(context.character().PARTNER() != null)
            {
                if (context.possibleAttributes().GetText().Equals("type"))
                {
                    return Provider.GetPartner().GetCharacterType();
                }
            }
            if(context.character().ME() != null)
            {
                return MeTypeAttribute(context);
            }
            if(context.character().MONSTER() != null)
            {
                if (context.possibleAttributes().GetText().Equals("type"))
                {
                    return Provider.GetMonster().GetCharacterType();
                }
            }
            ErrorList += ErrorMessages.ConditionError.MONSTER_ATTRIBUTE_ERROR;
            ErrorList += (context.GetText() + "\n");
            ErrorFound = true;
            return null;
        }

        public CharacterType TrapTypeAttribute(AttributeContext context)
        {
            if(context.character().PARTNER() != null)
            {
                if (context.possibleAttributes().GetText().Equals("type"))
                {
                    return Provider.GetPartner().GetCharacterType();
                }
                if (context.possibleAttributes().GetText().Equals("spawn_type"))
                {
                    if (Provider.GetPartner().GetCharacterType().SpawnType == null)
                    {
                        ErrorList += (ErrorMessages.ConditionError.CHARACTER_TYPE_HAS_NO_SPAWN_TYPE_DEFINED);
                        ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                        ErrorList += (context.GetText() + "\n");
                        ErrorFound = true;
                        return null;
                    }
                    if (Program.GetCharacterType(Provider.GetPartner().GetCharacterType().SpawnType.Name) == null)
                    {
                        ErrorList += (ErrorMessages.ConditionError.SPAWN_TYPE_NOT_FOUND);
                        ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                        ErrorList += (context.GetText() + "\n");
                        ErrorFound = true;
                        return null;
                    }
                    return Program.GetCharacterType(Provider.GetPartner().GetCharacterType().SpawnType.Name);
                }
            }
            if(context.character().ME() != null)
            {
                return MeTypeAttribute(context);
            }
            if(context.character().TRAP() != null)
            {
                if (context.possibleAttributes().GetText().Equals("type"))
                {
                    return Provider.GetTrap().GetCharacterType();
                }
                if (context.possibleAttributes().GetText().Equals("spawn_type"))
                {
                    if (Provider.GetTrap().GetCharacterType().SpawnType == null)
                    {
                        ErrorList += (ErrorMessages.ConditionError.CHARACTER_TYPE_HAS_NO_SPAWN_TYPE_DEFINED);
                        ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                        ErrorList += (context.GetText() + "\n");
                        ErrorFound = true;
                        return null;
                    }
                    if (Program.GetCharacterType(Provider.GetTrap().GetCharacterType().SpawnType.Name) == null)
                    {
                        ErrorList += (ErrorMessages.ConditionError.SPAWN_TYPE_NOT_FOUND);
                        ErrorList += (ErrorMessages.ConditionError.IN_PLACE);
                        ErrorList += (context.GetText() + "\n");
                        ErrorFound = true;
                        return null;
                    }
                    return Program.GetCharacterType(Provider.GetTrap().GetCharacterType().SpawnType.Name);
                }
            }
            ErrorList += ErrorMessages.ConditionError.TRAP_ATTRIBUTE_ERROR;
            ErrorList += (context.GetText() + "\n");
            ErrorFound = true;
            return null;
        }
    }
}
