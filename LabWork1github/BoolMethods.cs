using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github
{
    public static class BoolMethods
    {
        public static bool IsPlayerAlive(GameParamProvider provider, IfCommand command)
        {
            return provider.GetPlayer().Health > 0;
        }

        public static bool IsTrapAlive(GameParamProvider provider, IfCommand command)
        {
            return true;
        }

        public static bool IsMeAlive(GameParamProvider provider, IfCommand command)
        {
            return provider.GetMonster().Health > 0;
        }

        public static bool IsMonsterAlive(GameParamProvider provider, IfCommand command)
        {
            bool alive = false;
            foreach (Monster monster in provider.GetMonsters())
            {
                if (monster.Health > 0)
                {
                    alive = true;
                    break;
                }
            }
            return alive;
        }

        public static bool IsPlayerNear(GameParamProvider provider, IfCommand command)
        {
            bool near = false;
            if (provider.GetPlayer().Place.X > provider.GetMonster().Place.X) { 
                if ((provider.GetPlayer().Place.X - provider.GetMonster().Place.X) <= provider.getNear())
                    near = true;
            }
            else
                if ((provider.GetMonster().Place.X - provider.GetPlayer().Place.X) <= provider.getNear())
                    near = true;
            if (provider.GetPlayer().Place.Y > provider.GetMonster().Place.Y)
            {
                if ((provider.GetPlayer().Place.Y - provider.GetMonster().Place.Y) <= provider.getNear())
                    if(near)
                        return true;
            }
            else
                if ((provider.GetMonster().Place.Y - provider.GetPlayer().Place.Y) <= provider.getNear())
                    if(near)
                        return true;
            return near;
        }

        public static bool IsMeNear(GameParamProvider provider, IfCommand command)
        {
            return true;
        }
        
        public static bool IsMonsterNear(GameParamProvider provider)
        {
            foreach(Monster monster in provider.GetMonsters())
            {
                if(monster != provider.GetMonster())
                {
                    bool near = false;
                    if (monster.Place.X > provider.GetMonster().Place.X)
                    {
                        if ((monster.Place.X - provider.GetMonster().Place.X) <= provider.getNear())
                            near = true;
                    }
                    else
                        if ((provider.GetMonster().Place.X - monster.Place.X) <= provider.getNear())
                        near = true;
                    if (monster.Place.Y > provider.GetMonster().Place.Y)
                    {
                        if ((monster.Place.Y - provider.GetMonster().Place.Y) <= provider.getNear())
                            if (near)
                                return true;
                    }
                    else
                        if ((provider.GetMonster().Place.Y - monster.Place.Y) <= provider.getNear())
                        if (near)
                            return true;
                }
            }
            return false;
        }
        public static bool IsTrapNear(GameParamProvider provider)
        {
            foreach (Trap trap in provider.GetTraps())
            {
                    bool near = false;
                    if (trap.Place.X > provider.GetMonster().Place.X)
                    {
                        if ((trap.Place.X - provider.GetMonster().Place.X) <= provider.getNear())
                            near = true;
                    }
                    else
                        if ((provider.GetMonster().Place.X - trap.Place.X) <= provider.getNear())
                        near = true;
                    if (trap.Place.Y > provider.GetMonster().Place.Y)
                    {
                        if ((trap.Place.Y - provider.GetMonster().Place.Y) <= provider.getNear())
                            if (near)
                                return true;
                    }
                    else
                        if ((provider.GetMonster().Place.Y - trap.Place.Y) <= provider.getNear())
                        if (near)
                            return true;
                
            }
            return false;
        }

    }
}
