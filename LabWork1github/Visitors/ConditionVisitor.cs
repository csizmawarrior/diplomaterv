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

        public GameParamProvider provider { get; set; }

        public ConditionVisitor(GameParamProvider provider, BoolExpressionContext context)
        {
            this.BoolExpressionContext = context;
            this.provider = provider;
        }
        public bool CheckConditions()
        {
            return CheckBoolExpression(BoolExpressionContext);
        }
        public bool CheckBoolExpression(BoolExpressionContext context)
        {
            if (context.PARENTHESISSTART() != null)
            {
                return CheckBoolExpression(context.boolExpression().ElementAt(0));
            }
            if (context.NEGATE() != null)
            {
                return !CheckBoolExpression(context.boolExpression().ElementAt(0));
            }
            if (context.operation().ALIVE() != null) 
            {
                if (context.expression().ElementAt(0).something().character().ME() != null)
                    return provider.GetMe().GetHealth() > 0;
                if (context.expression().ElementAt(0).something().character().MONSTER() != null)
                {
                    foreach (Monster monster in this.provider.GetMonsters())
                    {
                        if (monster.GetHealth() > 0)
                            return true;
                    }
                    return false;
                }
                if (context.expression().ElementAt(0).something().character().TRAP() != null)
                {
                    foreach (Trap trap in this.provider.GetTraps())
                    {
                        if (trap.GetHealth() > 0)
                            return true;
                    }
                    return false;
                }
                if (context.expression().ElementAt(0).something().character().PLAYER() != null)
                    return provider.GetPlayer().GetHealth() > 0;
            }
            if (context.operation().NEAR() != null)
            {
                if (context.expression().ElementAt(0).something().character().ME() != null)
                    return true;
                if (context.expression().ElementAt(0).something().character().MONSTER() != null)
                {
                    foreach (Monster monster in this.provider.GetMonsters())
                    {
                        if (Math.Abs(this.provider.GetMe().Place.X - monster.Place.X) <= this.provider.getNear())
                            if (Math.Abs(this.provider.GetMe().Place.Y - monster.Place.Y) <= this.provider.getNear())
                                return true;
                    }
                    return false;
                }
                if (context.expression().ElementAt(0).something().character().TRAP() != null)
                {
                    foreach (Trap trap in this.provider.GetTraps())
                    {
                        if (Math.Abs(this.provider.GetMe().Place.X - trap.Place.X) <= this.provider.getNear())
                            if (Math.Abs(this.provider.GetMe().Place.Y - trap.Place.Y) <= this.provider.getNear())
                                return true;
                    }
                    return false;
                }
                if (context.expression().ElementAt(0).something().character().PLAYER() != null)
                {
                    if (Math.Abs(this.provider.GetMe().Place.X - this.provider.GetPlayer().Place.X) <= this.provider.getNear())
                        if (Math.Abs(this.provider.GetMe().Place.Y - this.provider.GetPlayer().Place.Y) <= this.provider.getNear())
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
                        return this.provider.GetPlayer().Place.X;
                    case "y":
                        return this.provider.GetPlayer().Place.Y;
                    case "health":
                        return this.provider.GetPlayer().GetHealth();
                    case "damage":
                        return this.provider.GetPlayer().Type.Damage;
                }
            }
            if (context.expression().ElementAt(0).something().character().MONSTER() != null)
            {
                switch (attribute)
                {
                    case "x":
                        return this.provider.GetMonster().Place.X;
                    case "y":
                        return this.provider.GetMonster().Place.Y;
                    case "health":
                        return this.provider.GetMonster().GetHealth();
                    case "damage":
                        return this.provider.GetMonster().Type.Damage;
                }
            }
            if (context.expression().ElementAt(0).something().character().TRAP() != null)
            {
                switch (attribute)
                {
                    case "x":
                        return this.provider.GetTrap().Place.X;
                    case "y":
                        return this.provider.GetTrap().Place.Y;
                    case "health":
                        return this.provider.GetTrap().GetHealth();
                    case "damage":
                        return this.provider.GetTrap().Type.Damage;
                    case "heal":
                        return this.provider.GetTrap().Type.Heal;
                    case "teleport.x":
                        return this.provider.GetTrap().Type.TeleportPlace.X;
                    case "teleport.y":
                        return this.provider.GetTrap().Type.TeleportPlace.Y;
                    case "spawn.x":
                        return this.provider.GetTrap().Type.SpawnPlace.X;
                    case "spawn.y":
                        return this.provider.GetTrap().Type.SpawnPlace.Y;
                }
            }
            if(context.expression().ElementAt(0).something().character().ME() != null)
            {
                if(provider.GetMe() is Trap)
                {
                    switch (attribute)
                    {
                        case "x":
                            return this.provider.GetMe().Place.X;
                        case "y":
                            return this.provider.GetMe().Place.Y;
                        case "health":
                            return this.provider.GetMe().GetHealth();
                        case "damage":
                            return this.provider.GetMe().GetType().Damage;
                        case "heal":
                            return this.provider.GetMe().GetType().Heal;
                        case "teleport.x":
                            return this.provider.GetMe().GetType().TeleportPlace.X;
                        case "teleport.y":
                            return this.provider.GetMe().GetType().TeleportPlace.Y;
                        case "spawn.x":
                            return this.provider.GetMe().GetType().SpawnPlace.X;
                        case "spawn.y":
                            return this.provider.GetMe().GetType().SpawnPlace.Y;
                    }
                }
                else
                    switch (attribute)
                    {
                        case "x":
                            return this.provider.GetMe().Place.X;
                        case "y":
                            return this.provider.GetMe().Place.Y;
                        case "health":
                            return this.provider.GetMe().GetHealth();
                        case "damage":
                            return this.provider.GetMe().GetType().Damage;
                    }
            }
            throw new InvalidOperationException("type check failed in attribute");
        }
    }
}
