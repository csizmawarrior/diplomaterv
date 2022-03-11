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

        public ConditionVisitor(GameParamProvider provider, BoolExpressionContext context)
        {
            this.BoolExpressionContext = context;
            this.Provider = provider;
        }
        public bool CheckConditions()
        {
            return CheckBoolExpression(BoolExpressionContext);
        }
        public bool CheckBoolExpression(BoolExpressionContext context)
        {
            if (context.nextBoolExpression().Length > 0)
            {
                CheckNextBoolExpression(context);
            }
            if (context.PARENTHESISSTART() != null)
            {
                return CheckBoolExpression(context.boolExpression());
            }
            if (context.NEGATE() != null)
            {
                return !CheckBoolExpression(context.boolExpression());
            }
            if (context.functionExpression() != null) 
            {
                return CheckFunctionExpression(context.functionExpression());
            }
            if(context.numberExpression().Length > 0)
            {
                return CheckNumberExpression(context);
            }
            if (context.operation().NEAR() != null)
            {
                if (context.expression().ElementAt(0).something().character().ME() != null)
                    return true;
                if (context.expression().ElementAt(0).something().character().MONSTER() != null)
                {
                    foreach (Monster monster in this.Provider.GetMonsters())
                    {
                        if (Math.Abs(this.Provider.GetMe().Place.X - monster.Place.X) <= this.Provider.getNear())
                            if (Math.Abs(this.Provider.GetMe().Place.Y - monster.Place.Y) <= this.Provider.getNear())
                                return true;
                    }
                    return false;
                }
                if (context.expression().ElementAt(0).something().character().TRAP() != null)
                {
                    foreach (Trap trap in this.Provider.GetTraps())
                    {
                        if (Math.Abs(this.Provider.GetMe().Place.X - trap.Place.X) <= this.Provider.getNear())
                            if (Math.Abs(this.Provider.GetMe().Place.Y - trap.Place.Y) <= this.Provider.getNear())
                                return true;
                    }
                    return false;
                }

                if (context.expression().ElementAt(0).something().character().PLAYER() != null)
                {
                    if (Math.Abs(this.Provider.GetMe().Place.X - this.Provider.GetPlayer().Place.X) <= this.Provider.getNear())
                        if (Math.Abs(this.Provider.GetMe().Place.Y - this.Provider.GetPlayer().Place.Y) <= this.Provider.getNear())
                            return true;
                    return false;
                }     
            }
            if(context.operation().BOOLCONNECTER() != null)
            {
                if (context.operation().BOOLCONNECTER().GetText().Equals("||"))
                    return CheckBoolExpression(context.expression().ElementAt(0)) || CheckBoolExpression(context.expression().ElementAt(1));
                else
                    return CheckBoolExpression(context.expression().ElementAt(0)) || CheckBoolExpression(context.expression().ElementAt(1));
            }
            if (context.operation().NUMCOMPARE() != null)
            {
                if(context.operation().NUMCOMPARE().GetText().Equals(">"))
                    return CheckNumberExpression(context.expression().ElementAt(0)) > CheckNumberExpression(context.expression().ElementAt(1));
                else
                    return CheckNumberExpression(context.expression().ElementAt(0)) < CheckNumberExpression(context.expression().ElementAt(1));
            }
            if(context.operation().COMPARE() != null)
            {
                return CheckCompareExpression(context);
            }
            throw new InvalidOperationException();
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

        public bool CheckNextBoolExpression(BoolExpressionContext context)
        {
            
        }
        public bool CheckCompareExpression(BoolExpressionContext Excontext)
        {
            try
            {
                bool helper1 = CheckBoolExpression(Excontext.expression().ElementAt(0));
                bool helper2 = CheckBoolExpression(Excontext.expression().ElementAt(1));
                if (Excontext.operation().COMPARE().GetText().Equals("=="))
                    return helper1 == helper2;
                else
                    return helper1 != helper2;
            }
            catch (Exception e)
            {
                if (!(e is InvalidOperationException || e is ArgumentException))
                    throw e;
                try
                {
                    int helper1 = CheckNumberExpression(Excontext.expression().ElementAt(0));
                    int helper2 = CheckNumberExpression(Excontext.expression().ElementAt(1));
                    if (Excontext.operation().COMPARE().GetText().Equals("=="))
                        return helper1 == helper2;
                    else
                        return helper1 != helper2;
                }
                catch (Exception e2)
                {
                    throw new InvalidOperationException("type check failed for compare");
                }
            }
        }
        //TODO: rethink and redo attribute handling because of changes
        public int CheckNumberExpression(BoolExpressionContext context)
        {
            if (context.ABSOLUTE().ToList().Count > 0)
                return Math.Abs(CheckNumberExpression(context.expression().ElementAt(0)));
            if (context.PARENTHESISSTART() != null)
                return CheckNumberExpression(context.expression().ElementAt(0));
            if(context.something() != null)
            {
                if (context.something().NUMBER() != null)
                    return int.Parse(context.something().NUMBER().GetText());
                return int.Parse(context.something().ROUND().GetText());
            }
            if (context.operation().DOT() != null)
                return this.CheckAttributeExpression(context);
            if(context.operation().NUMCONNECTER() != null)
            {
                string connecter = context.operation().NUMCONNECTER().GetText();
                switch (connecter)
                {
                    case "+":
                        return CheckNumberExpression(context.expression().ElementAt(0)) + CheckNumberExpression(context.expression().ElementAt(1));
                    case "-":
                        return CheckNumberExpression(context.expression().ElementAt(0)) - CheckNumberExpression(context.expression().ElementAt(1));
                    case "*":
                        return CheckNumberExpression(context.expression().ElementAt(0)) * CheckNumberExpression(context.expression().ElementAt(1));
                    case "/":
                        return CheckNumberExpression(context.expression().ElementAt(0)) / CheckNumberExpression(context.expression().ElementAt(1));
                    case "%":
                        return CheckNumberExpression(context.expression().ElementAt(0)) % CheckNumberExpression(context.expression().ElementAt(1));
                }
            }
            throw new InvalidOperationException("type check failed at Number check");
        }
        public int CheckAttributeExpression(BoolExpressionContext context)
        {
            string attribute = context.expression().ElementAt(1).something().possibleAttributes().GetText();
            if(context.expression().ElementAt(0).something().character().PLAYER() != null)
            {
                switch (attribute)
                {
                    case "x":
                        return this.Provider.GetPlayer().Place.X;
                    case "y":
                        return this.Provider.GetPlayer().Place.Y;
                    case "health":
                        return this.Provider.GetPlayer().GetHealth();
                    case "damage":
                        return this.Provider.GetPlayer().Type.Damage;
                }
            }
            if (context.expression().ElementAt(0).something().character().MONSTER() != null)
            {
                switch (attribute)
                {
                    case "x":
                        return this.Provider.GetMonster().Place.X;
                    case "y":
                        return this.Provider.GetMonster().Place.Y;
                    case "health":
                        return this.Provider.GetMonster().GetHealth();
                    case "damage":
                        return this.Provider.GetMonster().Type.Damage;
                }
            }
            if (context.expression().ElementAt(0).something().character().TRAP() != null)
            {
                switch (attribute)
                {
                    case "x":
                        return this.Provider.GetTrap().Place.X;
                    case "y":
                        return this.Provider.GetTrap().Place.Y;
                    case "health":
                        return this.Provider.GetTrap().GetHealth();
                    case "damage":
                        return this.Provider.GetTrap().Type.Damage;
                    case "heal":
                        return this.Provider.GetTrap().Type.Heal;
                    case "teleport.x":
                        return this.Provider.GetTrap().Type.TeleportPlace.X;
                    case "teleport.y":
                        return this.Provider.GetTrap().Type.TeleportPlace.Y;
                    case "spawn.x":
                        return this.Provider.GetTrap().Type.SpawnPlace.X;
                    case "spawn.y":
                        return this.Provider.GetTrap().Type.SpawnPlace.Y;
                }
            }
            if(context.expression().ElementAt(0).something().character().ME() != null)
            {
                if(Provider.GetMe() is Trap)
                {
                    switch (attribute)
                    {
                        case "x":
                            return this.Provider.GetMe().Place.X;
                        case "y":
                            return this.Provider.GetMe().Place.Y;
                        case "health":
                            return this.Provider.GetMe().GetHealth();
                        case "damage":
                            return this.Provider.GetMe().GetType().Damage;
                        case "heal":
                            return this.Provider.GetMe().GetType().Heal;
                        case "teleport.x":
                            return this.Provider.GetMe().GetType().TeleportPlace.X;
                        case "teleport.y":
                            return this.Provider.GetMe().GetType().TeleportPlace.Y;
                        case "spawn.x":
                            return this.Provider.GetMe().GetType().SpawnPlace.X;
                        case "spawn.y":
                            return this.Provider.GetMe().GetType().SpawnPlace.Y;
                    }
                }
                else
                    switch (attribute)
                    {
                        case "x":
                            return this.Provider.GetMe().Place.X;
                        case "y":
                            return this.Provider.GetMe().Place.Y;
                        case "health":
                            return this.Provider.GetMe().GetHealth();
                        case "damage":
                            return this.Provider.GetMe().GetType().Damage;
                    }
            }
            throw new InvalidOperationException("type check failed in attribute");
        }
    }
}
