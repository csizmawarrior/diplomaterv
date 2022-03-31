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
                    if (Math.Abs(Provider.GetMe().Place.X - Provider.GetMonster().Place.X) <= Provider.getNear() || Math.Abs(Provider.GetMonster().Place.X - Provider.GetMe().Place.X) <= Provider.getNear())
                    {
                        if (Math.Abs(Provider.GetMe().Place.Y - Provider.GetMonster().Place.Y) <= Provider.getNear() || Math.Abs(Provider.GetMonster().Place.Y - Provider.GetMe().Place.Y) <= Provider.getNear())
                            return true;
                    }
                }
                if (functionExpressionContext.character().TRAP() != null)
                {
                    if (Math.Abs(Provider.GetMe().Place.X - Provider.GetTrap().Place.X) <= Provider.getNear() || Math.Abs(Provider.GetTrap().Place.X - Provider.GetMe().Place.X) <= Provider.getNear())
                    {
                        if (Math.Abs(Provider.GetMe().Place.Y - Provider.GetTrap().Place.Y) <= Provider.getNear() || Math.Abs(Provider.GetTrap().Place.Y - Provider.GetMe().Place.Y) <= Provider.getNear())
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

        //TODO: rethink and redo attribute handling because of changes
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

            throw new ArgumentException("Unrecognized number expression");
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
                if(Provider.GetMe().GetType() is PlayerType)
                {
                    return PlayerAttribute(context);
                }

                if(Provider.GetMe().GetType() is MonsterType)
                {
                    return MonsterAttribute(context);
                }
                if (Provider.GetMe().GetType() is TrapType)
                {
                    return TrapAttribute(context);
                }
            }
            ErrorList += ("ERROR: An attribute caused error!\n");
            ErrorList += ("in place: \n");
            ErrorList += (context.GetText() + "\n");
            ErrorFound = true;
            return -1;
        }

        public bool CheckBoolAttributeExpression(BoolExpressionContext context)
        {
            if(context.attribute().ElementAt(0).character().MONSTER() != null || 
                (context.attribute().ElementAt(0).character().ME() != null && Provider.GetMe().GetType() is MonsterType))
            {
                if(context.attribute().ElementAt(0).possibleAttributes().GetText().Equals("type") && 
                    context.attribute().ElementAt(0).possibleAttributes().possibleAttributes().Length == 0)
                {
                    if (context.attribute().ElementAt(1).character().TRAP() != null)
                    {
                        if (context.attribute().ElementAt(0).character().MONSTER() != null)
                        {
                            if (context.COMPARE().GetText().Equals("=="))
                                return Provider.GetMonster().GetType() == Provider.GetTrap().GetType().SpawnType;
                            if (context.COMPARE().GetText().Equals("!="))
                                return Provider.GetMonster().GetType() != Provider.GetTrap().GetType().SpawnType;
                        }
                        else
                        {
                            if (context.COMPARE().GetText().Equals("=="))
                                return Provider.GetMe().GetType() == Provider.GetTrap().GetType().SpawnType;
                            if (context.COMPARE().GetText().Equals("!="))
                                return Provider.GetMe().GetType() != Provider.GetTrap().GetType().SpawnType;
                        }
                    }
                    if(context.attribute().ElementAt(1).character().ME() != null && Provider.GetMe().GetType() is TrapType)
                    {
                        if (context.attribute().ElementAt(0).character().MONSTER() != null)
                        {
                            if (context.COMPARE().GetText().Equals("=="))
                                return Provider.GetMonster().GetType() == Provider.GetMe().GetType().SpawnType;
                            if (context.COMPARE().GetText().Equals("!="))
                                return Provider.GetMonster().GetType() != Provider.GetMe().GetType().SpawnType;
                        }
                        else
                        {
                            ErrorList += ("ERROR: Me can't be both monster and trap!\n");
                            ErrorList += ("in place: \n");
                            ErrorList += (context.GetText() + "\n");
                            ErrorFound = true;
                            return false;
                        }
                    }
                }
                if(context.attribute().ElementAt(0).possibleAttributes().GetText().Equals("place") &&
                    context.attribute().ElementAt(0).possibleAttributes().possibleAttributes().Length == 0)
                {

                }
                else
                {
                    if (context.COMPARE().GetText().Equals("=="))
                        return CheckNumberAttributeExpression(context.attribute().ElementAt(0)) == CheckNumberAttributeExpression(context.attribute().ElementAt(1));
                    if (context.COMPARE().GetText().Equals("!="))
                        return CheckNumberAttributeExpression(context.attribute().ElementAt(0)) != CheckNumberAttributeExpression(context.attribute().ElementAt(1));
                }
            }
            
        }

        public double PlayerAttribute(AttributeContext context)
        {
            if (context.possibleAttributes().GetText().Equals("damage"))
                return Provider.GetPlayer().GetType().Damage;
            if (context.possibleAttributes().GetText().Equals("health"))
                return Provider.GetPlayer().GetHealth();
            if (context.possibleAttributes().GetText().Equals("place"))
            {
                if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("x"))
                    return Provider.GetPlayer().Place.X;
                else
                    return Provider.GetPlayer().Place.Y;
            }
            ErrorList += ("ERROR: A player caused error!\n");
            ErrorList += ("in place: \n");
            ErrorList += (context.GetText() + "\n");
            ErrorFound = true;
            return -1;
        }

        public double TrapAttribute(AttributeContext context)
        {
            if (context.possibleAttributes().GetText().Equals("damage"))
            {
                if (Provider.GetTrap().GetType().Damage == -1)
                    return 0;
                return Provider.GetTrap().GetType().Damage;
            }
            if (context.possibleAttributes().GetText().Equals("health"))
            {
                ErrorList += ("ERROR: A trap doesn't have health!\n");
                ErrorList += ("in place: \n");
                ErrorList += (context.GetText() + "\n");
                ErrorFound = true;
                return -1;
            }
            //assuming that traps return a default 0 for heal or damage when e.g. they only teleport
            if (context.possibleAttributes().GetText().Equals("heal"))
            {
                if (Provider.GetTrap().GetType().Heal == -1)
                    return 0;
                return Provider.GetTrap().GetType().Heal;
            }
            if (context.possibleAttributes().GetText().Equals("place"))
            {
                if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("x"))
                    return Provider.GetTrap().Place.X;
                else
                    return Provider.GetTrap().Place.Y;
            }
            if (context.possibleAttributes().GetText().Equals("teleport_place"))
            {
                if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("x"))
                    return Provider.GetTrap().GetType().TeleportPlace.X;
                else
                    return Provider.GetTrap().GetType().TeleportPlace.Y;
            }
            if (context.possibleAttributes().GetText().Equals("spawn_place"))
            {
                if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("x"))
                    return Provider.GetTrap().GetType().SpawnPlace.X;
                else
                    return Provider.GetTrap().GetType().SpawnPlace.Y;
            }
            if (context.possibleAttributes().GetText().Equals("type"))
            {
                if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("damage"))
                {
                    if (Provider.GetTrap().GetType().Damage == -1)
                    {
                        return 0;
                    }
                    return Provider.GetTrap().GetType().Damage;
                }
                if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("health"))
                {
                    ErrorList += ("ERROR: A trap doesn't have health!\n");
                    ErrorList += ("in place: \n");
                    ErrorList += (context.GetText() + "\n");
                    ErrorFound = true;
                    return -1;
                }
                if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("heal"))
                {
                    if (Provider.GetTrap().GetType().Heal == -1)
                    {
                        return 0;
                    }
                    return Provider.GetTrap().GetType().Heal;
                }
            }
            if (context.possibleAttributes().GetText().Equals("spawn_type"))
            {
                if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("damage"))
                {
                    if (Provider.GetTrap().GetType().SpawnType.Damage == -1)
                    {
                        ErrorList += ("ERROR: A monster needs damage!\n");
                        ErrorList += ("in place: \n");
                        ErrorList += (context.GetText() + "\n");
                        ErrorFound = true;
                        return -1;
                    }
                    return Provider.GetTrap().GetType().SpawnType.Damage;
                }
                if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("health"))
                    return Provider.GetTrap().GetType().SpawnType.Health;
                if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("heal"))
                {
                    ErrorList += ("ERROR: A monster can't heal!\n");
                    ErrorList += ("in place: \n");
                    ErrorList += (context.GetText() + "\n");
                    ErrorFound = true;
                    return -1;
                }
            }
            ErrorList += ("ERROR: A trap caused error!\n");
            ErrorList += ("in place: \n");
            ErrorList += (context.GetText() + "\n");
            ErrorFound = true;
            return -1;
        }

        public double MonsterAttribute(AttributeContext context)
        {
            if (context.possibleAttributes().GetText().Equals("damage"))
                if (Provider.GetMonster().GetType().Damage == -1)
                {
                    ErrorList += ("ERROR: A monster needs damage!\n");
                    ErrorList += ("in place: \n");
                    ErrorList += (context.GetText() + "\n");
                    ErrorFound = true;
                    return -1;
                }
            if (context.possibleAttributes().GetText().Equals("health"))
                return Provider.GetMonster().GetHealth();
            if (context.possibleAttributes().GetText().Equals("place"))
            {
                if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("x"))
                    return Provider.GetMonster().Place.X;
                else
                    return Provider.GetMonster().Place.Y;
            }
            if (context.possibleAttributes().GetText().Equals("type"))
            {
                if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("damage"))
                {
                    if (Provider.GetMonster().GetType().Damage == -1)
                    {
                        ErrorList += ("ERROR: A monster needs damage!\n");
                        ErrorList += ("in place: \n");
                        ErrorList += (context.GetText() + "\n");
                        ErrorFound = true;
                        return -1;
                    }
                    return Provider.GetMonster().GetType().Damage;
                }
                if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("health"))
                    return Provider.GetMonster().GetHealth();
                if (context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("heal"))
                {
                    ErrorList += ("ERROR: A monster can't heal!\n");
                    ErrorList += ("in place: \n");
                    ErrorList += (context.GetText() + "\n");
                    ErrorFound = true;
                    return -1;
                }
            }
            ErrorList += ("ERROR: A monster caused unexpected error!\n");
            ErrorList += ("in place: \n");
            ErrorList += (context.GetText() + "\n");
            ErrorFound = true;
            return -1;
        }

            //    string attribute = context.expression().ElementAt(1).something().possibleAttributes().GetText();
            //    if(context.expression().ElementAt(0).something().character().PLAYER() != null)
            //    {
            //        switch (attribute)
            //        {
            //            case "x":
            //                return this.Provider.GetPlayer().Place.X;
            //            case "y":
            //                return this.Provider.GetPlayer().Place.Y;
            //            case "health":
            //                return this.Provider.GetPlayer().GetHealth();
            //            case "damage":
            //                return this.Provider.GetPlayer().Type.Damage;
            //        }
            //    }
            //    if (context.expression().ElementAt(0).something().character().MONSTER() != null)
            //    {
            //        switch (attribute)
            //        {
            //            case "x":
            //                return this.Provider.GetMonster().Place.X;
            //            case "y":
            //                return this.Provider.GetMonster().Place.Y;
            //            case "health":
            //                return this.Provider.GetMonster().GetHealth();
            //            case "damage":
            //                return this.Provider.GetMonster().Type.Damage;
            //        }
            //    }
            //    if (context.expression().ElementAt(0).something().character().TRAP() != null)
            //    {
            //        switch (attribute)
            //        {
            //            case "x":
            //                return this.Provider.GetTrap().Place.X;
            //            case "y":
            //                return this.Provider.GetTrap().Place.Y;
            //            case "health":
            //                return this.Provider.GetTrap().GetHealth();
            //            case "damage":
            //                return this.Provider.GetTrap().Type.Damage;
            //            case "heal":
            //                return this.Provider.GetTrap().Type.Heal;
            //            case "teleport.x":
            //                return this.Provider.GetTrap().Type.TeleportPlace.X;
            //            case "teleport.y":
            //                return this.Provider.GetTrap().Type.TeleportPlace.Y;
            //            case "spawn.x":
            //                return this.Provider.GetTrap().Type.SpawnPlace.X;
            //            case "spawn.y":
            //                return this.Provider.GetTrap().Type.SpawnPlace.Y;
            //        }
            //    }
            //    if(context.something().ElementAt(0).something().character().ME() != null)
            //    {
            //        if(Provider.GetMe() is Trap)
            //        {
            //            switch (attribute)
            //            {
            //                case "x":
            //                    return this.Provider.GetMe().Place.X;
            //                case "y":
            //                    return this.Provider.GetMe().Place.Y;
            //                case "health":
            //                    return this.Provider.GetMe().GetHealth();
            //                case "damage":
            //                    return this.Provider.GetMe().GetType().Damage;
            //                case "heal":
            //                    return this.Provider.GetMe().GetType().Heal;
            //                case "teleport.x":
            //                    return this.Provider.GetMe().GetType().TeleportPlace.X;
            //                case "teleport.y":
            //                    return this.Provider.GetMe().GetType().TeleportPlace.Y;
            //                case "spawn.x":
            //                    return this.Provider.GetMe().GetType().SpawnPlace.X;
            //                case "spawn.y":
            //                    return this.Provider.GetMe().GetType().SpawnPlace.Y;
            //            }
            //        }
            //        else
            //            switch (attribute)
            //            {
            //                case "x":
            //                    return this.Provider.GetMe().Place.X;
            //                case "y":
            //                    return this.Provider.GetMe().Place.Y;
            //                case "health":
            //                    return this.Provider.GetMe().GetHealth();
            //                case "damage":
            //                    return this.Provider.GetMe().GetType().Damage;
            //            }
            //    }
            //    throw new InvalidOperationException("type check failed in attribute");
            //}
        }
}
