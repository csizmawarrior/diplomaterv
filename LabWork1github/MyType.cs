using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github
{
   public static class Types
    {
        public const string TRAP = "trap";
        public const string MONSTER = "monster";
        public const string PLAYER = "player";
        public const string DAMAGE = "damage";
        public const string HEAL = "heal";
        public const string HEALTH = "health";
        public const string TELEPORTPLACE = "teleport_place";
        public const string SPAWNPLACE = "spawn_place";
        public const string PLACE = "place";
        public const string DOTX = ".x";
        public const string DOTY = ".y";
        public const string NUMBER = "number";
    }

    public abstract class MyType
    {


        public abstract bool CompatibleCompare(string type);


        public abstract bool CompatibleEquals(string type);

    }
}
