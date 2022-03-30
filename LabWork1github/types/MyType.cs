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
    }

    public abstract class MyType
    {
        public abstract bool CompatibleCompare(object param2);
        public abstract bool CompatibleNumCompare(object param2);
        public abstract bool CompatibleAttribue(object param2);
        public abstract bool CompatibleNumConnecter(object param2);
        public abstract bool CompatibleBoolConnecter(object param2);
        public abstract bool CompatibleAlive(object param2);
        public abstract bool CompatibleIsNear(object param2);
        public abstract bool CompatibleAbsolue(object param2);
        public abstract bool CompatibleNegate(object param2);
    }
}
